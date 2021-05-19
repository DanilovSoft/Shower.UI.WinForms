
namespace ShowerUI.UserControls
{
    partial class ParametersControl
    {
        /// <summary> 
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором компонентов

        /// <summary> 
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.groupBox_properties = new System.Windows.Forms.GroupBox();
            this.panelProperties = new System.Windows.Forms.Panel();
            this.wiFiPower = new ShowerUI.UserControls.WiFiPowerControl();
            this.editText_wl_full = new ShowerUI.EditText();
            this.editText_button_time = new ShowerUI.EditText();
            this.editText_light_brightness = new ShowerUI.EditText();
            this.editText_wl_empty = new ShowerUI.EditText();
            this.editText_heating_time_limit = new ShowerUI.EditText();
            this.editText_abs_heating_time_limit = new ShowerUI.EditText();
            this.editText_min_water_heating_percent = new ShowerUI.EditText();
            this.panel_properties_custom = new System.Windows.Forms.FlowLayoutPanel();
            this.editText_WL_measure_interval = new ShowerUI.EditText();
            this.editText_wl_ring_buffer_size = new ShowerUI.EditText();
            this.editText_wl_cut_off_percent = new ShowerUI.EditText();
            this.editText_internal_temp_buf_size = new ShowerUI.EditText();
            this.editText_wl_usec_per_deg = new ShowerUI.EditText();
            this.button_clear = new System.Windows.Forms.Button();
            this.button_restart = new System.Windows.Forms.Button();
            this.checkBox_iwd = new System.Windows.Forms.CheckBox();
            this.button_cancel = new System.Windows.Forms.Button();
            this.button_save = new System.Windows.Forms.Button();
            this.button_load = new System.Windows.Forms.Button();
            this.groupBox_properties.SuspendLayout();
            this.panelProperties.SuspendLayout();
            this.panel_properties_custom.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox_properties
            // 
            this.groupBox_properties.Controls.Add(this.panelProperties);
            this.groupBox_properties.Controls.Add(this.panel_properties_custom);
            this.groupBox_properties.Controls.Add(this.button_clear);
            this.groupBox_properties.Controls.Add(this.button_restart);
            this.groupBox_properties.Controls.Add(this.checkBox_iwd);
            this.groupBox_properties.Controls.Add(this.button_cancel);
            this.groupBox_properties.Controls.Add(this.button_save);
            this.groupBox_properties.Controls.Add(this.button_load);
            this.groupBox_properties.Location = new System.Drawing.Point(15, 12);
            this.groupBox_properties.Name = "groupBox_properties";
            this.groupBox_properties.Size = new System.Drawing.Size(778, 491);
            this.groupBox_properties.TabIndex = 14;
            this.groupBox_properties.TabStop = false;
            // 
            // panelProperties
            // 
            this.panelProperties.Controls.Add(this.wiFiPower);
            this.panelProperties.Controls.Add(this.editText_wl_full);
            this.panelProperties.Controls.Add(this.editText_button_time);
            this.panelProperties.Controls.Add(this.editText_light_brightness);
            this.panelProperties.Controls.Add(this.editText_wl_empty);
            this.panelProperties.Controls.Add(this.editText_heating_time_limit);
            this.panelProperties.Controls.Add(this.editText_abs_heating_time_limit);
            this.panelProperties.Controls.Add(this.editText_min_water_heating_percent);
            this.panelProperties.Location = new System.Drawing.Point(6, 20);
            this.panelProperties.Name = "panelProperties";
            this.panelProperties.Size = new System.Drawing.Size(200, 405);
            this.panelProperties.TabIndex = 53;
            // 
            // wiFiPower
            // 
            this.wiFiPower.AutoSize = true;
            this.wiFiPower.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.wiFiPower.Location = new System.Drawing.Point(17, 297);
            this.wiFiPower.Name = "wiFiPower";
            this.wiFiPower.Size = new System.Drawing.Size(165, 42);
            this.wiFiPower.TabIndex = 37;
            this.wiFiPower.TextBoxHint = "Мощность Wi-Fi в диаппазоне от 10 до 20.5 dBm с шагом в 0.25";
            this.wiFiPower.Value = ((byte)(60));
            // 
            // editText_wl_full
            // 
            this.editText_wl_full.AutoSize = true;
            this.editText_wl_full.Caption = "Уровень полного бака (μs)";
            this.editText_wl_full.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.editText_wl_full.Location = new System.Drawing.Point(17, 5);
            this.editText_wl_full.Margin = new System.Windows.Forms.Padding(5);
            this.editText_wl_full.Name = "editText_wl_full";
            this.editText_wl_full.Size = new System.Drawing.Size(166, 39);
            this.editText_wl_full.TabIndex = 29;
            this.editText_wl_full.TextBoxHint = "Число микросекунд соответствующие расстоянию когда бак полностью заполнен водой";
            this.editText_wl_full.Value = null;
            // 
            // editText_button_time
            // 
            this.editText_button_time.AutoSize = true;
            this.editText_button_time.Caption = "Скорость отпроса кнопок (msec)";
            this.editText_button_time.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.editText_button_time.Location = new System.Drawing.Point(17, 348);
            this.editText_button_time.Margin = new System.Windows.Forms.Padding(5);
            this.editText_button_time.Name = "editText_button_time";
            this.editText_button_time.Size = new System.Drawing.Size(173, 39);
            this.editText_button_time.TabIndex = 36;
            this.editText_button_time.TextBoxHint = "";
            this.editText_button_time.Value = null;
            // 
            // editText_light_brightness
            // 
            this.editText_light_brightness.AutoSize = true;
            this.editText_light_brightness.Caption = "Light Brightness";
            this.editText_light_brightness.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.editText_light_brightness.Location = new System.Drawing.Point(17, 250);
            this.editText_light_brightness.Margin = new System.Windows.Forms.Padding(5);
            this.editText_light_brightness.Name = "editText_light_brightness";
            this.editText_light_brightness.Size = new System.Drawing.Size(166, 39);
            this.editText_light_brightness.TabIndex = 34;
            this.editText_light_brightness.TextBoxHint = "";
            this.editText_light_brightness.Value = null;
            // 
            // editText_wl_empty
            // 
            this.editText_wl_empty.AutoSize = true;
            this.editText_wl_empty.Caption = "Уровень пустого бака (μs)";
            this.editText_wl_empty.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.editText_wl_empty.Location = new System.Drawing.Point(17, 54);
            this.editText_wl_empty.Margin = new System.Windows.Forms.Padding(5);
            this.editText_wl_empty.Name = "editText_wl_empty";
            this.editText_wl_empty.Size = new System.Drawing.Size(166, 39);
            this.editText_wl_empty.TabIndex = 30;
            this.editText_wl_empty.TextBoxHint = "";
            this.editText_wl_empty.Value = null;
            // 
            // editText_heating_time_limit
            // 
            this.editText_heating_time_limit.AutoSize = true;
            this.editText_heating_time_limit.Caption = "Heating Time Limit";
            this.editText_heating_time_limit.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.editText_heating_time_limit.Location = new System.Drawing.Point(17, 201);
            this.editText_heating_time_limit.Margin = new System.Windows.Forms.Padding(5);
            this.editText_heating_time_limit.Name = "editText_heating_time_limit";
            this.editText_heating_time_limit.Size = new System.Drawing.Size(166, 39);
            this.editText_heating_time_limit.TabIndex = 33;
            this.editText_heating_time_limit.TextBoxHint = "";
            this.editText_heating_time_limit.Value = null;
            // 
            // editText_abs_heating_time_limit
            // 
            this.editText_abs_heating_time_limit.AutoSize = true;
            this.editText_abs_heating_time_limit.Caption = "Absolute Heating Time Limit";
            this.editText_abs_heating_time_limit.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.editText_abs_heating_time_limit.Location = new System.Drawing.Point(17, 152);
            this.editText_abs_heating_time_limit.Margin = new System.Windows.Forms.Padding(5);
            this.editText_abs_heating_time_limit.Name = "editText_abs_heating_time_limit";
            this.editText_abs_heating_time_limit.Size = new System.Drawing.Size(166, 39);
            this.editText_abs_heating_time_limit.TabIndex = 32;
            this.editText_abs_heating_time_limit.TextBoxHint = "";
            this.editText_abs_heating_time_limit.Value = null;
            // 
            // editText_min_water_heating_percent
            // 
            this.editText_min_water_heating_percent.AutoSize = true;
            this.editText_min_water_heating_percent.Caption = "Minimum Water Heating Percent";
            this.editText_min_water_heating_percent.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.editText_min_water_heating_percent.Location = new System.Drawing.Point(17, 103);
            this.editText_min_water_heating_percent.Margin = new System.Windows.Forms.Padding(5);
            this.editText_min_water_heating_percent.Name = "editText_min_water_heating_percent";
            this.editText_min_water_heating_percent.Size = new System.Drawing.Size(166, 39);
            this.editText_min_water_heating_percent.TabIndex = 31;
            this.editText_min_water_heating_percent.TextBoxHint = "";
            this.editText_min_water_heating_percent.Value = null;
            // 
            // panel_properties_custom
            // 
            this.panel_properties_custom.Controls.Add(this.editText_WL_measure_interval);
            this.panel_properties_custom.Controls.Add(this.editText_wl_ring_buffer_size);
            this.panel_properties_custom.Controls.Add(this.editText_wl_cut_off_percent);
            this.panel_properties_custom.Controls.Add(this.editText_internal_temp_buf_size);
            this.panel_properties_custom.Controls.Add(this.editText_wl_usec_per_deg);
            this.panel_properties_custom.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.panel_properties_custom.Location = new System.Drawing.Point(235, 20);
            this.panel_properties_custom.Name = "panel_properties_custom";
            this.panel_properties_custom.Size = new System.Drawing.Size(357, 287);
            this.panel_properties_custom.TabIndex = 15;
            // 
            // editText_WL_measure_interval
            // 
            this.editText_WL_measure_interval.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.editText_WL_measure_interval.Caption = "Интервал замера уровня воды (мсек)";
            this.editText_WL_measure_interval.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.editText_WL_measure_interval.Location = new System.Drawing.Point(5, 5);
            this.editText_WL_measure_interval.Margin = new System.Windows.Forms.Padding(5);
            this.editText_WL_measure_interval.Name = "editText_WL_measure_interval";
            this.editText_WL_measure_interval.Size = new System.Drawing.Size(245, 39);
            this.editText_WL_measure_interval.TabIndex = 36;
            this.editText_WL_measure_interval.TextBoxHint = "Длительность паузы перед повторным измерением расстояния до воды в баке. Рекоменд" +
    "уется не меньше 60 мсек";
            this.editText_WL_measure_interval.Value = null;
            // 
            // editText_wl_ring_buffer_size
            // 
            this.editText_wl_ring_buffer_size.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.editText_wl_ring_buffer_size.AutoSize = true;
            this.editText_wl_ring_buffer_size.Caption = "Размер медианного буффера для уровня воды";
            this.editText_wl_ring_buffer_size.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.editText_wl_ring_buffer_size.Location = new System.Drawing.Point(5, 54);
            this.editText_wl_ring_buffer_size.Margin = new System.Windows.Forms.Padding(5);
            this.editText_wl_ring_buffer_size.Name = "editText_wl_ring_buffer_size";
            this.editText_wl_ring_buffer_size.Size = new System.Drawing.Size(245, 39);
            this.editText_wl_ring_buffer_size.TabIndex = 37;
            this.editText_wl_ring_buffer_size.TextBoxHint = "";
            this.editText_wl_ring_buffer_size.Value = null;
            // 
            // editText_wl_cut_off_percent
            // 
            this.editText_wl_cut_off_percent.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.editText_wl_cut_off_percent.AutoSize = true;
            this.editText_wl_cut_off_percent.Caption = "Порог отключения набора воды (%)";
            this.editText_wl_cut_off_percent.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.editText_wl_cut_off_percent.Location = new System.Drawing.Point(5, 103);
            this.editText_wl_cut_off_percent.Margin = new System.Windows.Forms.Padding(5);
            this.editText_wl_cut_off_percent.Name = "editText_wl_cut_off_percent";
            this.editText_wl_cut_off_percent.Size = new System.Drawing.Size(245, 39);
            this.editText_wl_cut_off_percent.TabIndex = 39;
            this.editText_wl_cut_off_percent.TextBoxHint = "";
            this.editText_wl_cut_off_percent.Value = null;
            // 
            // editText_internal_temp_buf_size
            // 
            this.editText_internal_temp_buf_size.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.editText_internal_temp_buf_size.AutoSize = true;
            this.editText_internal_temp_buf_size.Caption = "TempSensor InternalTemp Buffer Size";
            this.editText_internal_temp_buf_size.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.editText_internal_temp_buf_size.Location = new System.Drawing.Point(5, 152);
            this.editText_internal_temp_buf_size.Margin = new System.Windows.Forms.Padding(5);
            this.editText_internal_temp_buf_size.Name = "editText_internal_temp_buf_size";
            this.editText_internal_temp_buf_size.Size = new System.Drawing.Size(245, 39);
            this.editText_internal_temp_buf_size.TabIndex = 41;
            this.editText_internal_temp_buf_size.TextBoxHint = "";
            this.editText_internal_temp_buf_size.Value = null;
            // 
            // editText_wl_usec_per_deg
            // 
            this.editText_wl_usec_per_deg.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.editText_wl_usec_per_deg.AutoSize = true;
            this.editText_wl_usec_per_deg.Caption = "WaterLevel Usec Per Deg";
            this.editText_wl_usec_per_deg.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.editText_wl_usec_per_deg.Location = new System.Drawing.Point(5, 201);
            this.editText_wl_usec_per_deg.Margin = new System.Windows.Forms.Padding(5);
            this.editText_wl_usec_per_deg.Name = "editText_wl_usec_per_deg";
            this.editText_wl_usec_per_deg.Size = new System.Drawing.Size(245, 39);
            this.editText_wl_usec_per_deg.TabIndex = 42;
            this.editText_wl_usec_per_deg.TextBoxHint = "";
            this.editText_wl_usec_per_deg.Value = null;
            // 
            // button_clear
            // 
            this.button_clear.Location = new System.Drawing.Point(87, 454);
            this.button_clear.Name = "button_clear";
            this.button_clear.Size = new System.Drawing.Size(75, 23);
            this.button_clear.TabIndex = 50;
            this.button_clear.Text = "Очистить";
            this.button_clear.UseVisualStyleBackColor = true;
            this.button_clear.Click += new System.EventHandler(this.Button_clear_Click);
            // 
            // button_restart
            // 
            this.button_restart.Location = new System.Drawing.Point(249, 454);
            this.button_restart.Name = "button_restart";
            this.button_restart.Size = new System.Drawing.Size(88, 23);
            this.button_restart.TabIndex = 49;
            this.button_restart.Text = "Перезапуск";
            this.button_restart.UseVisualStyleBackColor = true;
            this.button_restart.Click += new System.EventHandler(this.Button_restart_Click);
            // 
            // checkBox_iwd
            // 
            this.checkBox_iwd.AutoCheck = false;
            this.checkBox_iwd.AutoSize = true;
            this.checkBox_iwd.Checked = true;
            this.checkBox_iwd.CheckState = System.Windows.Forms.CheckState.Indeterminate;
            this.checkBox_iwd.Location = new System.Drawing.Point(348, 340);
            this.checkBox_iwd.Name = "checkBox_iwd";
            this.checkBox_iwd.Size = new System.Drawing.Size(76, 17);
            this.checkBox_iwd.TabIndex = 48;
            this.checkBox_iwd.Text = "Watchdog";
            this.checkBox_iwd.UseVisualStyleBackColor = true;
            // 
            // button_cancel
            // 
            this.button_cancel.Enabled = false;
            this.button_cancel.Location = new System.Drawing.Point(343, 454);
            this.button_cancel.Name = "button_cancel";
            this.button_cancel.Size = new System.Drawing.Size(75, 23);
            this.button_cancel.TabIndex = 47;
            this.button_cancel.Text = "Отмена";
            this.button_cancel.UseVisualStyleBackColor = true;
            this.button_cancel.Click += new System.EventHandler(this.Button_cancel_Click);
            // 
            // button_save
            // 
            this.button_save.Location = new System.Drawing.Point(168, 454);
            this.button_save.Name = "button_save";
            this.button_save.Size = new System.Drawing.Size(75, 23);
            this.button_save.TabIndex = 46;
            this.button_save.Text = "Записать";
            this.button_save.UseVisualStyleBackColor = true;
            this.button_save.Click += new System.EventHandler(this.Button_save_Click);
            // 
            // button_load
            // 
            this.button_load.Location = new System.Drawing.Point(6, 454);
            this.button_load.Name = "button_load";
            this.button_load.Size = new System.Drawing.Size(75, 23);
            this.button_load.TabIndex = 45;
            this.button_load.Text = "Загрузить";
            this.button_load.UseVisualStyleBackColor = true;
            this.button_load.Click += new System.EventHandler(this.Button_load_Click);
            // 
            // ParametersControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupBox_properties);
            this.Name = "ParametersControl";
            this.Size = new System.Drawing.Size(864, 553);
            this.groupBox_properties.ResumeLayout(false);
            this.groupBox_properties.PerformLayout();
            this.panelProperties.ResumeLayout(false);
            this.panelProperties.PerformLayout();
            this.panel_properties_custom.ResumeLayout(false);
            this.panel_properties_custom.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox_properties;
        private System.Windows.Forms.Panel panelProperties;
        private WiFiPowerControl wiFiPower;
        private EditText editText_wl_full;
        private EditText editText_button_time;
        private EditText editText_light_brightness;
        private EditText editText_wl_empty;
        private EditText editText_heating_time_limit;
        private EditText editText_abs_heating_time_limit;
        private EditText editText_min_water_heating_percent;
        private System.Windows.Forms.FlowLayoutPanel panel_properties_custom;
        private EditText editText_WL_measure_interval;
        private EditText editText_wl_ring_buffer_size;
        private EditText editText_wl_cut_off_percent;
        private EditText editText_internal_temp_buf_size;
        private EditText editText_wl_usec_per_deg;
        private System.Windows.Forms.Button button_clear;
        private System.Windows.Forms.Button button_restart;
        private System.Windows.Forms.CheckBox checkBox_iwd;
        private System.Windows.Forms.Button button_cancel;
        private System.Windows.Forms.Button button_save;
        private System.Windows.Forms.Button button_load;
    }
}
