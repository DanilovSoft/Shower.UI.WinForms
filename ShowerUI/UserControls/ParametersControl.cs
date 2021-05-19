using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using ShowerTcpClient;
using ShowerUI.Dto;

namespace ShowerUI.UserControls
{
    public partial class ParametersControl : UserControl
    {
        private CancellationTokenSource? _cts;

        public ParametersControl()
        {
            InitializeComponent();

            wiFiPower.ResetValue();
        }

        private async void Button_load_Click(object sender, EventArgs e)
        {
            panelProperties.Enabled = false;
            panel_properties_custom.Enabled = false;
            button_save.Enabled = false;
            button_load.Enabled = false;
            button_clear.Enabled = false;
            button_cancel.Enabled = true;

            var cts = _cts = new CancellationTokenSource();

            try
            {
                using (var con = await ConnectionHelper.CreateConnectionAsync(cts.Token))
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
            finally
            {
                cts.Cancel();
            }

            LoadPropsEnable();
        }

        private void LoadPropsEnable()
        {
            panelProperties.Enabled = true;
            panel_properties_custom.Enabled = true;
            button_save.Enabled = true;
            button_load.Enabled = true;
            button_clear.Enabled = true;
            button_cancel.Enabled = false;
        }

        private void Button_clear_Click(object sender, EventArgs e)
        {
            ClearProperties();
        }

        private void SaveProperties(ShowerConnection con)
        {
            if (editText_wl_full.HasChanges)
            {
                con.BuildRequest()
                   .Write(ShowerCodes.SetWaterLevelFullUsec)
                   .Write(editText_wl_full.GetValue<ushort>())
                   .ReadOK();

                editText_wl_full.ResetHasChanges();
            }

            if (editText_wl_empty.HasChanges)
            {
                con.BuildRequest()
                   .Write(ShowerCodes.SetWaterLevelEmptyUsec)
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

            if (wiFiPower.HasChanges)
            {
                con.BuildRequest()
                   .Write(ShowerCodes.SetWiFiPower)
                   .Write(wiFiPower.Value.Value)
                   .ReadOK();

                wiFiPower.ResetHasChanges();
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

        private async void Button_save_Click(object sender, EventArgs e)
        {
            groupBox_properties.Enabled = false;

            try
            {
                using (var con = await ConnectionHelper.CreateConnectionAsync(CancellationToken.None))
                {
                    SaveProperties(con);
                }
                button_load.PerformClick();
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

        private void Button_restart_Click(object sender, EventArgs e)
        {
            RestartDevice();
        }

        private async void RestartDevice()
        {
            try
            {
                Parent.Enabled = false;
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
                Parent.Enabled = true;
            }
        }

        private void Button_cancel_Click(object sender, EventArgs e)
        {
            _cts?.Cancel();
            LoadPropsEnable();
        }

        private async Task LoadPropertiesAsync(ShowerConnection con, CancellationToken cancellationToken)
        {
            var model = await Task.Run(() => LoadProperties(con), cancellationToken);

            ClearProperties();

            editText_wl_full.Value = model.WaterLevelFullUsec;
            editText_wl_empty.Value = model.WaterLevelEmptyUsec;
            editText_min_water_heating_percent.Value = model.MinimumWaterHeatingLevel;
            editText_abs_heating_time_limit.Value = model.AbsoluteHeatingTimeLimit;
            editText_heating_time_limit.Value = model.HeatingTimeLimit;
            editText_light_brightness.Value = model.LightBrightness;
            wiFiPower.Value = model.WiFiPower;
            editText_WL_measure_interval.Value = model.WaterLevelMeasureInterval;
            editText_wl_ring_buffer_size.Value = model.WaterLevelRingBufferSize;
            editText_wl_cut_off_percent.Value = model.WaterValveCutOffPercent;
            editText_internal_temp_buf_size.Value = model.TempSensorInternalTempBufferSize;
            editText_wl_usec_per_deg.Value = model.WaterLevelUsecPerDeg;
            editText_button_time.Value = model.ButtonTimeMsec;
            checkBox_iwd.Checked = model.WatchDogWasReset;
        }

        private void ClearProperties()
        {
            editText_wl_full.Value = null;
            editText_wl_empty.Value = null;
            editText_min_water_heating_percent.Value = null;
            editText_abs_heating_time_limit.Value = null;
            editText_heating_time_limit.Value = null;
            editText_light_brightness.Value = null;
            wiFiPower.Value = null;
            editText_WL_measure_interval.Value = null;
            editText_wl_ring_buffer_size.Value = null;
            editText_wl_cut_off_percent.Value = null;
            editText_internal_temp_buf_size.Value = null;
            editText_wl_usec_per_deg.Value = null;
            editText_button_time.Value = null;
            checkBox_iwd.CheckState = CheckState.Indeterminate;
            //wifiPower.ResetText();
        }

        private static PropertiesModel LoadProperties(ShowerConnection con)
        {
            var model = new PropertiesModel
            {
                WaterLevelFullUsec = con.Request<ushort>(ShowerCodes.GetWaterLevelFullUsec),
                WaterLevelEmptyUsec = con.Request<ushort>(ShowerCodes.GetWaterLevelEmptyUsec),
                MinimumWaterHeatingLevel = con.Request<byte>(ShowerCodes.GetMinimumWaterHeatingLevel),
                AbsoluteHeatingTimeLimit = con.Request<byte>(ShowerCodes.GetAbsoluteHeatingTimeLimit),
                HeatingTimeLimit = con.Request<byte>(ShowerCodes.GetHeatingTimeLimit),
                LightBrightness = con.Request<byte>(ShowerCodes.GetLightBrightness),
                WiFiPower = con.Request<byte>(ShowerCodes.GetWiFiPower),
                WaterLevelMeasureInterval = con.Request<byte>(ShowerCodes.GetWaterLevelMeasureInterval),
                WaterLevelRingBufferSize = con.GetWaterLevelBufferSize(),
                WaterValveCutOffPercent = con.Request<byte>(ShowerCodes.GetWaterValveCutOffPercent),
                TempSensorInternalTempBufferSize = con.Request<byte>(ShowerCodes.GetTempSensorInternalTempBufferSize),
                WaterLevelUsecPerDeg = con.Request<ushort>(ShowerCodes.GetWaterLevelUsecPerDeg) / 100f,
                ButtonTimeMsec = con.Request<byte>(ShowerCodes.GetButtonTimeMsec),
                WatchDogWasReset = con.GetWatchDogWasReset()
            };

            return model;
        }
    }
}
