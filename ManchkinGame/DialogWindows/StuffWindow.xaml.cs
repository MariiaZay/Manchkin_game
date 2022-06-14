using System;
using System.Collections.Generic;
using System.Configuration;
using System.Windows;
using System.Windows.Documents;
using ManchkinCore;
using ManchkinCore.Enums.Accessory;
using ManchkinCore.GameLogic;
using ManchkinCore.Implementation;
using ManchkinCore.Interfaces;

namespace ManchkinGame.DialogWindows;

public partial class StuffWindow : Window
{
    public StuffWindow()
    {
        InitializeComponent();
        SetInfo();
        OkButton.Click += OkButtonClick;
    }

    private void SetInfo()
    {
        var stuff = App.Current.Resources["STUFF"] as IStuff;
        StuffLabel.Text = App.Current.Resources["STUFF_TYPE"].ToString();
        NameLabel.Text = stuff.TextRepresentation;
        DamageLabel.Text = stuff.Damage.ToString();
        PriceLabel.Text = stuff.Price.ToString();
        WeightLabel.Text = stuff.Weight == Bulkiness.HUGE ? "большая" : "маленькая";
        if (stuff.Fullness == Arms.NO) FullnessLabel.Text = "без рук";
        else FullnessLabel.Text = stuff.Fullness == Arms.BOTH ? " в обе" : "в одну";
        CheatLabel.Text = stuff.Cheat ? "активен" : "неактивен";
        FlushingLabel.Text = stuff.FlushingBonus.ToString();
        RaceLabel.Text = GetAvailableRaces(stuff);
        ClassLabel.Text = GetAvailableClasses(stuff);
        GenderLabel.Text = GetAvailableGenders(stuff);
    }

    private string GetAvailableRaces(IStuff stuff)
    {
        var availableRaces = new List<string>();
        var cheat = false;
        foreach (var race in DITree.CardsBase.Races)
        {
            if (stuff.Cheat)
            {
                cheat = true;
                stuff.Cheat = false;
            }
            if(stuff.CanBeUsed(race as IRace))
                availableRaces.Add(race.TextRepresentation);
            if (cheat)
                stuff.Cheat = true;
        }

        return string.Join(", ", availableRaces);
    }
    
    private string GetAvailableClasses(IStuff stuff)
    {
        var availableClasses = new List<string>();
        var cheat = false;
        foreach (var _class in DITree.CardsBase.Classes)
        {
            if (stuff.Cheat)
            {
                cheat = true;
                stuff.Cheat = false;
            }
            if(stuff.CanBeUsed(_class as IClass))
                availableClasses.Add(_class.TextRepresentation);
            if (cheat)
                stuff.Cheat = true;
        }

        return string.Join(", ", availableClasses);
    }
    
    private string GetAvailableGenders(IStuff stuff)
    {
        var availableGender = new List<string>();
        var cheat = false;
        
        if (stuff.Cheat)
        {
            cheat = true;
            stuff.Cheat = false;
        }
        if(stuff.CanBeUsed(Genders.MALE))
            availableGender.Add("муж");
        if(stuff.CanBeUsed(Genders.FEMALE))
            availableGender.Add("жен");
        if (cheat)
            stuff.Cheat = true;
        
        return string.Join(", ", availableGender);
    }

    private void OkButtonClick(object sender, RoutedEventArgs e) => Close();
}