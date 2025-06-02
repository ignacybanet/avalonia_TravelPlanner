using Avalonia.Controls;

namespace avalonia_TravelPlanner;

public partial class FormResult : Window
{
    public FormResult()
    {
        InitializeComponent();
    }

    public void SetData(string data)
    {
        var submittedDataTextBlock = this.Find<TextBlock>("ResultText");
        submittedDataTextBlock.Text = data;
    }
}