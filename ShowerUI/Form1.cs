using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraBars.Docking2010.Views.WindowsUI;
using DevExpress.XtraCharts;
using Filters;
using Newtonsoft.Json;
using ShowerUI.Dto;
using ShowerTcpClient;
using DevExpress.XtraCharts.Native;

namespace ShowerUI
{
    public partial class Form1 : Form
    {
        private const string TempOnSeries = "TempOn";
        private const string TempOffSeries = "TempOff";
        private const string TempCalculatedSeries = "CalculatedTemp";
        private readonly List<ushort> _usecList = new(30000); // 20 минут
        private readonly Action<InternalTempModel> _addTemperatureRecordHandler;
        private readonly List<InternalTempModel> _temperatureList = new(600);
        private CancellationTokenSource? _tempRecorderCts;
        private CancellationTokenSource? _wl_cts;
        /// <summary>
        /// Доступ только через основной поток.
        /// </summary>
        private CancellationTokenSource? _pingCts;
        private CancellationTokenSource? _ctsWwaterLevelCalibration;

        // ctor
        public Form1()
        {
            InitializeComponent();

            _addTemperatureRecordHandler = AddTemperatureRecord;
        }

        private void LoadTempRecord()
        {
            using (var dialog = new OpenFileDialog())
            {
                dialog.Filter = "Json File|*.txt";
                dialog.Multiselect = false;
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    string json = File.ReadAllText(dialog.FileName);
                    var dataCollection = JsonConvert.DeserializeObject<List<InternalTempModel>>(json);

                    _temperatureList.Clear();
                    _temperatureList.AddRange(dataCollection);

                    chartControl_temperature.BeginInit();
                    foreach (var item in _temperatureList)
                    {
                        //chartControl1.Series[0].Points.Add(new SeriesPoint(item.Time, item.InternalTemp) { IsEmpty = !item.HeaterEnabled });
                        chartControl_temperature.Series[1].Points.Add(new SeriesPoint(item.Number, item.InternalTemp) { IsEmpty = !item.HeaterEnabled });
                        //chartControl1.Series[2].Points.Add(new SeriesPoint(item.Time, item.InternalTemp) { IsEmpty = item.HeaterEnabled });
                        chartControl_temperature.Series[3].Points.Add(new SeriesPoint(item.Number, item.InternalTemp) { IsEmpty = item.HeaterEnabled });
                    }
                    chartControl_temperature.EndInit();
                }
            }
        }

        /// <summary>
        /// Запись показаний температуры
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void Button_TempRecord_Click(object sender, EventArgs e)
        {
            var cts = _tempRecorderCts = new CancellationTokenSource();
            try
            {
                ClearChart();
                int reconnectCount = -1;

                buttonTempStartRecord.Enabled = false;
                button_temp_stop.Enabled = true;

            Retry:
                try
                {
                    using (var connection = await ConnectionHelper.CreateConnectionAsync(cts.Token))
                    {
                        reconnectCount++;
                        errorProvider1.SetError(buttonTempStartRecord, null);
                        await Task.Run(() => RecordTempAsync(connection, cts.Token));
                    }
                }
                catch when (cts.IsCancellationRequested)
                {
                    return;
                }
                catch (Exception ex)
                {
                    errorProvider1.SetError(buttonTempStartRecord, ex.Message);
                    labelTempReconnectCount.Text = $"{reconnectCount}";

                    try
                    {
                        await Task.Delay(2000, cts.Token);
                    }
                    catch (OperationCanceledException)
                    {
                        return;
                    }
                    goto Retry;
                }
            }
            finally
            {
                cts.Cancel();
            }
        }

