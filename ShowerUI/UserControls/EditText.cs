using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ShowerUI
{
    public partial class EditText : UserControl
    {
        private readonly static Color HasChangedColor = Color.FromArgb(255, 0, 0);
        private readonly Color _labelColor;
        private Type? _valueType;
        /// <summary>
        /// Измененное значение
        /// </summary>
        private object? _changedValue;
        /// <summary>
        /// Изначальное значение
        /// </summary>
        private object? _origValue;
        /// <summary>
        /// Изначальное текстовое значение
        /// </summary>
        private string? _origTextValue;

        public EditText()
        {
            InitializeComponent();
            _labelColor = label.ForeColor;
        }

        public bool HasChanges { get; private set; }

        public string TextBoxHint
        {
            get => toolTip1.GetToolTip(textBox1);
            set
            {
                toolTip1.SetToolTip(textBox1, value);
                toolTip1.SetToolTip(label, value);
            }
        }

        public string Caption
        {
            get => label.Text;
            set => label.Text = value;
        }

        public object? Value
        {
            get => GetValue();
            set
            {
                _changedValue = _origValue = value;
                _valueType = value?.GetType();
                _origTextValue = value?.ToString();
                textBox1.Text = _origTextValue;
                ResetHasChanges();
            }
        }

        private object? GetValue()
        {
            return _changedValue;
        }

        public T GetValue<T>()
        {
            if (_changedValue is T t)
            {
                return t;
            }
            else
            {
                return (T)Convert.ChangeType(_changedValue, typeof(T));
            }
        }

        public void ResetHasChanges()
        {
            InnerResetChanges();

            _origValue = _changedValue;
            _origTextValue = _origValue?.ToString();
        }

        private void InnerResetChanges()
        {
            HasChanges = false;
            label.ForeColor = _labelColor;
        }

        private void InnerHasChanges()
        {
            HasChanges = true;
            label.ForeColor = HasChangedColor;
        }

        private void TextBox1_Validating(object sender, CancelEventArgs e)
        {
            if (_origValue != null)
            {
                try
                {
                    _changedValue = Convert.ChangeType(textBox1.Text, _valueType);
                }
                catch
                {
                    errorProvider1.SetError(textBox1, $"Значение не соответствует типу {_valueType.Name}");
                    e.Cancel = true;
                    return;
                }

                errorProvider1.Clear();

                if(_origValue.Equals(_changedValue))
                {
                    InnerResetChanges();
                }
                else
                {
                    InnerHasChanges();
                }
            }
        }

        private void TextBox_TextChanged(object sender, EventArgs e)
        {
            if (textBox1.Text == _origTextValue)
            {
                InnerResetChanges();
            }
            else
            {
                InnerHasChanges();
            }
        }
    }
}
