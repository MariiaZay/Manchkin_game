using System.Windows;
using ManchkinCore.Interfaces;

namespace ManchkinGame.DialogWindows;

public partial class BothWeaponWindow : Window
{
    private IStuff _right;
    private IStuff _left;
    
    public BothWeaponWindow()
    {
        InitializeComponent();
        _right = App.Current.Resources["RIGHT"] as IStuff;
        _left = App.Current.Resources["LEFT"] as IStuff;
        SetParameters();
        OkButton.Click += OkButtonClick;
    }

    private void OkButtonClick(object sender, RoutedEventArgs e) => Close();

    private void SetParameters()
    {
        LeftNameLabel.Text = _left.TextRepresentation;
        RightNameLabel.Text = _right.TextRepresentation;

        LeftDamageLabel.Text = _left.Damage.ToString();
        RightDamageLabel.Text = _right.Damage.ToString();

        LeftPriceLabel.Text = _left.Price.ToString();
        RightPriceLabel.Text = _right.Price.ToString();

        LeftWeightLabel.Text = _left.Weight.ToString();
        RightWeightLabel.Text = _right.Weight.ToString();

        LeftCheatLabel.Text = _left.Cheat ? "активен" : "неактивен";
        RightCheatLabel.Text = _right.Cheat ? "активен" : "неактивен";

        LeftFlushingLabel.Text = _left.FlushingBonus.ToString();
        RightFlushingLabel.Text = _right.FlushingBonus.ToString();

        //TODO: доработть отображение рас, классв и гендеров
    }
}