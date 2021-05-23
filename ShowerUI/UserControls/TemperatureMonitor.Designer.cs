
namespace ShowerUI.UserControls
{
    partial class TemperatureMonitor
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
            DevExpress.XtraCharts.SwiftPlotDiagram swiftPlotDiagram3 = new DevExpress.XtraCharts.SwiftPlotDiagram();
            DevExpress.XtraCharts.Series series7 = new DevExpress.XtraCharts.Series();
            DevExpress.XtraCharts.SwiftPlotSeriesView swiftPlotSeriesView9 = new DevExpress.XtraCharts.SwiftPlotSeriesView();
            DevExpress.XtraCharts.Series series8 = new DevExpress.XtraCharts.Series();
            DevExpress.XtraCharts.SwiftPlotSeriesView swiftPlotSeriesView10 = new DevExpress.XtraCharts.SwiftPlotSeriesView();
            DevExpress.XtraCharts.Series series9 = new DevExpress.XtraCharts.Series();
            DevExpress.XtraCharts.SwiftPlotSeriesView swiftPlotSeriesView11 = new DevExpress.XtraCharts.SwiftPlotSeriesView();
            DevExpress.XtraCharts.SwiftPlotSeriesView swiftPlotSeriesView12 = new DevExpress.XtraCharts.SwiftPlotSeriesView();
            this.panel2 = new System.Windows.Forms.Panel();
            this.labelTempReconnectCount = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.labelTempReconnect = new System.Windows.Forms.Label();
            this.button_Stop = new System.Windows.Forms.Button();
            this.trackBar_TimeLeft = new System.Windows.Forms.TrackBar();
            this.labelTimeLeft = new System.Windows.Forms.Label();
            this.button_Start = new System.Windows.Forms.Button();
            this.button_SimulationStart = new System.Windows.Forms.Button();
            this.button_LoadFrom = new System.Windows.Forms.Button();
            this.button_SaveAs = new System.Windows.Forms.Button();
            this.button_Clear = new System.Windows.Forms.Button();
            this.chartControl_temperature = new DevExpress.XtraCharts.ChartControl();
            this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar_TimeLeft)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chartControl_temperature)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(swiftPlotDiagram3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(series7)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(swiftPlotSeriesView9)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(series8)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(swiftPlotSeriesView10)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(series9)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(swiftPlotSeriesView11)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(swiftPlotSeriesView12)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).BeginInit();
            this.SuspendLayout();
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.labelTempReconnectCount);
            this.panel2.Controls.Add(this.label7);
            this.panel2.Controls.Add(this.labelTempReconnect);
            this.panel2.Controls.Add(this.button_Stop);
            this.panel2.Controls.Add(this.trackBar_TimeLeft);
            this.panel2.Controls.Add(this.labelTimeLeft);
            this.panel2.Controls.Add(this.button_Start);
            this.panel2.Controls.Add(this.button_SimulationStart);
            this.panel2.Controls.Add(this.button_LoadFrom);
            this.panel2.Controls.Add(this.button_SaveAs);
            this.panel2.Controls.Add(this.button_Clear);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel2.Location = new System.Drawing.Point(0, 550);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1086, 59);
            this.panel2.TabIndex = 25;
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
            // 
            // labelTempReconnect
            // 
            this.labelTempReconnect.AutoSize = true;
            this.labelTempReconnect.Location = new System.Drawing.Point(5, 38);
            this.labelTempReconnect.Name = "labelTempReconnect";
            this.labelTempReconnect.Size = new System.Drawing.Size(136, 13);
            this.labelTempReconnect.TabIndex = 24;
            this.labelTempReconnect.Text = "Повторных подключений:";
            // 
            // button_Stop
            // 
            this.button_Stop.Location = new System.Drawing.Point(123, 6);
            this.button_Stop.Name = "button_Stop";
            this.button_Stop.Size = new System.Drawing.Size(75, 23);
            this.button_Stop.TabIndex = 15;
            this.button_Stop.Text = "Стоп";
            this.button_Stop.UseVisualStyleBackColor = true;
            this.button_Stop.Click += new System.EventHandler(this.Button_Stop_Click);
            // 
            // trackBar_TimeLeft
            // 
            this.trackBar_TimeLeft.Location = new System.Drawing.Point(642, 6);
            this.trackBar_TimeLeft.Maximum = 50;
            this.trackBar_TimeLeft.Name = "trackBar_TimeLeft";
            this.trackBar_TimeLeft.Size = new System.Drawing.Size(301, 45);
            this.trackBar_TimeLeft.SmallChange = 2;
            this.trackBar_TimeLeft.TabIndex = 22;
            this.trackBar_TimeLeft.Scroll += new System.EventHandler(this.TrackBar_TimeLeft_Scroll);
            // 
            // labelTimeLeft
            // 
            this.labelTimeLeft.AutoSize = true;
            this.labelTimeLeft.Location = new System.Drawing.Point(1015, 11);
            this.labelTimeLeft.Name = "labelTimeLeft";
            this.labelTimeLeft.Size = new System.Drawing.Size(49, 13);
            this.labelTimeLeft.TabIndex = 23;
            this.labelTimeLeft.Text = "__:__:__";
            // 
            // button_Start
            // 
            this.button_Start.Location = new System.Drawing.Point(5, 6);
            this.button_Start.Name = "button_Start";
            this.button_Start.Size = new System.Drawing.Size(75, 23);
            this.button_Start.TabIndex = 14;
            this.button_Start.Text = "Запись";
            this.button_Start.UseVisualStyleBackColor = true;
            this.button_Start.Click += new System.EventHandler(this.Button_Start_Click);
            // 
            // button_SimulationStart
            // 
            this.button_SimulationStart.Location = new System.Drawing.Point(495, 6);
            this.button_SimulationStart.Name = "button_SimulationStart";
            this.button_SimulationStart.Size = new System.Drawing.Size(75, 23);
            this.button_SimulationStart.TabIndex = 17;
            this.button_SimulationStart.Text = "Симуляция";
            this.button_SimulationStart.UseVisualStyleBackColor = true;
            this.button_SimulationStart.Click += new System.EventHandler(this.Button_SimulationStart_Click);
            // 
            // button_LoadFrom
            // 
            this.button_LoadFrom.Location = new System.Drawing.Point(390, 6);
            this.button_LoadFrom.Name = "button_LoadFrom";
            this.button_LoadFrom.Size = new System.Drawing.Size(99, 23);
            this.button_LoadFrom.TabIndex = 21;
            this.button_LoadFrom.Text = "Загрузить из...";
            this.button_LoadFrom.UseVisualStyleBackColor = true;
            this.button_LoadFrom.Click += new System.EventHandler(this.Button_LoadFrom_Click);
            // 
            // button_SaveAs
            // 
            this.button_SaveAs.Location = new System.Drawing.Point(285, 6);
            this.button_SaveAs.Name = "button_SaveAs";
            this.button_SaveAs.Size = new System.Drawing.Size(99, 23);
            this.button_SaveAs.TabIndex = 16;
            this.button_SaveAs.Text = "Сохранить в...";
            this.button_SaveAs.UseVisualStyleBackColor = true;
            this.button_SaveAs.Click += new System.EventHandler(this.Button_SaveAs_Click);
            // 
            // button_Clear
            // 
            this.button_Clear.Location = new System.Drawing.Point(204, 6);
            this.button_Clear.Name = "button_Clear";
            this.button_Clear.Size = new System.Drawing.Size(75, 23);
            this.button_Clear.TabIndex = 20;
            this.button_Clear.Text = "Очистить";
            this.button_Clear.UseVisualStyleBackColor = true;
            this.button_Clear.Click += new System.EventHandler(this.Button_Clear_Click);
            // 
            // chartControl_temperature
            // 
            this.chartControl_temperature.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            swiftPlotDiagram3.AxisX.Label.Visible = false;
            swiftPlotDiagram3.AxisX.MinorCount = 1;
            swiftPlotDiagram3.AxisX.Tickmarks.MinorVisible = false;
            swiftPlotDiagram3.AxisX.Visibility = DevExpress.Utils.DefaultBoolean.True;
            swiftPlotDiagram3.AxisX.VisibleInPanesSerializable = "-1";
            swiftPlotDiagram3.AxisY.MinorCount = 1;
            swiftPlotDiagram3.AxisY.Visibility = DevExpress.Utils.DefaultBoolean.True;
            swiftPlotDiagram3.AxisY.VisibleInPanesSerializable = "-1";
            swiftPlotDiagram3.AxisY.VisualRange.Auto = false;
            swiftPlotDiagram3.AxisY.VisualRange.MaxValueSerializable = "39";
            swiftPlotDiagram3.AxisY.VisualRange.MinValueSerializable = "15";
            swiftPlotDiagram3.AxisY.WholeRange.Auto = false;
            swiftPlotDiagram3.AxisY.WholeRange.MaxValueSerializable = "39";
            swiftPlotDiagram3.AxisY.WholeRange.MinValueSerializable = "15";
            this.chartControl_temperature.Diagram = swiftPlotDiagram3;
            this.chartControl_temperature.Dock = System.Windows.Forms.DockStyle.Fill;
            this.chartControl_temperature.Legend.Name = "Default Legend";
            this.chartControl_temperature.Legend.Visibility = DevExpress.Utils.DefaultBoolean.False;
            this.chartControl_temperature.Location = new System.Drawing.Point(0, 0);
            this.chartControl_temperature.Name = "chartControl_temperature";
            series7.ArgumentScaleType = DevExpress.XtraCharts.ScaleType.Numerical;
            series7.CrosshairTextOptions.EnableAntialiasing = DevExpress.Utils.DefaultBoolean.False;
            series7.CrosshairTextOptions.Tag = "Температура в баке";
            series7.Name = "TempOn";
            swiftPlotSeriesView9.Color = System.Drawing.Color.Red;
            series7.View = swiftPlotSeriesView9;
            series8.CrosshairTextOptions.EnableAntialiasing = DevExpress.Utils.DefaultBoolean.False;
            series8.CrosshairTextOptions.Tag = "Температура в баке";
            series8.LegendName = "Default Legend";
            series8.Name = "TempOff";
            swiftPlotSeriesView10.Color = System.Drawing.Color.Blue;
            series8.View = swiftPlotSeriesView10;
            series9.ArgumentScaleType = DevExpress.XtraCharts.ScaleType.Numerical;
            series9.Name = "CalculatedTemp";
            series9.ShowInLegend = false;
            swiftPlotSeriesView11.Color = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            series9.View = swiftPlotSeriesView11;
            this.chartControl_temperature.SeriesSerializable = new DevExpress.XtraCharts.Series[] {
        series7,
        series8,
        series9};
            swiftPlotSeriesView12.LineStyle.Thickness = 2;
            this.chartControl_temperature.SeriesTemplate.View = swiftPlotSeriesView12;
            this.chartControl_temperature.Size = new System.Drawing.Size(1086, 550);
            this.chartControl_temperature.TabIndex = 26;
            // 
            // errorProvider1
            // 
            this.errorProvider1.ContainerControl = this;
            // 
            // TemperatureMonitor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.chartControl_temperature);
            this.Controls.Add(this.panel2);
            this.Name = "TemperatureMonitor";
            this.Size = new System.Drawing.Size(1086, 609);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar_TimeLeft)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(swiftPlotDiagram3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(swiftPlotSeriesView9)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(series7)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(swiftPlotSeriesView10)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(series8)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(swiftPlotSeriesView11)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(series9)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(swiftPlotSeriesView12)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chartControl_temperature)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label labelTempReconnectCount;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label labelTempReconnect;
        private System.Windows.Forms.Button button_Stop;
        private System.Windows.Forms.TrackBar trackBar_TimeLeft;
        private System.Windows.Forms.Label labelTimeLeft;
        private System.Windows.Forms.Button button_Start;
        private System.Windows.Forms.Button button_SimulationStart;
        private System.Windows.Forms.Button button_LoadFrom;
        private System.Windows.Forms.Button button_SaveAs;
        private System.Windows.Forms.Button button_Clear;
        private DevExpress.XtraCharts.ChartControl chartControl_temperature;
        private System.Windows.Forms.ErrorProvider errorProvider1;
    }
}
