using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using ManchkinCore.GameLogic.Interfaces.Stuff;

namespace ManchkinGame.DialogWindows;

public partial class ReturnStuffWindow : Window
{
    private List<IStuff> _variants;

    public ReturnStuffWindow()
    {
        InitializeComponent();
        _variants = App.Current.Resources["STUFFS"] as List<IStuff>;

        StuffComboBox.Loaded += StuffComboBoxLoaded;
        DeleteButton.Click += DeleteButtonClick;
        CancelButton.Click += CancelButtonClick;
    }

    private void DeleteButtonClick(object sender, RoutedEventArgs e)
    {
        if(StuffComboBox.Text == "")
            UserMessage.CreateNotChosenItemMessage("шмотку для возвращения");
        else
        {
            var stuff = _variants.FirstOrDefault(vari => vari.TextRepresentation == StuffComboBox.Text);
            App.Current.Resources["CHOOSEN"] = stuff;
            Close();
        }
    }

    private void StuffComboBoxLoaded(object sender, RoutedEventArgs e)
    {
        foreach (var variant in _variants)
        {
            StuffComboBox.Items.Add(variant.TextRepresentation);
        }
    }


    private void CancelButtonClick(object sender, RoutedEventArgs e)
    {
        App.Current.Resources["CHOOSEN"] = null;
        Close();
    }
        
}