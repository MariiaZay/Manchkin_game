using System.Collections.Generic;
using System.Windows;
using System.Windows.Documents;
using ManchkinCore.Implementation;
using ManchkinCore.Interfaces;

namespace ManchkinGame.DialogWindows;

public partial class SellWindow : Window
{
    private IManchkin _manchkin;
    private List<IStuff> _variants;
    public SellWindow()
    {
        InitializeComponent();
        _manchkin = App.Current.Resources["MANCHKIN"] as IManchkin;
        
        _variants = _manchkin.SmallStuffs;
        _variants.AddRange(_manchkin.HugeStuffs);
        if (_manchkin.Race is Halfling && _manchkin.DoublePrice)
            SellDoublePriceButton.Style = (Style) FindResource("RoundedGreenButtonStyle");
            
        
        StuffComboBox.Loaded += StuffComboBoxLoaded;
        CancelButton.Click += CancelButtonClick;
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
    
    private void CancelButtonClick(object sender, RoutedEventArgs e)
        => Close();
}