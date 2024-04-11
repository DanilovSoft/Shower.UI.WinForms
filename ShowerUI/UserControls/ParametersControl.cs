using System.Text.Json;
using ShowerTcpClient;
using ShowerUI.Dto;

namespace ShowerUI.UserControls;

public partial class ParametersControl : UserControl
{
    private CancellationTokenSource? _cts;

    public ParametersControl()
    {
        InitializeComponent();
    }

    private async void Button_load_Click(object sender, EventArgs e)
    {
        DisableControls();
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

    private void DisableControls()
    {
        panelProperties.Enabled = false;
        button_save.Enabled = false;
        button_load.Enabled = false;
        button_clear.Enabled = false;
        button_cancel.Enabled = true;
        button_Export.Enabled = false;
        button_Import.Enabled = false;
    }

    private void LoadPropsEnable()
    {
        panelProperties.Enabled = true;
        button_save.Enabled = true;
        button_load.Enabled = true;
        button_clear.Enabled = true;
        button_cancel.Enabled = false;
        button_Export.Enabled = true;
        button_Import.Enabled = true;
    }

    private void Button_clear_Click(object sender, EventArgs e)
    {
        ClearProperties();
    }

    private void SaveProperties(ShowerConnection con)
    {
        if (editText_wl_full.HasChanges)
        {
            con.SetWaterLevelFullUsec(editText_wl_full.GetValue<ushort>());
            editText_wl_full.ResetHasChanges();
        }

        if (editText_wl_empty.HasChanges)
        {
            con.SetWaterLevelEmptyUsec(editText_wl_empty.GetValue<ushort>());
            editText_wl_empty.ResetHasChanges();
        }

        if (editText_min_water_heating_percent.HasChanges)
        {
            con.SetMinimumWaterHeatingLevel(editText_min_water_heating_percent.GetValue<byte>());
            editText_min_water_heating_percent.ResetHasChanges();
        }

        if (editText_abs_heating_time_limit.HasChanges)
        {
            con.SetAbsoluteHeatingTimeLimit(editText_abs_heating_time_limit.GetValue<byte>());
            editText_abs_heating_time_limit.ResetHasChanges();
        }

        if (editText_heating_time_limit.HasChanges)
        {
            con.SetHeatingTimeLimit(editText_heating_time_limit.GetValue<byte>());
            editText_heating_time_limit.ResetHasChanges();
        }

        if (editText_light_brightness.HasChanges)
        {
            con.SetLightBrightness(editText_light_brightness.GetValue<byte>());
            editText_light_brightness.ResetHasChanges();
        }

        if (wiFiPower.HasChanges)
        {
            con.SetWiFiPower(wiFiPower.Power);
            wiFiPower.ResetHasChanges();
        }

        if (editText_WL_measure_interval.HasChanges)
        {
            con.SetWaterLevelMeasureInterval(editText_WL_measure_interval.GetValue<byte>());
            editText_WL_measure_interval.ResetHasChanges();
        }

        if (editText_wl_median_buffer_size.HasChanges)
        {
            con.SetWaterLevelMedianBufferSize(editText_wl_median_buffer_size.GetValue<byte>());
            editText_wl_median_buffer_size.ResetHasChanges();
        }
        
        if (editText_wl_avg_buffer_size.HasChanges)
        {
            con.SetWaterLevelAverageBufferSize(editText_wl_avg_buffer_size.GetValue<byte>());
            editText_wl_avg_buffer_size.ResetHasChanges();
        }

        if (editText_wl_cut_off_percent.HasChanges)
        {
            con.SetWaterValveCutOffPercent(editText_wl_cut_off_percent.GetValue<byte>());
            editText_wl_cut_off_percent.ResetHasChanges();
        }

        if (editText_internal_temp_avg_size.HasChanges)
        {
            con.SetTempSensorInternalTempAverageSize(editText_internal_temp_avg_size.GetValue<byte>());
            editText_internal_temp_avg_size.ResetHasChanges();
        }

        if (editText_WaterVolumeLitre.HasChanges)
        {
            con.SetWaterTankVolumeLitre(editText_WaterVolumeLitre.GetValue<float>());
            editText_WaterVolumeLitre.ResetHasChanges();
        }

        if (editText_HeaterPowerKWatt.HasChanges)
        {
            con.SetWaterHeaterPowerKWatt(editText_HeaterPowerKWatt.GetValue<float>());
            editText_HeaterPowerKWatt.ResetHasChanges();
        }

        if (editText_button_time.HasChanges)
        {
            con.SetButtonTimeMsec(editText_button_time.GetValue<byte>());
            editText_button_time.ResetHasChanges();
        }

        if (editTextButtonLongTime.HasChanges)
        {
            con.SetButtonLongPressTimeMsec(editTextButtonLongTime.GetValue<ushort>());
            editTextButtonLongTime.ResetHasChanges();
        }

        if (editTextWaterLevelErrorThreshold.HasChanges)
        {
            con.SetWaterLevelErrorThreshold(editTextWaterLevelErrorThreshold.GetValue<byte>());
            editTextWaterLevelErrorThreshold.ResetHasChanges();
        }
        
        con.Save();
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
                connection.Reset();
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
        SetControlValues(model);
    }

    private void SetControlValues(PropertiesModel model)
    {
        editText_wl_full.Value = model.WaterLevelFullUsec;
        editText_wl_empty.Value = model.WaterLevelEmptyUsec;
        editText_min_water_heating_percent.Value = model.MinimumWaterHeatingLevel;
        editText_abs_heating_time_limit.Value = model.AbsoluteHeatingTimeLimit;
        editText_heating_time_limit.Value = model.HeatingTimeLimit;
        editText_light_brightness.Value = model.LightBrightness;
        wiFiPower.Value = model.WiFiPower;
        editText_WL_measure_interval.Value = model.WaterLevelMeasureInterval;
        editText_wl_median_buffer_size.Value = model.WaterLevelMedianBufferSize;
        editText_wl_avg_buffer_size.Value = model.WaterLevelAverageBufferSize;
        editText_wl_cut_off_percent.Value = model.WaterValveCutOffPercent;
        editText_internal_temp_avg_size.Value = model.TempSensorInternalTempAverageSize;
        editText_button_time.Value = model.ButtonPressTimeMsec;
        editTextButtonLongTime.Value = model.ButtonLongTimeMsec;
        checkBox_iwd.Checked = model.WatchDogWasReset ?? false;
        editText_WaterVolumeLitre.Value = model.WaterTankVolumeLitre;
        editText_HeaterPowerKWatt.Value = model.WaterHeaterPowerKWatt;
        editTextWaterLevelErrorThreshold.Value = model.WaterLevelErrorThreshhold;
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
        editText_wl_median_buffer_size.Value = null;
        editText_wl_cut_off_percent.Value = null;
        editText_internal_temp_avg_size.Value = null;
        editText_WaterVolumeLitre.Value = null;
        editText_HeaterPowerKWatt.Value = null;
        editText_button_time.Value = null;
        editTextWaterLevelErrorThreshold.Value = null;
        editTextButtonLongTime.Value = null;
        checkBox_iwd.CheckState = CheckState.Indeterminate;
    }

    private static PropertiesModel LoadProperties(ShowerConnection con)
    {
        var model = new PropertiesModel
        {
            WaterLevelFullUsec = con.GetWaterLevelFullUsec(),
            WaterLevelEmptyUsec = con.GetWaterLevelEmptyUsec(),
            MinimumWaterHeatingLevel = con.GetMinimumWaterHeatingLevel(),
            AbsoluteHeatingTimeLimit = con.GetAbsoluteHeatingTimeLimit(),
            HeatingTimeLimit = con.GetHeatingTimeLimit(),
            LightBrightness = con.GetLightBrightness(),
            WiFiPower = con.GetWiFiPower(),
            WaterLevelMeasureInterval = con.GetWaterLevelMeasureInterval(),
            WaterLevelMedianBufferSize = con.GetWaterLevelMedianSize(),
            WaterLevelAverageBufferSize = con.GetWaterLevelAverageFilterSize(),
            WaterValveCutOffPercent = con.GetWaterValveCutOffPercent(),
            TempSensorInternalTempAverageSize = con.GetTempSensorInternalTempAverageSize(),
            ButtonPressTimeMsec = con.GetButtonPressTimeMsec(),
            ButtonLongTimeMsec = con.GetButtonLongPressTimeMsec(),
            WatchDogWasReset = con.GetWatchDogWasReset(),
            WaterTankVolumeLitre = con.GetWaterTankVolumeLitre(),
            WaterHeaterPowerKWatt = con.GetWaterHeaterPowerKWatt(),
            WaterLevelErrorThreshhold = con.GetWaterLevelErrorThreshhold(),
        };

        return model;
    }

    private void Button_Export_Click(object sender, EventArgs e)
    {
        var model = new PropertiesModel
        {
            WaterLevelFullUsec = (ushort?)editText_wl_full.Value,
            WaterLevelEmptyUsec = (ushort?)editText_wl_empty.Value,
            MinimumWaterHeatingLevel = (byte?)editText_min_water_heating_percent.Value,
            AbsoluteHeatingTimeLimit = (byte?)editText_abs_heating_time_limit.Value,
            HeatingTimeLimit = (byte?)editText_heating_time_limit.Value,
            LightBrightness = (byte?)editText_light_brightness.Value,
            WiFiPower = (byte?)wiFiPower.Value,
            WaterLevelMeasureInterval = (byte?)editText_WL_measure_interval.Value,
            WaterLevelMedianBufferSize = (byte?)editText_wl_median_buffer_size.Value,
            WaterLevelAverageBufferSize = (byte?)editText_wl_avg_buffer_size.Value,
            WaterValveCutOffPercent = (byte?)editText_wl_cut_off_percent.Value,
            TempSensorInternalTempAverageSize = (byte?)editText_internal_temp_avg_size.Value,
            ButtonPressTimeMsec = (byte?)editText_button_time.Value,
            WaterTankVolumeLitre = (float?)editText_WaterVolumeLitre.Value,
            WaterHeaterPowerKWatt = (float?)editText_HeaterPowerKWatt.Value,
        };

        try
        {
            using (var dialog = new SaveFileDialog())
            {
                dialog.AutoUpgradeEnabled = true;
                dialog.DefaultExt = "shower.txt";
                dialog.Filter = "Json File|*.shower.txt";
                dialog.FileName = "Parameters";
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    string json = JsonSerializer.Serialize(model, new JsonSerializerOptions { WriteIndented = true });
                    File.WriteAllText(dialog.FileName, json);
                }
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show(this, ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    private void Button_Import_Click(object sender, EventArgs e)
    {
        using (var dialog = new OpenFileDialog())
        {
            dialog.Filter = "Json File|*.shower.txt";
            dialog.FileName = "Parameters";
            dialog.Multiselect = false;
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                string json = File.ReadAllText(dialog.FileName);
                var model = JsonSerializer.Deserialize<PropertiesModel>(json);
                if (model != null)
                {
                    ClearProperties();
                    SetControlValues(model);
                }
            }
        }
    }
}
