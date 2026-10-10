
using DevExpress.XtraCharts;

namespace ShowerUI.UserControls
{
    partial class WaterLevel
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
            components = new System.ComponentModel.Container();
            var swiftPlotDiagram1 = new DevExpress.XtraCharts.SwiftPlotDiagram();
            var swiftPlotDiagramSecondaryAxisy1 = new DevExpress.XtraCharts.SwiftPlotDiagramSecondaryAxisY();
            Series series1 = new DevExpress.XtraCharts.Series();
            var swiftPlotSeriesView1 = new DevExpress.XtraCharts.SwiftPlotSeriesView();
            panel_root = new Panel();
            panel_chart = new Panel();
            chartControl_water_level = new DevExpress.XtraCharts.ChartControl();
            panel_bottom = new Panel();
            label8 = new Label();
            button_loadFrom = new Button();
            checkBox_avg = new CheckBox();
            trackBar_avg = new TrackBar();
            trackBar_medianTrackBar = new TrackBar();
            button_saveAs = new Button();
            checkBox_median = new CheckBox();
            label2 = new Label();
            label_reconnect_count = new Label();
            textBox_max_water_level = new TextBox();
            label1 = new Label();
            checkBox_percent = new CheckBox();
            textBox_min_water_level = new TextBox();
            button_start = new Button();
            button_clear = new Button();
            checkBox_raw = new CheckBox();
            button_stop = new Button();
            errorProvider1 = new ErrorProvider(components);
            panel_root.SuspendLayout();
            panel_chart.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)chartControl_water_level).BeginInit();
            panel_bottom.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)trackBar_avg).BeginInit();
            ((System.ComponentModel.ISupportInitialize)trackBar_medianTrackBar).BeginInit();
            ((System.ComponentModel.ISupportInitialize)errorProvider1).BeginInit();
            SuspendLayout();
            // 
            // panel_root
            // 
            panel_root.Controls.Add(panel_chart);
            panel_root.Controls.Add(panel_bottom);
            panel_root.Dock = DockStyle.Fill;
            panel_root.Location = new Point(0, 0);
            panel_root.Margin = new Padding(4, 3, 4, 3);
            panel_root.Name = "panel_root";
            panel_root.Size = new Size(1429, 780);
            panel_root.TabIndex = 10;
            // 
            // panel_chart
            // 
            panel_chart.Controls.Add(chartControl_water_level);
            panel_chart.Dock = DockStyle.Fill;
            panel_chart.Location = new Point(0, 0);
            panel_chart.Margin = new Padding(4, 3, 4, 3);
            panel_chart.Name = "panel_chart";
            panel_chart.Size = new Size(1429, 654);
            panel_chart.TabIndex = 10;
            // 
            // chartControl_water_level
            // 
            swiftPlotDiagram1.AxisX.Visibility = DevExpress.Utils.DefaultBoolean.False;
            swiftPlotDiagram1.AxisX.VisibleInPanesSerializable = "-1";
            swiftPlotDiagram1.AxisY.GridLines.Visible = false;
            swiftPlotDiagram1.AxisY.Label.Visible = false;
            swiftPlotDiagram1.AxisY.Tickmarks.MinorVisible = false;
            swiftPlotDiagram1.AxisY.Tickmarks.Visible = false;
            swiftPlotDiagram1.AxisY.Visibility = DevExpress.Utils.DefaultBoolean.False;
            swiftPlotDiagram1.AxisY.VisibleInPanesSerializable = "-1";
            swiftPlotDiagram1.AxisY.VisualRange.Auto = false;
            swiftPlotDiagram1.AxisY.VisualRange.MaxValueSerializable = "9.5";
            swiftPlotDiagram1.AxisY.VisualRange.MinValueSerializable = "0";
            swiftPlotDiagram1.AxisY.WholeRange.Auto = false;
            swiftPlotDiagram1.AxisY.WholeRange.MaxValueSerializable = "9.5";
            swiftPlotDiagram1.AxisY.WholeRange.MinValueSerializable = "0";
            swiftPlotDiagram1.EnableAxisXScrolling = true;
            swiftPlotDiagram1.EnableAxisXZooming = true;
            swiftPlotDiagramSecondaryAxisy1.GridLines.Visible = true;
            swiftPlotDiagramSecondaryAxisy1.MinorCount = 1;
            swiftPlotDiagramSecondaryAxisy1.Name = "percent";
            swiftPlotDiagramSecondaryAxisy1.NumericScaleOptions.AutoGrid = false;
            swiftPlotDiagramSecondaryAxisy1.NumericScaleOptions.CustomGridAlignment = 2D;
            swiftPlotDiagramSecondaryAxisy1.NumericScaleOptions.GridAlignment = DevExpress.XtraCharts.NumericGridAlignment.Custom;
            swiftPlotDiagramSecondaryAxisy1.NumericScaleOptions.GridOffset = 2D;
            swiftPlotDiagramSecondaryAxisy1.NumericScaleOptions.GridSpacing = 2D;
            swiftPlotDiagramSecondaryAxisy1.Tickmarks.MinorVisible = false;
            swiftPlotDiagramSecondaryAxisy1.Tickmarks.Visible = false;
            swiftPlotDiagramSecondaryAxisy1.Visibility = DevExpress.Utils.DefaultBoolean.True;
            swiftPlotDiagramSecondaryAxisy1.VisibleInPanesSerializable = "-1";
            swiftPlotDiagramSecondaryAxisy1.VisualRange.Auto = false;
            swiftPlotDiagramSecondaryAxisy1.VisualRange.MaxValueSerializable = "99";
            swiftPlotDiagramSecondaryAxisy1.VisualRange.MinValueSerializable = "0";
            swiftPlotDiagramSecondaryAxisy1.WholeRange.Auto = false;
            swiftPlotDiagramSecondaryAxisy1.WholeRange.MaxValueSerializable = "99";
            swiftPlotDiagramSecondaryAxisy1.WholeRange.MinValueSerializable = "0";
            swiftPlotDiagram1.SecondaryAxesY.AddRange(new DevExpress.XtraCharts.SwiftPlotDiagramSecondaryAxisY[] { swiftPlotDiagramSecondaryAxisy1 });
            chartControl_water_level.Diagram = swiftPlotDiagram1;
            chartControl_water_level.Dock = DockStyle.Fill;
            chartControl_water_level.Legend.LegendID = -1;
            chartControl_water_level.Legend.Name = "Default Legend";
            chartControl_water_level.Location = new Point(0, 0);
            chartControl_water_level.Margin = new Padding(4, 3, 4, 3);
            chartControl_water_level.Name = "chartControl_water_level";
            series1.ArgumentScaleType = DevExpress.XtraCharts.ScaleType.Numerical;
            series1.LabelsVisibility = DevExpress.Utils.DefaultBoolean.False;
            series1.Name = "RAW";
            swiftPlotSeriesView1.Color = Color.Fuchsia;
            series1.View = swiftPlotSeriesView1;
            chartControl_water_level.SeriesSerializable = new DevExpress.XtraCharts.Series[]
    {
    series1
    };
            chartControl_water_level.Size = new Size(1429, 654);
            chartControl_water_level.TabIndex = 0;
            // 
            // panel_bottom
            // 
            panel_bottom.Controls.Add(label8);
            panel_bottom.Controls.Add(button_loadFrom);
            panel_bottom.Controls.Add(checkBox_avg);
            panel_bottom.Controls.Add(trackBar_avg);
            panel_bottom.Controls.Add(trackBar_medianTrackBar);
            panel_bottom.Controls.Add(button_saveAs);
            panel_bottom.Controls.Add(checkBox_median);
            panel_bottom.Controls.Add(label2);
            panel_bottom.Controls.Add(label_reconnect_count);
            panel_bottom.Controls.Add(textBox_max_water_level);
            panel_bottom.Controls.Add(label1);
            panel_bottom.Controls.Add(checkBox_percent);
            panel_bottom.Controls.Add(textBox_min_water_level);
            panel_bottom.Controls.Add(button_start);
            panel_bottom.Controls.Add(button_clear);
            panel_bottom.Controls.Add(checkBox_raw);
            panel_bottom.Controls.Add(button_stop);
            panel_bottom.Dock = DockStyle.Bottom;
            panel_bottom.Location = new Point(0, 654);
            panel_bottom.Margin = new Padding(4, 3, 4, 3);
            panel_bottom.Name = "panel_bottom";
            panel_bottom.Size = new Size(1429, 126);
            panel_bottom.TabIndex = 9;
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Location = new Point(1199, 88);
            label8.Margin = new Padding(4, 0, 4, 0);
            label8.Name = "label8";
            label8.Size = new Size(153, 15);
            label8.TabIndex = 16;
            label8.Text = "Повторных подключений:";
            // 
            // button_loadFrom
            // 
            button_loadFrom.Location = new Point(135, 50);
            button_loadFrom.Margin = new Padding(4, 3, 4, 3);
            button_loadFrom.Name = "button_loadFrom";
            button_loadFrom.Size = new Size(117, 27);
            button_loadFrom.TabIndex = 15;
            button_loadFrom.Text = "Загрузить из...";
            button_loadFrom.UseVisualStyleBackColor = true;
            button_loadFrom.Click += Button_LoadFromFile_Click;
            // 
            // checkBox_avg
            // 
            checkBox_avg.AutoSize = true;
            checkBox_avg.Location = new Point(813, 83);
            checkBox_avg.Margin = new Padding(4, 3, 4, 3);
            checkBox_avg.Name = "checkBox_avg";
            checkBox_avg.Size = new Size(72, 19);
            checkBox_avg.TabIndex = 14;
            checkBox_avg.Text = "Среднее";
            checkBox_avg.UseVisualStyleBackColor = true;
            checkBox_avg.CheckedChanged += CheckBox_avg_CheckedChanged;
            // 
            // trackBar_avg
            // 
            trackBar_avg.Location = new Point(312, 66);
            trackBar_avg.Margin = new Padding(4, 3, 4, 3);
            trackBar_avg.Maximum = 129;
            trackBar_avg.Minimum = 1;
            trackBar_avg.Name = "trackBar_avg";
            trackBar_avg.Size = new Size(484, 45);
            trackBar_avg.TabIndex = 13;
            trackBar_avg.Value = 1;
            trackBar_avg.Scroll += TrackBar_avg_Scroll;
            // 
            // trackBar_medianTrackBar
            // 
            trackBar_medianTrackBar.Location = new Point(312, 13);
            trackBar_medianTrackBar.Margin = new Padding(4, 3, 4, 3);
            trackBar_medianTrackBar.Maximum = 128;
            trackBar_medianTrackBar.Minimum = 1;
            trackBar_medianTrackBar.Name = "trackBar_medianTrackBar";
            trackBar_medianTrackBar.Size = new Size(484, 45);
            trackBar_medianTrackBar.TabIndex = 12;
            trackBar_medianTrackBar.TickFrequency = 2;
            trackBar_medianTrackBar.Value = 1;
            trackBar_medianTrackBar.Scroll += TrackBar_Median_Scroll;
            // 
            // button_saveAs
            // 
            button_saveAs.Location = new Point(13, 50);
            button_saveAs.Margin = new Padding(4, 3, 4, 3);
            button_saveAs.Name = "button_saveAs";
            button_saveAs.Size = new Size(115, 27);
            button_saveAs.TabIndex = 11;
            button_saveAs.Text = "Сохранить в...";
            button_saveAs.UseVisualStyleBackColor = true;
            button_saveAs.Click += Button_SaveAs_Click;
            // 
            // checkBox_median
            // 
            checkBox_median.AutoSize = true;
            checkBox_median.Location = new Point(948, 23);
            checkBox_median.Margin = new Padding(4, 3, 4, 3);
            checkBox_median.Name = "checkBox_median";
            checkBox_median.Size = new Size(98, 19);
            checkBox_median.TabIndex = 9;
            checkBox_median.Text = "Медиана (μs)";
            checkBox_median.UseVisualStyleBackColor = true;
            checkBox_median.CheckedChanged += CheckBox_Median_CheckedChanged;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(1283, 18);
            label2.Margin = new Padding(4, 0, 4, 0);
            label2.Name = "label2";
            label2.Size = new Size(53, 15);
            label2.TabIndex = 6;
            label2.Text = "Max (μs)";
            // 
            // label_reconnect_count
            // 
            label_reconnect_count.AutoSize = true;
            label_reconnect_count.Location = new Point(1371, 88);
            label_reconnect_count.Margin = new Padding(4, 0, 4, 0);
            label_reconnect_count.Name = "label_reconnect_count";
            label_reconnect_count.Size = new Size(13, 15);
            label_reconnect_count.TabIndex = 7;
            label_reconnect_count.Text = "0";
            // 
            // textBox_max_water_level
            // 
            textBox_max_water_level.Location = new Point(1287, 36);
            textBox_max_water_level.Margin = new Padding(4, 3, 4, 3);
            textBox_max_water_level.Name = "textBox_max_water_level";
            textBox_max_water_level.ReadOnly = true;
            textBox_max_water_level.Size = new Size(98, 23);
            textBox_max_water_level.TabIndex = 4;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(1136, 18);
            label1.Margin = new Padding(4, 0, 4, 0);
            label1.Name = "label1";
            label1.Size = new Size(51, 15);
            label1.TabIndex = 5;
            label1.Text = "Min (μs)";
            // 
            // checkBox_percent
            // 
            checkBox_percent.AutoSize = true;
            checkBox_percent.Checked = true;
            checkBox_percent.CheckState = CheckState.Checked;
            checkBox_percent.Location = new Point(948, 83);
            checkBox_percent.Margin = new Padding(4, 3, 4, 3);
            checkBox_percent.Name = "checkBox_percent";
            checkBox_percent.Size = new Size(70, 19);
            checkBox_percent.TabIndex = 4;
            checkBox_percent.Text = "0-99 (%)";
            checkBox_percent.UseVisualStyleBackColor = true;
            checkBox_percent.CheckedChanged += CheckBox_Percent_CheckedChanged;
            // 
            // textBox_min_water_level
            // 
            textBox_min_water_level.Location = new Point(1140, 37);
            textBox_min_water_level.Margin = new Padding(4, 3, 4, 3);
            textBox_min_water_level.Name = "textBox_min_water_level";
            textBox_min_water_level.ReadOnly = true;
            textBox_min_water_level.Size = new Size(95, 23);
            textBox_min_water_level.TabIndex = 3;
            // 
            // button_start
            // 
            button_start.Location = new Point(13, 16);
            button_start.Margin = new Padding(4, 3, 4, 3);
            button_start.Name = "button_start";
            button_start.Size = new Size(88, 27);
            button_start.TabIndex = 0;
            button_start.Text = "Запись";
            button_start.UseVisualStyleBackColor = true;
            button_start.Click += Button_Start_Click;
            // 
            // button_clear
            // 
            button_clear.Location = new Point(202, 16);
            button_clear.Margin = new Padding(4, 3, 4, 3);
            button_clear.Name = "button_clear";
            button_clear.Size = new Size(88, 27);
            button_clear.TabIndex = 2;
            button_clear.Text = "Очистить";
            button_clear.UseVisualStyleBackColor = true;
            button_clear.Click += Button_Clear_Click;
            // 
            // checkBox_raw
            // 
            checkBox_raw.AutoSize = true;
            checkBox_raw.Checked = true;
            checkBox_raw.CheckState = CheckState.Checked;
            checkBox_raw.Location = new Point(813, 23);
            checkBox_raw.Margin = new Padding(4, 3, 4, 3);
            checkBox_raw.Name = "checkBox_raw";
            checkBox_raw.Size = new Size(74, 19);
            checkBox_raw.TabIndex = 3;
            checkBox_raw.Text = "RAW (μs)";
            checkBox_raw.UseVisualStyleBackColor = true;
            checkBox_raw.CheckedChanged += CheckBox_RAW_CheckedChanged;
            // 
            // button_stop
            // 
            button_stop.Enabled = false;
            button_stop.Location = new Point(107, 16);
            button_stop.Margin = new Padding(4, 3, 4, 3);
            button_stop.Name = "button_stop";
            button_stop.Size = new Size(88, 27);
            button_stop.TabIndex = 1;
            button_stop.Text = "Стоп";
            button_stop.UseVisualStyleBackColor = true;
            button_stop.Click += Button_Stop_Click;
            // 
            // errorProvider1
            // 
            errorProvider1.ContainerControl = this;
            // 
            // WaterLevel
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(panel_root);
            Margin = new Padding(4, 3, 4, 3);
            Name = "WaterLevel";
            Size = new Size(1429, 780);
            panel_root.ResumeLayout(false);
            panel_chart.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)chartControl_water_level).EndInit();
            panel_bottom.ResumeLayout(false);
            panel_bottom.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)trackBar_avg).EndInit();
            ((System.ComponentModel.ISupportInitialize)trackBar_medianTrackBar).EndInit();
            ((System.ComponentModel.ISupportInitialize)errorProvider1).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.Panel panel_root;
        private DevExpress.XtraCharts.ChartControl chartControl_water_level;
        private System.Windows.Forms.Panel panel_bottom;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Button button_loadFrom;
        private System.Windows.Forms.CheckBox checkBox_avg;
        private System.Windows.Forms.TrackBar trackBar_avg;
        private System.Windows.Forms.TrackBar trackBar_medianTrackBar;
        private System.Windows.Forms.Button button_saveAs;
        private System.Windows.Forms.CheckBox checkBox_median;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label_reconnect_count;
        private System.Windows.Forms.TextBox textBox_max_water_level;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox checkBox_percent;
        private System.Windows.Forms.TextBox textBox_min_water_level;
        private System.Windows.Forms.Button button_start;
        private System.Windows.Forms.Button button_clear;
        private System.Windows.Forms.CheckBox checkBox_raw;
        private System.Windows.Forms.Button button_stop;
        private System.Windows.Forms.Panel panel_chart;
        private System.Windows.Forms.ErrorProvider errorProvider1;
    }
}
