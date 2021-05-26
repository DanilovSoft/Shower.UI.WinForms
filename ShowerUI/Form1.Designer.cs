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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPageTemp = new System.Windows.Forms.TabPage();
            this.temperatureMonitor1 = new ShowerUI.UserControls.TemperatureMonitor();
            this.Температуры = new System.Windows.Forms.TabPage();
            this.button2 = new System.Windows.Forms.Button();
            this.tabPage_wl = new System.Windows.Forms.TabPage();
            this.waterLevel1 = new ShowerUI.UserControls.WaterLevel();
            this.tabPageCalibration = new System.Windows.Forms.TabPage();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label_elapsedWL = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.numericWaterLevelCalibInterval = new System.Windows.Forms.NumericUpDown();
            this.button_stop = new System.Windows.Forms.Button();
            this.buttonStartCalib = new System.Windows.Forms.Button();
            this.chartControl_wl_calibration = new DevExpress.XtraCharts.ChartControl();
            this.tabPageSettings = new System.Windows.Forms.TabPage();
            this.parametersControl1 = new ShowerUI.UserControls.ParametersControl();
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
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.командаToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.resetToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pnigToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.tabControl1.SuspendLayout();
            this.tabPageTemp.SuspendLayout();
            this.Температуры.SuspendLayout();
            this.tabPage_wl.SuspendLayout();
            this.tabPageCalibration.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericWaterLevelCalibInterval)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chartControl_wl_calibration)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(swiftPlotDiagram1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(series1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(swiftPlotSeriesView1)).BeginInit();
            this.tabPageSettings.SuspendLayout();
            this.tabPageDebug.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).BeginInit();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPageTemp);
            this.tabControl1.Controls.Add(this.Температуры);
            this.tabControl1.Controls.Add(this.tabPage_wl);
            this.tabControl1.Controls.Add(this.tabPageCalibration);
            this.tabControl1.Controls.Add(this.tabPageSettings);
            this.tabControl1.Controls.Add(this.tabPageDebug);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 24);
            this.tabControl1.Margin = new System.Windows.Forms.Padding(3, 0, 3, 3);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(1348, 712);
            this.tabControl1.TabIndex = 14;
            this.tabControl1.Selected += new System.Windows.Forms.TabControlEventHandler(this.TabControl1_Selected);
            // 
            // tabPageTemp
            // 
            this.tabPageTemp.BackColor = System.Drawing.Color.Transparent;
            this.tabPageTemp.Controls.Add(this.temperatureMonitor1);
            this.tabPageTemp.Location = new System.Drawing.Point(4, 22);
            this.tabPageTemp.Name = "tabPageTemp";
            this.tabPageTemp.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageTemp.Size = new System.Drawing.Size(1340, 686);
            this.tabPageTemp.TabIndex = 0;
            this.tabPageTemp.Text = "Температура";
            // 
            // temperatureMonitor1
            // 
            this.temperatureMonitor1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.temperatureMonitor1.Location = new System.Drawing.Point(3, 3);
            this.temperatureMonitor1.Name = "temperatureMonitor1";
            this.temperatureMonitor1.Size = new System.Drawing.Size(1334, 680);
            this.temperatureMonitor1.TabIndex = 0;
            // 
            // Температуры
            // 
            this.Температуры.BackColor = System.Drawing.SystemColors.Control;
            this.Температуры.Controls.Add(this.button2);
            this.Температуры.Location = new System.Drawing.Point(4, 22);
            this.Температуры.Name = "Температуры";
            this.Температуры.Padding = new System.Windows.Forms.Padding(3);
            this.Температуры.Size = new System.Drawing.Size(1210, 592);
            this.Температуры.TabIndex = 5;
            this.Температуры.Text = "Температуры";
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(408, 191);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(188, 23);
            this.button2.TabIndex = 0;
            this.button2.Text = "Загрузить таблицу температур";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.Button2_Click);
            // 
            // tabPage_wl
            // 
            this.tabPage_wl.BackColor = System.Drawing.SystemColors.Control;
            this.tabPage_wl.Controls.Add(this.waterLevel1);
            this.tabPage_wl.Location = new System.Drawing.Point(4, 22);
            this.tabPage_wl.Name = "tabPage_wl";
            this.tabPage_wl.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage_wl.Size = new System.Drawing.Size(1210, 592);
            this.tabPage_wl.TabIndex = 1;
            this.tabPage_wl.Text = "Уровень воды";
            // 
            // waterLevel1
            // 
            this.waterLevel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.waterLevel1.Location = new System.Drawing.Point(3, 3);
            this.waterLevel1.Name = "waterLevel1";
            this.waterLevel1.Size = new System.Drawing.Size(1204, 586);
            this.waterLevel1.TabIndex = 17;
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
            this.panel1.Controls.Add(this.label_elapsedWL);
            this.panel1.Controls.Add(this.label1);
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
            // label_elapsedWL
            // 
            this.label_elapsedWL.AutoSize = true;
            this.label_elapsedWL.Location = new System.Drawing.Point(519, 8);
            this.label_elapsedWL.Name = "label_elapsedWL";
            this.label_elapsedWL.Size = new System.Drawing.Size(19, 13);
            this.label_elapsedWL.TabIndex = 5;
            this.label_elapsedWL.Text = "__";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(425, 8);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(88, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "Среднее время:";
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
            swiftPlotDiagram1.AxisX.Label.Visible = false;
            swiftPlotDiagram1.AxisX.Tickmarks.MinorVisible = false;
            swiftPlotDiagram1.AxisX.Tickmarks.Visible = false;
            swiftPlotDiagram1.AxisX.Visibility = DevExpress.Utils.DefaultBoolean.True;
            swiftPlotDiagram1.AxisX.VisibleInPanesSerializable = "-1";
            swiftPlotDiagram1.AxisX.VisualRange.Auto = false;
            swiftPlotDiagram1.AxisX.VisualRange.MaxValueSerializable = "10";
            swiftPlotDiagram1.AxisX.VisualRange.MinValueSerializable = "0";
            swiftPlotDiagram1.AxisX.WholeRange.Auto = false;
            swiftPlotDiagram1.AxisX.WholeRange.MaxValueSerializable = "10";
            swiftPlotDiagram1.AxisX.WholeRange.MinValueSerializable = "0";
            swiftPlotDiagram1.AxisY.VisibleInPanesSerializable = "-1";
            this.chartControl_wl_calibration.Diagram = swiftPlotDiagram1;
            this.chartControl_wl_calibration.Legend.Name = "Default Legend";
            this.chartControl_wl_calibration.Legend.Visibility = DevExpress.Utils.DefaultBoolean.False;
            this.chartControl_wl_calibration.Location = new System.Drawing.Point(8, 6);
            this.chartControl_wl_calibration.Name = "chartControl_wl_calibration";
            series1.Name = "usec";
            swiftPlotSeriesView1.Color = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            swiftPlotSeriesView1.LineStyle.Thickness = 2;
            series1.View = swiftPlotSeriesView1;
            this.chartControl_wl_calibration.SeriesSerializable = new DevExpress.XtraCharts.Series[] {
        series1};
            this.chartControl_wl_calibration.Size = new System.Drawing.Size(1194, 545);
            this.chartControl_wl_calibration.TabIndex = 0;
            // 
            // tabPageSettings
            // 
            this.tabPageSettings.BackColor = System.Drawing.SystemColors.Control;
            this.tabPageSettings.Controls.Add(this.parametersControl1);
            this.tabPageSettings.Location = new System.Drawing.Point(4, 22);
            this.tabPageSettings.Name = "tabPageSettings";
            this.tabPageSettings.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageSettings.Size = new System.Drawing.Size(1210, 592);
            this.tabPageSettings.TabIndex = 2;
            this.tabPageSettings.Text = "Параметры";
            // 
            // parametersControl1
            // 
            this.parametersControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.parametersControl1.Location = new System.Drawing.Point(3, 3);
            this.parametersControl1.Name = "parametersControl1";
            this.parametersControl1.Size = new System.Drawing.Size(1204, 586);
            this.parametersControl1.TabIndex = 0;
            // 
            // tabPageDebug
            // 
            this.tabPageDebug.BackColor = System.Drawing.SystemColors.Control;
            this.tabPageDebug.Controls.Add(this.groupBox2);
            this.tabPageDebug.Controls.Add(this.button10);
            this.tabPageDebug.Controls.Add(this.button7);
            this.tabPageDebug.Location = new System.Drawing.Point(4, 22);
            this.tabPageDebug.Name = "tabPageDebug";
            this.tabPageDebug.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageDebug.Size = new System.Drawing.Size(1210, 592);
            this.tabPageDebug.TabIndex = 3;
            this.tabPageDebug.Text = "Отладка";
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
            this.button10.Click += new System.EventHandler(this.Button_PingCancel_Click);
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
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.командаToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1348, 24);
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
            // toolTip1
            // 
            this.toolTip1.AutoPopDelay = 3000;
            this.toolTip1.InitialDelay = 500;
            this.toolTip1.ReshowDelay = 100;
            this.toolTip1.ShowAlways = true;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1348, 736);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.menuStrip1);
            this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.IsMdiContainer = true;
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Shower";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.tabControl1.ResumeLayout(false);
            this.tabPageTemp.ResumeLayout(false);
            this.Температуры.ResumeLayout(false);
            this.tabPage_wl.ResumeLayout(false);
            this.tabPageCalibration.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericWaterLevelCalibInterval)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(swiftPlotDiagram1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(swiftPlotSeriesView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(series1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chartControl_wl_calibration)).EndInit();
            this.tabPageSettings.ResumeLayout(false);
            this.tabPageDebug.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPageTemp;
        private System.Windows.Forms.TabPage tabPage_wl;
        private System.Windows.Forms.TabPage tabPageSettings;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem командаToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem resetToolStripMenuItem;
        private System.Windows.Forms.ErrorProvider errorProvider1;
        private System.Windows.Forms.ToolStripMenuItem pnigToolStripMenuItem;
        private System.Windows.Forms.TabPage tabPageDebug;
        private System.Windows.Forms.Button button10;
        private System.Windows.Forms.Button button7;
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
        private System.Windows.Forms.NumericUpDown numericWaterLevelCalibInterval;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TabPage Температуры;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.ToolTip toolTip1;
        private UserControls.WaterLevel waterLevel1;
        private UserControls.ParametersControl parametersControl1;
        private System.Windows.Forms.Label label_elapsedWL;
        private System.Windows.Forms.Label label1;
        private UserControls.TemperatureMonitor temperatureMonitor1;
    }
}