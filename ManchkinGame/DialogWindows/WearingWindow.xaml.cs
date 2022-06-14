using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using ManchkinCore;
using ManchkinCore.GameLogic;
using ManchkinCore.GameLogic.Implementation;
using ManchkinCore.Interfaces;

namespace ManchkinGame.DialogWindows;

public partial class WearingWindow : Window
{
    private string _typeOfVariants;
    private List<IDescriptable> _variants;
    private string _current;
    private IManchkin _manchkin;

    public WearingWindow()
    {
        InitializeComponent();
        _typeOfVariants = App.Current.Resources["TYPE_OF_VARIANTS"].ToString();
        _current = App.Current.Resources["CURRENT"].ToString();
        _manchkin = App.Current.Resources["MANCHKIN"] as Manchkin;

        _variants = _typeOfVariants switch
        {
            "броник" => _variants = CardsBase.Armors,
            "обувка" => _variants = CardsBase.Shoeses,
            "головняк" => _variants = CardsBase.Hats,
            "мелкие шмотки" => _variants = CardsBase.SmallStuffs,
            "крупные шмотки" => _variants = CardsBase.HugeStuffs,
            "оружие" => _variants = CardsBase.BothHandWeapons
        };

        if (_typeOfVariants != "мелкие шмотки" && _typeOfVariants != "крупные шмотки")
            VariantsComboBox.Loaded += VariantsComboBoxLoadedEquipment;
        else
            VariantsComboBox.Loaded += VariantsComboBoxLoadedStuff;

        WearingButton.Click += WearingButtonClick;
        CancelButton.Click += CancelButtonClick;
        CheatButton.Click += CheatButtonClick;
    }


    private void CheatButtonClick(object sender, RoutedEventArgs e)
    {
        CheatButton.Content = ReferenceEquals(CheatButton.Content, "НЕ ЧИТ!") ? "ЧИТ" : "НЕ ЧИТ!";

        if ((string) CheatButton.Content == "НЕ ЧИТ!")
            CheatButton.Style = (Style) FindResource("RoundedRedButtonStyle");
        else
            CheatButton.Style = (Style) FindResource("RoundedGreenButtonStyle");
    }

    private void WearingButtonClick(object sender, RoutedEventArgs e)
    {
        if (VariantsComboBox.Text == "")
            UserMessage.CreateNotChosenItemMessage("новую шмотку");
        else if (AskBeforeChanging())
        {
            var variant = _variants.FirstOrDefault(vari => vari.TextRepresentation == VariantsComboBox.Text);
            var v = variant as IStuff;

            if (ReferenceEquals(CheatButton.Content, "НЕ ЧИТ!"))
                _manchkin.UseCheat(v);

            if (!_manchkin.TakeStuff(v))
                UserMessage.CreateImpossibleTakingStuffMessage(v.TextRepresentation);
            else
                Close();
        }
    }

    private bool AskBeforeChanging()
    {
        bool ok;
        switch (_typeOfVariants)
        {
            case "броник":
            case "обувка":
            case "головняк":
                if (_current == "")
                    ok = true;
                else
                    ok = UserMessage.CreateChangeEquipmentMessage(_current, VariantsComboBox.Text);
                break;
            case "оружие":
                if (_manchkin.Hands.LeftHand == null && _manchkin.Hands.RightHand == null)
                    ok = true;
                else
                    ok = UserMessage.CreateChangeBothWeaponMessage(VariantsComboBox.Text);
                break;
            default:
                ok = true;
                break;
        }

        return ok;
    }

    private void VariantsComboBoxLoadedEquipment(object sender, RoutedEventArgs e)
    {
        foreach (var variant in _variants.Where(variant => _current != variant.TextRepresentation))
            VariantsComboBox.Items.Add(variant.TextRepresentation);
    }

    private void VariantsComboBoxLoadedStuff(object sender, RoutedEventArgs e)
    {
        var manchkinStuff = _typeOfVariants == "мелкие шмотки" ? _manchkin.SmallStuffs : _manchkin.HugeStuffs;

        foreach (var variant in from variant in _variants 
                 let v = variant as IStuff where !manchkinStuff.Contains(v) select variant)
        {
            VariantsComboBox.Items.Add(variant.TextRepresentation);
        }
    }


    private void CancelButtonClick(object sender, RoutedEventArgs e)
    {
        Application.Current.Resources["NEW"] = null;
        Close();
    }
}