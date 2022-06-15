using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Documents;
using ManchkinCore.GameLogic;
using ManchkinCore.GameLogic.Implementation;
using ManchkinCore.Interfaces;
using Microsoft.VisualBasic;

namespace ManchkinGame;

public partial class ChooseWindow : Window
{
    private string _typeOfVariants;
    private List<IDescriptable> _variants;
    private IManchkin _manchkin;
    private string _current;

    public ChooseWindow()
    {
        InitializeComponent();
        _typeOfVariants = App.Current.Resources["TYPE_OF_VARIANTS"].ToString();
        _manchkin = Application.Current.Resources["MANCHKIN"] as IManchkin;
        _current = App.Current.Resources["CURRENT"].ToString();

        _variants = _typeOfVariants switch
        {
            "расу" => _variants = DITree.CardsBase.Races,
            "класс" => _variants = DITree.CardsBase.Classes
        };
        ChooseBlock.Text = String.Format("Выберите {0}", _typeOfVariants);
        VariantsComboBox.Loaded += VariantsComboBoxLoaded;

        ApplyButton.Click += ApplyButtonClick;
        CancelButton.Click += CancelButtonClick;
    }


    private void VariantsComboBoxLoaded(object sender, RoutedEventArgs e)
    {
        var extra = _typeOfVariants switch
        {
            "расу" =>  _manchkin.IsHalfBlood && _manchkin.HalfBlood.SecondRace != null 
                ? _manchkin.HalfBlood.SecondRace.TextRepresentation
                : "",
            "класс" => _manchkin.IsSuperManchkin && _manchkin.SuperManchkin.SecondClass != null 
                ? _manchkin.SuperManchkin.SecondClass.TextRepresentation
                : ""
        };
        
        var empty = _typeOfVariants switch
        {
            "расу" =>  "человек",
            "класс" => "никто"
        };
        foreach (var variant in
                 _variants.Where(variant => _current != variant.TextRepresentation 
                                            && extra != variant.TextRepresentation
                                            && empty != variant.TextRepresentation
                                            ))
        {
            VariantsComboBox.Items.Add(variant.TextRepresentation);
        }
    }

    private void ApplyButtonClick(object sender, RoutedEventArgs e)
    {
        if (VariantsComboBox.Text == "")
            switch (_typeOfVariants)
            {
                case "расу":
                    UserMessage.CreateNotChosenItemMessage("новую расу");
                    break;
                case "класс":
                    UserMessage.CreateNotChosenItemMessage("новый класс");
                    break;
            }
        
        var variant = _variants.Where(variant => variant.TextRepresentation == VariantsComboBox.Text).FirstOrDefault();

        var manchkin = App.Current.Resources["MANCHKIN"] as IManchkin;

        if (App.Current.Resources["EXTRA"] == null || (bool) App.Current.Resources["EXTRA"])
        {
            if (!manchkin.CheckStuffBeforeChanging(variant) && !UserMessage.CreateAskingMessage(_typeOfVariants))
                        return;
        }
        
        App.Current.Resources["NEW"] = variant;
        App.Current.Resources["OK"] = true;
        Close();
        
    }

    private void CancelButtonClick(object sender, RoutedEventArgs e)
    {
        App.Current.Resources["NEW"] = null;
        App.Current.Resources["OK"] = false;
        Close();
    }
}