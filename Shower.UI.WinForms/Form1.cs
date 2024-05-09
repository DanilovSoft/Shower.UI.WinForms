using System.Diagnostics;
using System.Text;
using DevExpress.XtraCharts;
using System.Net.NetworkInformation;
using Shower.Domain.RpcClient;
using Shower.Domain.Filters;

namespace ShowerUI;

public partial class Form1 : Form
{
    /// <summary>
    /// Доступ только через основной поток.
    /// </summary>
    private CancellationTokenSource? _pingCts;
    private CancellationTokenSource? _ctsWaterLevelCalibration; // доступ из основного потока.

    public Form1()
    {
        InitializeComponent();
    }

    private async Task SetAP()
    {
        string apMac = "D4:CA:6D:11:38:AF".ToLower();
        var ap = $@"""Miles"",""KLEZM00D"",""{apMac}""";
        _ = Encoding.ASCII.GetBytes(ap);
        int length = ap.Length;  // 38

        using var connection = await ConnectionHelper.CreateConnectionAsync(CancellationToken.None).ConfigureAwait(false);
        //connection.Writer.Write(ShowerCodes.SetDefAP);
        //connection.Writer.Write(buf);
        //connection.Writer.End();
        //connection.Writer.Send();

        //connection.Reader.ReadOK();
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
            using var connection = await ConnectionHelper.CreateConnectionAsync(CancellationToken.None);
            connection.Reset();
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
        using var connection = ConnectionHelper.CreateConnectionAsync(CancellationToken.None).Result;
        var code = connection.Ping();
    }

