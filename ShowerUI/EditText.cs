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
        private readonly static Color _hasChangedColor = Color.FromArgb(0, 0, 255);
        private readonly Color _labelColor;
        private Type _valueType;
        /// <summary>
        /// Измененное значение
        /// </summary>
        private object _changedValue;
        /// <summary>
        /// Изначальное значение
        /// </summary>
        private object _origValue;
        /// <summary>
        /// Изначальное текстовое значение
        /// </summary>
        private string _origTextValue;

        public EditText()
        {
            InitializeComponent();
            _labelColor = label.ForeColor;
        }

        public string Caption
        {
            get => label.Text;
            set => label.Text = value;
        }

        public object Value
        {
            get => GetValue();
            set
            {
                _changedValue = _origValue = value;
                _valueType = value?.GetType();
                _origTextValue = value?.ToString();
                textBox.Text = _origTextValue;
                ResetHasChanges();
            }
        }

        public bool HasChanges { get; private set; }

        private object GetValue()
        {
            return _changedValue;
        }

        public T GetValue<T>()
        {
            if (_changedValue is T)
            {
                return (T)_changedValue;
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
            label.ForeColor = _hasChangedColor;
        }

        private void textBox1_Validating(object sender, CancelEventArgs e)
        {
            if(_origValue != null)
            {
                try
                {
                    _changedValue = Convert.ChangeType(textBox.Text, _valueType);
                }
                catch
                {
                    errorProvider1.SetError(textBox, $"Значение не соответствует типу {_valueType.Name}");
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

        private void textBox_TextChanged(object sender, EventArgs e)
        {
            if (textBox.Text == _origTextValue)
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
