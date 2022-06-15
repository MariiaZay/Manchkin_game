using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Documents;
using ManchkinCore.Implementation;
using ManchkinCore.Interfaces;

namespace ManchkinGame.DialogWindows;

public partial class SellWindow : Window
{
    private IManchkin _manchkin;
    private List<IStuff> _variants;
    private int _price;
    public SellWindow()
    {
        InitializeComponent();
        _manchkin = App.Current.Resources["MANCHKIN"] as IManchkin;
        
        _variants = _manchkin.SmallStuffs;
        _variants.AddRange(_manchkin.HugeStuffs);
        if (_manchkin.Race is Halfling && _manchkin.DoublePrice)
            SellDoublePriceButton.Style = (Style) FindResource("RoundedGreenButtonStyle");

        ShowStuffButton.Click += ShowStuffButtonClick;
        StuffComboBox.Loaded += StuffComboBoxLoaded;
        SellAllButton.Click += SellAllButtonClick;
        CancelButton.Click += CancelButtonClick;
    }

    private void SellAllButtonClick(object sender, RoutedEventArgs e)
    {
        _manchkin.GetLevel(_manchkin.SellStuffs(_variants));
        Close();
    }

    private void ShowStuffButtonClick(object sender, RoutedEventArgs e)
    {
        if(StuffComboBox.Text == "")
            UserMessage.CreateNotChosenItemMessage("шмотку для просмотра");
        else
        {
            var variant = _variants.FirstOrDefault(vari => vari.TextRepresentation == StuffComboBox.Text);
            ShowStuff(variant);
        }
    }

    private void StuffComboBoxLoaded(object sender, RoutedEventArgs e)
    {
        foreach (var variant in _variants)
            StuffComboBox.Items.Add(variant.TextRepresentation);
    }
    
    private void RefreshComboBox()
    {
        StuffComboBox.Items.Clear();
        foreach (var variant in _variants)
            StuffComboBox.Items.Add(variant.TextRepresentation);
    }
    
    private void ShowStuff(IStuff stuff)
    {
        if(stuff == null)
            UserMessage.CreateEmptyStuffMessage();
        else
        {
            Application.Current.Resources["STUFF"] = stuff;
            Application.Current.Resources["STUFF_TYPE"] = stuff switch
            {
                Armor => "броник",
                Shoes => "обувка",
                Weapon => "оружие",
                Hat => "головняк",
                _ => "просто шмотка"
            };
            DialogWindow.Show(new StuffWindow(), this);
        }
    }
    private void CancelButtonClick(object sender, RoutedEventArgs e)
        => Close();
}