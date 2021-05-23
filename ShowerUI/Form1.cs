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
        /// <summary>
        /// Доступ только через основной поток.
        /// </summary>
        private CancellationTokenSource? _pingCts;
        private CancellationTokenSource? _ctsWwaterLevelCalibration;

        // ctor
        public Form1()
        {
            InitializeComponent();
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

        private void Button_PingCancel_Click(object sender, EventArgs e)
        {
            _pingCts?.Cancel();
        }

        private async void ButtonStartCalibrationClick(object sender, EventArgs e)
        {
            var cts = _ctsWwaterLevelCalibration = new CancellationTokenSource();
            button_stop.Enabled = true;
            buttonStartCalib.Enabled = false;

            var sw = new Stopwatch();
            var median = new FastMedianFilter(9);
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
                                var usec = await Task.Run(async () => 
                                {
                                    await Task.Delay(interval);
                                    return con.GetWaterLevelRaw();
                                });
                                sw.Stop();

                                var elapsed = median.Add((int)sw.ElapsedMilliseconds);
                                if (median.IsInitialized)
                                {
                                    label_elapsedWL.Text = elapsed + " мсек";
                                }

                                AddUsec(usec);
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
            const int dataIntervalSec = 40;
            const int maxPoints = dataIntervalSec * 1000 / 100;

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
