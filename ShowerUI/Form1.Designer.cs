using DevExpress.XtraCharts;

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
            components = new System.ComponentModel.Container();
            var swiftPlotDiagram1 = new DevExpress.XtraCharts.SwiftPlotDiagram();
            Series series1 = new DevExpress.XtraCharts.Series();
            var swiftPlotSeriesView1 = new DevExpress.XtraCharts.SwiftPlotSeriesView();
            var resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            tabControl1 = new TabControl();
            tabPageTemp = new TabPage();
            temperatureMonitor1 = new UserControls.TemperatureMonitor();
            Температуры = new TabPage();
            button2 = new Button();
            tabPage_wl = new TabPage();
            waterLevel1 = new UserControls.WaterLevel();
            tabPageCalibration = new TabPage();
            panel2 = new Panel();
            chartControl_wl_calibration = new DevExpress.XtraCharts.ChartControl();
            panel1 = new Panel();
            label_elapsedWL = new Label();
            label1 = new Label();
            label6 = new Label();
            numericWaterLevelCalibInterval = new NumericUpDown();
            button_stop = new Button();
            buttonStartCalib = new Button();
            tabPageSettings = new TabPage();
            parametersControl1 = new UserControls.ParametersControl();
            tabPageDebug = new TabPage();
            groupBox2 = new GroupBox();
            label5 = new Label();
            label4 = new Label();
            label3 = new Label();
            button_wifi_def = new Button();
            button_wifi_cur = new Button();
            textBox_ssid = new TextBox();
            textBox_bsid = new TextBox();
            textBox_ap_pass = new TextBox();
            button10 = new Button();
            button7 = new Button();
            menuStrip1 = new MenuStrip();
            командаToolStripMenuItem = new ToolStripMenuItem();
            resetToolStripMenuItem = new ToolStripMenuItem();
            pnigToolStripMenuItem = new ToolStripMenuItem();
            errorProvider1 = new ErrorProvider(components);
            toolTip1 = new ToolTip(components);
            tabControl1.SuspendLayout();
            tabPageTemp.SuspendLayout();
            Температуры.SuspendLayout();
            tabPage_wl.SuspendLayout();
            tabPageCalibration.SuspendLayout();
            panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)chartControl_wl_calibration).BeginInit();
            panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)numericWaterLevelCalibInterval).BeginInit();
            tabPageSettings.SuspendLayout();
            tabPageDebug.SuspendLayout();
            groupBox2.SuspendLayout();
            menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)errorProvider1).BeginInit();
            SuspendLayout();
            // 
            // tabControl1
            // 
            tabControl1.Controls.Add(tabPageTemp);
            tabControl1.Controls.Add(Температуры);
            tabControl1.Controls.Add(tabPage_wl);
            tabControl1.Controls.Add(tabPageCalibration);
            tabControl1.Controls.Add(tabPageSettings);
            tabControl1.Controls.Add(tabPageDebug);
            tabControl1.Dock = DockStyle.Fill;
            tabControl1.Location = new Point(0, 24);
            tabControl1.Margin = new Padding(3, 0, 3, 3);
            tabControl1.Name = "tabControl1";
            tabControl1.SelectedIndex = 0;
            tabControl1.Size = new Size(1348, 712);
            tabControl1.TabIndex = 14;
            tabControl1.Selected += TabControl1_Selected;
            // 
            // tabPageTemp
            // 
            tabPageTemp.BackColor = Color.Transparent;
            tabPageTemp.Controls.Add(temperatureMonitor1);
            tabPageTemp.Location = new Point(4, 22);
            tabPageTemp.Name = "tabPageTemp";
            tabPageTemp.Padding = new Padding(3);
            tabPageTemp.Size = new Size(1340, 686);
            tabPageTemp.TabIndex = 0;
            tabPageTemp.Text = "Температура";
            // 
            // temperatureMonitor1
            // 
            temperatureMonitor1.Dock = DockStyle.Fill;
            temperatureMonitor1.Location = new Point(3, 3);
            temperatureMonitor1.Margin = new Padding(4, 3, 4, 3);
            temperatureMonitor1.Name = "temperatureMonitor1";
            temperatureMonitor1.Size = new Size(1334, 680);
            temperatureMonitor1.TabIndex = 0;
            // 
            // Температуры
            // 
            Температуры.BackColor = SystemColors.Control;
            Температуры.Controls.Add(button2);
            Температуры.Location = new Point(4, 24);
            Температуры.Name = "Температуры";
            Температуры.Padding = new Padding(3);
            Температуры.Size = new Size(1340, 684);
            Температуры.TabIndex = 5;
            Температуры.Text = "Температуры";
            // 
            // button2
            // 
            button2.Location = new Point(408, 191);
            button2.Name = "button2";
            button2.Size = new Size(188, 23);
            button2.TabIndex = 0;
            button2.Text = "Загрузить таблицу температур";
            button2.UseVisualStyleBackColor = true;
            button2.Click += Button2_Click;
            // 
            // tabPage_wl
            // 
            tabPage_wl.BackColor = SystemColors.Control;
            tabPage_wl.Controls.Add(waterLevel1);
            tabPage_wl.Location = new Point(4, 24);
            tabPage_wl.Name = "tabPage_wl";
            tabPage_wl.Padding = new Padding(3);
            tabPage_wl.Size = new Size(1340, 684);
            tabPage_wl.TabIndex = 1;
            tabPage_wl.Text = "Уровень воды";
            // 
            // waterLevel1
            // 
            waterLevel1.Dock = DockStyle.Fill;
            waterLevel1.Location = new Point(3, 3);
            waterLevel1.Margin = new Padding(4, 3, 4, 3);
            waterLevel1.Name = "waterLevel1";
            waterLevel1.Size = new Size(1334, 678);
            waterLevel1.TabIndex = 17;
            // 
            // tabPageCalibration
            // 
            tabPageCalibration.BackColor = SystemColors.Control;
            tabPageCalibration.Controls.Add(panel2);
            tabPageCalibration.Controls.Add(panel1);
            tabPageCalibration.Location = new Point(4, 24);
            tabPageCalibration.Name = "tabPageCalibration";
            tabPageCalibration.Padding = new Padding(3);
            tabPageCalibration.Size = new Size(1340, 684);
            tabPageCalibration.TabIndex = 4;
            tabPageCalibration.Text = "Калибровка уровня";
            // 
            // panel2
            // 
            panel2.Controls.Add(chartControl_wl_calibration);
            panel2.Dock = DockStyle.Fill;
            panel2.Location = new Point(3, 3);
            panel2.Name = "panel2";
            panel2.Size = new Size(1334, 646);
            panel2.TabIndex = 2;
            // 
            // chartControl_wl_calibration
            // 
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
            chartControl_wl_calibration.Diagram = swiftPlotDiagram1;
            chartControl_wl_calibration.Dock = DockStyle.Fill;
            chartControl_wl_calibration.Legend.LegendID = -1;
            chartControl_wl_calibration.Legend.Name = "Default Legend";
            chartControl_wl_calibration.Legend.Visibility = DevExpress.Utils.DefaultBoolean.False;
            chartControl_wl_calibration.Location = new Point(0, 0);
            chartControl_wl_calibration.Name = "chartControl_wl_calibration";
            series1.Name = "usec";
            swiftPlotSeriesView1.Color = Color.FromArgb(240, 0, 0);
            swiftPlotSeriesView1.LineStyle.Thickness = 2;
            series1.View = swiftPlotSeriesView1;
            chartControl_wl_calibration.SeriesSerializable = new DevExpress.XtraCharts.Series[]
    {
    series1
    };
            chartControl_wl_calibration.Size = new Size(1334, 646);
            chartControl_wl_calibration.TabIndex = 0;
            // 
            // panel1
            // 
            panel1.Controls.Add(label_elapsedWL);
            panel1.Controls.Add(label1);
            panel1.Controls.Add(label6);
            panel1.Controls.Add(numericWaterLevelCalibInterval);
            panel1.Controls.Add(button_stop);
            panel1.Controls.Add(buttonStartCalib);
            panel1.Dock = DockStyle.Bottom;
            panel1.Location = new Point(3, 649);
            panel1.Name = "panel1";
            panel1.Size = new Size(1334, 32);
            panel1.TabIndex = 1;
            // 
            // label_elapsedWL
            // 
            label_elapsedWL.AutoSize = true;
            label_elapsedWL.Location = new Point(519, 8);
            label_elapsedWL.Name = "label_elapsedWL";
            label_elapsedWL.Size = new Size(19, 13);
            label_elapsedWL.TabIndex = 5;
            label_elapsedWL.Text = "__";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(425, 8);
            label1.Name = "label1";
            label1.Size = new Size(88, 13);
            label1.TabIndex = 4;
            label1.Text = "Среднее время:";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(243, 8);
            label6.Name = "label6";
            label6.Size = new Size(95, 13);
            label6.TabIndex = 3;
            label6.Text = "Интервал (msec):";
            // 
            // numericWaterLevelCalibInterval
            // 
            numericWaterLevelCalibInterval.Increment = new decimal(new int[] { 10, 0, 0, 0 });
            numericWaterLevelCalibInterval.Location = new Point(344, 6);
            numericWaterLevelCalibInterval.Maximum = new decimal(new int[] { 200, 0, 0, 0 });
            numericWaterLevelCalibInterval.Name = "numericWaterLevelCalibInterval";
            numericWaterLevelCalibInterval.Size = new Size(66, 21);
            numericWaterLevelCalibInterval.TabIndex = 2;
            numericWaterLevelCalibInterval.Value = new decimal(new int[] { 100, 0, 0, 0 });
            // 
            // button_stop
            // 
            button_stop.Enabled = false;
            button_stop.Location = new Point(119, 3);
            button_stop.Name = "button_stop";
            button_stop.Size = new Size(75, 23);
            button_stop.TabIndex = 1;
            button_stop.Text = "Стоп";
            button_stop.UseVisualStyleBackColor = true;
            button_stop.Click += Button_stop_Click;
            // 
            // buttonStartCalib
            // 
            buttonStartCalib.Location = new Point(5, 3);
            buttonStartCalib.Name = "buttonStartCalib";
            buttonStartCalib.Size = new Size(75, 23);
            buttonStartCalib.TabIndex = 0;
            buttonStartCalib.Text = "Старт";
            buttonStartCalib.UseVisualStyleBackColor = true;
            buttonStartCalib.Click += ButtonStartCalibrationClick;
            // 
            // tabPageSettings
            // 
            tabPageSettings.BackColor = SystemColors.Control;
            tabPageSettings.Controls.Add(parametersControl1);
            tabPageSettings.Location = new Point(4, 22);
            tabPageSettings.Name = "tabPageSettings";
            tabPageSettings.Padding = new Padding(3);
            tabPageSettings.Size = new Size(1340, 686);
            tabPageSettings.TabIndex = 2;
            tabPageSettings.Text = "Параметры";
            // 
            // parametersControl1
            // 
            parametersControl1.Dock = DockStyle.Fill;
            parametersControl1.Location = new Point(3, 3);
            parametersControl1.Margin = new Padding(4, 3, 4, 3);
            parametersControl1.Name = "parametersControl1";
            parametersControl1.Size = new Size(1334, 680);
            parametersControl1.TabIndex = 0;
            // 
            // tabPageDebug
            // 
            tabPageDebug.BackColor = SystemColors.Control;
            tabPageDebug.Controls.Add(groupBox2);
            tabPageDebug.Controls.Add(button10);
            tabPageDebug.Controls.Add(button7);
            tabPageDebug.Location = new Point(4, 22);
            tabPageDebug.Name = "tabPageDebug";
            tabPageDebug.Padding = new Padding(3);
            tabPageDebug.Size = new Size(1340, 686);
            tabPageDebug.TabIndex = 3;
            tabPageDebug.Text = "Отладка";
            // 
            // groupBox2
            // 
            groupBox2.Controls.Add(label5);
            groupBox2.Controls.Add(label4);
            groupBox2.Controls.Add(label3);
            groupBox2.Controls.Add(button_wifi_def);
            groupBox2.Controls.Add(button_wifi_cur);
            groupBox2.Controls.Add(textBox_ssid);
            groupBox2.Controls.Add(textBox_bsid);
            groupBox2.Controls.Add(textBox_ap_pass);
            groupBox2.Location = new Point(301, 34);
            groupBox2.Name = "groupBox2";
            groupBox2.Size = new Size(275, 167);
            groupBox2.TabIndex = 2;
            groupBox2.TabStop = false;
            groupBox2.Text = "Wi-Fi";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(62, 75);
            label5.Name = "label5";
            label5.Size = new Size(29, 13);
            label5.TabIndex = 7;
            label5.Text = "MAC";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(39, 49);
            label4.Name = "label4";
            label4.Size = new Size(53, 13);
            label4.TabIndex = 6;
            label4.Text = "Password";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(60, 23);
            label3.Name = "label3";
            label3.Size = new Size(30, 13);
            label3.TabIndex = 5;
            label3.Text = "SSID";
            // 
            // button_wifi_def
            // 
            button_wifi_def.Location = new Point(123, 112);
            button_wifi_def.Name = "button_wifi_def";
            button_wifi_def.Size = new Size(75, 23);
            button_wifi_def.TabIndex = 6;
            button_wifi_def.Text = "Def";
            toolTip1.SetToolTip(button_wifi_def, "Подключение к точке доступа, внесенные данные сохраняются как данные по умолчанию.");
            button_wifi_def.UseVisualStyleBackColor = true;
            button_wifi_def.Click += Button_wifi_SetDefault_Click;
            // 
            // button_wifi_cur
            // 
            button_wifi_cur.Location = new Point(42, 112);
            button_wifi_cur.Name = "button_wifi_cur";
            button_wifi_cur.Size = new Size(75, 23);
            button_wifi_cur.TabIndex = 5;
            button_wifi_cur.Text = "Cur";
            toolTip1.SetToolTip(button_wifi_cur, "Подключение к точке доступа в текущей сессии.");
            button_wifi_cur.UseVisualStyleBackColor = true;
            button_wifi_cur.Click += Button_wifi_SetCurrent_Click;
            // 
            // textBox_ssid
            // 
            textBox_ssid.Location = new Point(98, 20);
            textBox_ssid.Name = "textBox_ssid";
            textBox_ssid.Size = new Size(121, 21);
            textBox_ssid.TabIndex = 2;
            // 
            // textBox_bsid
            // 
            textBox_bsid.Location = new Point(98, 72);
            textBox_bsid.Name = "textBox_bsid";
            textBox_bsid.Size = new Size(121, 21);
            textBox_bsid.TabIndex = 4;
            // 
            // textBox_ap_pass
            // 
            textBox_ap_pass.Location = new Point(98, 46);
            textBox_ap_pass.Name = "textBox_ap_pass";
            textBox_ap_pass.Size = new Size(121, 21);
            textBox_ap_pass.TabIndex = 3;
            // 
            // button10
            // 
            button10.Location = new Point(158, 109);
            button10.Name = "button10";
            button10.Size = new Size(75, 23);
            button10.TabIndex = 1;
            button10.Text = "Отмена";
            button10.UseVisualStyleBackColor = true;
            button10.Click += Button_PingCancel_Click;
            // 
            // button7
            // 
            button7.Location = new Point(158, 75);
            button7.Name = "button7";
            button7.Size = new Size(75, 23);
            button7.TabIndex = 0;
            button7.Text = "Ping";
            button7.UseVisualStyleBackColor = true;
            button7.Click += Button7_Click;
            // 
            // menuStrip1
            // 
            menuStrip1.Items.AddRange(new ToolStripItem[] { командаToolStripMenuItem });
            menuStrip1.Location = new Point(0, 0);
            menuStrip1.Name = "menuStrip1";
            menuStrip1.Size = new Size(1348, 24);
            menuStrip1.TabIndex = 15;
            menuStrip1.Text = "menuStrip1";
            // 
            // командаToolStripMenuItem
            // 
            командаToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { resetToolStripMenuItem, pnigToolStripMenuItem });
            командаToolStripMenuItem.Name = "командаToolStripMenuItem";
            командаToolStripMenuItem.Size = new Size(67, 20);
            командаToolStripMenuItem.Text = "Команда";
            // 
            // resetToolStripMenuItem
            // 
            resetToolStripMenuItem.Name = "resetToolStripMenuItem";
            resetToolStripMenuItem.Size = new Size(138, 22);
            resetToolStripMenuItem.Text = "Перезапуск";
            resetToolStripMenuItem.Click += ResetToolStripMenuItem_Reset_Click;
            // 
            // pnigToolStripMenuItem
            // 
            pnigToolStripMenuItem.Name = "pnigToolStripMenuItem";
            pnigToolStripMenuItem.Size = new Size(138, 22);
            pnigToolStripMenuItem.Text = "Ping";
            pnigToolStripMenuItem.Click += PingToolStripMenuItem_Click;
            // 
            // errorProvider1
            // 
            errorProvider1.ContainerControl = this;
            // 
            // toolTip1
            // 
            toolTip1.AutoPopDelay = 3000;
            toolTip1.InitialDelay = 500;
            toolTip1.ReshowDelay = 100;
            toolTip1.ShowAlways = true;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(6F, 13F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1348, 736);
            Controls.Add(tabControl1);
            Controls.Add(menuStrip1);
            Font = new Font("Tahoma", 8.25F, FontStyle.Regular, GraphicsUnit.Point, 204);
            Icon = (Icon)resources.GetObject("$this.Icon");
            IsMdiContainer = true;
            MainMenuStrip = menuStrip1;
            Name = "Form1";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Shower";
            FormClosing += Form1_FormClosing;
            tabControl1.ResumeLayout(false);
            tabPageTemp.ResumeLayout(false);
            Температуры.ResumeLayout(false);
            tabPage_wl.ResumeLayout(false);
            tabPageCalibration.ResumeLayout(false);
            panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)chartControl_wl_calibration).EndInit();
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)numericWaterLevelCalibInterval).EndInit();
            tabPageSettings.ResumeLayout(false);
            tabPageDebug.ResumeLayout(false);
            groupBox2.ResumeLayout(false);
            groupBox2.PerformLayout();
            menuStrip1.ResumeLayout(false);
            menuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)errorProvider1).EndInit();
            ResumeLayout(false);
            PerformLayout();
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
        private System.Windows.Forms.TextBox textBox_ssid;
        private System.Windows.Forms.TextBox textBox_bsid;
        private System.Windows.Forms.TextBox textBox_ap_pass;
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
        private System.Windows.Forms.Panel panel2;
    }
}