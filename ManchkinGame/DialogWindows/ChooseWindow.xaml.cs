using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Documents;
using ManchkinCore.GameLogic;
using ManchkinCore.Interfaces;
using Microsoft.VisualBasic;

namespace ManchkinGame;

public partial class ChooseWindow : Window
{
    private string _typeOfVariants;
    private List<IDescriptable> _variants;
    private string _current;
    public ChooseWindow()
    {
        InitializeComponent();
        _typeOfVariants = App.Current.Resources["TYPE_OF_VARIANTS"].ToString();
        _current = App.Current.Resources["CURRENT"].ToString();

        _variants = _typeOfVariants switch
        {
            "расу" => _variants = CardsBase.Races,
            "класс" => _variants = CardsBase.Classes
        };
        ChooseBlock.Text = String.Format("Выберите {0}", _typeOfVariants);
        VariantsComboBox.Loaded += VariantsComboBoxLoaded;

        ApplyButton.Click += ApplyButtonClick;
        CancelButton.Click += CancelButtonClick;
    }

    

    private void VariantsComboBoxLoaded(object sender, RoutedEventArgs e)
    {
        foreach (var variant in 
                 _variants.Where(variant => _current != variant.TextRepresentation))
        {
            VariantsComboBox.Items.Add(variant.TextRepresentation);
        }
    }
    
    private void ApplyButtonClick(object sender, RoutedEventArgs e)
    {
        if(VariantsComboBox.Text == "")
            UserMessage.CreateNotChosenItemMessage("новую рассу");
        foreach (var variant in _variants.Where(variant => variant.TextRepresentation == VariantsComboBox.Text))
        {
            App.Current.Resources["NEW"] = variant;
            break;
        }
        Close();
    }
    
    private void CancelButtonClick(object sender, RoutedEventArgs e)
    {
        App.Current.Resources["NEW"] = null;
        Close();
    }
}