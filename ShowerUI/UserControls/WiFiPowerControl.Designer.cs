
namespace ShowerUI.UserControls
{
    partial class WiFiPowerControl
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
            this.label1 = new System.Windows.Forms.Label();
            this.wifiPower = new MyNumericUpDown();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.wifiPower)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(159, 13);
            this.label1.TabIndex = 54;
            this.label1.Text = "Мощность Wi-Fi (10..20.5 dBm)";
            // 
            // wifiPower
            // 
            this.wifiPower.DecimalPlaces = 2;
            this.wifiPower.Increment = new decimal(new int[] {
            25,
            0,
            0,
            131072});
            this.wifiPower.Location = new System.Drawing.Point(3, 19);
            this.wifiPower.Maximum = new decimal(new int[] {
            205,
            0,
            0,
            65536});
            this.wifiPower.Minimum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.wifiPower.Name = "wifiPower";
            this.wifiPower.Size = new System.Drawing.Size(79, 20);
            this.wifiPower.TabIndex = 53;
            this.wifiPower.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.wifiPower.ValueChanged += new System.EventHandler(this.WifiPower_ValueChanged);
            // 
            // WiFiPowerControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.Controls.Add(this.label1);
            this.Controls.Add(this.wifiPower);
            this.Name = "WiFiPowerControl";
            this.Size = new System.Drawing.Size(165, 42);
            ((System.ComponentModel.ISupportInitialize)(this.wifiPower)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private MyNumericUpDown wifiPower;
        private System.Windows.Forms.ToolTip toolTip1;
    }
}
