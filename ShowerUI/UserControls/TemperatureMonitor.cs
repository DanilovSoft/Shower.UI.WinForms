using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraCharts;
using Newtonsoft.Json;
using ShowerTcpClient;
using ShowerUI.Dto;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace ShowerUI.UserControls
{
    public partial class TemperatureMonitor : UserControl
    {
        private const double HeaterEfficiency = 0.96; // Экспериментально определяем КПД нагревателя.
        private const int CalculationTempLimit = 40;
        private const string TempOnSeries = "TempOn";
        private const string TempOffSeries = "TempOff";
        private const string TempCalculatedSeries = "CalculatedTemp";

        private readonly List<InternalTempModel> _temperatureList = new(600);
        private readonly HeatingTimeLeft _heatingTimeLeft;
        private readonly Action<InternalTempModel> _addTemperatureRecordHandler;
        private CancellationTokenSource? _tempRecorderCts;

        public TemperatureMonitor()
        {
            InitializeComponent();

            double heaterPowerWatt = HeatingTimeLeft.CalcHeaterPower(heaterResistanceOhm: 36);
            
            heaterPowerWatt *= HeaterEfficiency; // Учтём КПД нагревателя.

            float heaterPowerKWatt = (float)(heaterPowerWatt / 1000);
            _heatingTimeLeft = new HeatingTimeLeft(tankHeightMil: 297, tankDiameterMil: 400, heaterPowerKWatt);

            _addTemperatureRecordHandler = AddTemperatureRecord;
        }

        private async void Button_Start_Click(object sender, EventArgs e)
        {
            var cts = _tempRecorderCts = new CancellationTokenSource();
            try
            {
                ClearChart();
                int reconnectCount = -1;

                button_Start.Enabled = false;
                button_Stop.Enabled = true;

            Retry:
                try
                {
                    using (var connection = await ConnectionHelper.CreateConnectionAsync(cts.Token))
                    {
                        reconnectCount++;
                        errorProvider1.SetError(button_Start, null);
                        await Task.Run(() => RecordTempAsync(connection, cts.Token));
                    }
                }
                catch when (cts.IsCancellationRequested)
                {
                    return;
                }
                catch (Exception ex)
                {
                    errorProvider1.SetError(button_Start, ex.Message);
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

        private void Button_Stop_Click(object sender, EventArgs e)
        {
            _tempRecorderCts?.Cancel();
            button_Start.Enabled = true;
            button_Stop.Enabled = false;
        }

        private void Button_Clear_Click(object sender, EventArgs e)
        {
            ClearChart();
        }

        private void Button_SaveAs_Click(object sender, EventArgs e)
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

        private void Button_LoadFrom_Click(object sender, EventArgs e)
        {
            ClearChart();
            LoadTempRecord();

            if (trackBar_TimeLeft.Value != 0)
            {
                CalcTimeLeft();
            }
        }

        private void Button_SimulationStart_Click(object sender, EventArgs e)
        {
            try
            {
                button_SimulationStart.Enabled = false;
                ClearChart();
                StartSimulation();
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                button_SimulationStart.Enabled = true;
            }
        }

        private void TrackBar_TimeLeft_Scroll(object sender, EventArgs e)
        {
            if (trackBar_TimeLeft.Value == 0)
            {
                chartControl_temperature.Series[TempCalculatedSeries].Points.Clear();

                if (_temperatureList.Count > 0)
                {
                    DisplayTimeLeft(_temperatureList[^1].TimeLeft);
                }
            }
            else
            {
                CalcTimeLeft();
            }
        }

        private void DisplayTimeLeft(TimeSpan timeLeft)
        {
            labelTimeLeft.Text = timeLeft.ToString("hh\\:mm\\:ss");
        }

        private void ClearChart()
        {
            _temperatureList.Clear();
            labelTimeLeft.Text = "__:__:__";
            chartControl_temperature.Series[TempOnSeries].Points.Clear();
            chartControl_temperature.Series[TempOffSeries].Points.Clear();
            chartControl_temperature.Series[TempCalculatedSeries].Points.Clear();
        }

        private async Task RecordTempAsync(ShowerConnection con, CancellationToken cancellationToken)
        {
            var sw = Stopwatch.StartNew();
            while (!cancellationToken.IsCancellationRequested)
            {
                var second = (int)sw.Elapsed.TotalSeconds;

                float internalTemp = con.GetInternalTemperature();
                bool heaterEnabled = con.GetHeaterEnabled();
                int minutesLeft = con.GetMinutesLeft();

                var model = new InternalTempModel
                {
                    Second = second,
                    InternalTemp = internalTemp,
                    HeaterEnabled = heaterEnabled,
                    TimeLeft = TimeSpan.FromMinutes(minutesLeft),
                };

                BeginInvoke(_addTemperatureRecordHandler, model);

                await Task.Delay(1000);
            }
        }

        private void AddTemperatureRecord(InternalTempModel model)
        {
            _temperatureList.Add(model);

            if (trackBar_TimeLeft.Value == 0)
            {
                DisplayTimeLeft(model.TimeLeft);
            }
            chartControl_temperature.Series[TempOnSeries].Points.Add(new SeriesPoint(model.Second, model.InternalTemp) { IsEmpty = !model.HeaterEnabled });
            chartControl_temperature.Series[TempOffSeries].Points.Add(new SeriesPoint(model.Second, model.InternalTemp) { IsEmpty = model.HeaterEnabled });
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
                    foreach (var model in _temperatureList)
                    {
                        chartControl_temperature.Series[TempOnSeries].Points.Add(new SeriesPoint(model.Second, model.InternalTemp) { IsEmpty = !model.HeaterEnabled });
                        chartControl_temperature.Series[TempOffSeries].Points.Add(new SeriesPoint(model.Second, model.InternalTemp) { IsEmpty = model.HeaterEnabled });
                    }
                    chartControl_temperature.EndInit();
                }
            }
        }

        private void CalcTimeLeft()
        {
            Debug.Assert(trackBar_TimeLeft.Value != 0);

            int startMinute = trackBar_TimeLeft.Value;
            int startSec = startMinute * 60;

            if (_temperatureList.FirstOrDefault(x => x.HeaterEnabled) is { } startPoint)
            {
                int time = startPoint.Second + startSec;
                if (_temperatureList.FirstOrDefault(x => x.HeaterEnabled && x.Second >= time) is { } point)
                {
                    TimeSpan timeLeft = _heatingTimeLeft.CalcTimeLeft(point.InternalTemp, CalculationTempLimit);
                    DisplayTimeLeft(timeLeft);

                    int sec = (int)timeLeft.TotalSeconds;
                    sec += point.Second;

                    chartControl_temperature.Series[TempCalculatedSeries].Points.Clear();
                    chartControl_temperature.Series[TempCalculatedSeries].Points.Add(new SeriesPoint(point.Second, point.InternalTemp));
                    chartControl_temperature.Series[TempCalculatedSeries].Points.Add(new SeriesPoint(sec, CalculationTempLimit));
                }
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

                chartControl_temperature.Series[1].Points.Add(new SeriesPoint(measure.Second, measure.InternalTemp) { IsEmpty = !measure.HeaterEnabled });
                chartControl_temperature.Series[3].Points.Add(new SeriesPoint(measure.Second, measure.InternalTemp) { IsEmpty = measure.HeaterEnabled });

                interval = TimeSpan.FromMilliseconds(10);
            }
        }
    }
}
