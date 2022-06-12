using System.Windows;

namespace ManchkinGame.DialogWindows;

public partial class AskingChangeWeaponWindow : Window
{
    public AskingChangeWeaponWindow()
    {
        InitializeComponent();
        SingleButton.Click += SingleButtonClick;
        BothButton.Click += BothButtonClick;
        CancelButton.Click += CancelButtonClick;
    }

    private void CancelButtonClick(object sender, RoutedEventArgs e)
    {
        App.Current.Resources["ANSWER"] = "";
        Close();
    }

    private void BothButtonClick(object sender, RoutedEventArgs e)
    {
        App.Current.Resources["ANSWER"] = "BOTH";
        Close();
    }

    private void SingleButtonClick(object sender, RoutedEventArgs e)
    {
        App.Current.Resources["ANSWER"] = "SINGLE";
        Close();
    }
}