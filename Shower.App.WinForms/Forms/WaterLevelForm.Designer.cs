namespace ShowerUI.Forms
{
    partial class WaterLevelForm
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
            DevExpress.XtraCharts.SwiftPlotDiagramSecondaryAxisY swiftPlotDiagramSecondaryAxisY1 = new DevExpress.XtraCharts.SwiftPlotDiagramSecondaryAxisY();
            DevExpress.XtraCharts.Series series1 = new DevExpress.XtraCharts.Series();
            DevExpress.XtraCharts.SwiftPlotSeriesView swiftPlotSeriesView1 = new DevExpress.XtraCharts.SwiftPlotSeriesView();
            this.chartControl_water_level = new DevExpress.XtraCharts.ChartControl();
            ((System.ComponentModel.ISupportInitialize)(this.chartControl_water_level)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(swiftPlotDiagram1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(swiftPlotDiagramSecondaryAxisY1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(series1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(swiftPlotSeriesView1)).BeginInit();
            this.SuspendLayout();
            // 
            // chartControl_water_level
            // 
            this.chartControl_water_level.DataBindings = null;
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
            swiftPlotDiagramSecondaryAxisY1.AxisID = 0;
            swiftPlotDiagramSecondaryAxisY1.GridLines.MinorVisible = true;
            swiftPlotDiagramSecondaryAxisY1.MinorCount = 1;
            swiftPlotDiagramSecondaryAxisY1.Name = "Вторичная ось Y1";
            swiftPlotDiagramSecondaryAxisY1.NumericScaleOptions.AutoGrid = false;
            swiftPlotDiagramSecondaryAxisY1.NumericScaleOptions.GridSpacing = 2D;
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
            series1.Name = "usec";
            series1.View = swiftPlotSeriesView1;
            this.chartControl_water_level.SeriesSerializable = new DevExpress.XtraCharts.Series[] {
        series1};
            this.chartControl_water_level.Size = new System.Drawing.Size(800, 450);
            this.chartControl_water_level.TabIndex = 1;
            // 
            // WaterLevelForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.ControlBox = false;
            this.Controls.Add(this.chartControl_water_level);
            this.Name = "WaterLevelForm";
            this.Text = "Уровень воды";
            ((System.ComponentModel.ISupportInitialize)(swiftPlotDiagramSecondaryAxisY1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(swiftPlotDiagram1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(swiftPlotSeriesView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(series1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chartControl_water_level)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraCharts.ChartControl chartControl_water_level;
    }
}