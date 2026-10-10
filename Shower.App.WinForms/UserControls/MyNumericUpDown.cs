using System.ComponentModel;

namespace ShowerUI.UserControls;

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
