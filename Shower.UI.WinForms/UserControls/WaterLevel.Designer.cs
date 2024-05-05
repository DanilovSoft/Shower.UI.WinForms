
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
            this.components = new System.ComponentModel.Container();
            DevExpress.XtraCharts.SwiftPlotDiagram swiftPlotDiagram1 = new DevExpress.XtraCharts.SwiftPlotDiagram();
            DevExpress.XtraCharts.SwiftPlotDiagramSecondaryAxisY swiftPlotDiagramSecondaryAxisY1 = new DevExpress.XtraCharts.SwiftPlotDiagramSecondaryAxisY();
            DevExpress.XtraCharts.Series series1 = new DevExpress.XtraCharts.Series();
            DevExpress.XtraCharts.SwiftPlotSeriesView swiftPlotSeriesView1 = new DevExpress.XtraCharts.SwiftPlotSeriesView();
            this.panel_root = new System.Windows.Forms.Panel();
            this.panel_chart = new System.Windows.Forms.Panel();
            this.chartControl_water_level = new DevExpress.XtraCharts.ChartControl();
            this.panel_bottom = new System.Windows.Forms.Panel();
            this.label8 = new System.Windows.Forms.Label();
            this.button_loadFrom = new System.Windows.Forms.Button();
            this.checkBox_avg = new System.Windows.Forms.CheckBox();
            this.trackBar_avg = new System.Windows.Forms.TrackBar();
            this.trackBar_medianTrackBar = new System.Windows.Forms.TrackBar();
            this.button_saveAs = new System.Windows.Forms.Button();
            this.checkBox_median = new System.Windows.Forms.CheckBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label_reconnect_count = new System.Windows.Forms.Label();
            this.textBox_max_water_level = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.checkBox_percent = new System.Windows.Forms.CheckBox();
            this.textBox_min_water_level = new System.Windows.Forms.TextBox();
            this.button_start = new System.Windows.Forms.Button();
            this.button_clear = new System.Windows.Forms.Button();
            this.checkBox_raw = new System.Windows.Forms.CheckBox();
            this.button_stop = new System.Windows.Forms.Button();
            this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
            this.panel_root.SuspendLayout();
            this.panel_chart.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chartControl_water_level)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(swiftPlotDiagram1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(swiftPlotDiagramSecondaryAxisY1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(series1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(swiftPlotSeriesView1)).BeginInit();
            this.panel_bottom.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar_avg)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar_medianTrackBar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).BeginInit();
            this.SuspendLayout();
            // 
            // panel_root
            // 
            this.panel_root.Controls.Add(this.panel_chart);
            this.panel_root.Controls.Add(this.panel_bottom);
            this.panel_root.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel_root.Location = new System.Drawing.Point(0, 0);
            this.panel_root.Name = "panel_root";
            this.panel_root.Size = new System.Drawing.Size(1225, 676);
            this.panel_root.TabIndex = 10;
            // 
            // panel_chart
            // 
            this.panel_chart.Controls.Add(this.chartControl_water_level);
            this.panel_chart.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel_chart.Location = new System.Drawing.Point(0, 0);
            this.panel_chart.Name = "panel_chart";
            this.panel_chart.Size = new System.Drawing.Size(1225, 567);
            this.panel_chart.TabIndex = 10;
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
            swiftPlotDiagramSecondaryAxisY1.AxisID = 0;
            swiftPlotDiagramSecondaryAxisY1.GridLines.Visible = true;
            swiftPlotDiagramSecondaryAxisY1.MinorCount = 1;
            swiftPlotDiagramSecondaryAxisY1.Name = "percent";
            swiftPlotDiagramSecondaryAxisY1.NumericScaleOptions.AutoGrid = false;
            swiftPlotDiagramSecondaryAxisY1.NumericScaleOptions.CustomGridAlignment = 2D;
            swiftPlotDiagramSecondaryAxisY1.NumericScaleOptions.GridAlignment = DevExpress.XtraCharts.NumericGridAlignment.Custom;
            swiftPlotDiagramSecondaryAxisY1.NumericScaleOptions.GridOffset = 2D;
            swiftPlotDiagramSecondaryAxisY1.NumericScaleOptions.GridSpacing = 2D;
            swiftPlotDiagramSecondaryAxisY1.Tickmarks.MinorVisible = false;
            swiftPlotDiagramSecondaryAxisY1.Tickmarks.Visible = false;
            swiftPlotDiagramSecondaryAxisY1.Visibility = DevExpress.Utils.DefaultBoolean.True;
            swiftPlotDiagramSecondaryAxisY1.VisibleInPanesSerializable = "-1";
            swiftPlotDiagramSecondaryAxisY1.VisualRange.Auto = false;
            swiftPlotDiagramSecondaryAxisY1.VisualRange.MaxValueSerializable = "99";
            swiftPlotDiagramSecondaryAxisY1.VisualRange.MinValueSerializable = "0";
            swiftPlotDiagramSecondaryAxisY1.WholeRange.Auto = false;
            swiftPlotDiagramSecondaryAxisY1.WholeRange.MaxValueSerializable = "99";
            swiftPlotDiagramSecondaryAxisY1.WholeRange.MinValueSerializable = "0";
            swiftPlotDiagram1.SecondaryAxesY.AddRange(new DevExpress.XtraCharts.SwiftPlotDiagramSecondaryAxisY[] {
            swiftPlotDiagramSecondaryAxisY1});
            this.chartControl_water_level.Diagram = swiftPlotDiagram1;
            this.chartControl_water_level.Dock = System.Windows.Forms.DockStyle.Fill;
            this.chartControl_water_level.Legend.Name = "Default Legend";
            this.chartControl_water_level.Location = new System.Drawing.Point(0, 0);
            this.chartControl_water_level.Name = "chartControl_water_level";
            series1.ArgumentScaleType = DevExpress.XtraCharts.ScaleType.Numerical;
            series1.LabelsVisibility = DevExpress.Utils.DefaultBoolean.False;
            series1.Name = "RAW";
            swiftPlotSeriesView1.Color = System.Drawing.Color.Fuchsia;
            series1.View = swiftPlotSeriesView1;
            this.chartControl_water_level.SeriesSerializable = new DevExpress.XtraCharts.Series[] {
        series1};
            this.chartControl_water_level.Size = new System.Drawing.Size(1225, 567);
            this.chartControl_water_level.TabIndex = 0;
            // 
            // panel_bottom
            // 
            this.panel_bottom.Controls.Add(this.label8);
            this.panel_bottom.Controls.Add(this.button_loadFrom);
            this.panel_bottom.Controls.Add(this.checkBox_avg);
            this.panel_bottom.Controls.Add(this.trackBar_avg);
            this.panel_bottom.Controls.Add(this.trackBar_medianTrackBar);
            this.panel_bottom.Controls.Add(this.button_saveAs);
            this.panel_bottom.Controls.Add(this.checkBox_median);
            this.panel_bottom.Controls.Add(this.label2);
            this.panel_bottom.Controls.Add(this.label_reconnect_count);
            this.panel_bottom.Controls.Add(this.textBox_max_water_level);
            this.panel_bottom.Controls.Add(this.label1);
            this.panel_bottom.Controls.Add(this.checkBox_percent);
            this.panel_bottom.Controls.Add(this.textBox_min_water_level);
            this.panel_bottom.Controls.Add(this.button_start);
            this.panel_bottom.Controls.Add(this.button_clear);
            this.panel_bottom.Controls.Add(this.checkBox_raw);
            this.panel_bottom.Controls.Add(this.button_stop);
            this.panel_bottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel_bottom.Location = new System.Drawing.Point(0, 567);
            this.panel_bottom.Name = "panel_bottom";
            this.panel_bottom.Size = new System.Drawing.Size(1225, 109);
            this.panel_bottom.TabIndex = 9;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(1028, 76);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(136, 13);
            this.label8.TabIndex = 16;
            this.label8.Text = "Повторных подключений:";
            // 
            // button_loadFrom
            // 
            this.button_loadFrom.Location = new System.Drawing.Point(116, 43);
            this.button_loadFrom.Name = "button_loadFrom";
            this.button_loadFrom.Size = new System.Drawing.Size(100, 23);
            this.button_loadFrom.TabIndex = 15;
            this.button_loadFrom.Text = "Загрузить из...";
            this.button_loadFrom.UseVisualStyleBackColor = true;
            this.button_loadFrom.Click += new System.EventHandler(this.Button_LoadFromFile_Click);
            // 
            // checkBox_avg
            // 
            this.checkBox_avg.AutoSize = true;
            this.checkBox_avg.Checked = true;
            this.checkBox_avg.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox_avg.Location = new System.Drawing.Point(697, 72);
            this.checkBox_avg.Name = "checkBox_avg";
            this.checkBox_avg.Size = new System.Drawing.Size(110, 17);
            this.checkBox_avg.TabIndex = 14;
            this.checkBox_avg.Text = "М + Среднее (μs)";
            this.checkBox_avg.UseVisualStyleBackColor = true;
            this.checkBox_avg.CheckedChanged += new System.EventHandler(this.CheckBox_avg_CheckedChanged);
            // 
            // trackBar_avg
            // 
            this.trackBar_avg.Location = new System.Drawing.Point(267, 57);
            this.trackBar_avg.Maximum = 129;
            this.trackBar_avg.Minimum = 1;
            this.trackBar_avg.Name = "trackBar_avg";
            this.trackBar_avg.Size = new System.Drawing.Size(415, 45);
            this.trackBar_avg.TabIndex = 13;
            this.trackBar_avg.Value = 1;
            this.trackBar_avg.Scroll += new System.EventHandler(this.TrackBar_avg_Scroll);
            // 
            // trackBar_medianTrackBar
            // 
            this.trackBar_medianTrackBar.Location = new System.Drawing.Point(267, 11);
            this.trackBar_medianTrackBar.Maximum = 128;
            this.trackBar_medianTrackBar.Minimum = 1;
            this.trackBar_medianTrackBar.Name = "trackBar_medianTrackBar";
            this.trackBar_medianTrackBar.Size = new System.Drawing.Size(415, 45);
            this.trackBar_medianTrackBar.TabIndex = 12;
            this.trackBar_medianTrackBar.TickFrequency = 2;
            this.trackBar_medianTrackBar.Value = 1;
            this.trackBar_medianTrackBar.Scroll += new System.EventHandler(this.TrackBar_Median_Scroll);
            // 
            // button_saveAs
            // 
            this.button_saveAs.Location = new System.Drawing.Point(11, 43);
            this.button_saveAs.Name = "button_saveAs";
            this.button_saveAs.Size = new System.Drawing.Size(99, 23);
            this.button_saveAs.TabIndex = 11;
            this.button_saveAs.Text = "Сохранить в...";
            this.button_saveAs.UseVisualStyleBackColor = true;
            this.button_saveAs.Click += new System.EventHandler(this.Button_SaveAs_Click);
            // 
            // checkBox_median
            // 
            this.checkBox_median.AutoSize = true;
            this.checkBox_median.Location = new System.Drawing.Point(813, 20);
            this.checkBox_median.Name = "checkBox_median";
            this.checkBox_median.Size = new System.Drawing.Size(91, 17);
            this.checkBox_median.TabIndex = 9;
            this.checkBox_median.Text = "Медиана (μs)";
            this.checkBox_median.UseVisualStyleBackColor = true;
            this.checkBox_median.CheckedChanged += new System.EventHandler(this.CheckBox_Median_CheckedChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(1100, 16);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(47, 13);
            this.label2.TabIndex = 6;
            this.label2.Text = "Max (μs)";
            // 
            // label_reconnect_count
            // 
            this.label_reconnect_count.AutoSize = true;
            this.label_reconnect_count.Location = new System.Drawing.Point(1175, 76);
            this.label_reconnect_count.Name = "label_reconnect_count";
            this.label_reconnect_count.Size = new System.Drawing.Size(13, 13);
            this.label_reconnect_count.TabIndex = 7;
            this.label_reconnect_count.Text = "0";
            // 
            // textBox_max_water_level
            // 
            this.textBox_max_water_level.Location = new System.Drawing.Point(1103, 31);
            this.textBox_max_water_level.Name = "textBox_max_water_level";
            this.textBox_max_water_level.ReadOnly = true;
            this.textBox_max_water_level.Size = new System.Drawing.Size(85, 20);
            this.textBox_max_water_level.TabIndex = 4;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(974, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(44, 13);
            this.label1.TabIndex = 5;
            this.label1.Text = "Min (μs)";
            // 
            // checkBox_percent
            // 
            this.checkBox_percent.AutoSize = true;
            this.checkBox_percent.Location = new System.Drawing.Point(813, 72);
            this.checkBox_percent.Name = "checkBox_percent";
            this.checkBox_percent.Size = new System.Drawing.Size(64, 17);
            this.checkBox_percent.TabIndex = 4;
            this.checkBox_percent.Text = "0-99 (%)";
            this.checkBox_percent.UseVisualStyleBackColor = true;
            this.checkBox_percent.CheckedChanged += new System.EventHandler(this.CheckBox_Percent_CheckedChanged);
            // 
            // textBox_min_water_level
            // 
            this.textBox_min_water_level.Location = new System.Drawing.Point(977, 32);
            this.textBox_min_water_level.Name = "textBox_min_water_level";
            this.textBox_min_water_level.ReadOnly = true;
            this.textBox_min_water_level.Size = new System.Drawing.Size(82, 20);
            this.textBox_min_water_level.TabIndex = 3;
            // 
            // button_start
            // 
            this.button_start.Location = new System.Drawing.Point(11, 14);
            this.button_start.Name = "button_start";
            this.button_start.Size = new System.Drawing.Size(75, 23);
            this.button_start.TabIndex = 0;
            this.button_start.Text = "Запись";
            this.button_start.UseVisualStyleBackColor = true;
            this.button_start.Click += new System.EventHandler(this.Button_Start_Click);
            // 
            // button_clear
            // 
            this.button_clear.Location = new System.Drawing.Point(173, 14);
            this.button_clear.Name = "button_clear";
            this.button_clear.Size = new System.Drawing.Size(75, 23);
            this.button_clear.TabIndex = 2;
            this.button_clear.Text = "Очистить";
            this.button_clear.UseVisualStyleBackColor = true;
            this.button_clear.Click += new System.EventHandler(this.Button_Clear_Click);
            // 
            // checkBox_raw
            // 
            this.checkBox_raw.AutoSize = true;
            this.checkBox_raw.Checked = true;
            this.checkBox_raw.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox_raw.Location = new System.Drawing.Point(697, 20);
            this.checkBox_raw.Name = "checkBox_raw";
            this.checkBox_raw.Size = new System.Drawing.Size(72, 17);
            this.checkBox_raw.TabIndex = 3;
            this.checkBox_raw.Text = "RAW (μs)";
            this.checkBox_raw.UseVisualStyleBackColor = true;
            this.checkBox_raw.CheckedChanged += new System.EventHandler(this.CheckBox_RAW_CheckedChanged);
            // 
            // button_stop
            // 
            this.button_stop.Enabled = false;
            this.button_stop.Location = new System.Drawing.Point(92, 14);
            this.button_stop.Name = "button_stop";
            this.button_stop.Size = new System.Drawing.Size(75, 23);
            this.button_stop.TabIndex = 1;
            this.button_stop.Text = "Стоп";
            this.button_stop.UseVisualStyleBackColor = true;
            this.button_stop.Click += new System.EventHandler(this.Button_Stop_Click);
            // 
            // errorProvider1
            // 
            this.errorProvider1.ContainerControl = this;
            // 
            // WaterLevel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panel_root);
            this.Name = "WaterLevel";
            this.Size = new System.Drawing.Size(1225, 676);
            this.panel_root.ResumeLayout(false);
            this.panel_chart.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(swiftPlotDiagramSecondaryAxisY1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(swiftPlotDiagram1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(swiftPlotSeriesView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(series1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chartControl_water_level)).EndInit();
            this.panel_bottom.ResumeLayout(false);
            this.panel_bottom.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar_avg)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar_medianTrackBar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).EndInit();
            this.ResumeLayout(false);

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
