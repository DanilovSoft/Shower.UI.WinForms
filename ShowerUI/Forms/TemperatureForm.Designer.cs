namespace ShowerUI.Forms
{
    partial class TemperatureForm
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
            DevExpress.XtraCharts.SwiftPlotDiagram swiftPlotDiagram1 = new DevExpress.XtraCharts.SwiftPlotDiagram();
            DevExpress.XtraCharts.Series series1 = new DevExpress.XtraCharts.Series();
            DevExpress.XtraCharts.SwiftPlotSeriesView swiftPlotSeriesView1 = new DevExpress.XtraCharts.SwiftPlotSeriesView();
            DevExpress.XtraCharts.Series series2 = new DevExpress.XtraCharts.Series();
            DevExpress.XtraCharts.SwiftPlotSeriesView swiftPlotSeriesView2 = new DevExpress.XtraCharts.SwiftPlotSeriesView();
            DevExpress.XtraCharts.Series series3 = new DevExpress.XtraCharts.Series();
            DevExpress.XtraCharts.SwiftPlotSeriesView swiftPlotSeriesView3 = new DevExpress.XtraCharts.SwiftPlotSeriesView();
            DevExpress.XtraCharts.Series series4 = new DevExpress.XtraCharts.Series();
            DevExpress.XtraCharts.SwiftPlotSeriesView swiftPlotSeriesView4 = new DevExpress.XtraCharts.SwiftPlotSeriesView();
            DevExpress.XtraCharts.Series series5 = new DevExpress.XtraCharts.Series();
            DevExpress.XtraCharts.SwiftPlotSeriesView swiftPlotSeriesView5 = new DevExpress.XtraCharts.SwiftPlotSeriesView();
            DevExpress.XtraCharts.Series series6 = new DevExpress.XtraCharts.Series();
            DevExpress.XtraCharts.SwiftPlotSeriesView swiftPlotSeriesView6 = new DevExpress.XtraCharts.SwiftPlotSeriesView();
            DevExpress.XtraCharts.SwiftPlotSeriesView swiftPlotSeriesView7 = new DevExpress.XtraCharts.SwiftPlotSeriesView();
            this.chartControl_temperature = new DevExpress.XtraCharts.ChartControl();
            ((System.ComponentModel.ISupportInitialize)(this.chartControl_temperature)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(swiftPlotDiagram1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(series1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(swiftPlotSeriesView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(series2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(swiftPlotSeriesView2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(series3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(swiftPlotSeriesView3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(series4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(swiftPlotSeriesView4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(series5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(swiftPlotSeriesView5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(series6)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(swiftPlotSeriesView6)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(swiftPlotSeriesView7)).BeginInit();
            this.SuspendLayout();
            // 
            // chartControl_temperature
            // 
            this.chartControl_temperature.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.chartControl_temperature.DataBindings = null;
            swiftPlotDiagram1.AxisX.Visibility = DevExpress.Utils.DefaultBoolean.True;
            swiftPlotDiagram1.AxisX.VisibleInPanesSerializable = "-1";
            swiftPlotDiagram1.AxisY.MinorCount = 1;
            swiftPlotDiagram1.AxisY.VisibleInPanesSerializable = "-1";
            swiftPlotDiagram1.AxisY.VisualRange.Auto = false;
            swiftPlotDiagram1.AxisY.VisualRange.MaxValueSerializable = "39";
            swiftPlotDiagram1.AxisY.VisualRange.MinValueSerializable = "21";
            swiftPlotDiagram1.AxisY.WholeRange.Auto = false;
            swiftPlotDiagram1.AxisY.WholeRange.MaxValueSerializable = "39";
            swiftPlotDiagram1.AxisY.WholeRange.MinValueSerializable = "21";
            this.chartControl_temperature.Diagram = swiftPlotDiagram1;
            this.chartControl_temperature.Dock = System.Windows.Forms.DockStyle.Fill;
            this.chartControl_temperature.Legend.Name = "Default Legend";
            this.chartControl_temperature.Legend.Visibility = DevExpress.Utils.DefaultBoolean.False;
            this.chartControl_temperature.Location = new System.Drawing.Point(0, 0);
            this.chartControl_temperature.Name = "chartControl_temperature";
            series1.ArgumentScaleType = DevExpress.XtraCharts.ScaleType.Numerical;
            series1.Name = "Temp";
            swiftPlotSeriesView1.Color = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(64)))), ((int)(((byte)(0)))));
            series1.View = swiftPlotSeriesView1;
            series2.ArgumentScaleType = DevExpress.XtraCharts.ScaleType.Numerical;
            series2.Name = "AvgTemp";
            swiftPlotSeriesView2.Color = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            series2.View = swiftPlotSeriesView2;
            series3.Name = "TempOff";
            swiftPlotSeriesView3.Color = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(255)))), ((int)(((byte)(0)))));
            series3.View = swiftPlotSeriesView3;
            series4.Name = "AvgTempOff";
            swiftPlotSeriesView4.Color = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(255)))));
            series4.View = swiftPlotSeriesView4;
            series5.ArgumentScaleType = DevExpress.XtraCharts.ScaleType.Numerical;
            series5.Name = "Calc";
            series5.ShowInLegend = false;
            swiftPlotSeriesView5.Color = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            series5.View = swiftPlotSeriesView5;
            series6.Name = "PID";
            swiftPlotSeriesView6.Color = System.Drawing.Color.FromArgb(((int)(((byte)(84)))), ((int)(((byte)(141)))), ((int)(((byte)(212)))));
            series6.View = swiftPlotSeriesView6;
            this.chartControl_temperature.SeriesSerializable = new DevExpress.XtraCharts.Series[] {
        series1,
        series2,
        series3,
        series4,
        series5,
        series6};
            swiftPlotSeriesView7.LineStyle.Thickness = 2;
            this.chartControl_temperature.SeriesTemplate.View = swiftPlotSeriesView7;
            this.chartControl_temperature.Size = new System.Drawing.Size(1178, 743);
            this.chartControl_temperature.TabIndex = 3;
            // 
            // TemperatureForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1178, 743);
            this.Controls.Add(this.chartControl_temperature);
            this.Name = "TemperatureForm";
            this.Text = "TemperatureForm";
            this.TopMost = true;
            ((System.ComponentModel.ISupportInitialize)(swiftPlotDiagram1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(swiftPlotSeriesView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(series1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(swiftPlotSeriesView2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(series2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(swiftPlotSeriesView3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(series3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(swiftPlotSeriesView4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(series4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(swiftPlotSeriesView5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(series5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(swiftPlotSeriesView6)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(series6)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(swiftPlotSeriesView7)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chartControl_temperature)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraCharts.ChartControl chartControl_temperature;
    }
}