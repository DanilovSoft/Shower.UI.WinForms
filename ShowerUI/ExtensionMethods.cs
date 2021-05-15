using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ShowerUI
{
    public static class ExtensionMethods
    {
        public static float? TryParse(string value)
        {
            return float.TryParse(value, out float f) ? f : (float?)null;
        }

        public static void BoldText(this Label label)
        {
            label.Font = new Font(label.Font, FontStyle.Bold);
        }

        public static void RegularText(this Label label)
        {
            if (label.Font.Bold)
            {
                label.Font = new Font(label.Font, FontStyle.Regular);
            }
        }
    }
}
