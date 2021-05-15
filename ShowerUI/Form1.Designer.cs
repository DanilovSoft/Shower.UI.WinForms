namespace ShowerUI
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            DevExpress.XtraCharts.SwiftPlotDiagram swiftPlotDiagram1 = new DevExpress.XtraCharts.SwiftPlotDiagram();
            DevExpress.XtraCharts.Series series1 = new DevExpress.XtraCharts.Series();
            DevExpress.XtraCharts.SwiftPlotSeriesView swiftPlotSeriesView1 = new DevExpress.XtraCharts.SwiftPlotSeriesView();
            DevExpress.XtraCharts.Series series2 = new DevExpress.XtraCharts.Series();
            DevExpress.XtraCharts.SwiftPlotSeriesView swiftPlotSeriesView2 = new DevExpress.XtraCharts.SwiftPlotSeriesView();
            DevExpress.XtraCharts.Series series3 = new DevExpress.XtraCharts.Series();
            DevExpress.XtraCharts.SwiftPlotSeriesView swiftPlotSeriesView3 = new DevExpress.XtraCharts.SwiftPlotSeriesView();
            DevExpress.XtraCharts.SwiftPlotSeriesView swiftPlotSeriesView4 = new DevExpress.XtraCharts.SwiftPlotSeriesView();
            DevExpress.XtraCharts.SwiftPlotDiagram swiftPlotDiagram2 = new DevExpress.XtraCharts.SwiftPlotDiagram();
            DevExpress.XtraCharts.Series series4 = new DevExpress.XtraCharts.Series();
            DevExpress.XtraCharts.SwiftPlotSeriesView swiftPlotSeriesView5 = new DevExpress.XtraCharts.SwiftPlotSeriesView();
            DevExpress.XtraCharts.SwiftPlotDiagram swiftPlotDiagram3 = new DevExpress.XtraCharts.SwiftPlotDiagram();
            DevExpress.XtraCharts.Series series5 = new DevExpress.XtraCharts.Series();
            DevExpress.XtraCharts.SwiftPlotSeriesView swiftPlotSeriesView6 = new DevExpress.XtraCharts.SwiftPlotSeriesView();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPageTemp = new System.Windows.Forms.TabPage();
            this.panel2 = new System.Windows.Forms.Panel();
            this.labelTempReconnectCount = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.labelTempReconnect = new System.Windows.Forms.Label();
            this.button_temp_stop = new System.Windows.Forms.Button();
            this.trackBar1 = new System.Windows.Forms.TrackBar();
            this.labelTimeLeft = new System.Windows.Forms.Label();
            this.buttonTempStartRecord = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.button6 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.button5 = new System.Windows.Forms.Button();
            this.chartControl_temperature = new DevExpress.XtraCharts.ChartControl();
            this.tabPage_wl = new System.Windows.Forms.TabPage();
            this.panel4 = new System.Windows.Forms.Panel();
            this.chartControl_water_level = new DevExpress.XtraCharts.ChartControl();
            this.panel3 = new System.Windows.Forms.Panel();
            this.button9 = new System.Windows.Forms.Button();
            this.checkBox_avg = new System.Windows.Forms.CheckBox();
            this.trackBar_avg = new System.Windows.Forms.TrackBar();
            this.trackBar_median = new System.Windows.Forms.TrackBar();
            this.button8 = new System.Windows.Forms.Button();
            this.checkBox_median = new System.Windows.Forms.CheckBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label_reconnect_count = new System.Windows.Forms.Label();
            this.textBox_max_water_level = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.checkBox4 = new System.Windows.Forms.CheckBox();
            this.textBox_min_water_level = new System.Windows.Forms.TextBox();
            this.button_wl = new System.Windows.Forms.Button();
            this.button_clear_wl = new System.Windows.Forms.Button();
            this.checkBox3 = new System.Windows.Forms.CheckBox();
            this.button_wl_stop = new System.Windows.Forms.Button();
            this.tabPageCalibration = new System.Windows.Forms.TabPage();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label6 = new System.Windows.Forms.Label();
            this.numericWaterLevelCalibInterval = new System.Windows.Forms.NumericUpDown();
            this.button_stop = new System.Windows.Forms.Button();
            this.buttonStartCalib = new System.Windows.Forms.Button();
            this.chartControl_wl_calibration = new DevExpress.XtraCharts.ChartControl();
            this.tabPageSettings = new System.Windows.Forms.TabPage();
            this.groupBox_properties = new System.Windows.Forms.GroupBox();
            this.button1 = new System.Windows.Forms.Button();
            this.checkBox_iwd = new System.Windows.Forms.CheckBox();
            this.button_load_props_canc = new System.Windows.Forms.Button();
            this.panel_properties = new System.Windows.Forms.FlowLayoutPanel();
            this.editText_wl_full = new ShowerUI.EditText();
            this.editText_wl_empty = new ShowerUI.EditText();
            this.editText_min_water_heating_percent = new ShowerUI.EditText();
            this.editText_abs_heating_time_limit = new ShowerUI.EditText();
            this.editText_heating_time_limit = new ShowerUI.EditText();
            this.editText_light_brightness = new ShowerUI.EditText();
            this.editText_wifi_power = new ShowerUI.EditText();
            this.editText_button_time = new ShowerUI.EditText();
            this.button_save_properties = new System.Windows.Forms.Button();
            this.buttonLoadProps = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.panel_properties_custom = new System.Windows.Forms.FlowLayoutPanel();
            this.editText_WL_measure_interval = new ShowerUI.EditText();
            this.editText_wl_ring_buffer_size = new ShowerUI.EditText();
            this.editText_wl_cut_off_percent = new ShowerUI.EditText();
            this.editText_internal_temp_buf_size = new ShowerUI.EditText();
            this.editText_wl_usec_per_deg = new ShowerUI.EditText();
            this.tabPageDebug = new System.Windows.Forms.TabPage();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.button_wifi_def = new System.Windows.Forms.Button();
            this.button_wifi_cur = new System.Windows.Forms.Button();
            this.textBox3 = new System.Windows.Forms.TextBox();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.button10 = new System.Windows.Forms.Button();
            this.button7 = new System.Windows.Forms.Button();
            this.Температуры = new System.Windows.Forms.TabPage();
            this.button2 = new System.Windows.Forms.Button();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.командаToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.resetToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pnigToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.tabControl1.SuspendLayout();
            this.tabPageTemp.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chartControl_temperature)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(swiftPlotDiagram1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(series1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(swiftPlotSeriesView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(series2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(swiftPlotSeriesView2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(series3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(swiftPlotSeriesView3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(swiftPlotSeriesView4)).BeginInit();
            this.tabPage_wl.SuspendLayout();
            this.panel4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chartControl_water_level)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(swiftPlotDiagram2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(series4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(swiftPlotSeriesView5)).BeginInit();
            this.panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar_avg)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar_median)).BeginInit();
            this.tabPageCalibration.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericWaterLevelCalibInterval)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chartControl_wl_calibration)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(swiftPlotDiagram3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(series5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(swiftPlotSeriesView6)).BeginInit();
            this.tabPageSettings.SuspendLayout();
            this.groupBox_properties.SuspendLayout();
            this.panel_properties.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.panel_properties_custom.SuspendLayout();
            this.tabPageDebug.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.Температуры.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).BeginInit();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPageTemp);
            this.tabControl1.Controls.Add(this.tabPage_wl);
            this.tabControl1.Controls.Add(this.tabPageCalibration);
            this.tabControl1.Controls.Add(this.tabPageSettings);
            this.tabControl1.Controls.Add(this.tabPageDebug);
            this.tabControl1.Controls.Add(this.Температуры);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 24);
            this.tabControl1.Margin = new System.Windows.Forms.Padding(3, 0, 3, 3);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(1218, 618);
            this.tabControl1.TabIndex = 14;
            this.tabControl1.Selected += new System.Windows.Forms.TabControlEventHandler(this.TabControl1_Selected);
            // 
            // tabPageTemp
            // 
            this.tabPageTemp.BackColor = System.Drawing.Color.Transparent;
            this.tabPageTemp.Controls.Add(this.panel2);
            this.tabPageTemp.Controls.Add(this.chartControl_temperature);
            this.tabPageTemp.Location = new System.Drawing.Point(4, 22);
            this.tabPageTemp.Name = "tabPageTemp";
            this.tabPageTemp.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageTemp.Size = new System.Drawing.Size(1210, 592);
            this.tabPageTemp.TabIndex = 0;
            this.tabPageTemp.Text = "Температура";
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.labelTempReconnectCount);
            this.panel2.Controls.Add(this.label7);
            this.panel2.Controls.Add(this.labelTempReconnect);
            this.panel2.Controls.Add(this.button_temp_stop);
            this.panel2.Controls.Add(this.trackBar1);
            this.panel2.Controls.Add(this.labelTimeLeft);
            this.panel2.Controls.Add(this.buttonTempStartRecord);
            this.panel2.Controls.Add(this.button4);
            this.panel2.Controls.Add(this.button6);
            this.panel2.Controls.Add(this.button3);
            this.panel2.Controls.Add(this.button5);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel2.Location = new System.Drawing.Point(3, 530);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1204, 59);
            this.panel2.TabIndex = 24;
            // 
            // labelTempReconnectCount
            // 
            this.labelTempReconnectCount.AutoSize = true;
            this.labelTempReconnectCount.Location = new System.Drawing.Point(152, 38);
            this.labelTempReconnectCount.Name = "labelTempReconnectCount";
            this.labelTempReconnectCount.Size = new System.Drawing.Size(13, 13);
            this.labelTempReconnectCount.TabIndex = 26;
            this.labelTempReconnectCount.Text = "0";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(950, 11);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(59, 13);
            this.label7.TabIndex = 25;
            this.label7.Text = "Осталось:";
            this.toolTip1.SetToolTip(this.label7, "Расчётное время до окончания нагрева");
            // 
            // labelTempReconnect
            // 
            this.labelTempReconnect.AutoSize = true;
            this.labelTempReconnect.Location = new System.Drawing.Point(5, 38);
            this.labelTempReconnect.Name = "labelTempReconnect";
            this.labelTempReconnect.Size = new System.Drawing.Size(141, 13);
            this.labelTempReconnect.TabIndex = 24;
            this.labelTempReconnect.Text = "Повторных подключений:";
            // 
            // button_temp_stop
            // 
            this.button_temp_stop.Location = new System.Drawing.Point(123, 6);
            this.button_temp_stop.Name = "button_temp_stop";
            this.button_temp_stop.Size = new System.Drawing.Size(75, 23);
            this.button_temp_stop.TabIndex = 15;
            this.button_temp_stop.Text = "Стоп";
            this.button_temp_stop.UseVisualStyleBackColor = true;
            this.button_temp_stop.Click += new System.EventHandler(this.Button_TemperatureStop_Click);
            // 
            // trackBar1
            // 
            this.trackBar1.Location = new System.Drawing.Point(642, 6);
            this.trackBar1.Maximum = 23;
            this.trackBar1.Minimum = 3;
            this.trackBar1.Name = "trackBar1";
            this.trackBar1.Size = new System.Drawing.Size(301, 45);
            this.trackBar1.SmallChange = 2;
            this.trackBar1.TabIndex = 22;
            this.trackBar1.TickFrequency = 2;
            this.trackBar1.Value = 3;
            this.trackBar1.Scroll += new System.EventHandler(this.TrackBar1_Scroll);
            // 
            // labelTimeLeft
            // 
            this.labelTimeLeft.AutoSize = true;
            this.labelTimeLeft.Location = new System.Drawing.Point(1015, 11);
            this.labelTimeLeft.Name = "labelTimeLeft";
            this.labelTimeLeft.Size = new System.Drawing.Size(51, 13);
            this.labelTimeLeft.TabIndex = 23;
            this.labelTimeLeft.Text = "__:__:__";
            // 
            // buttonTempStartRecord
            // 
            this.buttonTempStartRecord.Location = new System.Drawing.Point(5, 6);
            this.buttonTempStartRecord.Name = "buttonTempStartRecord";
            this.buttonTempStartRecord.Size = new System.Drawing.Size(75, 23);
            this.buttonTempStartRecord.TabIndex = 14;
            this.buttonTempStartRecord.Text = "Запись";
            this.buttonTempStartRecord.UseVisualStyleBackColor = true;
            this.buttonTempStartRecord.Click += new System.EventHandler(this.Button_TempRecord_Click);
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(495, 6);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(75, 23);
            this.button4.TabIndex = 17;
            this.button4.Text = "Симуляция";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.Button_Simulation_Click);
            // 
            // button6
            // 
            this.button6.Location = new System.Drawing.Point(390, 6);
            this.button6.Name = "button6";
            this.button6.Size = new System.Drawing.Size(99, 23);
            this.button6.TabIndex = 21;
            this.button6.Text = "Загрузить из...";
            this.button6.UseVisualStyleBackColor = true;
            this.button6.Click += new System.EventHandler(this.Button_Load_Click);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(285, 6);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(99, 23);
            this.button3.TabIndex = 16;
            this.button3.Text = "Сохранить в...";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.Button_Save_Click);
            // 
            // button5
            // 
            this.button5.Location = new System.Drawing.Point(204, 6);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(75, 23);
            this.button5.TabIndex = 20;
            this.button5.Text = "Очистить";
            this.button5.UseVisualStyleBackColor = true;
            this.button5.Click += new System.EventHandler(this.Button_Clear_Click);
            // 
            // chartControl_temperature
            // 
            this.chartControl_temperature.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.chartControl_temperature.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            swiftPlotDiagram1.AxisX.Label.Visible = false;
            swiftPlotDiagram1.AxisX.MinorCount = 1;
            swiftPlotDiagram1.AxisX.Tickmarks.MinorVisible = false;
            swiftPlotDiagram1.AxisX.Visibility = DevExpress.Utils.DefaultBoolean.True;
            swiftPlotDiagram1.AxisX.VisibleInPanesSerializable = "-1";
            swiftPlotDiagram1.AxisY.MinorCount = 1;
            swiftPlotDiagram1.AxisY.Visibility = DevExpress.Utils.DefaultBoolean.True;
            swiftPlotDiagram1.AxisY.VisibleInPanesSerializable = "-1";
            swiftPlotDiagram1.AxisY.VisualRange.Auto = false;
            swiftPlotDiagram1.AxisY.VisualRange.MaxValueSerializable = "39";
            swiftPlotDiagram1.AxisY.VisualRange.MinValueSerializable = "15";
            swiftPlotDiagram1.AxisY.WholeRange.Auto = false;
            swiftPlotDiagram1.AxisY.WholeRange.MaxValueSerializable = "39";
            swiftPlotDiagram1.AxisY.WholeRange.MinValueSerializable = "15";
            this.chartControl_temperature.Diagram = swiftPlotDiagram1;
            this.chartControl_temperature.Legend.Name = "Default Legend";
            this.chartControl_temperature.Legend.Visibility = DevExpress.Utils.DefaultBoolean.False;
            this.chartControl_temperature.Location = new System.Drawing.Point(0, 0);
            this.chartControl_temperature.Name = "chartControl_temperature";
            series1.ArgumentScaleType = DevExpress.XtraCharts.ScaleType.Numerical;
            series1.CrosshairTextOptions.EnableAntialiasing = DevExpress.Utils.DefaultBoolean.False;
            series1.CrosshairTextOptions.Tag = "Температура в баке";
            series1.Name = "TempOn";
            swiftPlotSeriesView1.Color = System.Drawing.Color.Red;
            series1.View = swiftPlotSeriesView1;
            series2.CrosshairTextOptions.EnableAntialiasing = DevExpress.Utils.DefaultBoolean.False;
            series2.CrosshairTextOptions.Tag = "Температура в баке";
            series2.LegendName = "Default Legend";
            series2.Name = "TempOff";
            swiftPlotSeriesView2.Color = System.Drawing.Color.Blue;
            series2.View = swiftPlotSeriesView2;
            series3.ArgumentScaleType = DevExpress.XtraCharts.ScaleType.Numerical;
            series3.Name = "CalculatedTemp";
            series3.ShowInLegend = false;
            swiftPlotSeriesView3.Color = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            series3.View = swiftPlotSeriesView3;
            this.chartControl_temperature.SeriesSerializable = new DevExpress.XtraCharts.Series[] {
        series1,
        series2,
        series3};
            swiftPlotSeriesView4.LineStyle.Thickness = 2;
            this.chartControl_temperature.SeriesTemplate.View = swiftPlotSeriesView4;
            this.chartControl_temperature.Size = new System.Drawing.Size(1210, 524);
            this.chartControl_temperature.TabIndex = 2;
            // 
            // tabPage_wl
            // 
            this.tabPage_wl.BackColor = System.Drawing.SystemColors.Control;
            this.tabPage_wl.Controls.Add(this.panel4);
            this.tabPage_wl.Controls.Add(this.panel3);
            this.tabPage_wl.Location = new System.Drawing.Point(4, 22);
            this.tabPage_wl.Name = "tabPage_wl";
            this.tabPage_wl.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage_wl.Size = new System.Drawing.Size(1210, 592);
            this.tabPage_wl.TabIndex = 1;
            this.tabPage_wl.Text = "Уровень воды";
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.chartControl_water_level);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel4.Location = new System.Drawing.Point(3, 3);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(1204, 473);
            this.panel4.TabIndex = 9;
            // 
            // chartControl_water_level
            // 
            swiftPlotDiagram2.AxisX.Visibility = DevExpress.Utils.DefaultBoolean.False;
            swiftPlotDiagram2.AxisX.VisibleInPanesSerializable = "-1";
            swiftPlotDiagram2.AxisY.GridLines.Visible = false;
            swiftPlotDiagram2.AxisY.Label.Visible = false;
            swiftPlotDiagram2.AxisY.Tickmarks.MinorVisible = false;
            swiftPlotDiagram2.AxisY.Tickmarks.Visible = false;
            swiftPlotDiagram2.AxisY.Visibility = DevExpress.Utils.DefaultBoolean.False;
            swiftPlotDiagram2.AxisY.VisibleInPanesSerializable = "-1";
            swiftPlotDiagram2.AxisY.VisualRange.Auto = false;
            swiftPlotDiagram2.AxisY.VisualRange.MaxValueSerializable = "9.5";
            swiftPlotDiagram2.AxisY.VisualRange.MinValueSerializable = "0";
            swiftPlotDiagram2.AxisY.WholeRange.Auto = false;
            swiftPlotDiagram2.AxisY.WholeRange.MaxValueSerializable = "9.5";
            swiftPlotDiagram2.AxisY.WholeRange.MinValueSerializable = "0";
            swiftPlotDiagram2.EnableAxisXScrolling = true;
            swiftPlotDiagram2.EnableAxisXZooming = true;
            this.chartControl_water_level.Diagram = swiftPlotDiagram2;
            this.chartControl_water_level.Dock = System.Windows.Forms.DockStyle.Fill;
            this.chartControl_water_level.Legend.Name = "Default Legend";
            this.chartControl_water_level.Location = new System.Drawing.Point(0, 0);
            this.chartControl_water_level.Name = "chartControl_water_level";
            series4.ArgumentScaleType = DevExpress.XtraCharts.ScaleType.Numerical;
            series4.LabelsVisibility = DevExpress.Utils.DefaultBoolean.False;
            series4.Name = "RAW (μs)";
            swiftPlotSeriesView5.Color = System.Drawing.Color.Fuchsia;
            series4.View = swiftPlotSeriesView5;
            this.chartControl_water_level.SeriesSerializable = new DevExpress.XtraCharts.Series[] {
        series4};
            this.chartControl_water_level.Size = new System.Drawing.Size(1204, 473);
            this.chartControl_water_level.TabIndex = 0;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.button9);
            this.panel3.Controls.Add(this.checkBox_avg);
            this.panel3.Controls.Add(this.trackBar_avg);
            this.panel3.Controls.Add(this.trackBar_median);
            this.panel3.Controls.Add(this.button8);
            this.panel3.Controls.Add(this.checkBox_median);
            this.panel3.Controls.Add(this.label2);
            this.panel3.Controls.Add(this.label_reconnect_count);
            this.panel3.Controls.Add(this.textBox_max_water_level);
            this.panel3.Controls.Add(this.label1);
            this.panel3.Controls.Add(this.checkBox4);
            this.panel3.Controls.Add(this.textBox_min_water_level);
            this.panel3.Controls.Add(this.button_wl);
            this.panel3.Controls.Add(this.button_clear_wl);
            this.panel3.Controls.Add(this.checkBox3);
            this.panel3.Controls.Add(this.button_wl_stop);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel3.Location = new System.Drawing.Point(3, 476);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(1204, 113);
            this.panel3.TabIndex = 8;
            // 
            // button9
            // 
            this.button9.Location = new System.Drawing.Point(110, 35);
            this.button9.Name = "button9";
            this.button9.Size = new System.Drawing.Size(100, 23);
            this.button9.TabIndex = 15;
            this.button9.Text = "Загрузить из...";
            this.button9.UseVisualStyleBackColor = true;
            this.button9.Click += new System.EventHandler(this.Button9_Click_1);
            // 
            // checkBox_avg
            // 
            this.checkBox_avg.AutoSize = true;
            this.checkBox_avg.Checked = true;
            this.checkBox_avg.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox_avg.Location = new System.Drawing.Point(610, 63);
            this.checkBox_avg.Name = "checkBox_avg";
            this.checkBox_avg.Size = new System.Drawing.Size(92, 17);
            this.checkBox_avg.TabIndex = 14;
            this.checkBox_avg.Text = "Среднее (μs)";
            this.checkBox_avg.UseVisualStyleBackColor = true;
            this.checkBox_avg.CheckedChanged += new System.EventHandler(this.CheckBox_avg_CheckedChanged);
            // 
            // trackBar_avg
            // 
            this.trackBar_avg.Location = new System.Drawing.Point(291, 49);
            this.trackBar_avg.Maximum = 32;
            this.trackBar_avg.Minimum = 1;
            this.trackBar_avg.Name = "trackBar_avg";
            this.trackBar_avg.Size = new System.Drawing.Size(313, 45);
            this.trackBar_avg.TabIndex = 13;
            this.trackBar_avg.Value = 1;
            this.trackBar_avg.Scroll += new System.EventHandler(this.TrackBar2_Scroll_1);
            // 
            // trackBar_median
            // 
            this.trackBar_median.Location = new System.Drawing.Point(291, 3);
            this.trackBar_median.Maximum = 200;
            this.trackBar_median.Minimum = 1;
            this.trackBar_median.Name = "trackBar_median";
            this.trackBar_median.Size = new System.Drawing.Size(313, 45);
            this.trackBar_median.TabIndex = 12;
            this.trackBar_median.TickFrequency = 2;
            this.trackBar_median.Value = 1;
            this.trackBar_median.Scroll += new System.EventHandler(this.TrackBar_median_Scroll);
            // 
            // button8
            // 
            this.button8.Location = new System.Drawing.Point(5, 35);
            this.button8.Name = "button8";
            this.button8.Size = new System.Drawing.Size(99, 23);
            this.button8.TabIndex = 11;
            this.button8.Text = "Сохранить в...";
            this.button8.UseVisualStyleBackColor = true;
            this.button8.Click += new System.EventHandler(this.Button8_Click);
            // 
            // checkBox_median
            // 
            this.checkBox_median.AutoSize = true;
            this.checkBox_median.Checked = true;
            this.checkBox_median.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox_median.Location = new System.Drawing.Point(609, 9);
            this.checkBox_median.Name = "checkBox_median";
            this.checkBox_median.Size = new System.Drawing.Size(93, 17);
            this.checkBox_median.TabIndex = 9;
            this.checkBox_median.Text = "Медиана (μs)";
            this.checkBox_median.UseVisualStyleBackColor = true;
            this.checkBox_median.CheckedChanged += new System.EventHandler(this.CheckBox_WL_Median_CheckedChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(1094, 8);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(49, 13);
            this.label2.TabIndex = 6;
            this.label2.Text = "Max (μs)";
            // 
            // label_reconnect_count
            // 
            this.label_reconnect_count.AutoSize = true;
            this.label_reconnect_count.Location = new System.Drawing.Point(1058, 67);
            this.label_reconnect_count.Name = "label_reconnect_count";
            this.label_reconnect_count.Size = new System.Drawing.Size(101, 13);
            this.label_reconnect_count.TabIndex = 7;
            this.label_reconnect_count.Text = "Reconnect count: 0";
            // 
            // textBox_max_water_level
            // 
            this.textBox_max_water_level.Location = new System.Drawing.Point(1097, 23);
            this.textBox_max_water_level.Name = "textBox_max_water_level";
            this.textBox_max_water_level.ReadOnly = true;
            this.textBox_max_water_level.Size = new System.Drawing.Size(85, 21);
            this.textBox_max_water_level.TabIndex = 4;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(968, 8);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(45, 13);
            this.label1.TabIndex = 5;
            this.label1.Text = "Min (μs)";
            // 
            // checkBox4
            // 
            this.checkBox4.AutoSize = true;
            this.checkBox4.Checked = true;
            this.checkBox4.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox4.Location = new System.Drawing.Point(722, 63);
            this.checkBox4.Name = "checkBox4";
            this.checkBox4.Size = new System.Drawing.Size(70, 17);
            this.checkBox4.TabIndex = 4;
            this.checkBox4.Text = "0-99 (%)";
            this.checkBox4.UseVisualStyleBackColor = true;
            this.checkBox4.CheckedChanged += new System.EventHandler(this.CheckBox_percent_CheckedChanged);
            // 
            // textBox_min_water_level
            // 
            this.textBox_min_water_level.Location = new System.Drawing.Point(971, 24);
            this.textBox_min_water_level.Name = "textBox_min_water_level";
            this.textBox_min_water_level.ReadOnly = true;
            this.textBox_min_water_level.Size = new System.Drawing.Size(82, 21);
            this.textBox_min_water_level.TabIndex = 3;
            // 
            // button_wl
            // 
            this.button_wl.Location = new System.Drawing.Point(5, 6);
            this.button_wl.Name = "button_wl";
            this.button_wl.Size = new System.Drawing.Size(75, 23);
            this.button_wl.TabIndex = 0;
            this.button_wl.Text = "Старт";
            this.button_wl.UseVisualStyleBackColor = true;
            this.button_wl.Click += new System.EventHandler(this.Button_WaterLevelStart_Click);
            // 
            // button_clear_wl
            // 
            this.button_clear_wl.Location = new System.Drawing.Point(167, 6);
            this.button_clear_wl.Name = "button_clear_wl";
            this.button_clear_wl.Size = new System.Drawing.Size(75, 23);
            this.button_clear_wl.TabIndex = 2;
            this.button_clear_wl.Text = "Очистить";
            this.button_clear_wl.UseVisualStyleBackColor = true;
            this.button_clear_wl.Click += new System.EventHandler(this.Button_clear_wl_Click);
            // 
            // checkBox3
            // 
            this.checkBox3.AutoSize = true;
            this.checkBox3.Checked = true;
            this.checkBox3.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox3.Location = new System.Drawing.Point(722, 9);
            this.checkBox3.Name = "checkBox3";
            this.checkBox3.Size = new System.Drawing.Size(72, 17);
            this.checkBox3.TabIndex = 3;
            this.checkBox3.Text = "RAW (μs)";
            this.checkBox3.UseVisualStyleBackColor = true;
            this.checkBox3.CheckedChanged += new System.EventHandler(this.CheckBox_uSec_CheckedChanged);
            // 
            // button_wl_stop
            // 
            this.button_wl_stop.Enabled = false;
            this.button_wl_stop.Location = new System.Drawing.Point(86, 6);
            this.button_wl_stop.Name = "button_wl_stop";
            this.button_wl_stop.Size = new System.Drawing.Size(75, 23);
            this.button_wl_stop.TabIndex = 1;
            this.button_wl_stop.Text = "Стоп";
            this.button_wl_stop.UseVisualStyleBackColor = true;
            this.button_wl_stop.Click += new System.EventHandler(this.Button_WaterLevelStop_Click);
            // 
            // tabPageCalibration
            // 
            this.tabPageCalibration.BackColor = System.Drawing.SystemColors.Control;
            this.tabPageCalibration.Controls.Add(this.panel1);
            this.tabPageCalibration.Controls.Add(this.chartControl_wl_calibration);
            this.tabPageCalibration.Location = new System.Drawing.Point(4, 22);
            this.tabPageCalibration.Name = "tabPageCalibration";
            this.tabPageCalibration.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageCalibration.Size = new System.Drawing.Size(1210, 592);
            this.tabPageCalibration.TabIndex = 4;
            this.tabPageCalibration.Text = "Калибровка уровня";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.label6);
            this.panel1.Controls.Add(this.numericWaterLevelCalibInterval);
            this.panel1.Controls.Add(this.button_stop);
            this.panel1.Controls.Add(this.buttonStartCalib);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(3, 557);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1204, 32);
            this.panel1.TabIndex = 1;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(243, 8);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(95, 13);
            this.label6.TabIndex = 3;
            this.label6.Text = "Интервал (msec):";
            // 
            // numericWaterLevelCalibInterval
            // 
            this.numericWaterLevelCalibInterval.Increment = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.numericWaterLevelCalibInterval.Location = new System.Drawing.Point(344, 6);
            this.numericWaterLevelCalibInterval.Maximum = new decimal(new int[] {
            200,
            0,
            0,
            0});
            this.numericWaterLevelCalibInterval.Minimum = new decimal(new int[] {
            30,
            0,
            0,
            0});
            this.numericWaterLevelCalibInterval.Name = "numericWaterLevelCalibInterval";
            this.numericWaterLevelCalibInterval.Size = new System.Drawing.Size(66, 21);
            this.numericWaterLevelCalibInterval.TabIndex = 2;
            this.numericWaterLevelCalibInterval.Value = new decimal(new int[] {
            100,
            0,
            0,
            0});
            // 
            // button_stop
            // 
            this.button_stop.Enabled = false;
            this.button_stop.Location = new System.Drawing.Point(119, 3);
            this.button_stop.Name = "button_stop";
            this.button_stop.Size = new System.Drawing.Size(75, 23);
            this.button_stop.TabIndex = 1;
            this.button_stop.Text = "Стоп";
            this.button_stop.UseVisualStyleBackColor = true;
            this.button_stop.Click += new System.EventHandler(this.Button_stop_Click);
            // 
            // buttonStartCalib
            // 
            this.buttonStartCalib.Location = new System.Drawing.Point(5, 3);
            this.buttonStartCalib.Name = "buttonStartCalib";
            this.buttonStartCalib.Size = new System.Drawing.Size(75, 23);
            this.buttonStartCalib.TabIndex = 0;
            this.buttonStartCalib.Text = "Старт";
            this.buttonStartCalib.UseVisualStyleBackColor = true;
            this.buttonStartCalib.Click += new System.EventHandler(this.ButtonStartCalibrationClick);
            // 
            // chartControl_wl_calibration
            // 
            this.chartControl_wl_calibration.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            swiftPlotDiagram3.AxisX.Label.Visible = false;
            swiftPlotDiagram3.AxisX.Tickmarks.MinorVisible = false;
            swiftPlotDiagram3.AxisX.Tickmarks.Visible = false;
            swiftPlotDiagram3.AxisX.Visibility = DevExpress.Utils.DefaultBoolean.True;
            swiftPlotDiagram3.AxisX.VisibleInPanesSerializable = "-1";
            swiftPlotDiagram3.AxisX.VisualRange.Auto = false;
            swiftPlotDiagram3.AxisX.VisualRange.MaxValueSerializable = "10";
            swiftPlotDiagram3.AxisX.VisualRange.MinValueSerializable = "0";
            swiftPlotDiagram3.AxisX.WholeRange.Auto = false;
            swiftPlotDiagram3.AxisX.WholeRange.MaxValueSerializable = "10";
            swiftPlotDiagram3.AxisX.WholeRange.MinValueSerializable = "0";
            swiftPlotDiagram3.AxisY.VisibleInPanesSerializable = "-1";
            this.chartControl_wl_calibration.Diagram = swiftPlotDiagram3;
            this.chartControl_wl_calibration.Legend.Name = "Default Legend";
            this.chartControl_wl_calibration.Legend.Visibility = DevExpress.Utils.DefaultBoolean.False;
            this.chartControl_wl_calibration.Location = new System.Drawing.Point(8, 6);
            this.chartControl_wl_calibration.Name = "chartControl_wl_calibration";
            series5.Name = "usec";
            swiftPlotSeriesView6.Color = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            swiftPlotSeriesView6.LineStyle.Thickness = 2;
            series5.View = swiftPlotSeriesView6;
            this.chartControl_wl_calibration.SeriesSerializable = new DevExpress.XtraCharts.Series[] {
        series5};
            this.chartControl_wl_calibration.Size = new System.Drawing.Size(1194, 545);
            this.chartControl_wl_calibration.TabIndex = 0;
            // 
            // tabPageSettings
            // 
            this.tabPageSettings.BackColor = System.Drawing.SystemColors.Control;
            this.tabPageSettings.Controls.Add(this.groupBox_properties);
            this.tabPageSettings.Location = new System.Drawing.Point(4, 22);
            this.tabPageSettings.Name = "tabPageSettings";
            this.tabPageSettings.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageSettings.Size = new System.Drawing.Size(1210, 592);
            this.tabPageSettings.TabIndex = 2;
            this.tabPageSettings.Text = "Настройки";
            // 
            // groupBox_properties
            // 
            this.groupBox_properties.Controls.Add(this.button1);
            this.groupBox_properties.Controls.Add(this.checkBox_iwd);
            this.groupBox_properties.Controls.Add(this.button_load_props_canc);
            this.groupBox_properties.Controls.Add(this.panel_properties);
            this.groupBox_properties.Controls.Add(this.button_save_properties);
            this.groupBox_properties.Controls.Add(this.buttonLoadProps);
            this.groupBox_properties.Controls.Add(this.groupBox1);
            this.groupBox_properties.Location = new System.Drawing.Point(6, 6);
            this.groupBox_properties.Name = "groupBox_properties";
            this.groupBox_properties.Size = new System.Drawing.Size(461, 483);
            this.groupBox_properties.TabIndex = 13;
            this.groupBox_properties.TabStop = false;
            this.groupBox_properties.Text = "Properties";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(168, 454);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(88, 23);
            this.button1.TabIndex = 49;
            this.button1.Text = "Перезапуск";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.Button1_Click);
            // 
            // checkBox_iwd
            // 
            this.checkBox_iwd.AutoCheck = false;
            this.checkBox_iwd.AutoSize = true;
            this.checkBox_iwd.Location = new System.Drawing.Point(223, 345);
            this.checkBox_iwd.Name = "checkBox_iwd";
            this.checkBox_iwd.Size = new System.Drawing.Size(75, 17);
            this.checkBox_iwd.TabIndex = 48;
            this.checkBox_iwd.Text = "Watchdog";
            this.checkBox_iwd.UseVisualStyleBackColor = true;
            // 
            // button_load_props_canc
            // 
            this.button_load_props_canc.Enabled = false;
            this.button_load_props_canc.Location = new System.Drawing.Point(262, 454);
            this.button_load_props_canc.Name = "button_load_props_canc";
            this.button_load_props_canc.Size = new System.Drawing.Size(75, 23);
            this.button_load_props_canc.TabIndex = 47;
            this.button_load_props_canc.Text = "Отмена";
            this.button_load_props_canc.UseVisualStyleBackColor = true;
            this.button_load_props_canc.Click += new System.EventHandler(this.Button9_Click);
            // 
            // panel_properties
            // 
            this.panel_properties.AutoSize = true;
            this.panel_properties.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.panel_properties.Controls.Add(this.editText_wl_full);
            this.panel_properties.Controls.Add(this.editText_wl_empty);
            this.panel_properties.Controls.Add(this.editText_min_water_heating_percent);
            this.panel_properties.Controls.Add(this.editText_abs_heating_time_limit);
            this.panel_properties.Controls.Add(this.editText_heating_time_limit);
            this.panel_properties.Controls.Add(this.editText_light_brightness);
            this.panel_properties.Controls.Add(this.editText_wifi_power);
            this.panel_properties.Controls.Add(this.editText_button_time);
            this.panel_properties.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.panel_properties.Location = new System.Drawing.Point(24, 19);
            this.panel_properties.Name = "panel_properties";
            this.panel_properties.Size = new System.Drawing.Size(176, 392);
            this.panel_properties.TabIndex = 14;
            // 
            // editText_wl_full
            // 
            this.editText_wl_full.AutoSize = true;
            this.editText_wl_full.Caption = "WaterLevel Full";
            this.editText_wl_full.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.editText_wl_full.Location = new System.Drawing.Point(5, 5);
            this.editText_wl_full.Margin = new System.Windows.Forms.Padding(5);
            this.editText_wl_full.Name = "editText_wl_full";
            this.editText_wl_full.Size = new System.Drawing.Size(166, 39);
            this.editText_wl_full.TabIndex = 29;
            this.editText_wl_full.Value = null;
            // 
            // editText_wl_empty
            // 
            this.editText_wl_empty.AutoSize = true;
            this.editText_wl_empty.Caption = "WaterLevel Empty";
            this.editText_wl_empty.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.editText_wl_empty.Location = new System.Drawing.Point(5, 54);
            this.editText_wl_empty.Margin = new System.Windows.Forms.Padding(5);
            this.editText_wl_empty.Name = "editText_wl_empty";
            this.editText_wl_empty.Size = new System.Drawing.Size(166, 39);
            this.editText_wl_empty.TabIndex = 30;
            this.editText_wl_empty.Value = null;
            // 
            // editText_min_water_heating_percent
            // 
            this.editText_min_water_heating_percent.AutoSize = true;
            this.editText_min_water_heating_percent.Caption = "Minimum Water Heating Percent";
            this.editText_min_water_heating_percent.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.editText_min_water_heating_percent.Location = new System.Drawing.Point(5, 103);
            this.editText_min_water_heating_percent.Margin = new System.Windows.Forms.Padding(5);
            this.editText_min_water_heating_percent.Name = "editText_min_water_heating_percent";
            this.editText_min_water_heating_percent.Size = new System.Drawing.Size(166, 39);
            this.editText_min_water_heating_percent.TabIndex = 31;
            this.editText_min_water_heating_percent.Value = null;
            // 
            // editText_abs_heating_time_limit
            // 
            this.editText_abs_heating_time_limit.AutoSize = true;
            this.editText_abs_heating_time_limit.Caption = "Absolute Heating Time Limit";
            this.editText_abs_heating_time_limit.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.editText_abs_heating_time_limit.Location = new System.Drawing.Point(5, 152);
            this.editText_abs_heating_time_limit.Margin = new System.Windows.Forms.Padding(5);
            this.editText_abs_heating_time_limit.Name = "editText_abs_heating_time_limit";
            this.editText_abs_heating_time_limit.Size = new System.Drawing.Size(166, 39);
            this.editText_abs_heating_time_limit.TabIndex = 32;
            this.editText_abs_heating_time_limit.Value = null;
            // 
            // editText_heating_time_limit
            // 
            this.editText_heating_time_limit.AutoSize = true;
            this.editText_heating_time_limit.Caption = "Heating Time Limit";
            this.editText_heating_time_limit.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.editText_heating_time_limit.Location = new System.Drawing.Point(5, 201);
            this.editText_heating_time_limit.Margin = new System.Windows.Forms.Padding(5);
            this.editText_heating_time_limit.Name = "editText_heating_time_limit";
            this.editText_heating_time_limit.Size = new System.Drawing.Size(166, 39);
            this.editText_heating_time_limit.TabIndex = 33;
            this.editText_heating_time_limit.Value = null;
            // 
            // editText_light_brightness
            // 
            this.editText_light_brightness.AutoSize = true;
            this.editText_light_brightness.Caption = "Light Brightness";
            this.editText_light_brightness.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.editText_light_brightness.Location = new System.Drawing.Point(5, 250);
            this.editText_light_brightness.Margin = new System.Windows.Forms.Padding(5);
            this.editText_light_brightness.Name = "editText_light_brightness";
            this.editText_light_brightness.Size = new System.Drawing.Size(166, 39);
            this.editText_light_brightness.TabIndex = 34;
            this.editText_light_brightness.Value = null;
            // 
            // editText_wifi_power
            // 
            this.editText_wifi_power.AutoSize = true;
            this.editText_wifi_power.Caption = "Wi–Fi Power";
            this.editText_wifi_power.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.editText_wifi_power.Location = new System.Drawing.Point(5, 299);
            this.editText_wifi_power.Margin = new System.Windows.Forms.Padding(5);
            this.editText_wifi_power.Name = "editText_wifi_power";
            this.editText_wifi_power.Size = new System.Drawing.Size(166, 39);
            this.editText_wifi_power.TabIndex = 35;
            this.editText_wifi_power.Value = null;
            // 
            // editText_button_time
            // 
            this.editText_button_time.AutoSize = true;
            this.editText_button_time.Caption = "Button Time";
            this.editText_button_time.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.editText_button_time.Location = new System.Drawing.Point(5, 348);
            this.editText_button_time.Margin = new System.Windows.Forms.Padding(5);
            this.editText_button_time.Name = "editText_button_time";
            this.editText_button_time.Size = new System.Drawing.Size(166, 39);
            this.editText_button_time.TabIndex = 36;
            this.editText_button_time.Value = null;
            // 
            // button_save_properties
            // 
            this.button_save_properties.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.button_save_properties.Location = new System.Drawing.Point(87, 454);
            this.button_save_properties.Name = "button_save_properties";
            this.button_save_properties.Size = new System.Drawing.Size(75, 23);
            this.button_save_properties.TabIndex = 46;
            this.button_save_properties.Text = "Записать";
            this.button_save_properties.UseVisualStyleBackColor = true;
            this.button_save_properties.Click += new System.EventHandler(this.Button_Save_Properties_Click);
            // 
            // buttonLoadProps
            // 
            this.buttonLoadProps.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonLoadProps.Location = new System.Drawing.Point(6, 454);
            this.buttonLoadProps.Name = "buttonLoadProps";
            this.buttonLoadProps.Size = new System.Drawing.Size(75, 23);
            this.buttonLoadProps.TabIndex = 45;
            this.buttonLoadProps.Text = "Загрузить";
            this.buttonLoadProps.UseVisualStyleBackColor = true;
            this.buttonLoadProps.Click += new System.EventHandler(this.Button_Load_Properties_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.AutoSize = true;
            this.groupBox1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.groupBox1.Controls.Add(this.panel_properties_custom);
            this.groupBox1.Location = new System.Drawing.Point(223, 19);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(210, 284);
            this.groupBox1.TabIndex = 12;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Custom";
            // 
            // panel_properties_custom
            // 
            this.panel_properties_custom.AutoSize = true;
            this.panel_properties_custom.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.panel_properties_custom.Controls.Add(this.editText_WL_measure_interval);
            this.panel_properties_custom.Controls.Add(this.editText_wl_ring_buffer_size);
            this.panel_properties_custom.Controls.Add(this.editText_wl_cut_off_percent);
            this.panel_properties_custom.Controls.Add(this.editText_internal_temp_buf_size);
            this.panel_properties_custom.Controls.Add(this.editText_wl_usec_per_deg);
            this.panel_properties_custom.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.panel_properties_custom.Location = new System.Drawing.Point(6, 19);
            this.panel_properties_custom.Name = "panel_properties_custom";
            this.panel_properties_custom.Size = new System.Drawing.Size(198, 245);
            this.panel_properties_custom.TabIndex = 15;
            // 
            // editText_WL_measure_interval
            // 
            this.editText_WL_measure_interval.AutoSize = true;
            this.editText_WL_measure_interval.Caption = "WaterLevel Measure Interval";
            this.editText_WL_measure_interval.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.editText_WL_measure_interval.Location = new System.Drawing.Point(5, 5);
            this.editText_WL_measure_interval.Margin = new System.Windows.Forms.Padding(5);
            this.editText_WL_measure_interval.Name = "editText_WL_measure_interval";
            this.editText_WL_measure_interval.Size = new System.Drawing.Size(188, 39);
            this.editText_WL_measure_interval.TabIndex = 36;
            this.editText_WL_measure_interval.Value = null;
            // 
            // editText_wl_ring_buffer_size
            // 
            this.editText_wl_ring_buffer_size.AutoSize = true;
            this.editText_wl_ring_buffer_size.Caption = "WaterLevel Ring Buffer Size";
            this.editText_wl_ring_buffer_size.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.editText_wl_ring_buffer_size.Location = new System.Drawing.Point(5, 54);
            this.editText_wl_ring_buffer_size.Margin = new System.Windows.Forms.Padding(5);
            this.editText_wl_ring_buffer_size.Name = "editText_wl_ring_buffer_size";
            this.editText_wl_ring_buffer_size.Size = new System.Drawing.Size(188, 39);
            this.editText_wl_ring_buffer_size.TabIndex = 37;
            this.editText_wl_ring_buffer_size.Value = null;
            // 
            // editText_wl_cut_off_percent
            // 
            this.editText_wl_cut_off_percent.AutoSize = true;
            this.editText_wl_cut_off_percent.Caption = "WaterValve Cut Off Percent";
            this.editText_wl_cut_off_percent.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.editText_wl_cut_off_percent.Location = new System.Drawing.Point(5, 103);
            this.editText_wl_cut_off_percent.Margin = new System.Windows.Forms.Padding(5);
            this.editText_wl_cut_off_percent.Name = "editText_wl_cut_off_percent";
            this.editText_wl_cut_off_percent.Size = new System.Drawing.Size(188, 39);
            this.editText_wl_cut_off_percent.TabIndex = 39;
            this.editText_wl_cut_off_percent.Value = null;
            // 
            // editText_internal_temp_buf_size
            // 
            this.editText_internal_temp_buf_size.AutoSize = true;
            this.editText_internal_temp_buf_size.Caption = "TempSensor InternalTemp Buffer Size";
            this.editText_internal_temp_buf_size.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.editText_internal_temp_buf_size.Location = new System.Drawing.Point(5, 152);
            this.editText_internal_temp_buf_size.Margin = new System.Windows.Forms.Padding(5);
            this.editText_internal_temp_buf_size.Name = "editText_internal_temp_buf_size";
            this.editText_internal_temp_buf_size.Size = new System.Drawing.Size(188, 39);
            this.editText_internal_temp_buf_size.TabIndex = 41;
            this.editText_internal_temp_buf_size.Value = null;
            // 
            // editText_wl_usec_per_deg
            // 
            this.editText_wl_usec_per_deg.AutoSize = true;
            this.editText_wl_usec_per_deg.Caption = "WaterLevel Usec Per Deg";
            this.editText_wl_usec_per_deg.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.editText_wl_usec_per_deg.Location = new System.Drawing.Point(5, 201);
            this.editText_wl_usec_per_deg.Margin = new System.Windows.Forms.Padding(5);
            this.editText_wl_usec_per_deg.Name = "editText_wl_usec_per_deg";
            this.editText_wl_usec_per_deg.Size = new System.Drawing.Size(188, 39);
            this.editText_wl_usec_per_deg.TabIndex = 42;
            this.editText_wl_usec_per_deg.Value = null;
            // 
            // tabPageDebug
            // 
            this.tabPageDebug.Controls.Add(this.groupBox2);
            this.tabPageDebug.Controls.Add(this.button10);
            this.tabPageDebug.Controls.Add(this.button7);
            this.tabPageDebug.Location = new System.Drawing.Point(4, 22);
            this.tabPageDebug.Name = "tabPageDebug";
            this.tabPageDebug.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageDebug.Size = new System.Drawing.Size(1210, 592);
            this.tabPageDebug.TabIndex = 3;
            this.tabPageDebug.Text = "Отладка";
            this.tabPageDebug.UseVisualStyleBackColor = true;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.button_wifi_def);
            this.groupBox2.Controls.Add(this.button_wifi_cur);
            this.groupBox2.Controls.Add(this.textBox3);
            this.groupBox2.Controls.Add(this.textBox2);
            this.groupBox2.Controls.Add(this.textBox1);
            this.groupBox2.Location = new System.Drawing.Point(301, 34);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(275, 167);
            this.groupBox2.TabIndex = 2;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Wi-Fi";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(62, 75);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(29, 13);
            this.label5.TabIndex = 7;
            this.label5.Text = "MAC";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(39, 49);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(53, 13);
            this.label4.TabIndex = 6;
            this.label4.Text = "Password";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(60, 23);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(30, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "SSID";
            // 
            // button_wifi_def
            // 
            this.button_wifi_def.Location = new System.Drawing.Point(123, 112);
            this.button_wifi_def.Name = "button_wifi_def";
            this.button_wifi_def.Size = new System.Drawing.Size(75, 23);
            this.button_wifi_def.TabIndex = 4;
            this.button_wifi_def.Text = "Def";
            this.button_wifi_def.UseVisualStyleBackColor = true;
            this.button_wifi_def.Click += new System.EventHandler(this.Button_wifi_SetDefault_Click);
            // 
            // button_wifi_cur
            // 
            this.button_wifi_cur.Location = new System.Drawing.Point(42, 112);
            this.button_wifi_cur.Name = "button_wifi_cur";
            this.button_wifi_cur.Size = new System.Drawing.Size(75, 23);
            this.button_wifi_cur.TabIndex = 3;
            this.button_wifi_cur.Text = "Cur";
            this.button_wifi_cur.UseVisualStyleBackColor = true;
            this.button_wifi_cur.Click += new System.EventHandler(this.Button_wifi_SetCurrent_Click);
            // 
            // textBox3
            // 
            this.textBox3.Location = new System.Drawing.Point(98, 20);
            this.textBox3.Name = "textBox3";
            this.textBox3.Size = new System.Drawing.Size(121, 21);
            this.textBox3.TabIndex = 2;
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(98, 72);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(121, 21);
            this.textBox2.TabIndex = 1;
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(98, 46);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(121, 21);
            this.textBox1.TabIndex = 0;
            // 
            // button10
            // 
            this.button10.Location = new System.Drawing.Point(158, 124);
            this.button10.Name = "button10";
            this.button10.Size = new System.Drawing.Size(75, 23);
            this.button10.TabIndex = 1;
            this.button10.Text = "Отмена";
            this.button10.UseVisualStyleBackColor = true;
            this.button10.Click += new System.EventHandler(this.Button10_Click);
            // 
            // button7
            // 
            this.button7.Location = new System.Drawing.Point(158, 75);
            this.button7.Name = "button7";
            this.button7.Size = new System.Drawing.Size(75, 23);
            this.button7.TabIndex = 0;
            this.button7.Text = "Ping";
            this.button7.UseVisualStyleBackColor = true;
            this.button7.Click += new System.EventHandler(this.Button7_Click);
            // 
            // Температуры
            // 
            this.Температуры.Controls.Add(this.button2);
            this.Температуры.Location = new System.Drawing.Point(4, 22);
            this.Температуры.Name = "Температуры";
            this.Температуры.Padding = new System.Windows.Forms.Padding(3);
            this.Температуры.Size = new System.Drawing.Size(1210, 592);
            this.Температуры.TabIndex = 5;
            this.Температуры.Text = "tabPage6";
            this.Температуры.UseVisualStyleBackColor = true;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(408, 191);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 0;
            this.button2.Text = "button2";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.Button2_Click);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.командаToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1218, 24);
            this.menuStrip1.TabIndex = 15;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // командаToolStripMenuItem
            // 
            this.командаToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.resetToolStripMenuItem,
            this.pnigToolStripMenuItem});
            this.командаToolStripMenuItem.Name = "командаToolStripMenuItem";
            this.командаToolStripMenuItem.Size = new System.Drawing.Size(67, 20);
            this.командаToolStripMenuItem.Text = "Команда";
            // 
            // resetToolStripMenuItem
            // 
            this.resetToolStripMenuItem.Name = "resetToolStripMenuItem";
            this.resetToolStripMenuItem.Size = new System.Drawing.Size(138, 22);
            this.resetToolStripMenuItem.Text = "Перезапуск";
            this.resetToolStripMenuItem.Click += new System.EventHandler(this.ResetToolStripMenuItem_Reset_Click);
            // 
            // pnigToolStripMenuItem
            // 
            this.pnigToolStripMenuItem.Name = "pnigToolStripMenuItem";
            this.pnigToolStripMenuItem.Size = new System.Drawing.Size(138, 22);
            this.pnigToolStripMenuItem.Text = "Ping";
            this.pnigToolStripMenuItem.Click += new System.EventHandler(this.PingToolStripMenuItem_Click);
            // 
            // errorProvider1
            // 
            this.errorProvider1.ContainerControl = this;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1218, 642);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.menuStrip1);
            this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.IsMdiContainer = true;
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Shower";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.tabControl1.ResumeLayout(false);
            this.tabPageTemp.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(swiftPlotDiagram1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(swiftPlotSeriesView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(series1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(swiftPlotSeriesView2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(series2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(swiftPlotSeriesView3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(series3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(swiftPlotSeriesView4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chartControl_temperature)).EndInit();
            this.tabPage_wl.ResumeLayout(false);
            this.panel4.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(swiftPlotDiagram2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(swiftPlotSeriesView5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(series4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chartControl_water_level)).EndInit();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar_avg)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar_median)).EndInit();
            this.tabPageCalibration.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericWaterLevelCalibInterval)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(swiftPlotDiagram3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(swiftPlotSeriesView6)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(series5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chartControl_wl_calibration)).EndInit();
            this.tabPageSettings.ResumeLayout(false);
            this.groupBox_properties.ResumeLayout(false);
            this.groupBox_properties.PerformLayout();
            this.panel_properties.ResumeLayout(false);
            this.panel_properties.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.panel_properties_custom.ResumeLayout(false);
            this.panel_properties_custom.PerformLayout();
            this.tabPageDebug.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.Температуры.ResumeLayout(false);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPageTemp;
        private System.Windows.Forms.Label labelTimeLeft;
        private System.Windows.Forms.TrackBar trackBar1;
        private System.Windows.Forms.Button button6;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button_temp_stop;
        private System.Windows.Forms.Button buttonTempStartRecord;
        private DevExpress.XtraCharts.ChartControl chartControl_temperature;
        private System.Windows.Forms.TabPage tabPage_wl;
        private System.Windows.Forms.TabPage tabPageSettings;
        private System.Windows.Forms.GroupBox groupBox_properties;
        private System.Windows.Forms.Button button_save_properties;
        private System.Windows.Forms.Button buttonLoadProps;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem командаToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem resetToolStripMenuItem;
        private System.Windows.Forms.ErrorProvider errorProvider1;
        private System.Windows.Forms.Panel panel2;
        private EditText editText_wl_full;
        private System.Windows.Forms.FlowLayoutPanel panel_properties;
        private EditText editText_wl_empty;
        private EditText editText_min_water_heating_percent;
        private EditText editText_abs_heating_time_limit;
        private EditText editText_heating_time_limit;
        private EditText editText_light_brightness;
        private EditText editText_wifi_power;
        private EditText editText_WL_measure_interval;
        private EditText editText_wl_ring_buffer_size;
        private System.Windows.Forms.FlowLayoutPanel panel_properties_custom;
        private EditText editText_wl_cut_off_percent;
        private EditText editText_internal_temp_buf_size;
        private EditText editText_wl_usec_per_deg;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBox_max_water_level;
        private System.Windows.Forms.CheckBox checkBox4;
        private System.Windows.Forms.Button button_wl;
        private System.Windows.Forms.CheckBox checkBox3;
        private System.Windows.Forms.Button button_wl_stop;
        private System.Windows.Forms.TextBox textBox_min_water_level;
        private System.Windows.Forms.Button button_clear_wl;
        private System.Windows.Forms.Label label_reconnect_count;
        private System.Windows.Forms.Label labelTempReconnect;
        private System.Windows.Forms.ToolStripMenuItem pnigToolStripMenuItem;
        private System.Windows.Forms.TabPage tabPageDebug;
        private System.Windows.Forms.Button button10;
        private System.Windows.Forms.Button button7;
        private System.Windows.Forms.Button button_load_props_canc;
        private System.Windows.Forms.CheckBox checkBox_iwd;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button button_wifi_def;
        private System.Windows.Forms.Button button_wifi_cur;
        private System.Windows.Forms.TextBox textBox3;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.TabPage tabPageCalibration;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button buttonStartCalib;
        private DevExpress.XtraCharts.ChartControl chartControl_wl_calibration;
        private System.Windows.Forms.Button button_stop;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.NumericUpDown numericWaterLevelCalibInterval;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Panel panel3;
        private EditText editText_button_time;
        private System.Windows.Forms.TabPage Температуры;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Panel panel4;
        private DevExpress.XtraCharts.ChartControl chartControl_water_level;
        private System.Windows.Forms.CheckBox checkBox_median;
        private System.Windows.Forms.Button button8;
        private System.Windows.Forms.TrackBar trackBar_median;
        private System.Windows.Forms.TrackBar trackBar_avg;
        private System.Windows.Forms.CheckBox checkBox_avg;
        private System.Windows.Forms.Button button9;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.Label labelTempReconnectCount;
    }
}