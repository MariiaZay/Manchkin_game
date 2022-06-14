using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Documents;
using ManchkinCore.GameLogic;
using ManchkinCore.GameLogic.Implementation;
using ManchkinCore.Implementation.Gears;
using ManchkinCore.Interfaces;

namespace ManchkinGame.DialogWindows;

public partial class LostStuffWindow : Window
{
    private IManchkin _manchkin;
    private List<IStuff> _variants;
    private string _typeOfVariants;
    
    public LostStuffWindow()
    {
        InitializeComponent();
        _typeOfVariants = App.Current.Resources["TYPE_OF_VARIANTS"].ToString();
        _manchkin = App.Current.Resources["MANCHKIN"] as IManchkin;
        _variants = GetStuff();
        VariantsComboBox.Loaded += VariantsComboBoxLoad;
        CancelButton.Click += CancelButtonClick;
        LostButton.Click += LostButtonClick;
        LostAllButton.Click += LostAllButtonClick;
    }

    private void LostAllButtonClick(object sender, RoutedEventArgs e)
    {
        while (_variants.Count != 0)
        {
            var stuff = _variants.Last();
            _manchkin.LostStuff(stuff);
            _variants.Remove(stuff);
        }
        Close();
    }

    private void LostButtonClick(object sender, RoutedEventArgs e)
    {
        if(VariantsComboBox.Text == "")
            UserMessage.CreateNotChosenItemMessage("шмотку, которую потеряешь");
        else
        {
            var stuff = _variants.FirstOrDefault(vari => vari.TextRepresentation == VariantsComboBox.Text);
            _manchkin.LostStuff(stuff);
            _variants = GetStuff();
            if(_variants.Count == 0)
                Close();
            else
            {
                VariantsComboBox.Items.Clear();
                RefreshComboBox();
            }
        }
    }

    private void CancelButtonClick(object sender, RoutedEventArgs e)
        => Close();

    private List<IStuff> GetStuff()
        => _typeOfVariants == "мелкие" ? _manchkin.SmallStuffs : _manchkin.HugeStuffs;

    private void VariantsComboBoxLoad(object sender, RoutedEventArgs e)
    {
        foreach (var variant in _variants)
            VariantsComboBox.Items.Add(variant.TextRepresentation);
    }

    private void RefreshComboBox()
    {
        foreach (var variant in _variants)
            VariantsComboBox.Items.Add(variant.TextRepresentation);
    }
}