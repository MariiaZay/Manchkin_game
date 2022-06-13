using System.Windows;
using ManchkinCore.Interfaces;

namespace ManchkinGame.DialogWindows;

public partial class AskingExtraWindow : Window
{
    private string _extraType;
    private IManchkin _manchkin;

    public AskingExtraWindow()
    {
        InitializeComponent();
        _extraType = App.Current.Resources["EXTRA_TYPE"].ToString();
        _manchkin = App.Current.Resources["MANCHKIN"] as IManchkin;
        switch (_extraType)
        {
            case "halfblood":
                TitleLabel.Text = "Какой тип полукровки?";
                ExtraButton.Content = "ДОПОЛНИТЕЛЬНАЯ РАСА";
                CleanButton.Content = "ЧИСТАЯ";
                App.Current.Resources["TYPE_OF_VARIANTS"] = "расу";
                App.Current.Resources["CURRENT"] = _manchkin.Race.TextRepresentation;
                break;
            case "super":
                TitleLabel.Text = "Какой тип суперманчкина?";
                ExtraButton.Content = "ДОПОЛНИТЕЛЬНАЯ КЛАСС";
                CleanButton.Content = "ЧИСТЫЙ";
                App.Current.Resources["TYPE_OF_VARIANTS"] = "класс";
                App.Current.Resources["CURRENT"] = _manchkin.Class.TextRepresentation;
                break;
        }

        CancelButton.Click += CancelButtonClick;
        CleanButton.Click += CleanButtonClick;
        ExtraButton.Click += ExtraButtonClick;
    }

    private void ExtraButtonClick(object sender, RoutedEventArgs e)
    {
        App.Current.Resources["EXTRA"] = true;
        DialogWindow.Show(new ChooseWindow(), this);
        var newRace = App.Current.Resources["NEW"] as IRace;
        var newClass = App.Current.Resources["NEW"] as IClass;
        if (newRace != null)
            _manchkin.BecameHalfBlood(newRace);
        if (newClass != null)
            _manchkin.BecameSuperManchkin(newClass);
        if ((bool) App.Current.Resources["OK"])
            Close();
    }

    private void CleanButtonClick(object sender, RoutedEventArgs e)
    {
        if (_extraType == "halfblood")
            _manchkin.BecameHalfBlood();
        else
            _manchkin.BecameSuperManchkin();
        Close();
    }

    private void CancelButtonClick(object sender, RoutedEventArgs e)
        => Close();
}