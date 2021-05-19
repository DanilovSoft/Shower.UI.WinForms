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
using Filters;
using Newtonsoft.Json;
using ShowerTcpClient;

namespace ShowerUI.UserControls
{
    public partial class WaterLevel : UserControl
    {
        //private readonly List<ushort> _usecList = new(30000); // 20 минут
        private readonly SwiftPlotDiagramSecondaryAxisY _median = new("Медиана");
        private readonly SwiftPlotDiagramSecondaryAxisY _avg = new("Среднее");
        private readonly Series _usecSeries;
        private readonly Series _percentSeries;
        private readonly Series _medianSeries;
        private readonly Series _averageSeries;
        private WaterLevelSession? _session;
        private FastMedianFilter? _medianFilter;
        private AverageFilter? _avgFilter;

        public WaterLevel()
        {
            InitializeComponent();

            UpdateMedianCheckBoxText();
            UpdateAverageCheckBox();

            chartControl_water_level.Series.Clear();
            _usecSeries = chartControl_water_level.Series[chartControl_water_level.Series.Add("Usec", ViewType.SwiftPlot)];
            _medianSeries = chartControl_water_level.Series[chartControl_water_level.Series.Add("Median", ViewType.SwiftPlot)];
            _percentSeries = chartControl_water_level.Series[chartControl_water_level.Series.Add("%", ViewType.SwiftPlot)];
            _averageSeries = chartControl_water_level.Series[chartControl_water_level.Series.Add("Avg", ViewType.SwiftPlot)];

            _percentSeries.LegendText = "%";
            _usecSeries.LabelsVisibility = DevExpress.Utils.DefaultBoolean.False;

            
            if (_usecSeries.View is SwiftPlotSeriesView usecView)
            {
                usecView.Color = Color.Blue;
            }

            // быстрая диаграмма, не поддерживает инверсию осей.
            if (chartControl_water_level.Diagram is SwiftPlotDiagram swiftDiagram)
            {
                swiftDiagram.SecondaryAxesY.Add(_median);
                swiftDiagram.SecondaryAxesY.Add(_avg);
                //swiftDiagram.SecondaryAxesY.Add(percentAxisY);

                // не отображать шкалу микросекунд.
                swiftDiagram.AxisY.Visibility = DevExpress.Utils.DefaultBoolean.False;
            }

            if (_percentSeries.View is SwiftPlotSeriesView swiftView)
            {
                var percentAxisY = ((SwiftPlotDiagram)chartControl_water_level.Diagram).SecondaryAxesY["percent"];

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
            _usecSeries.CrosshairLabelVisibility = DevExpress.Utils.DefaultBoolean.True;
            _medianSeries.CrosshairLabelVisibility = DevExpress.Utils.DefaultBoolean.True;
            _percentSeries.CrosshairLabelVisibility = DevExpress.Utils.DefaultBoolean.False;
            _averageSeries.CrosshairLabelVisibility = DevExpress.Utils.DefaultBoolean.False;

            _medianSeries.Visible = checkBox_median.Checked;
            _averageSeries.Visible = checkBox_avg.Checked;
            _usecSeries.Visible = checkBox_raw.Checked;
            _percentSeries.Visible = checkBox_percent.Checked;
        }

        private async void Button_Start_Click(object sender, EventArgs e)
        {
            button_start.Enabled = false;
            button_stop.Enabled = true;
            var session = _session = new WaterLevelSession 
            {
                LastPointIndex = _session?.LastPointIndex ?? 0
            };
            int reconnectCount = 0;
            session.Running = true;

            try
            {
                while (!session.Cts.IsCancellationRequested)
                {
                    try
                    {
                        using (var con = await ConnectionHelper.CreateConnectionAsync(session.Cts.Token))
                        {
                            errorProvider1.SetError(button_start, null);
                            label_reconnect_count.Text = $"{reconnectCount}";
                            reconnectCount++;

                            await RecordWaterLevelAsync(con, session);
                        }
                    }
                    catch when (session.Cts.IsCancellationRequested)
                    {
                        return;
                    }
                    catch (Exception ex)
                    {
                        errorProvider1.SetError(button_start, ex.Message);
                        try
                        {
                            await Task.Delay(3000, session.Cts.Token);
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
                session.Cts.Cancel();
                session.Running = false;
            }
        }

        private async Task RecordWaterLevelAsync(ShowerConnection con, WaterLevelSession session)
        {
            session.WaterLevelEmpty = await con.GetWaterLevelEmptyAsync(session.Cts.Token);
            session.WaterLevelFull = await con.GetWaterLevelFullAsync(session.Cts.Token);

            // Число должно быть не чётным.
            byte bufSize = await con.GetWaterLevelBufferSizeAsync();
            if (bufSize % 2 != 1)
            {
                bufSize++;
            }

            trackBar_medianTrackBar.Value = (bufSize - 1) / 2;
            UpdateMedianCheckBoxText();

            SetMinMaxWaterLevel(session.WaterLevelEmpty, session.WaterLevelFull);

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

                _medianSeries.Points.BeginUpdate();
                _averageSeries.Points.BeginUpdate();
                _percentSeries.Points.BeginUpdate();

                ClearMedianSeries();
                ClearAverageSeries();
                ClearPercentSeries();

                for (int i = 0; i < session.UsecList.Count; i++)
                {
                    ushort medianUsec = medianFilter.Add(session.UsecList[i]);
                    if (medianFilter.IsInitialized)
                    {
                        AddMedianSeriesPoint(i, medianUsec);

                        byte percent = session.CalcPercent(medianUsec);
                        AddPercentSeriesPoint(i, percent);

                        ushort avg = avgFilter.AddNextValue(medianUsec);
                        if (avgFilter.IsInitialized)
                        {
                            AddAverageSeriesPoint(i, avg);
                        }
                    }
                }
                _medianSeries.Points.EndUpdate();
                _averageSeries.Points.EndUpdate();
                _percentSeries.Points.EndUpdate();
            }

            trackBar_medianTrackBar.ValueChanged += OnTrackBar;
            trackBar_avg.ValueChanged += OnTrackBar;

            try
            {
                while (!session.Cts.IsCancellationRequested)
                {
                    short rawUsec = await Task.Run(async () =>
                    {
                        await Task.Delay(40);
                        return await con.GetWaterLevelRawAsync(session.Cts.Token);
                    });

                    if (rawUsec != -1)
                    {
                        ushort invertedUsec = (ushort)(session.WaterLevelEmpty - (rawUsec - session.WaterLevelFull));
                        
                        session.UsecList.Add(invertedUsec);

                        ushort medianUsec = medianFilter.Add(invertedUsec);

                        AddRawSeriesPoint(session.LastPointIndex, invertedUsec);

                        if (medianFilter.IsInitialized)
                        {
                            AddMedianSeriesPoint(session.LastPointIndex, medianUsec);

                            byte percent = session.CalcPercent(medianUsec);
                            AddPercentSeriesPoint(session.LastPointIndex, percent);

                            ushort avg = avgFilter.AddNextValue(medianUsec);
                            if (avgFilter.IsInitialized)
                            {
                                AddAverageSeriesPoint(session.LastPointIndex, avg);
                            }
                        }
                    }
                    else
                    {
                        //_usecSeries.Points.Add(new SeriesPoint(argument: t));
                        //_percentSeries.Points.Add(new SeriesPoint(t));
                        //AddUsecMedian(t);
                    }

                    session.LastPointIndex++;
                }
            }
            finally
            {
                trackBar_medianTrackBar.ValueChanged -= OnTrackBar;
                trackBar_avg.ValueChanged -= OnTrackBar;
            }
        }

        private void SetMinMaxWaterLevel(ushort waterLevelEmptyUsec, ushort waterLevelFullUsec)
        {
            if (chartControl_water_level.Diagram is SwiftPlotDiagram swiftDiagram)
            {
                swiftDiagram.AxisY.WholeRange.Auto = false;
                swiftDiagram.AxisY.WholeRange.SetMinMaxValues(waterLevelFullUsec, waterLevelEmptyUsec);

                swiftDiagram.AxisY.VisualRange.Auto = false;
                swiftDiagram.AxisY.VisualRange.SetMinMaxValues(waterLevelFullUsec, waterLevelEmptyUsec);
            }

            _median.WholeRange.Auto = false;
            _median.VisualRange.Auto = false;
            _median.WholeRange.SetMinMaxValues(waterLevelFullUsec, waterLevelEmptyUsec);
            _median.VisualRange.SetMinMaxValues(waterLevelFullUsec, waterLevelEmptyUsec);

            _avg.WholeRange.Auto = false;
            _avg.VisualRange.Auto = false;
            _avg.WholeRange.SetMinMaxValues(waterLevelFullUsec, waterLevelEmptyUsec);
            _avg.VisualRange.SetMinMaxValues(waterLevelFullUsec, waterLevelEmptyUsec);

            textBox_min_water_level.Text = waterLevelFullUsec.ToString();
            textBox_max_water_level.Text = waterLevelEmptyUsec.ToString();
        }

        private void Button_Stop_Click(object sender, EventArgs e)
        {
            _session?.Cts.Cancel();
            button_start.Enabled = true;
            button_stop.Enabled = false;
        }

        private void Button_Clear_Click(object sender, EventArgs e)
        {
            if (_session is { } s)
            {
                s.LastPointIndex = 0;
                s.UsecList.Clear();
            }

            foreach (Series series in chartControl_water_level.Series)
            {
                series.Points.Clear();
            }
        }

        private void Button_SaveAs_Click(object sender, EventArgs e)
        {
            if (_session is { } session)
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
                            string json = JsonConvert.SerializeObject(session.UsecList);
                            File.WriteAllText(dialog.FileName, json);
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(this, ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void Button_LoadFromFile_Click(object sender, EventArgs e)
        {
            using (var dialog = new OpenFileDialog())
            {
                dialog.Filter = "Json File|*.txt";
                dialog.Multiselect = false;
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    string json = File.ReadAllText(dialog.FileName);
                    var usecList = JsonConvert.DeserializeObject<ushort[]>(json);

                    var session = _session = new WaterLevelSession();
                    session.UsecList.Clear();
                    session.UsecList.AddRange(usecList);

                    using (var con = ConnectionHelper.CreateConnectionAsync(CancellationToken.None).Result)
                    {
                        session.WaterLevelEmpty = con.GetWaterLevelEmptyAsync(CancellationToken.None).Result;
                        session.WaterLevelFull = con.GetWaterLevelFullAsync(CancellationToken.None).Result;

                        SetMinMaxWaterLevel(session.WaterLevelEmpty, session.WaterLevelFull);
                    }

                    ShowWaterLevel(session);

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

        private void ShowWaterLevel(WaterLevelSession session)
        {
            var medianWindowSize = GetMedianWindowSize();
            var avgWindowSize = GetAverageTrackBar();

            //var fastMedianFilter = new FastMedianFilter(medianWindowSize);
            var medianFilter = new MedianFilter(medianWindowSize);
            var avgFilter = new AverageFilter(avgWindowSize);

            _usecSeries.Points.BeginUpdate();
            _medianSeries.Points.BeginUpdate();
            _averageSeries.Points.BeginUpdate();
            _percentSeries.Points.BeginUpdate();

            ClearUsec();
            ClearMedianSeries();
            ClearAverageSeries();
            ClearPercentSeries();

            for (int i = 0; i < session.UsecList.Count; i++)
            {
                ushort rawUsec = session.UsecList[i];
                AddRawSeriesPoint(i, rawUsec);

                ushort medianUsec = medianFilter.Add(rawUsec);

                if (medianFilter.IsInitialized)
                {
                    AddMedianSeriesPoint(i, medianUsec);

                    byte percent = session.CalcPercent(medianUsec);
                    AddPercentSeriesPoint(i, percent);

                    ushort avgUsec = avgFilter.AddNextValue(medianUsec);
                    if (avgFilter.IsInitialized)
                    {
                        AddAverageSeriesPoint(i, avgUsec);
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
            int windowSize = trackBar_medianTrackBar.Value * 2 + 1;
            return windowSize;
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

        private void ClearUsec()
        {
            _usecSeries.Points.Clear();
        }

        private void ClearMedianSeries()
        {
            _medianSeries.Points.Clear();
        }

        private void ClearPercentSeries()
        {
            _percentSeries.Points.Clear();
        }

        private void AddPercentSeriesPoint(int pointIndex, byte percent)
        {
            _percentSeries.Points.Add(new SeriesPoint(pointIndex, percent));
        }

        private void AddMedianSeriesPoint(int pointIndex, ushort value)
        {
            _medianSeries.Points.Add(new SeriesPoint(pointIndex, value));
        }

        private void AddRawSeriesPoint(int pointIndex, ushort value)
        {
            _usecSeries.Points.Add(new SeriesPoint(pointIndex, value));
        }

        private void ClearAverageSeries()
        {
            _averageSeries.Points.Clear();
        }

        private void AddAverageSeriesPoint(int pointIndex, ushort value)
        {
            _averageSeries.Points.Add(new SeriesPoint(pointIndex, value));
        }

        private void TrackBar_Median_Scroll(object sender, EventArgs e)
        {
            UpdateMedianCheckBoxText();

            if (_session is { } session)
            {
                if (!session.Running)
                {
                    ShowWaterLevel(session);
                }
            }
        }

        private void TrackBar_avg_Scroll(object sender, EventArgs e)
        {
            UpdateAverageCheckBox();

            if (_session is { } session)
            {
                if (!session.Running)
                {
                    ShowWaterLevel(session);
                }
            }
        }

        private void UpdateMedianCheckBoxText()
        {
            checkBox_median.Text = $"Медиана ({GetMedianWindowSize()})";
        }

        private void CheckBox_Median_CheckedChanged(object sender, EventArgs e)
        {
            _medianSeries.Visible = checkBox_median.Checked;
        }

        private void CheckBox_RAW_CheckedChanged(object sender, EventArgs e)
        {
            _usecSeries.Visible = checkBox_raw.Checked;
        }

        private void CheckBox_avg_CheckedChanged(object sender, EventArgs e)
        {
            _averageSeries.Visible = checkBox_avg.Checked;
        }

        private void CheckBox_Percent_CheckedChanged(object sender, EventArgs e)
        {
            _percentSeries.Visible = checkBox_percent.Checked;
        }
    }
}