        private async Task RecordTempAsync(ShowerConnection con, CancellationToken cancellationToken)
        {
            int number = 0;
            while (!cancellationToken.IsCancellationRequested)
            {
                float internalTemp = await con.GetInternalTemperatureAsync(cancellationToken);
                bool heaterEnabled = await con.GetHeaterEnabledAsync(cancellationToken);
                int minutesLeft = await con.GetMinutesLeftAsync(cancellationToken);

                var model = new InternalTempModel
                {
                    Number = number,
                    InternalTemp = internalTemp,
                    HeaterEnabled = heaterEnabled,
                    TimeLeft = TimeSpan.FromMinutes(minutesLeft),
                };
                number++;

                BeginInvoke(_addTemperatureRecordHandler, model);

                await Task.Delay(1000);
            }
        }

        private void AddTemperatureRecord(InternalTempModel model)
        {
            _temperatureList.Add(model);

            labelTimeLeft.Text = model.TimeLeft.ToString();
            chartControl_temperature.Series[TempOnSeries].Points.Add(new SeriesPoint(model.Number, model.InternalTemp) { IsEmpty = !model.HeaterEnabled });
            chartControl_temperature.Series[TempOffSeries].Points.Add(new SeriesPoint(model.Number, model.InternalTemp) { IsEmpty = model.HeaterEnabled });
        }

        private void Button_TemperatureStop_Click(object sender, EventArgs e)
        {
            _tempRecorderCts?.Cancel();
            buttonTempStartRecord.Enabled = true;
            button_temp_stop.Enabled = false;
        }

