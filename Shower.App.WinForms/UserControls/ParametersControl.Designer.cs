
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
            this.components = new System.ComponentModel.Container();
            this.groupBox_properties = new System.Windows.Forms.GroupBox();
            this.button_Import = new System.Windows.Forms.Button();
            this.button_Export = new System.Windows.Forms.Button();
            this.panelProperties = new System.Windows.Forms.Panel();
            this.button_clear = new System.Windows.Forms.Button();
            this.button_restart = new System.Windows.Forms.Button();
            this.checkBox_iwd = new System.Windows.Forms.CheckBox();
            this.button_cancel = new System.Windows.Forms.Button();
            this.button_save = new System.Windows.Forms.Button();
            this.button_load = new System.Windows.Forms.Button();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.editTextWaterLevelErrorThreshold = new ShowerUI.EditText();
            this.editTextButtonLongTime = new ShowerUI.EditText();
            this.editText_WL_measure_interval = new ShowerUI.EditText();
            this.editText_wl_median_buffer_size = new ShowerUI.EditText();
            this.wiFiPower = new ShowerUI.UserControls.WiFiPowerControl();
            this.editText_wl_avg_buffer_size = new ShowerUI.EditText();
            this.editText_wl_full = new ShowerUI.EditText();
            this.editText_wl_cut_off_percent = new ShowerUI.EditText();
            this.editText_button_time = new ShowerUI.EditText();
            this.editText_internal_temp_avg_size = new ShowerUI.EditText();
            this.editText_light_brightness = new ShowerUI.EditText();
            this.editText_WaterVolumeLitre = new ShowerUI.EditText();
            this.editText_wl_empty = new ShowerUI.EditText();
            this.editText_HeaterPowerKWatt = new ShowerUI.EditText();
            this.editText_heating_time_limit = new ShowerUI.EditText();
            this.editText_abs_heating_time_limit = new ShowerUI.EditText();
            this.editText_min_water_heating_percent = new ShowerUI.EditText();
            this.groupBox_properties.SuspendLayout();
            this.panelProperties.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox_properties
            // 
            this.groupBox_properties.Controls.Add(this.button_Import);
            this.groupBox_properties.Controls.Add(this.button_Export);
            this.groupBox_properties.Controls.Add(this.panelProperties);
            this.groupBox_properties.Controls.Add(this.button_clear);
            this.groupBox_properties.Controls.Add(this.button_restart);
            this.groupBox_properties.Controls.Add(this.button_cancel);
            this.groupBox_properties.Controls.Add(this.button_save);
            this.groupBox_properties.Controls.Add(this.button_load);
            this.groupBox_properties.Location = new System.Drawing.Point(15, 12);
            this.groupBox_properties.Name = "groupBox_properties";
            this.groupBox_properties.Size = new System.Drawing.Size(927, 588);
            this.groupBox_properties.TabIndex = 14;
            this.groupBox_properties.TabStop = false;
            // 
            // button_Import
            // 
            this.button_Import.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.button_Import.Location = new System.Drawing.Point(505, 551);
            this.button_Import.Name = "button_Import";
            this.button_Import.Size = new System.Drawing.Size(75, 23);
            this.button_Import.TabIndex = 51;
            this.button_Import.Text = "Импорт";
            this.button_Import.UseVisualStyleBackColor = true;
            this.button_Import.Click += new System.EventHandler(this.Button_Import_Click);
            // 
            // button_Export
            // 
            this.button_Export.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.button_Export.Location = new System.Drawing.Point(424, 551);
            this.button_Export.Name = "button_Export";
            this.button_Export.Size = new System.Drawing.Size(75, 23);
            this.button_Export.TabIndex = 50;
            this.button_Export.Text = "Экспорт";
            this.button_Export.UseVisualStyleBackColor = true;
            this.button_Export.Click += new System.EventHandler(this.Button_Export_Click);
            // 
            // panelProperties
            // 
            this.panelProperties.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelProperties.Controls.Add(this.editTextWaterLevelErrorThreshold);
            this.panelProperties.Controls.Add(this.editTextButtonLongTime);
            this.panelProperties.Controls.Add(this.editText_WL_measure_interval);
            this.panelProperties.Controls.Add(this.editText_wl_median_buffer_size);
            this.panelProperties.Controls.Add(this.wiFiPower);
            this.panelProperties.Controls.Add(this.checkBox_iwd);
            this.panelProperties.Controls.Add(this.editText_wl_avg_buffer_size);
            this.panelProperties.Controls.Add(this.editText_wl_full);
            this.panelProperties.Controls.Add(this.editText_wl_cut_off_percent);
            this.panelProperties.Controls.Add(this.editText_button_time);
            this.panelProperties.Controls.Add(this.editText_internal_temp_avg_size);
            this.panelProperties.Controls.Add(this.editText_light_brightness);
            this.panelProperties.Controls.Add(this.editText_WaterVolumeLitre);
            this.panelProperties.Controls.Add(this.editText_wl_empty);
            this.panelProperties.Controls.Add(this.editText_HeaterPowerKWatt);
            this.panelProperties.Controls.Add(this.editText_heating_time_limit);
            this.panelProperties.Controls.Add(this.editText_abs_heating_time_limit);
            this.panelProperties.Controls.Add(this.editText_min_water_heating_percent);
            this.panelProperties.Location = new System.Drawing.Point(6, 20);
            this.panelProperties.Name = "panelProperties";
            this.panelProperties.Size = new System.Drawing.Size(493, 462);
            this.panelProperties.TabIndex = 53;
            // 
            // button_clear
            // 
            this.button_clear.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.button_clear.Location = new System.Drawing.Point(87, 551);
            this.button_clear.Name = "button_clear";
            this.button_clear.Size = new System.Drawing.Size(75, 23);
            this.button_clear.TabIndex = 46;
            this.button_clear.Text = "Очистить";
            this.button_clear.UseVisualStyleBackColor = true;
            this.button_clear.Click += new System.EventHandler(this.Button_clear_Click);
            // 
            // button_restart
            // 
            this.button_restart.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.button_restart.Location = new System.Drawing.Point(249, 551);
            this.button_restart.Name = "button_restart";
            this.button_restart.Size = new System.Drawing.Size(88, 23);
            this.button_restart.TabIndex = 48;
            this.button_restart.Text = "Перезапуск";
            this.toolTip1.SetToolTip(this.button_restart, "Отправляет запрос микроконтроллеру на перезагрузку микроконтроллера");
            this.button_restart.UseVisualStyleBackColor = true;
            this.button_restart.Click += new System.EventHandler(this.Button_restart_Click);
            // 
            // checkBox_iwd
            // 
            this.checkBox_iwd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.checkBox_iwd.AutoCheck = false;
            this.checkBox_iwd.AutoSize = true;
            this.checkBox_iwd.Checked = true;
            this.checkBox_iwd.CheckState = System.Windows.Forms.CheckState.Indeterminate;
            this.checkBox_iwd.Location = new System.Drawing.Point(224, 420);
            this.checkBox_iwd.Name = "checkBox_iwd";
            this.checkBox_iwd.Size = new System.Drawing.Size(76, 17);
            this.checkBox_iwd.TabIndex = 0;
            this.checkBox_iwd.TabStop = false;
            this.checkBox_iwd.Text = "Watchdog";
            this.toolTip1.SetToolTip(this.checkBox_iwd, "Показывает сработал ли сторожевой таймер в микроконтроллере с прошлого запуска");
            this.checkBox_iwd.UseVisualStyleBackColor = true;
            // 
            // button_cancel
            // 
            this.button_cancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.button_cancel.Enabled = false;
            this.button_cancel.Location = new System.Drawing.Point(343, 551);
            this.button_cancel.Name = "button_cancel";
            this.button_cancel.Size = new System.Drawing.Size(75, 23);
            this.button_cancel.TabIndex = 49;
            this.button_cancel.Text = "Отмена";
            this.button_cancel.UseVisualStyleBackColor = true;
            this.button_cancel.Click += new System.EventHandler(this.Button_cancel_Click);
            // 
            // button_save
            // 
            this.button_save.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.button_save.Location = new System.Drawing.Point(168, 551);
            this.button_save.Name = "button_save";
            this.button_save.Size = new System.Drawing.Size(75, 23);
            this.button_save.TabIndex = 47;
            this.button_save.Text = "Записать";
            this.button_save.UseVisualStyleBackColor = true;
            this.button_save.Click += new System.EventHandler(this.Button_save_Click);
            // 
            // button_load
            // 
            this.button_load.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.button_load.Location = new System.Drawing.Point(6, 551);
            this.button_load.Name = "button_load";
            this.button_load.Size = new System.Drawing.Size(75, 23);
            this.button_load.TabIndex = 45;
            this.button_load.Text = "Запросить";
            this.button_load.UseVisualStyleBackColor = true;
            this.button_load.Click += new System.EventHandler(this.Button_load_Click);
            // 
            // toolTip1
            // 
            this.toolTip1.AutoPopDelay = 15000;
            this.toolTip1.InitialDelay = 500;
            this.toolTip1.ReshowDelay = 100;
            // 
            // editTextWaterLevelErrorThreshold
            // 
            this.editTextWaterLevelErrorThreshold.AutoSize = true;
            this.editTextWaterLevelErrorThreshold.Caption = "Порог ошибок уровня воды";
            this.editTextWaterLevelErrorThreshold.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.editTextWaterLevelErrorThreshold.Location = new System.Drawing.Point(224, 152);
            this.editTextWaterLevelErrorThreshold.Margin = new System.Windows.Forms.Padding(5);
            this.editTextWaterLevelErrorThreshold.Name = "editTextWaterLevelErrorThreshold";
            this.editTextWaterLevelErrorThreshold.Size = new System.Drawing.Size(269, 39);
            this.editTextWaterLevelErrorThreshold.TabIndex = 46;
            this.editTextWaterLevelErrorThreshold.TextBoxHint = "Число неудачных измерений уровня воды после которого на индикаторе отобразятся пр" +
    "очерки (от 1 до 255)";
            this.editTextWaterLevelErrorThreshold.Value = null;
            // 
            // editTextButtonLongTime
            // 
            this.editTextButtonLongTime.AutoSize = true;
            this.editTextButtonLongTime.Caption = "Долгое нажатие кнопок (msec)";
            this.editTextButtonLongTime.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.editTextButtonLongTime.Location = new System.Drawing.Point(17, 398);
            this.editTextButtonLongTime.Margin = new System.Windows.Forms.Padding(5);
            this.editTextButtonLongTime.Name = "editTextButtonLongTime";
            this.editTextButtonLongTime.Size = new System.Drawing.Size(187, 39);
            this.editTextButtonLongTime.TabIndex = 45;
            this.editTextButtonLongTime.TextBoxHint = "Время зажатой кнопки для определения длительного нажатия, от 1000 до 10000 мсек.";
            this.editTextButtonLongTime.Value = null;
            // 
            // editText_WL_measure_interval
            // 
            this.editText_WL_measure_interval.Caption = "Интервал замера уровня воды (msec)";
            this.editText_WL_measure_interval.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.editText_WL_measure_interval.Location = new System.Drawing.Point(224, 5);
            this.editText_WL_measure_interval.Margin = new System.Windows.Forms.Padding(5);
            this.editText_WL_measure_interval.Name = "editText_WL_measure_interval";
            this.editText_WL_measure_interval.Size = new System.Drawing.Size(269, 39);
            this.editText_WL_measure_interval.TabIndex = 37;
            this.editText_WL_measure_interval.TextBoxHint = "Длительность паузы перед повторным измерением расстояния до воды в баке. Рекоменд" +
    "уется не меньше 60 мсек";
            this.editText_WL_measure_interval.Value = null;
            // 
            // editText_wl_median_buffer_size
            // 
            this.editText_wl_median_buffer_size.AutoSize = true;
            this.editText_wl_median_buffer_size.Caption = "Медианный фильтр для уровня воды";
            this.editText_wl_median_buffer_size.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.editText_wl_median_buffer_size.Location = new System.Drawing.Point(224, 54);
            this.editText_wl_median_buffer_size.Margin = new System.Windows.Forms.Padding(5);
            this.editText_wl_median_buffer_size.Name = "editText_wl_median_buffer_size";
            this.editText_wl_median_buffer_size.Size = new System.Drawing.Size(269, 39);
            this.editText_wl_median_buffer_size.TabIndex = 38;
            this.editText_wl_median_buffer_size.TextBoxHint = "Размер буфера медианного фильтра для RAW показаний датчика уровня воды (от 1 до 2" +
    "55)";
            this.editText_wl_median_buffer_size.Value = null;
            // 
            // wiFiPower
            // 
            this.wiFiPower.Caption = "Мощность Wi-Fi (10..20.5 dBm)";
            this.wiFiPower.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.wiFiPower.Location = new System.Drawing.Point(17, 299);
            this.wiFiPower.Margin = new System.Windows.Forms.Padding(5);
            this.wiFiPower.Name = "wiFiPower";
            this.wiFiPower.Size = new System.Drawing.Size(187, 40);
            this.wiFiPower.TabIndex = 35;
            this.wiFiPower.TextBoxHint = "";
            this.wiFiPower.Value = null;
            // 
            // editText_wl_avg_buffer_size
            // 
            this.editText_wl_avg_buffer_size.AutoSize = true;
            this.editText_wl_avg_buffer_size.Caption = "Cкользящее среднее для уровня воды";
            this.editText_wl_avg_buffer_size.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.editText_wl_avg_buffer_size.Location = new System.Drawing.Point(224, 103);
            this.editText_wl_avg_buffer_size.Margin = new System.Windows.Forms.Padding(5);
            this.editText_wl_avg_buffer_size.Name = "editText_wl_avg_buffer_size";
            this.editText_wl_avg_buffer_size.Size = new System.Drawing.Size(269, 39);
            this.editText_wl_avg_buffer_size.TabIndex = 42;
            this.editText_wl_avg_buffer_size.TextBoxHint = "Размер буфера фильтра \'скользящее среднее\' для значений медианы датчика уровня во" +
    "ды (от 1 до 129)";
            this.editText_wl_avg_buffer_size.Value = null;
            // 
            // editText_wl_full
            // 
            this.editText_wl_full.AutoSize = true;
            this.editText_wl_full.Caption = "Уровень полного бака (μs)";
            this.editText_wl_full.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.editText_wl_full.Location = new System.Drawing.Point(17, 5);
            this.editText_wl_full.Margin = new System.Windows.Forms.Padding(5);
            this.editText_wl_full.Name = "editText_wl_full";
            this.editText_wl_full.Size = new System.Drawing.Size(187, 39);
            this.editText_wl_full.TabIndex = 29;
            this.editText_wl_full.TextBoxHint = "Число микросекунд соответствующие расстоянию когда бак полностью заполнен водой";
            this.editText_wl_full.Value = null;
            // 
            // editText_wl_cut_off_percent
            // 
            this.editText_wl_cut_off_percent.AutoSize = true;
            this.editText_wl_cut_off_percent.Caption = "Порог отключения набора воды (%)";
            this.editText_wl_cut_off_percent.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.editText_wl_cut_off_percent.Location = new System.Drawing.Point(224, 201);
            this.editText_wl_cut_off_percent.Margin = new System.Windows.Forms.Padding(5);
            this.editText_wl_cut_off_percent.Name = "editText_wl_cut_off_percent";
            this.editText_wl_cut_off_percent.Size = new System.Drawing.Size(269, 39);
            this.editText_wl_cut_off_percent.TabIndex = 39;
            this.editText_wl_cut_off_percent.TextBoxHint = "";
            this.editText_wl_cut_off_percent.Value = null;
            // 
            // editText_button_time
            // 
            this.editText_button_time.AutoSize = true;
            this.editText_button_time.Caption = "Время срабатывания кнопок (msec)";
            this.editText_button_time.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.editText_button_time.Location = new System.Drawing.Point(17, 349);
            this.editText_button_time.Margin = new System.Windows.Forms.Padding(5);
            this.editText_button_time.Name = "editText_button_time";
            this.editText_button_time.Size = new System.Drawing.Size(187, 39);
            this.editText_button_time.TabIndex = 36;
            this.editText_button_time.TextBoxHint = "Время нажатия кнопки для её срабатывания (антидребезг), от 20 до 80 мсек.";
            this.editText_button_time.Value = null;
            // 
            // editText_internal_temp_avg_size
            // 
            this.editText_internal_temp_avg_size.AutoSize = true;
            this.editText_internal_temp_avg_size.Caption = "Скользящее среднее для температуры в баке";
            this.editText_internal_temp_avg_size.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.editText_internal_temp_avg_size.Location = new System.Drawing.Point(224, 250);
            this.editText_internal_temp_avg_size.Margin = new System.Windows.Forms.Padding(5);
            this.editText_internal_temp_avg_size.Name = "editText_internal_temp_avg_size";
            this.editText_internal_temp_avg_size.Size = new System.Drawing.Size(269, 39);
            this.editText_internal_temp_avg_size.TabIndex = 40;
            this.editText_internal_temp_avg_size.TextBoxHint = "Размер буфера фильтра \'скользящее среднее\' для значений датчика температуры внутр" +
    "и бака (от 1 до 8)";
            this.editText_internal_temp_avg_size.Value = null;
            // 
            // editText_light_brightness
            // 
            this.editText_light_brightness.AutoSize = true;
            this.editText_light_brightness.Caption = "Яркость освещения (%)";
            this.editText_light_brightness.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.editText_light_brightness.Location = new System.Drawing.Point(17, 250);
            this.editText_light_brightness.Margin = new System.Windows.Forms.Padding(5);
            this.editText_light_brightness.Name = "editText_light_brightness";
            this.editText_light_brightness.Size = new System.Drawing.Size(187, 39);
            this.editText_light_brightness.TabIndex = 34;
            this.editText_light_brightness.TextBoxHint = "Яркость светодиодного освещения от 0 до 100%";
            this.editText_light_brightness.Value = null;
            // 
            // editText_WaterVolumeLitre
            // 
            this.editText_WaterVolumeLitre.AutoSize = true;
            this.editText_WaterVolumeLitre.Caption = "Объём воды полного бака (л)";
            this.editText_WaterVolumeLitre.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.editText_WaterVolumeLitre.Location = new System.Drawing.Point(224, 299);
            this.editText_WaterVolumeLitre.Margin = new System.Windows.Forms.Padding(5);
            this.editText_WaterVolumeLitre.Name = "editText_WaterVolumeLitre";
            this.editText_WaterVolumeLitre.Size = new System.Drawing.Size(269, 39);
            this.editText_WaterVolumeLitre.TabIndex = 43;
            this.editText_WaterVolumeLitre.TextBoxHint = "";
            this.editText_WaterVolumeLitre.Value = null;
            // 
            // editText_wl_empty
            // 
            this.editText_wl_empty.AutoSize = true;
            this.editText_wl_empty.Caption = "Уровень пустого бака (μs)";
            this.editText_wl_empty.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.editText_wl_empty.Location = new System.Drawing.Point(17, 54);
            this.editText_wl_empty.Margin = new System.Windows.Forms.Padding(5);
            this.editText_wl_empty.Name = "editText_wl_empty";
            this.editText_wl_empty.Size = new System.Drawing.Size(187, 39);
            this.editText_wl_empty.TabIndex = 30;
            this.editText_wl_empty.TextBoxHint = "Число микросекунд соответствующие расстоянию когда бак полностью пустой";
            this.editText_wl_empty.Value = null;
            // 
            // editText_HeaterPowerKWatt
            // 
            this.editText_HeaterPowerKWatt.AutoSize = true;
            this.editText_HeaterPowerKWatt.Caption = "Мощность ТЭНа с учётом КПД (кВт)";
            this.editText_HeaterPowerKWatt.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.editText_HeaterPowerKWatt.Location = new System.Drawing.Point(224, 348);
            this.editText_HeaterPowerKWatt.Margin = new System.Windows.Forms.Padding(5);
            this.editText_HeaterPowerKWatt.Name = "editText_HeaterPowerKWatt";
            this.editText_HeaterPowerKWatt.Size = new System.Drawing.Size(269, 39);
            this.editText_HeaterPowerKWatt.TabIndex = 44;
            this.editText_HeaterPowerKWatt.TextBoxHint = "";
            this.editText_HeaterPowerKWatt.Value = null;
            // 
            // editText_heating_time_limit
            // 
            this.editText_heating_time_limit.AutoSize = true;
            this.editText_heating_time_limit.Caption = "Максимальное время нагрева (мин)";
            this.editText_heating_time_limit.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.editText_heating_time_limit.Location = new System.Drawing.Point(17, 152);
            this.editText_heating_time_limit.Margin = new System.Windows.Forms.Padding(5);
            this.editText_heating_time_limit.Name = "editText_heating_time_limit";
            this.editText_heating_time_limit.Size = new System.Drawing.Size(187, 39);
            this.editText_heating_time_limit.TabIndex = 32;
            this.editText_heating_time_limit.TextBoxHint = "Если на протяжении заданного времени не была достигнута желаемая температура воды" +
    " в баке, то нагрев прекращается и алгоритм переходит в аварийное состояние";
            this.editText_heating_time_limit.Value = null;
            // 
            // editText_abs_heating_time_limit
            // 
            this.editText_abs_heating_time_limit.AutoSize = true;
            this.editText_abs_heating_time_limit.Caption = "Абсолютное время нагрева (ч)";
            this.editText_abs_heating_time_limit.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.editText_abs_heating_time_limit.Location = new System.Drawing.Point(17, 201);
            this.editText_abs_heating_time_limit.Margin = new System.Windows.Forms.Padding(5);
            this.editText_abs_heating_time_limit.Name = "editText_abs_heating_time_limit";
            this.editText_abs_heating_time_limit.Size = new System.Drawing.Size(187, 39);
            this.editText_abs_heating_time_limit.TabIndex = 33;
            this.editText_abs_heating_time_limit.TextBoxHint = "Если на протяжении заданного времени, автомат нагревателя не был физически выключ" +
    "ен то нагрев прекратится и контроллер перейдёт в аварийное состояние";
            this.editText_abs_heating_time_limit.Value = null;
            // 
            // editText_min_water_heating_percent
            // 
            this.editText_min_water_heating_percent.AutoSize = true;
            this.editText_min_water_heating_percent.Caption = "Минимальный уровень воды (%)";
            this.editText_min_water_heating_percent.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.editText_min_water_heating_percent.Location = new System.Drawing.Point(17, 103);
            this.editText_min_water_heating_percent.Margin = new System.Windows.Forms.Padding(5);
            this.editText_min_water_heating_percent.Name = "editText_min_water_heating_percent";
            this.editText_min_water_heating_percent.Size = new System.Drawing.Size(187, 39);
            this.editText_min_water_heating_percent.TabIndex = 31;
            this.editText_min_water_heating_percent.TextBoxHint = "Уровень воды в баке ниже которого нагреватель не будет включаться";
            this.editText_min_water_heating_percent.Value = null;
            // 
            // ParametersControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupBox_properties);
            this.Name = "ParametersControl";
            this.Size = new System.Drawing.Size(1002, 713);
            this.groupBox_properties.ResumeLayout(false);
            this.panelProperties.ResumeLayout(false);
            this.panelProperties.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox_properties;
        private System.Windows.Forms.Panel panelProperties;
        private EditText editText_wl_full;
        private EditText editText_button_time;
        private EditText editText_light_brightness;
        private EditText editText_wl_empty;
        private EditText editText_heating_time_limit;
        private EditText editText_abs_heating_time_limit;
        private EditText editText_min_water_heating_percent;
        private EditText editText_WL_measure_interval;
        private EditText editText_wl_median_buffer_size;
        private EditText editText_wl_cut_off_percent;
        private EditText editText_internal_temp_avg_size;
        private System.Windows.Forms.Button button_clear;
        private System.Windows.Forms.Button button_restart;
        private System.Windows.Forms.CheckBox checkBox_iwd;
        private System.Windows.Forms.Button button_cancel;
        private System.Windows.Forms.Button button_save;
        private System.Windows.Forms.Button button_load;
        private System.Windows.Forms.Button button_Export;
        private WiFiPowerControl wiFiPower;
        private System.Windows.Forms.Button button_Import;
        private System.Windows.Forms.ToolTip toolTip1;
        private EditText editText_wl_avg_buffer_size;
        private EditText editText_WaterVolumeLitre;
        private EditText editText_HeaterPowerKWatt;
        private EditText editTextButtonLongTime;
        private EditText editTextWaterLevelErrorThreshold;
    }
}
