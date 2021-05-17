using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ShowerUI.UserControls
{
    public partial class MyNumericUpDown : NumericUpDown
    {
        public MyNumericUpDown()
        {
            InitializeComponent();
        }

        public MyNumericUpDown(IContainer container)
        {
            container.Add(this);

            InitializeComponent();
        }

        public void UpdateText()
        {
            BeginInit();
            EndInit();
        }
    }
}