        private void Button_Save_Click(object sender, EventArgs e)
        {
            try
            {
                using (var dialog = new SaveFileDialog())
                {
                    dialog.AutoUpgradeEnabled = true;
                    dialog.DefaultExt = "txt";
                    dialog.Filter = "Json File|*.txt";
                    if (dialog.ShowDialog() == DialogResult.OK)
                    {
                        string json = JsonConvert.SerializeObject(_temperatureList);
                        File.WriteAllText(dialog.FileName, json);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Button_Simulation_Click(object sender, EventArgs e)
        {
            try
            {
                button4.Enabled = false;
                ClearChart();
                StartSimulation();
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                button4.Enabled = true;
            }
        }

        private async void StartSimulation()
        {
            string filePath;
            using (var openFile = new OpenFileDialog())
            {
                openFile.Filter = "Json File|*.txt";
                openFile.Multiselect = false;

                if (openFile.ShowDialog() != DialogResult.OK)
                {
                    return;
                }
                filePath = openFile.FileName;
            }

            var collection = JsonConvert.DeserializeObject<InternalTempModel[]>(File.ReadAllText(filePath));
            if (collection == null)
            {
                return;
            }

            TimeSpan interval = TimeSpan.Zero;
            for (int i = 0; i < collection.Length; i++)
            {
                InternalTempModel measure = collection[i];

                await Task.Delay(interval);
                if (IsDisposed)
                {
                    return;
                }

                chartControl_temperature.Series[1].Points.Add(new SeriesPoint(measure.Number, measure.InternalTemp) { IsEmpty = !measure.HeaterEnabled });
                chartControl_temperature.Series[3].Points.Add(new SeriesPoint(measure.Number, measure.InternalTemp) { IsEmpty = measure.HeaterEnabled });

                interval = TimeSpan.FromMilliseconds(10);
            }
        }

        private async Task SetAP()
        {
            string apMac = "D4:CA:6D:11:38:AF".ToLower();
            string ap = $@"""Miles"",""KLEZM00D"",""{apMac}""";
            _ = Encoding.ASCII.GetBytes(ap);
            int length = ap.Length;  // 38

            using (var connection = await ConnectionHelper.CreateConnectionAsync(CancellationToken.None).ConfigureAwait(false))
            {
                //connection.Writer.Write(ShowerCodes.SetDefAP);
                //connection.Writer.Write(buf);
                //connection.Writer.End();
                //connection.Writer.Send();

                //connection.Reader.ReadOK();
            }
        }

        private void Button_Clear_Click(object sender, EventArgs e)
        {
            ClearChart();
        }

        private void ClearChart()
        {
            _temperatureList.Clear();
            labelTimeLeft.Text = "__:__:__";
            chartControl_temperature.Series[TempOnSeries].Points.Clear();
            chartControl_temperature.Series[TempOffSeries].Points.Clear();
            chartControl_temperature.Series[TempCalculatedSeries].Points.Clear();
        }

        private void Button_Load_Click(object sender, EventArgs e)
        {
            ClearChart();
            LoadTempRecord();
            CalcTimeLeft();
        }

        private void TrackBar1_Scroll(object sender, EventArgs e)
        {
            CalcTimeLeft();
        }

        private void CalcTimeLeft()
        {
            int startMinute = trackBar1.Value;
            int startSec = startMinute * 60;

            var startPoint = _temperatureList.FirstOrDefault(x => x.HeaterEnabled);
            if (startPoint != null)
            {
                int time = startPoint.Number + startSec;
                var point = _temperatureList.FirstOrDefault(x => x.HeaterEnabled && x.Number >= time);
                if (point != null)
                {
                    double timeH = CalcFormula(point.InternalTemp, 37);
                    int sec = (int)(timeH * 60 * 60);
                    sec += point.Number;

                    chartControl_temperature.Series[4].Points.Clear();
                    chartControl_temperature.Series[4].Points.Add(new SeriesPoint(point.Number, point.InternalTemp));
                    chartControl_temperature.Series[4].Points.Add(new SeriesPoint(sec, 37));
                }
            }
        }

        private static double CalcFormula(float internalTemp, float limitTemp)
        {
            const double pi = 3.14159265358979323846;
            const float Q = 0.00117f;
            const int Resistance = 31;

            const int Tank_Height = 297;
            const int Tank_Diameter = 400;
            //const float Heater_Power = 1.55f;

            // V = Pi * (r^2) * h
            double TANK_VOLUME = (pi * Math.Pow(Tank_Diameter / 2.0, 2) * Tank_Height) / 1000000.0;     // Объем в литрах

            //const float HEATER_POWER = (220.0 * (220.0 / Heater_Resistance)) / 1000.0;		// Мощность в киловатах

            // Формула расчета времени нагрева
            // T= 0,00117*V*(tк-tн)/W
            double Heater_Power = 220.0 / Resistance * 220.0 / 1000.0;
            double timeH = Q * TANK_VOLUME * (limitTemp - internalTemp) / Heater_Power;
            return timeH;

        }

        private void ResetToolStripMenuItem_Reset_Click(object sender, EventArgs e)
        {
            RestartDevice();
        }

        private async void RestartDevice()
        {
            try
            {
                Enabled = false;
                using (var connection = await ConnectionHelper.CreateConnectionAsync(CancellationToken.None))
                {
                    connection.BuildRequest()
                        .Write(ShowerCodes.Reset)
                        .ReadOK();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                Enabled = true;
            }
        }

        private void PingToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (var connection = ConnectionHelper.CreateConnectionAsync(CancellationToken.None).Result)
            {
                var code = connection.BuildRequest()
                    .Write(ShowerCodes.Ping)
                    .ReadCodeAsync(CancellationToken.None).GetAwaiter().GetResult();
            }
        }

        private async void Button7_Click(object sender, EventArgs e)
        {
            var me = (Button)sender;
            var cts = _pingCts = new CancellationTokenSource();
            me.Enabled = false;

            try
            {
                using (var connection = await ConnectionHelper.CreateConnectionAsync(cts.Token))
                {
                    var code = await connection.BuildRequest()
                        .Write(ShowerCodes.Ping)
                        .ReadCodeAsync(cts.Token);
                }
            }
            catch (Exception)
            {

            }
            finally
            {
                cts.Cancel();
                me.Enabled = true;
            }
        }

        private void Button10_Click(object sender, EventArgs e)
        {
            _pingCts?.Cancel();
        }

        private async void ButtonStartCalibrationClick(object sender, EventArgs e)
        {
            var cts = _ctsWwaterLevelCalibration = new CancellationTokenSource();
            button_stop.Enabled = true;
            buttonStartCalib.Enabled = false;

            var sw = new Stopwatch();
            try
            {
                while (!cts.IsCancellationRequested)
                {
                    try
                    {
                        using (var con = await ConnectionHelper.CreateConnectionAsync(cts.Token))
                        {
                            ushort waterLevelEmpty = await con.RequestAsync<ushort>(ShowerCodes.GetWaterLevelEmptyUsec, cts.Token);
                            ushort waterLevelFull = await con.RequestAsync<ushort>(ShowerCodes.GetWaterLevelFullUsec, cts.Token);

                            SetWLCalibration(waterLevelEmpty, waterLevelFull);
                            errorProvider1.SetError(buttonStartCalib, null);

                            while (!cts.IsCancellationRequested)
                            {
                                var interval = TimeSpan.FromMilliseconds((int)numericWaterLevelCalibInterval.Value);

                                sw.Restart();
                                short usec = await con.GetWaterLevelRawAsync(cts.Token);

                                AddUsec(usec);

                                var pause = interval - sw.Elapsed;
                                if (pause > TimeSpan.Zero)
                                {
                                    await Task.Delay(pause);
                                }
                            }
                        }
                    }
                    catch when (cts.IsCancellationRequested)
                    {
                        return;
                    }
                    catch (Exception ex)
                    {
                        errorProvider1.SetError(buttonStartCalib, ex.Message);
                        try
                        {
                            await Task.Delay(3000, cts.Token);
                            continue;
                        }
                        catch (OperationCanceledException)
                        {
                            return;
                        }
                    }
                }
            }
            finally
            {
                cts.Cancel();
            }
        }

        private void SetWLCalibration(ushort waterLevelEmpty, ushort waterLevelFull)
        {
            var diagram = (SwiftPlotDiagram)chartControl_wl_calibration.Diagram;

            double emptyCm = waterLevelEmpty /*/ 58d*/ * 1.08;  // +2%
            double fullCm = waterLevelFull /*/ 58d*/ * 0.98;    // +2%

            var wholeRange = diagram.AxisY.WholeRange;
            wholeRange.Auto = false;
            wholeRange.MinValue = fullCm;
            wholeRange.MaxValue = emptyCm;
            wholeRange.SideMarginsValue = 2;

            var visualRange = diagram.AxisY.VisualRange;
            visualRange.Auto = false;
            visualRange.MinValue = fullCm;
            visualRange.MaxValue = emptyCm;
            visualRange.SideMarginsValue = 2;
        }

        private void AddUsec(short usec)
        {
            const int dataIntervalSec = 10;
            int maxPoints = (dataIntervalSec * 1000 / 100);

            //decimal cm = usec / 58m;
            //cm = Math.Round(cm, 3);

            Series ser = chartControl_wl_calibration.Series[0];

            if (usec != -1)
            {
                ser.Points.Add(new SeriesPoint(DateTime.Now, usec));
            }
            else
            {
                ser.Points.Add(new SeriesPoint(DateTime.Now));
            }

            if (ser.Points.Count > maxPoints)
            {
                ser.Points.RemoveAt(0);
            }
        }

        private void Button_stop_Click(object sender, EventArgs e)
        {
            button_stop.Enabled = false;
            buttonStartCalib.Enabled = true;

            _ctsWwaterLevelCalibration?.Cancel();
            _ctsWwaterLevelCalibration = null;
        }

        private async void Button2_Click(object sender, EventArgs e)
        {
            var cts = new CancellationTokenSource();

            try
            {
                using (var con = await ConnectionHelper.CreateConnectionAsync(cts.Token))
                {
                    var chart = con.GetTempChart();
                }
            }
            catch when (cts.IsCancellationRequested)
            {
                return;
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            _wl_cts?.Cancel();
        }

        private void TabControl1_Selected(object sender, TabControlEventArgs e)
        {

        }

        private void Button_wifi_SetCurrent_Click(object sender, EventArgs e)
        {

        }

        private void Button_wifi_SetDefault_Click(object sender, EventArgs e)
        {

        }
    }
}
