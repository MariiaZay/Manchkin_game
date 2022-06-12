using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
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
    private string _currentRightText;
    private string _currentLeftText;
    private IStuff? _currentLeft;
    private IStuff? _currentRight;
    private IManchkin _manchkin;

    public SingleWeaponWindow()
    {
        InitializeComponent();
        _manchkin = App.Current.Resources["MANCHKIN"] as Manchkin;
        _variants = CardsBase.SingleHandWeapons;

        _currentRight = Application.Current.Resources["CURRENT_RIGHT_HAND"] as IStuff;
        _currentRightText = _currentRight == null ? "" : _currentRight.TextRepresentation;

        _currentLeft = Application.Current.Resources["CURRENT_LEFT_HAND"] as IStuff;
        _currentLeftText = _currentLeft == null ? "" : _currentLeft.TextRepresentation;

        LeftVariantsComboBox.Loaded += LeftVariantsComboBoxStartLoaded;
        RightVariantsComboBox.Loaded += RightVariantsComboBoxStartLoaded;

        LeftCheatButton.Click += LeftCheatButtonClick;
        RightCheatButton.Click += RightCheatButtonClick;

        WearingButton.Click += WearingButtonClick;
        CancelButton.Click += CancelButtonClick;
    }

    private void WearingButtonClick(object sender, RoutedEventArgs e)
    {
        //TODO: еще поработать на взаимодействием и сменой оружия
        if (LeftVariantsComboBox.Text == "" && RightVariantsComboBox.Text == "")
            UserMessage.CreateNotChosenItemMessage("оружия");
        else if (LeftVariantsComboBox.Text == RightVariantsComboBox.Text)
            UserMessage.CreateOneWeaponInBothHandsMessage();
        else
        {
            var ok = true;
            if (LeftVariantsComboBox.Text != "")
            {
                var change = true;
                
                if (_currentLeftText != "")
                    change = UserMessage
                        .CreateChangeSingleWeaponMessage(_currentLeftText, LeftVariantsComboBox.Text, "левую руку");
                if (change)
                {
                    var stuff = ProcessChoice(LeftVariantsComboBox, LeftCheatButton);
                    if (!_manchkin.TakeSingleWeaponLeftHand(stuff))
                    {
                        UserMessage.CreateImpossibleTakingStuffMessage();
                        ok = false;
                    }
                }
            }

            if (RightVariantsComboBox.Text != "")
            {
                var change = true;
                if (_currentRightText != "")
                    change = UserMessage
                        .CreateChangeSingleWeaponMessage(_currentRightText, RightVariantsComboBox.Text, "правую руку");
                
                if(change)
                {
                    var stuff = ProcessChoice(RightVariantsComboBox, RightCheatButton);
                    if (!_manchkin.TakeSingleWeaponRightHand(stuff))
                    {
                        UserMessage.CreateImpossibleTakingStuffMessage();
                        ok = false;
                    }
                }
            }

            if (ok)
                Close();
        }
    }

    private IStuff ProcessChoice(ComboBox comboBox, Button cheatButton)
    {
        var variant = _variants.FirstOrDefault(vari => vari.TextRepresentation == comboBox.Text);
        var v = variant as IStuff;
        if (ReferenceEquals(cheatButton.Content, "НЕ ЧИТ!") && v != null)
            _manchkin.UseCheat(v);
        return v;
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
                     .Where(variant => _currentLeftText != variant.TextRepresentation
                                       && _currentRightText != variant.TextRepresentation))
            LeftVariantsComboBox.Items.Add(variant.TextRepresentation);

        LeftVariantsComboBox.Items.Add(new EmptyWeapon().TextRepresentation);
    }

    private void RightVariantsComboBoxStartLoaded(object sender, RoutedEventArgs e)
    {
        foreach (var variant in _variants
                     .Where(variant => _currentLeftText != variant.TextRepresentation
                                       && _currentRightText != variant.TextRepresentation))
            RightVariantsComboBox.Items.Add(variant.TextRepresentation);
        RightVariantsComboBox.Items.Add(new EmptyWeapon().TextRepresentation);
    }


    private void CancelButtonClick(object sender, RoutedEventArgs e)
    {
        Application.Current.Resources["NEW_RIGHT"] = null;
        Application.Current.Resources["NEW_LEFT"] = null;
        Close();
    }
}