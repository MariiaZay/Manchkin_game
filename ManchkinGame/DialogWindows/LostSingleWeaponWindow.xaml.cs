using System.Windows;
using System.Windows.Controls;
using ManchkinCore.GameLogic.Interfaces.Manchkin;
using ManchkinCore.GameLogic.Interfaces.Stuff;

namespace ManchkinGame.DialogWindows;

public partial class LostSingleWeaponWindow : Window
{
    private IManchkin _manchkin;
    public LostSingleWeaponWindow()
    {
        InitializeComponent();
        _manchkin = App.Current.Resources["MANCHKIN"] as IManchkin;
        OkButton.Click += OkButtonClick;
        LostAllButton.Click += LostAllButtonClick;

        LeftStuffButton.Click += LeftStuffButtonClick;
        LeftLostButton.Click += LeftLostButtonClick;

        RightStuffButton.Click += RightStuffButtonClick;
        RightLostButton.Click += RightLostButtonClick;

    }

    private void RightStuffButtonClick(object sender, RoutedEventArgs e)
        => ShowStuff(_manchkin.Hands.RightHand);


    private void RightLostButtonClick(object sender, RoutedEventArgs e)
        => Lost(_manchkin.Hands.RightHand, RightLostButton, _manchkin.Hands.LeftHand);


    private void LeftLostButtonClick(object sender, RoutedEventArgs e)
        => Lost(_manchkin.Hands.LeftHand, LeftLostButton, _manchkin.Hands.RightHand);

    private void LeftStuffButtonClick(object sender, RoutedEventArgs e)
        => ShowStuff(_manchkin.Hands.LeftHand);

    private void LostAllButtonClick(object sender, RoutedEventArgs e)
    {
        if(_manchkin.Hands.LeftHand != null)
            _manchkin.LostStuff(_manchkin.Hands.LeftHand);
        if(_manchkin.Hands.RightHand != null)
            _manchkin.LostStuff(_manchkin.Hands.RightHand);
        Close();
    }

    private void OkButtonClick(object sender, RoutedEventArgs e)
        => Close();
    
    private void ShowStuff(IStuff stuff)
    {
        if(stuff == null)
            UserMessage.CreateEmptyStuffMessage();
        else
        {
            Application.Current.Resources["STUFF"] = stuff;
            Application.Current.Resources["STUFF_TYPE"] = "оружие";
            DialogWindow.Show(new StuffWindow(), this);
        }
    }

    private void Lost(IStuff? current, Button button, IStuff? opposite)
    {
        if(current == null)
            UserMessage.CreateAlreadyLostWeapon();
        else
        {
            _manchkin.LostStuff(current);
            button.Style = (Style) FindResource("RoundedNotActiveRedButtonStyle");
            if(opposite == null)
                Close();
        }
    }
}