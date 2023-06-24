using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics.CodeAnalysis;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ShowerUI.UserControls
{
    public partial class WiFiPowerControl : EditText
    {
        private const byte MinValue = 40;
        private const byte MaxValue = 82;
        
        public WiFiPowerControl()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Возвращает <see cref="byte"/> от 40 до 82.
        /// </summary>
        public byte Power => (byte)Value!;

        /// <summary>
        /// Возвращает <see cref="byte"/> от 40 до 82.
        /// </summary>
        public override object? Value
        {
            get => base.Value switch
            {
                null => null,
                string s => ParseValidatedText(s),
                _ => throw new NotSupportedException()
            };
            set
            {
                switch (value)
                {
                    case null:
                        base.Value = null;
                        break;
                    case byte bt:
                    {
                        // Значение должно быть в диаппазоне.
                        if (bt < MinValue || bt > MaxValue)
                        {
                            throw new ArgumentOutOfRangeException(nameof(value), $"Значение должно быть в диаппазоне от {MinValue} до {MaxValue}");
                        }

                        double dbmValue = bt * 0.25;

                        base.Value = dbmValue.ToString(CultureInfo.InvariantCulture);
                        break;
                    }
                    default:
                        throw new NotSupportedException();
                }
            }
        }

        protected override void ValidatingText(object sender, CancelEventArgs e)
        {
            base.ValidatingText(sender, e);

            if (ValidateWiFiPower(textBox1.Text, out var errorMsg, out _))
            {
                errorProvider1.SetError(textBox1, null);
            }
            else
            {
                //e.Cancel = true; // Лучше не отменять - не даст покинуть фокус контрола.
                errorProvider1.SetError(textBox1, errorMsg);
            }
        }

        private byte ParseValidatedText(string value, [CallerMemberName] string? propertyName = null)
        {
            if (ValidateWiFiPower(value, out var errorMsg, out var rawValue))
            {
                if (rawValue != null)
                {
                    return rawValue.Value;
                }
                throw new ArgumentOutOfRangeException(propertyName);
            }
            else
            {
                throw new ArgumentOutOfRangeException(propertyName, errorMsg);
            }
        }

        private bool ValidateWiFiPower(string value, [MaybeNullWhen(true)] out string? errorMsg, [MaybeNullWhen(false)] out byte? rawValue)
        {
            if (double.TryParse(value, NumberStyles.AllowDecimalPoint, CultureInfo.InvariantCulture, out double dValue))
            {
                if (dValue % 0.25 != 0)
                {
                    errorMsg = "Число должно быть кратно 0.25";
                    rawValue = default;
                    return false;
                }

                rawValue = (byte)(dValue / 0.25);

                if (rawValue < MinValue || rawValue > MaxValue)
                {
                    errorMsg = "Число должно быть в диаппазоне от 10 до 20.5";
                    return false;
                }

                errorMsg = null;
                return true;
            }
            else
            {
                errorMsg = null;
                rawValue = default;
                return true;
            }
        }
    }
}
