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
using ShowerUI.ShowerClient;

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
        private CancellationTokenSource? _cts_waterLevelCalibration;
        private CancellationTokenSource? _cts_loadProperties;
        private Series? _usecSeries;
        private Series? _percentSeries;
        private Series? _medianSeries;
        private Series? _averageSeries;
        private SwiftPlotDiagramSecondaryAxisY? _median;
        private SwiftPlotDiagramSecondaryAxisY? _avg;
        private ushort _waterLevelEmpty;
        private ushort _waterLevelFull;

        // ctor
        public Form1()
        {
            InitializeComponent();
            _addTemperatureRecordHandler = AddTemperatureRecord;

            UpdateMedianCheckBox();
            UpdateAverageCheckBox();
        }

        private void UpdateMedianCheckBox()
        {
            checkBox_median.Text = $"Медиана ({GetMedianWindowSize()})";
        }

        // инициализация
        private void InitWaterLevelChart()
        {
            chartControl_water_level.Series.Clear();

            _usecSeries = chartControl_water_level.Series[chartControl_water_level.Series.Add("Usec", ViewType.SwiftPlot)];
            _medianSeries = chartControl_water_level.Series[chartControl_water_level.Series.Add("Median", ViewType.SwiftPlot)];
            _percentSeries = chartControl_water_level.Series[chartControl_water_level.Series.Add("%", ViewType.SwiftPlot)];
            _averageSeries = chartControl_water_level.Series[chartControl_water_level.Series.Add("Avg", ViewType.SwiftPlot)];

            _percentSeries.LegendText = "%";
            _usecSeries.LabelsVisibility = DevExpress.Utils.DefaultBoolean.False;

            var percentAxisY = new SwiftPlotDiagramSecondaryAxisY("Уровень в процентах");
            percentAxisY.WholeRange.Auto = false;
            percentAxisY.WholeRange.SetMinMaxValues(0, 99);
            percentAxisY.VisualRange.Auto = false;
            percentAxisY.VisualRange.SetMinMaxValues(0, 99);
            percentAxisY.Visibility = DevExpress.Utils.DefaultBoolean.True;
            percentAxisY.MinorCount = 1;
            percentAxisY.GridLines.Visible = true;
            percentAxisY.NumericScaleOptions.AutoGrid = false;
            percentAxisY.NumericScaleOptions.CustomGridAlignment = 2;
            percentAxisY.NumericScaleOptions.GridSpacing = 2;
            percentAxisY.NumericScaleOptions.GridOffset = 2;
            percentAxisY.Tickmarks.MinorVisible = false;
            percentAxisY.Tickmarks.Visible = false;

            _median = new SwiftPlotDiagramSecondaryAxisY("Медиана");
            _avg = new SwiftPlotDiagramSecondaryAxisY("Среднее");

            if (_usecSeries.View is SwiftPlotSeriesView usecView)
            {
                usecView.Color = Color.Blue;
            }

            // быстрая диаграмма, не поддерживает инверсию осей.
            if (chartControl_water_level.Diagram is SwiftPlotDiagram swiftDiagram)
            {
                swiftDiagram.SecondaryAxesY.Add(_median);
                swiftDiagram.SecondaryAxesY.Add(_avg);
                swiftDiagram.SecondaryAxesY.Add(percentAxisY);

                // не отображать шкалу микросекунд.
                swiftDiagram.AxisY.Visibility = DevExpress.Utils.DefaultBoolean.False;
            }

            if (_percentSeries.View is SwiftPlotSeriesView swiftView)
            {
                swiftView.AxisY = percentAxisY;
                //swiftView.AxisY.Visibility = DevExpress.Utils.DefaultBoolean.False;
                swiftView.Color = Color.Red;
            }

            if (_medianSeries.View is SwiftPlotSeriesView swiftMedian)
            {
                swiftMedian.AxisY = _median;
                swiftMedian.AxisY.Visibility = DevExpress.Utils.DefaultBoolean.False;
                swiftMedian.Color = Color.Violet;
            }

            if (_averageSeries.View is SwiftPlotSeriesView swiftAvg)
            {
                swiftAvg.AxisY = _avg;
                swiftAvg.AxisY.Visibility = DevExpress.Utils.DefaultBoolean.False;
                swiftAvg.Color = Color.DarkGreen;
            }

            // выключить отображение значений микросекунд.
            _usecSeries.CrosshairLabelVisibility = DevExpress.Utils.DefaultBoolean.False;
            _medianSeries.CrosshairLabelVisibility = DevExpress.Utils.DefaultBoolean.False;
            _percentSeries.CrosshairLabelVisibility = DevExpress.Utils.DefaultBoolean.False;
            _averageSeries.CrosshairLabelVisibility = DevExpress.Utils.DefaultBoolean.False;
        }

        private void SetMinMaxWaterLevel(ushort waterLevelEmpty, ushort waterLevelFull)
        {
            if (chartControl_water_level.Diagram is SwiftPlotDiagram swiftDiagram)
            {
                swiftDiagram.AxisY.WholeRange.Auto = false;
                swiftDiagram.AxisY.WholeRange.SetMinMaxValues(waterLevelFull, waterLevelEmpty);

                swiftDiagram.AxisY.VisualRange.Auto = false;
                swiftDiagram.AxisY.VisualRange.SetMinMaxValues(waterLevelFull, waterLevelEmpty);
            }

            _median.WholeRange.Auto = false;
            _median.VisualRange.Auto = false;
            _median.WholeRange.SetMinMaxValues(waterLevelFull, waterLevelEmpty);
            _median.VisualRange.SetMinMaxValues(waterLevelFull, waterLevelEmpty);

            _avg.WholeRange.Auto = false;
            _avg.VisualRange.Auto = false;
            _avg.WholeRange.SetMinMaxValues(waterLevelFull, waterLevelEmpty);
            _avg.VisualRange.SetMinMaxValues(waterLevelFull, waterLevelEmpty);

            textBox_min_water_level.Text = waterLevelFull.ToString();
            textBox_max_water_level.Text = waterLevelEmpty.ToString();
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
                    using (var connection = await ShowerConnection.CreateConnectionAsync(cts.Token))
                    {
                        reconnectCount++;
                        errorProvider1.SetError(buttonTempStartRecord, null);
                        await Task.Run(() => RecordDataAsync(connection, cts.Token));
                    }
                }
                catch when (cts.IsCancellationRequested)
                {
                    return;
                }
                catch (Exception ex)
                {
                    errorProvider1.SetError(buttonTempStartRecord, ex.Message);
                    label_temp_reconnect.Text = $"Reconnect count: {reconnectCount}";

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

        private async Task RecordDataAsync(ShowerConnection con, CancellationToken cancellationToken)
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

            //var pid = new PidController.PidController(0.2, 0.001, 1, 39, 22)
            //{

            //    // Уставка.
            //    SetPoint = 38
            //};

            TimeSpan interval = TimeSpan.Zero;
            for (int i = 0; i < collection.Length; i++)
            {
                InternalTempModel measure = collection[i];

                await Task.Delay(interval);
                if (IsDisposed)
                    return;

                // Сколько получили на самом деле.
                //pid.ProcessVariable = measure.InternalTemp;

                // Сколько времени ушло на измерение.
                //double pidVal = pid.ControlVariable(interval);

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

            using (var connection = await ShowerConnection.CreateConnectionAsync(CancellationToken.None).ConfigureAwait(false))
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

        private bool _wl_started;

        private async void Button_WaterLevelStart_Click(object sender, EventArgs e)
        {
            var cts = _wl_cts = new CancellationTokenSource();
            int reconnect_count = 0;

            button_wl.Enabled = false;
            button_wl_stop.Enabled = true;

            _wl_started = true;

            while (true)
            {
                try
                {
                    using (var con = await ShowerConnection.CreateConnectionAsync(cts.Token))
                    {
                        label_reconnect_count.Text = $"Reconnect count: {reconnect_count}";
                        reconnect_count++;

                        await RecordWaterLevelAsync(con, cts.Token);
                    }
                }
                catch when (cts.IsCancellationRequested)
                {
                    _wl_started = false;
                    return;
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex);
                    try
                    {
                        await Task.Delay(3000, cts.Token);
                    }
                    catch { return; }
                }
            }
        }

        private void Button_WaterLevelStop_Click(object sender, EventArgs e)
        {
            _wl_cts?.Cancel();
            button_wl.Enabled = true;
            button_wl_stop.Enabled = false;
        }

        private void ClearChartWL()
        {
            foreach (Series series in chartControl_water_level.Series)
            {
                series.Points.Clear();
            }
        }

        private async Task GetWaterLevelLoopAsync(ShowerConnection con, CancellationToken cancellationToken)
        {
            //int time = 0;
            //TimeSpan interval = TimeSpan.FromMinutes(60);
            const int intervalMsec = 300;

            async Task<(ushort usec, byte percent)> LoadAsync()
            {
                await Task.Delay(intervalMsec, _wl_cts.Token);
                var value = await con.GetWaterLevelAsync(cancellationToken);
                var percent = await con.GetWaterPercent(cancellationToken);
                //time++;

                return (value, percent);
            }

            while (true)
            {
                var (usec, percent) = await Task.Run(() => LoadAsync());

                var series1 = chartControl_water_level.Series[0];
                var series2 = chartControl_water_level.Series[1];

                //series1.Points.Add(new SeriesPoint(DateTime.Now, usec));
                series2.Points.Add(new SeriesPoint(DateTime.Now, percent));

                //if(series1.Points.Count > 500)
                //{

                //}
            }
        }

        private void TrackBar2_Scroll(object sender, EventArgs e)
        {

        }

        private async Task RecordWaterLevelAsync(ShowerConnection con, CancellationToken cancellationToken)
        {
            _waterLevelEmpty = await con.GetWaterLevelEmptyAsync(cancellationToken);
            _waterLevelFull = await con.GetWaterLevelFullAsync(cancellationToken);

            _usecList.Clear();

            SetMinMaxWaterLevel(_waterLevelEmpty, _waterLevelFull);

            int medianWindowSize = GetMedianWindowSize();
            var medianFilter = new FastMedianFilter(medianWindowSize);

            int avgWindowSize = GetAverageTrackBar();
            var avgFilter = new AverageFilter(avgWindowSize);

            void OnTrackBar(object sender, EventArgs e)
            {
                medianWindowSize = GetMedianWindowSize();
                avgWindowSize = GetAverageTrackBar();

                medianFilter = new FastMedianFilter(medianWindowSize);
                avgFilter = new AverageFilter(avgWindowSize);

                ClearMedian();
                ClearAverage();
                ClearPercent();

                int t = 0;

                _medianSeries.Points.BeginUpdate();
                _averageSeries.Points.BeginUpdate();
                _percentSeries.Points.BeginUpdate();
                for (int i = 0; i < _usecList.Count; i++)
                {
                    t++;
                    ushort medianUsec = medianFilter.Add(_usecList[i]);
                    if (medianFilter.IsInitialized)
                    {
                        AddUsecMedian(t, medianUsec);

                        ushort avg = avgFilter.AddNextValue(medianUsec);
                        if (avgFilter.IsInitialized)
                        {
                            AddAverageUsec(t, avg);
                            byte percent = GetPercent(avg);
                            AddPercent(t, percent);
                        }
                    }
                }
                _medianSeries.Points.EndUpdate();
                _averageSeries.Points.EndUpdate();
                _percentSeries.Points.EndUpdate();
            }

            trackBar_median.ValueChanged += OnTrackBar;
            trackBar_avg.ValueChanged += OnTrackBar;

            try
            {
                int t = 0;

                while (true)
                {
                    short usec = 0;

                    await Task.Run(async () =>
                    {
                        await Task.Delay(40);
                        usec = await con.GetWaterLevelRawAsync(cancellationToken);
                    });

                    t++;

                    if (usec != -1)
                    {
                        ushort invertedUsec = (ushort)(_waterLevelEmpty - (usec - _waterLevelFull));
                        ushort medianUsec = medianFilter.Add(invertedUsec);

                        _usecSeries.Points.Add(new SeriesPoint(t, invertedUsec));

                        _usecList.Add(invertedUsec);

                        if (medianFilter.IsInitialized)
                        {
                            AddUsecMedian(t, medianUsec);

                            ushort avg = avgFilter.AddNextValue(medianUsec);
                            if (avgFilter.IsInitialized)
                            {
                                AddAverageUsec(t, avg);
                                byte percent = GetPercent(avg);
                                AddPercent(t, percent);
                            }
                        }
                    }
                    else
                    {
                        //_usecSeries.Points.Add(new SeriesPoint(argument: t));
                        //_percentSeries.Points.Add(new SeriesPoint(t));
                        //AddUsecMedian(t);
                    }
                }
            }
            finally
            {
                trackBar_median.ValueChanged -= OnTrackBar;
                trackBar_avg.ValueChanged -= OnTrackBar;
            }
        }

        // From 0.0 to 50
        private float GetPoint(ushort usec)
        {
            var usec_range = (ushort)(_waterLevelEmpty - _waterLevelFull);

            /* Поправка на выход из диаппазона */
            FixRawRange(ref usec);

            /* Смещение */
            usec -= _waterLevelFull;

            /* Уровень воды в микросекундах */
            usec = (ushort)(usec_range - usec);

            double tmp = usec * (100 / 2d);

            /* Сколько пунктов из 50 */
            double point = tmp / usec_range;

            float pointf = (float)point;

            return pointf;
        }

        private void FixRawRange(ref ushort raw_value)
        {
            if (raw_value < _waterLevelFull)
                raw_value = _waterLevelFull;
            else
            if (raw_value > _waterLevelEmpty)
                raw_value = _waterLevelEmpty;
        }

        private byte GetPercent(ushort usec)
        {
            ushort invertedUsec = (ushort)(_waterLevelEmpty - (usec - _waterLevelFull));

            float pointf = GetPoint(invertedUsec);

            byte point = (byte)pointf;
            //byte point = (byte)Math.Round(pointf);

            int percent = (point * 2);

            //percent = (100 - percent);

            if (percent > 99)
                percent = 99;

            return (byte)percent;
        }

        private void TrackBar_median_Scroll(object sender, EventArgs e)
        {
            UpdateMedianCheckBox();

            if (!_wl_started)
            {
                ShowWaterLevel();
            }
        }

        private void ShowWaterLevel()
        {
            var medianWindowSize = GetMedianWindowSize();
            var avgWindowSize = GetAverageTrackBar();

            //var fastMedianFilter = new FastMedianFilter(medianWindowSize);
            var medianFilter = new MedianFilter(medianWindowSize);
            var avgFilter = new AverageFilter(avgWindowSize);

            //chartControl_water_level.BeginInit();
            _usecSeries.Points.BeginUpdate();
            _medianSeries.Points.BeginUpdate();
            _averageSeries.Points.BeginUpdate();
            _percentSeries.Points.BeginUpdate();

            ClearUsec();
            ClearMedian();
            ClearAverage();
            ClearPercent();

            int t = 0;


            for (int i = 0; i < _usecList.Count; i++)
            {
                t++;

                ushort usec = _usecList[i];
                AddUsec(t, usec);

                ushort medianUsec = medianFilter.Add(usec);
                //var fastMedian = fastMedianFilter.Add(usec);
                //if(fastMedian != medianUsec)
                //{

                //}

                if (medianFilter.IsInitialized)
                {
                    AddUsecMedian(t, medianUsec);

                    ushort avg = avgFilter.AddNextValue(medianUsec);
                    if (avgFilter.IsInitialized)
                    {
                        AddAverageUsec(t, avg);
                        byte percent = GetPercent(avg);
                        AddPercent(t, percent);
                    }
                }
            }
            _usecSeries.Points.EndUpdate();
            _medianSeries.Points.EndUpdate();
            _averageSeries.Points.EndUpdate();
            _percentSeries.Points.EndUpdate();
        }

        private int GetMedianWindowSize()
        {
            int windowSize = trackBar_median.Value * 2 + 1;
            return windowSize;
        }

        private void TrackBar2_Scroll_1(object sender, EventArgs e)
        {
            UpdateAverageCheckBox();

            if (!_wl_started)
            {
                ShowWaterLevel();
            }
        }

        private int GetAverageTrackBar()
        {
            int value = trackBar_avg.Value * 8;
            return value;
        }

        private void UpdateAverageCheckBox()
        {
            checkBox_avg.Text = $"Среднее ({GetAverageTrackBar()})";
        }

        private void CheckBox_avg_CheckedChanged(object sender, EventArgs e)
        {
            _averageSeries.Visible = checkBox_avg.Checked;
        }

        private void Button8_Click(object sender, EventArgs e)
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
                        string json = JsonConvert.SerializeObject(_usecList);
                        File.WriteAllText(dialog.FileName, json);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ClearUsec()
        {
            _usecSeries.Points.Clear();
        }

        private void ClearMedian()
        {
            _medianSeries.Points.Clear();
        }

        private void ClearPercent()
        {
            _percentSeries.Points.Clear();
        }

        private void AddPercent(int t, byte percent)
        {
            _percentSeries.Points.Add(new SeriesPoint(t, percent));
        }

        private void AddUsec(int t, ushort value)
        {
            _usecSeries.Points.Add(new SeriesPoint(t, value));
        }

        private void AddUsecMedian(int t, ushort value)
        {
            _medianSeries.Points.Add(new SeriesPoint(t, value));
        }

        private void ClearAverage()
        {
            _averageSeries.Points.Clear();
        }

        private void AddAverageUsec(int t, ushort value)
        {
            _averageSeries.Points.Add(new SeriesPoint(t, value));
        }

        private void AddUsecMedian(int t)
        {
            _medianSeries.Points.Add(new SeriesPoint(t));
        }

        private void Button_clear_wl_Click(object sender, EventArgs e)
        {
            ClearChartWL();
        }

        private async void Button_Load_Properties_Click(object sender, EventArgs e)
        {
            panel_properties.Enabled = false;
            panel_properties_custom.Enabled = false;
            button_save_properties.Enabled = false;
            buttonLoadProps.Enabled = false;
            button_load_props_canc.Enabled = true;

            var cts = _cts_loadProperties = new CancellationTokenSource();

            try
            {
                using (var con = await ShowerConnection.CreateConnectionAsync(cts.Token))
                {
                    await LoadPropertiesAsync(con, cts.Token);
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

            LoadPropsEnable();
        }

        private void LoadPropsEnable()
        {
            panel_properties.Enabled = true;
            panel_properties_custom.Enabled = true;
            button_save_properties.Enabled = true;
            buttonLoadProps.Enabled = true;
            button_load_props_canc.Enabled = false;
        }

        private async Task LoadPropertiesAsync(ShowerConnection con, CancellationToken cancellationToken)
        {
            editText_wl_full.Value = await con.RequestAsync<ushort>(ShowerCodes.GetWaterLevelFull, cancellationToken);
            editText_wl_empty.Value = await con.RequestAsync<ushort>(ShowerCodes.GetWaterLevelEmpty, cancellationToken);
            editText_min_water_heating_percent.Value = await con.RequestAsync<byte>(ShowerCodes.GetMinimumWaterHeatingLevel, cancellationToken);
            editText_abs_heating_time_limit.Value = await con.RequestAsync<byte>(ShowerCodes.GetAbsoluteHeatingTimeLimit, cancellationToken);
            editText_heating_time_limit.Value = await con.RequestAsync<byte>(ShowerCodes.GetHeatingTimeLimit, cancellationToken);
            editText_light_brightness.Value = await con.RequestAsync<byte>(ShowerCodes.GetLightBrightness, cancellationToken);
            editText_wifi_power.Value = await con.RequestAsync<byte>(ShowerCodes.GetWiFiPower, cancellationToken);

            editText_WL_measure_interval.Value = await con.RequestAsync<byte>(ShowerCodes.GetWaterLevelMeasureInterval, cancellationToken);
            editText_wl_ring_buffer_size.Value = await con.RequestAsync<byte>(ShowerCodes.GetWaterLevelRingBufferSize, cancellationToken);
            editText_wl_cut_off_percent.Value = await con.RequestAsync<byte>(ShowerCodes.GetWaterValveCutOffPercent, cancellationToken);
            editText_internal_temp_buf_size.Value = await con.RequestAsync<byte>(ShowerCodes.GetTempSensorInternalTempBufferSize, cancellationToken);
            editText_wl_usec_per_deg.Value = await con.RequestAsync<ushort>(ShowerCodes.GetWaterLevelUsecPerDeg, cancellationToken) / 100f;
            editText_button_time.Value = await con.RequestAsync<byte>(ShowerCodes.GetButtonTimeMsec, cancellationToken);

            byte iwd = await con.RequestAsync<byte>(ShowerCodes.GetWatchDogWasReset, cancellationToken);
            checkBox_iwd.Checked = Convert.ToBoolean(iwd);
        }

        private void SaveProperties(ShowerConnection con)
        {
            if (editText_wl_full.HasChanges)
            {
                con.BuildRequest()
                   .Write(ShowerCodes.SetWaterLevelFull)
                   .Write(editText_wl_full.GetValue<ushort>())
                   .ReadOK();

                editText_wl_full.ResetHasChanges();
            }

            if (editText_wl_empty.HasChanges)
            {
                con.BuildRequest()
                   .Write(ShowerCodes.SetWaterLevelEmpty)
                   .Write(editText_wl_empty.GetValue<ushort>())
                   .ReadOK();

                editText_wl_empty.ResetHasChanges();
            }

            if (editText_min_water_heating_percent.HasChanges)
            {
                con.BuildRequest()
                   .Write(ShowerCodes.SetMinimumWaterHeatingLevel)
                   .Write(editText_min_water_heating_percent.GetValue<byte>())
                   .ReadOK();

                editText_min_water_heating_percent.ResetHasChanges();
            }

            if (editText_abs_heating_time_limit.HasChanges)
            {
                con.BuildRequest()
                   .Write(ShowerCodes.SetAbsoluteHeatingTimeLimit)
                   .Write(editText_abs_heating_time_limit.GetValue<byte>())
                   .ReadOK();

                editText_abs_heating_time_limit.ResetHasChanges();
            }

            if (editText_heating_time_limit.HasChanges)
            {
                con.BuildRequest()
                   .Write(ShowerCodes.SetHeatingTimeLimit)
                   .Write(editText_heating_time_limit.GetValue<byte>())
                   .ReadOK();

                editText_heating_time_limit.ResetHasChanges();
            }

            if (editText_light_brightness.HasChanges)
            {
                con.BuildRequest()
                   .Write(ShowerCodes.SetLightBrightness)
                   .Write(editText_light_brightness.GetValue<byte>())
                   .ReadOK();

                editText_light_brightness.ResetHasChanges();
            }

            if (editText_wifi_power.HasChanges)
            {
                con.BuildRequest()
                   .Write(ShowerCodes.SetWiFiPower)
                   .Write(editText_wifi_power.GetValue<byte>())
                   .ReadOK();

                editText_wifi_power.ResetHasChanges();
            }

            if (editText_WL_measure_interval.HasChanges)
            {
                con.BuildRequest()
                   .Write(ShowerCodes.SetWaterLevelMeasureInterval)
                   .Write(editText_WL_measure_interval.GetValue<byte>())
                   .ReadOK();

                editText_WL_measure_interval.ResetHasChanges();
            }

            if (editText_wl_ring_buffer_size.HasChanges)
            {
                con.BuildRequest()
                   .Write(ShowerCodes.SetWaterLevelRingBufferSize)
                   .Write(editText_wl_ring_buffer_size.GetValue<byte>())
                   .ReadOK();

                editText_wl_ring_buffer_size.ResetHasChanges();
            }

            if (editText_wl_cut_off_percent.HasChanges)
            {
                con.BuildRequest()
                  .Write(ShowerCodes.SetWaterValveCutOffPercent)
                  .Write(editText_wl_cut_off_percent.GetValue<byte>())
                  .ReadOK();

                editText_wl_cut_off_percent.ResetHasChanges();
            }

            if (editText_internal_temp_buf_size.HasChanges)
            {
                con.BuildRequest()
                   .Write(ShowerCodes.SetTempSensorInternalTempBufferSize)
                   .Write(editText_internal_temp_buf_size.GetValue<byte>())
                   .ReadOK();

                editText_internal_temp_buf_size.ResetHasChanges();
            }

            if (editText_wl_usec_per_deg.HasChanges)
            {
                con.BuildRequest()
                  .Write(ShowerCodes.SetWaterLevelUsecPerDeg)
                  .Write((ushort)(editText_wl_usec_per_deg.GetValue<float>() * 100))
                  .ReadOK();

                editText_wl_usec_per_deg.ResetHasChanges();
            }

            if (editText_button_time.HasChanges)
            {
                con.BuildRequest()
                  .Write(ShowerCodes.SetButtonTimeMsec)
                  .Write(editText_button_time.GetValue<byte>())
                  .ReadOK();

                editText_button_time.ResetHasChanges();
            }

            con.BuildRequest()
                .Write(ShowerCodes.Save)
                .ReadOK();
        }

        private void ResetToolStripMenuItem_Reset_Click(object sender, EventArgs e)
        {
            Restart();
        }

        private async void Restart()
        {
            try
            {
                Enabled = false;
                using (var connection = await ShowerConnection.CreateConnectionAsync(CancellationToken.None))
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

        private async void Button_Save_Properties_Click(object sender, EventArgs e)
        {
            groupBox_properties.Enabled = false;

            try
            {
                using (var con = await ShowerConnection.CreateConnectionAsync(CancellationToken.None))
                {
                    SaveProperties(con);
                }
                buttonLoadProps.PerformClick();
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                groupBox_properties.Enabled = true;
            }
        }

        private void CheckBox_uSec_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox3.Checked)
            {
                _usecSeries.Visible = true;
            }
            else
            {
                _usecSeries.Visible = false;
            }
        }

        private void CheckBox_WL_Median_CheckedChanged(object sender, EventArgs e)
        {
            _medianSeries.Visible = checkBox_median.Checked;
        }

        private void CheckBox_percent_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox4.Checked)
            {
                _percentSeries.Visible = true;
            }
            else
            {
                _percentSeries.Visible = false;
            }
        }

        private void PingToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (var connection = ShowerConnection.CreateConnectionAsync(CancellationToken.None).Result)
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
                using (var connection = await ShowerConnection.CreateConnectionAsync(cts.Token))
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
                me.Enabled = true;
            }
        }

        private void Button10_Click(object sender, EventArgs e)
        {
            _pingCts?.Cancel();
        }

        private void Button_wifi_cur_Click(object sender, EventArgs e)
        {

        }

        private async void Button_start_Click(object sender, EventArgs e)
        {
            var cts = _cts_waterLevelCalibration = new CancellationTokenSource();
            button_stop.Enabled = true;
            button_start.Enabled = false;

            TimeSpan interval;// = TimeSpan.FromMilliseconds(100);
            var sw = new Stopwatch();

            while (true)
            {
                try
                {
                    using (var con = await ShowerConnection.CreateConnectionAsync(cts.Token))
                    {
                        ushort waterLevelEmpty = await con.RequestAsync<ushort>(ShowerCodes.GetWaterLevelEmpty, cts.Token);
                        ushort waterLevelFull = await con.RequestAsync<ushort>(ShowerCodes.GetWaterLevelFull, cts.Token);

                        SetWLCalibration(waterLevelEmpty, waterLevelFull);

                        while (true)
                        {
                            interval = TimeSpan.FromMilliseconds((int)numeric_wl_interval.Value);

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
                catch
                {
                    Debug.WriteLine("Error");

                    try
                    {
                        await Task.Delay(3000, cts.Token);
                        continue;
                    }
                    catch
                    {
                        return;
                    }
                }
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

            decimal cm = usec / 58m;

            cm = Math.Round(cm, 3);

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
            button_start.Enabled = true;

            _cts_waterLevelCalibration?.Cancel();
            _cts_waterLevelCalibration = null;
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            Restart();
        }

        private void Button9_Click(object sender, EventArgs e)
        {
            _cts_loadProperties?.Cancel();
            _cts_loadProperties = null;

            LoadPropsEnable();
        }

        private async void Button2_Click(object sender, EventArgs e)
        {
            var cts = new CancellationTokenSource();

            try
            {
                using (var con = await ShowerConnection.CreateConnectionAsync(cts.Token))
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

        private void Form1_Load(object sender, EventArgs e)
        {
            InitWaterLevelChart();
        }

        private void TabControl1_Selected(object sender, TabControlEventArgs e)
        {

        }

        private void Button9_Click_1(object sender, EventArgs e)
        {
            using (var dialog = new OpenFileDialog())
            {
                dialog.Filter = "Json File|*.txt";
                dialog.Multiselect = false;
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    string json = File.ReadAllText(dialog.FileName);
                    var usecList = JsonConvert.DeserializeObject<List<ushort>>(json);
                    _usecList.Clear();
                    _usecList.AddRange(usecList);

                    using (var con = ShowerConnection.CreateConnectionAsync(CancellationToken.None).Result)
                    {
                        _waterLevelEmpty = con.GetWaterLevelEmptyAsync(CancellationToken.None).Result;
                        _waterLevelFull = con.GetWaterLevelFullAsync(CancellationToken.None).Result;

                        SetMinMaxWaterLevel(_waterLevelEmpty, _waterLevelFull);
                    }

                    ShowWaterLevel();

                    //chartControl_temperature.BeginInit();
                    //foreach (var item in _dataCollection)
                    //{
                    //    //chartControl1.Series[0].Points.Add(new SeriesPoint(item.Time, item.InternalTemp) { IsEmpty = !item.HeaterEnabled });
                    //    chartControl_temperature.Series[1].Points.Add(new SeriesPoint(item.Time, item.AverageInternalTemp) { IsEmpty = !item.HeaterEnabled });
                    //    //chartControl1.Series[2].Points.Add(new SeriesPoint(item.Time, item.InternalTemp) { IsEmpty = item.HeaterEnabled });
                    //    chartControl_temperature.Series[3].Points.Add(new SeriesPoint(item.Time, item.AverageInternalTemp) { IsEmpty = item.HeaterEnabled });
                    //}
                    //chartControl_temperature.EndInit();
                }
            }
        }
    }

    internal static class ExtensionMethods
    {
        public static void BoldText(this Label label)
        {
            label.Font = new Font(label.Font, FontStyle.Bold);
        }

        public static void RegularText(this Label label)
        {
            if (label.Font.Bold)
            {
                label.Font = new Font(label.Font, FontStyle.Regular);
            }
        }
    }
}
