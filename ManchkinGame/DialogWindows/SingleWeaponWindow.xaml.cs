using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using ManchkinCore;
using ManchkinCore.GameLogic;
using ManchkinCore.GameLogic.Implementation;
using ManchkinCore.Implementation;
using ManchkinCore.Interfaces;

namespace ManchkinGame.DialogWindows;

public partial class SingleWeaponWindow : Window
{
    private List<IDescriptable> _variants;
    private string _currentRight;
    private string _currentLeft;
    private IManchkin _manchkin;

    public SingleWeaponWindow()
    {
        InitializeComponent();
        _manchkin = App.Current.Resources["MANCHKIN"] as Manchkin;
        _variants = CardsBase.SingleHandWeapons;
        _currentRight = Application.Current.Resources["CURRENT_RIGHT_HAND"].ToString();
        _currentLeft = Application.Current.Resources["CURRENT_LEFT_HAND"].ToString();

        LeftVariantsComboBox.Loaded += LeftVariantsComboBoxStartLoaded;
        RightVariantsComboBox.Loaded += RightVariantsComboBoxStartLoaded;

        LeftCheatButton.Click += LeftCheatButtonClick;
        RightCheatButton.Click += RightCheatButtonClick;

        WearingButton.Click += WearingButtonClick;
        CancelButton.Click += CancelButtonClick;
    }

    private void WearingButtonClick(object sender, RoutedEventArgs e)
    {
        if (LeftVariantsComboBox.Text == "" && RightVariantsComboBox.Text == "")
            UserMessage.CreateNotChosenItemMessage("оружия");
        else if (LeftVariantsComboBox.Text == RightVariantsComboBox.Text)
            UserMessage.CreateOneWeaponInBothHandsMessage();
        //TODO: дописать функционал

    }

    private void RightCheatButtonClick(object sender, RoutedEventArgs e)
    {
        RightCheatButton.Content = ReferenceEquals(RightCheatButton.Content, "НЕ ЧИТ!") ? "ЧИТ" : "НЕ ЧИТ!";

        if ((string) RightCheatButton.Content == "НЕ ЧИТ!")
            RightCheatButton.Style = (Style) FindResource("RoundedRedButtonStyle");
        else
            RightCheatButton.Style = (Style) FindResource("RoundedGreenButtonStyle");
    }


    private void LeftCheatButtonClick(object sender, RoutedEventArgs e)
    {
        LeftCheatButton.Content = ReferenceEquals(LeftCheatButton.Content, "НЕ ЧИТ!") ? "ЧИТ" : "НЕ ЧИТ!";

        if ((string) LeftCheatButton.Content == "НЕ ЧИТ!")
            LeftCheatButton.Style = (Style) FindResource("RoundedRedButtonStyle");
        else
            LeftCheatButton.Style = (Style) FindResource("RoundedGreenButtonStyle");
    }

    private void LeftVariantsComboBoxStartLoaded(object sender, RoutedEventArgs e)
    {
        foreach (var variant in _variants
                     .Where(variant => _currentLeft != variant.TextRepresentation
                                       && _currentRight != variant.TextRepresentation))
            LeftVariantsComboBox.Items.Add(variant.TextRepresentation);

        LeftVariantsComboBox.Items.Add(new EmptyWeapon().TextRepresentation);
        LeftVariantsComboBox.Text = _currentLeft;
    }

    private void RightVariantsComboBoxStartLoaded(object sender, RoutedEventArgs e)
    {
        foreach (var variant in _variants
                     .Where(variant => _currentLeft != variant.TextRepresentation
                                       && _currentRight != variant.TextRepresentation))
            RightVariantsComboBox.Items.Add(variant.TextRepresentation);
        RightVariantsComboBox.Items.Add(new EmptyWeapon().TextRepresentation);
        RightVariantsComboBox.Text = _currentRight;
    }


    private void CancelButtonClick(object sender, RoutedEventArgs e)
    {
        Application.Current.Resources["NEW_RIGHT"] = null;
        Application.Current.Resources["NEW_LEFT"] = null;
        Close();
    }

}