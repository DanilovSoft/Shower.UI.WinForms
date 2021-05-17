using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace ShowerUI.UserControls
{
    public partial class WiFiPowerControl : UserControl
    {
        private byte? _origValue;

        public WiFiPowerControl()
        {
            InitializeComponent();
        }

        public bool HasChanges
        {
            get
            {
                return false;
            }
        }

        public byte? Value
        {
            get
            {
                var v = (double)wifiPower.Value / 0.25;
                if (v % 1 != 0)
                {
                    throw new NotSupportedException("Число должно было получиться целым");
                }

                byte value = (byte)v;
                return value;
            }
            set
            {
                if (value != null)
                {
                    var v = value.Value * 0.25m;
                    wifiPower.Value = v;
                    wifiPower.UpdateText();
                }
                else
                {
                    wifiPower.ResetText();
                }
            }
        }

        [Bindable(true)]
        [SettingsBindable(true)]
        public string TextBoxHint
        {
            get => toolTip1.GetToolTip(wifiPower);
            set
            {
                toolTip1.SetToolTip(wifiPower, value);
                toolTip1.SetToolTip(label1, value);
            }
        }

        public void ResetHasChanges()
        {

        }

        private void WifiPower_ValueChanged(object sender, EventArgs e)
        {
            
        }

        public void ResetValue()
        {
            wifiPower.ResetText();
        }

        //public void ResetText()
        //{
        //    wifiPower.ResetText();
        //}
    }
}
