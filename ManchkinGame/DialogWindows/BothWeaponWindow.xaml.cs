using System.Collections.Generic;
using System.Windows;
using ManchkinCore.CardEnums.Accessory;
using ManchkinCore.GameLogic;
using ManchkinCore.GameLogic.Interfaces.Accessory;
using ManchkinCore.GameLogic.Interfaces.Stuff;

namespace ManchkinGame.DialogWindows;

public partial class BothWeaponWindow : Window
{
    private IStuff _right;
    private IStuff _left;
    
    public BothWeaponWindow()
    {
        InitializeComponent();
        _right = App.Current.Resources["RIGHT"] as IStuff;
        _left = App.Current.Resources["LEFT"] as IStuff;
        SetParameters();
        OkButton.Click += OkButtonClick;
    }

    private void OkButtonClick(object sender, RoutedEventArgs e) => Close();

    private void SetParameters()
    {
        LeftNameLabel.Text = _left.TextRepresentation;
        RightNameLabel.Text = _right.TextRepresentation;

        LeftDamageLabel.Text = _left.Damage.ToString();
        RightDamageLabel.Text = _right.Damage.ToString();

        LeftPriceLabel.Text = _left.Price.ToString();
        RightPriceLabel.Text = _right.Price.ToString();

        LeftWeightLabel.Text = _left.Weight.ToString();
        RightWeightLabel.Text = _right.Weight.ToString();

        LeftCheatLabel.Text = _left.Cheat ? "активен" : "неактивен";
        RightCheatLabel.Text = _right.Cheat ? "активен" : "неактивен";

        LeftFlushingLabel.Text = _left.FlushingBonus.ToString();
        RightFlushingLabel.Text = _right.FlushingBonus.ToString();

        LeftRaceLabel.Text = GetAvailableRaces(_left);
        RightRaceLabel.Text = GetAvailableRaces(_right);

        LeftClassLabel.Text = GetAvailableClasses(_left);
        RightClassLabel.Text = GetAvailableClasses(_right);

        LeftGenderLabel.Text = GetAvailableGenders(_left);
        RightGenderLabel.Text = GetAvailableGenders(_right);
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
}