using System.Collections.Generic;
using System.Linq;
using System.Windows;
using ManchkinCore.GameLogic.Implementation.MainOutfit.Armor;
using ManchkinCore.GameLogic.Implementation.MainOutfit.Hats;
using ManchkinCore.GameLogic.Implementation.MainOutfit.Shoes;
using ManchkinCore.GameLogic.Implementation.MainOutfit.Weapons;
using ManchkinCore.GameLogic.Interfaces.Manchkin;
using ManchkinCore.GameLogic.Interfaces.Stuff;

namespace ManchkinGame.DialogWindows;

public partial class ShowStuffWindow : Window
{
    
    private IManchkin _manchkin;
    private List<IStuff> _variants;
    private string _typeOfVariants;
    
    public ShowStuffWindow()
    {
        InitializeComponent();
        _typeOfVariants = App.Current.Resources["TYPE_OF_VARIANTS"].ToString();
        _manchkin = App.Current.Resources["MANCHKIN"] as IManchkin;
        _variants = GetStuff();
        VariantsComboBox.Loaded += VariantsComboBoxLoad;
        CancelButton.Click += CancelButtonClick;
        ShowButton.Click += ShowButtonClick;
    }

    private void ShowButtonClick(object sender, RoutedEventArgs e)
    {
        if(VariantsComboBox.Text == "")
            UserMessage.CreateNotChosenItemMessage("шмотку, которую хочешь посмотреть");
        else
        {
            var stuff = _variants.FirstOrDefault(vari => vari.TextRepresentation == VariantsComboBox.Text);
            ShowStuff(stuff);
        }
    }

    private void VariantsComboBoxLoad(object sender, RoutedEventArgs e)
    {
        foreach (var variant in _variants)
            VariantsComboBox.Items.Add(variant.TextRepresentation);
    }
    
    private void CancelButtonClick(object sender, RoutedEventArgs e)
        => Close();

    private List<IStuff> GetStuff()
        => _typeOfVariants == "мелкие" ? _manchkin.SmallStuffs : _manchkin.HugeStuffs;
    
    private void ShowStuff(IStuff stuff)
    {
        Application.Current.Resources["STUFF"] = stuff;
        Application.Current.Resources["STUFF_TYPE"] = stuff switch
        {
            Armor => "броник",
            Hat => "головняк",
            Shoes => "обувка",
            Weapon => "оружие",
            _ => "просто шмотка"
        };
        DialogWindow.Show(new StuffWindow(), this);
    }
}