    private async void Button7_Click(object sender, EventArgs e)
    {
        var me = (Button)sender;
        var cts = _pingCts = new CancellationTokenSource();
        me.Enabled = false;

        try
        {
            using var connection = await ConnectionHelper.CreateConnectionAsync(cts.Token);
            var code = await connection.PingAsync(cts.Token);
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

    private async void Button_WaterCalibration_Click(object sender, EventArgs e)
    {
        if (_ctsWaterLevelCalibration is null || !_ctsWaterLevelCalibration.TryReset())
        {
            _ctsWaterLevelCalibration = new();
        }

        await WaterCalibration(_ctsWaterLevelCalibration.Token);
    }

    private async Task WaterCalibration(CancellationToken ct)
    {
        button_stop.Enabled = true;
        buttonStartCalib.Enabled = false;

        var sw = new Stopwatch();
        //var median = new MedianFilter<ushort>(7);

        while (!ct.IsCancellationRequested)
        {
            // будем периодически контроллировать число ошибок датчика.

            try
            {
                using var con = await ConnectionHelper.CreateConnectionAsync(ct);
                using var _ = StartWaterCallibrationTimer(con);
                var waterLevelEmpty = await con.RequestAsync<ushort>(ShowerCodes.GetWaterLevelEmptyUsec, ct);
                var waterLevelFull = await con.RequestAsync<ushort>(ShowerCodes.GetWaterLevelFullUsec, ct);

                SetWLCalibration(waterLevelEmpty, waterLevelFull);
                errorProvider1.SetError(buttonStartCalib, null);

                while (!ct.IsCancellationRequested)
                {
                    var interval = TimeSpan.FromMilliseconds((int)numericWaterLevelCalibInterval.Value);

                    sw.Restart();
                    await Task.Delay(interval, ct);
                    var usec = await con.GetWaterLevelRawAsync(ct);
                    sw.Stop();

                    var elapsed = (ushort)sw.ElapsedMilliseconds;
                    //var elapsed = median.Add((ushort)sw.ElapsedMilliseconds);
                    //if (median.IsInitialized)
                    {
                        label_elapsedWL.Text = elapsed + " мсек";
                    }

                    AddUsec(usec);
                }
            }
            catch when (ct.IsCancellationRequested)
            {
                return;
            }
            catch (Exception ex)
            {
                errorProvider1.SetError(buttonStartCalib, ex.Message);
                try
                {
                    await Task.Delay(3000, ct);
                    continue;
                }
                catch (OperationCanceledException)
                {
                    return;
                }
            }
        }
    }

    private void SetWLCalibration(ushort waterLevelEmpty, ushort waterLevelFull)
    {
        var diagram = (SwiftPlotDiagram)chartControl_wl_calibration.Diagram;

        var emptyCm = waterLevelEmpty /*/ 58d*/ * 1.05;  // +5%
        var fullCm = 116;// waterLevelFull /*/ 58d*/ * 0.80;    // -20%

        var wholeRange = diagram.AxisY.WholeRange;
        wholeRange.Auto = false;
        wholeRange.MinValue = fullCm;
        wholeRange.MaxValue = emptyCm;
        wholeRange.SideMarginsValue = 1;

        var visualRange = diagram.AxisY.VisualRange;
        visualRange.Auto = false;
        visualRange.MinValue = fullCm;
        visualRange.MaxValue = emptyCm;
        visualRange.SideMarginsValue = 1;

        chartControl_wl_calibration.Series[0].Points.Clear();
    }

    private IDisposable StartWaterCallibrationTimer(ShowerConnection con)
    {
        var timer = new System.Threading.Timer(state => OnTimer(), this, -1, 1000);
        return timer;

        void OnTimer()
        {
            var overflowCount = con.Request<ushort>(ShowerCodes.GetWaterLevelOverflowCounter);
            var noiseErrors = con.Request<ushort>(ShowerCodes.GetWaterLevelNoiseErrorCounter);

            BeginInvoke(delegate (ushort overflowCount, ushort noiseErrors) 
            {
                //label_wl_calib_errors.Text = $"Ошибки: Overflow: {overflowCount}, Noise: {noiseErrors}";
            });
        }
    }

    private void AddUsec(short usec)
    {
        const int dataIntervalSec = 30;
        const int maxPoints = dataIntervalSec * 1000 / 100;

        //decimal cm = usec / 58m;
        //cm = Math.Round(cm, 3);

        var now = DateTime.Now;
        var point = usec != -1 ? new SeriesPoint(now, usec) : new SeriesPoint(now);

        var series = chartControl_wl_calibration.Series[0];
        series.Points.Add(point);

        if (series.Points.Count > maxPoints)
        {
            series.Points.RemoveAt(0);
        }
    }

    private void Button_stop_Click(object sender, EventArgs e)
    {
        button_stop.Enabled = false;
        buttonStartCalib.Enabled = true;

        _ctsWaterLevelCalibration?.Cancel();
        _ctsWaterLevelCalibration = null;
    }

    private async void Button2_Click(object sender, EventArgs e)
    {
        var cts = new CancellationTokenSource();

        try
        {
            using var con = await ConnectionHelper.CreateConnectionAsync(cts.Token);
            var chart = con.GetTempChart();
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

    private async void Button_wifi_SetCurrent_Click(object sender, EventArgs e)
    {
        var me = (Button)sender;
        var cts = _pingCts = new CancellationTokenSource();
        me.Enabled = false;

        try
        {
            string ssid = textBox_ssid.Text;
            string password = textBox_ap_pass.Text;
            string bsid = textBox_bsid.Text;

            PhysicalAddress? bsidValue = string.IsNullOrEmpty(bsid) ? null : PhysicalAddress.Parse(bsid.Replace(":", "").Replace("-", ""));

            using var con = await ConnectionHelper.CreateConnectionAsync(cts.Token);
            con.SetCurAP(ssid, password, bsidValue);
        }
        catch (Exception ex)
        {
            MessageBox.Show(this, ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        finally
        {
            cts.Cancel();
            me.Enabled = true;
        }
    }

    private async void Button_wifi_SetDefault_Click(object sender, EventArgs e)
    {
        var me = (Button)sender;
        var cts = _pingCts = new CancellationTokenSource();
        me.Enabled = false;

        try
        {
            string ssid = textBox_ssid.Text;
            string password = textBox_ap_pass.Text;
            string bsid = textBox_bsid.Text;

            PhysicalAddress? bsidValue = string.IsNullOrEmpty(bsid) ? null : PhysicalAddress.Parse(bsid.Replace(":", "").Replace("-", ""));

            using var con = await ConnectionHelper.CreateConnectionAsync(cts.Token);
            con.SetDefAP(ssid, password, bsidValue);
        }
        catch (Exception ex)
        {
            MessageBox.Show(this, ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        finally
        {
            cts.Cancel();
            me.Enabled = true;
        }
    }
}
