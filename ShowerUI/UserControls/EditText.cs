﻿using System.ComponentModel;
using System.Globalization;

namespace ShowerUI;

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

    //[Browsable(true), EditorBrowsable(EditorBrowsableState.Always), Bindable(true)]
    //[DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    public string Caption
    {
        get => label.Text;
        set => label.Text = value;
    }

    public virtual object? Value
    {
        get => GetValue();
        set
        {
            _changedValue = _origValue = value;
            _valueType = value?.GetType();
            _origTextValue = Convert.ToString(value, CultureInfo.InvariantCulture);
            textBox1.Text = _origTextValue;
            ResetHasChanges();
        }
    }

    protected virtual void ValidatingText(object sender, CancelEventArgs e)
    {
        if (_origValue != null && _valueType != null)
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

            errorProvider1.SetError(textBox1, null);

            if (_origValue.Equals(_changedValue))
            {
                InnerResetChanges();
            }
            else
            {
                InnerHasChanges();
            }
        }
    }

    protected virtual void ValidatedText(object sender, EventArgs e)
    {
        errorProvider1.SetError(textBox1, null);
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
            return (T)Convert.ChangeType(_changedValue, typeof(T), CultureInfo.InvariantCulture);
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
        ValidatingText(sender, e);
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
