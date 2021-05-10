namespace ShowerUI.Forms
{
    partial class WaterLevelSettings
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
            this.panel3 = new System.Windows.Forms.Panel();
            this.trackBar1 = new System.Windows.Forms.TrackBar();
            this.checkBox5 = new System.Windows.Forms.CheckBox();
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
            this.panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar1)).BeginInit();
            this.SuspendLayout();
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.trackBar1);
            this.panel3.Controls.Add(this.checkBox5);
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
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.Location = new System.Drawing.Point(0, 0);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(629, 441);
            this.panel3.TabIndex = 9;
            // 
            // trackBar1
            // 
            this.trackBar1.Location = new System.Drawing.Point(115, 270);
            this.trackBar1.Name = "trackBar1";
            this.trackBar1.Size = new System.Drawing.Size(104, 45);
            this.trackBar1.TabIndex = 10;
            // 
            // checkBox5
            // 
            this.checkBox5.AutoSize = true;
            this.checkBox5.Checked = true;
            this.checkBox5.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox5.Location = new System.Drawing.Point(115, 32);
            this.checkBox5.Name = "checkBox5";
            this.checkBox5.Size = new System.Drawing.Size(71, 17);
            this.checkBox5.TabIndex = 9;
            this.checkBox5.Text = "Медиана";
            this.checkBox5.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(442, 82);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(88, 13);
            this.label2.TabIndex = 6;
            this.label2.Text = "Max Water Level";
            // 
            // label_reconnect_count
            // 
            this.label_reconnect_count.AutoSize = true;
            this.label_reconnect_count.Location = new System.Drawing.Point(66, 101);
            this.label_reconnect_count.Name = "label_reconnect_count";
            this.label_reconnect_count.Size = new System.Drawing.Size(102, 13);
            this.label_reconnect_count.TabIndex = 7;
            this.label_reconnect_count.Text = "Reconnect count: 0";
            // 
            // textBox_max_water_level
            // 
            this.textBox_max_water_level.Location = new System.Drawing.Point(445, 97);
            this.textBox_max_water_level.Name = "textBox_max_water_level";
            this.textBox_max_water_level.ReadOnly = true;
            this.textBox_max_water_level.Size = new System.Drawing.Size(85, 20);
            this.textBox_max_water_level.TabIndex = 4;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(316, 82);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(85, 13);
            this.label1.TabIndex = 5;
            this.label1.Text = "Min Water Level";
            // 
            // checkBox4
            // 
            this.checkBox4.AutoSize = true;
            this.checkBox4.Checked = true;
            this.checkBox4.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox4.Location = new System.Drawing.Point(247, 97);
            this.checkBox4.Name = "checkBox4";
            this.checkBox4.Size = new System.Drawing.Size(34, 17);
            this.checkBox4.TabIndex = 4;
            this.checkBox4.Text = "%";
            this.checkBox4.UseVisualStyleBackColor = true;
            // 
            // textBox_min_water_level
            // 
            this.textBox_min_water_level.Location = new System.Drawing.Point(319, 98);
            this.textBox_min_water_level.Name = "textBox_min_water_level";
            this.textBox_min_water_level.ReadOnly = true;
            this.textBox_min_water_level.Size = new System.Drawing.Size(82, 20);
            this.textBox_min_water_level.TabIndex = 3;
            // 
            // button_wl
            // 
            this.button_wl.Location = new System.Drawing.Point(3, 3);
            this.button_wl.Name = "button_wl";
            this.button_wl.Size = new System.Drawing.Size(75, 23);
            this.button_wl.TabIndex = 0;
            this.button_wl.Text = "СТАРТ";
            this.button_wl.UseVisualStyleBackColor = true;
            // 
            // button_clear_wl
            // 
            this.button_clear_wl.Location = new System.Drawing.Point(165, 3);
            this.button_clear_wl.Name = "button_clear_wl";
            this.button_clear_wl.Size = new System.Drawing.Size(75, 23);
            this.button_clear_wl.TabIndex = 2;
            this.button_clear_wl.Text = "ОЧИСТИТЬ";
            this.button_clear_wl.UseVisualStyleBackColor = true;
            // 
            // checkBox3
            // 
            this.checkBox3.AutoSize = true;
            this.checkBox3.Checked = true;
            this.checkBox3.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox3.Location = new System.Drawing.Point(183, 97);
            this.checkBox3.Name = "checkBox3";
            this.checkBox3.Size = new System.Drawing.Size(51, 17);
            this.checkBox3.TabIndex = 3;
            this.checkBox3.Text = "uSec";
            this.checkBox3.UseVisualStyleBackColor = true;
            // 
            // button_wl_stop
            // 
            this.button_wl_stop.Enabled = false;
            this.button_wl_stop.Location = new System.Drawing.Point(84, 3);
            this.button_wl_stop.Name = "button_wl_stop";
            this.button_wl_stop.Size = new System.Drawing.Size(75, 23);
            this.button_wl_stop.TabIndex = 1;
            this.button_wl_stop.Text = "СТОП";
            this.button_wl_stop.UseVisualStyleBackColor = true;
            // 
            // WaterLevelSettings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(629, 441);
            this.Controls.Add(this.panel3);
            this.Name = "WaterLevelSettings";
            this.Text = "Настройки уровня воды";
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.CheckBox checkBox5;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label_reconnect_count;
        private System.Windows.Forms.TextBox textBox_max_water_level;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox checkBox4;
        private System.Windows.Forms.TextBox textBox_min_water_level;
        private System.Windows.Forms.Button button_wl;
        private System.Windows.Forms.Button button_clear_wl;
        private System.Windows.Forms.CheckBox checkBox3;
        private System.Windows.Forms.Button button_wl_stop;
        private System.Windows.Forms.TrackBar trackBar1;
    }
}