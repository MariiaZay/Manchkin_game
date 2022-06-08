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
            "оружие" => _variants = GetHugeWeapon()
        };
        VariantsComboBox.Loaded += VariantsComboBoxLoaded;
        
        WearingButton.Click += WearingButtonClick;
        CancelButton.Click += CancelButtonClick;
        CheatButton.Click += CheatButtonClick;
    }

    private List<IDescriptable> GetHugeWeapon()
        => (from variant in CardsBase.Weapons let v = variant as IStuff where v.Fullness == Arms.BOTH select variant).ToList();
    

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
        if(VariantsComboBox.Text == "")
            UserMessage.CreateNotChosenItemMessage("новую шмотку");
        else
        {
            var variant = _variants.FirstOrDefault(vari => vari.TextRepresentation == VariantsComboBox.Text);
            var v = variant as IStuff;
            
            if (ReferenceEquals(CheatButton.Content, "НЕ ЧИТ!"))
                _manchkin.UseCheat(v);
            
            if (!_manchkin.TakeStuff(v)) //TODO: здесь работает неправилно
                UserMessage.CreateImpossibleTakingStuffMessage();
            else
            {
                Application.Current.Resources["NEW"] = v;
                Close();
            }
        }
    }

    private void VariantsComboBoxLoaded(object sender, RoutedEventArgs e)
    {
        foreach (var variant in _variants.Where(variant => _current != variant.TextRepresentation))
            VariantsComboBox.Items.Add(variant.TextRepresentation);
    }


    private void CancelButtonClick(object sender, RoutedEventArgs e)
    {
        Application.Current.Resources["NEW"] = null;
        Close();
    }
}