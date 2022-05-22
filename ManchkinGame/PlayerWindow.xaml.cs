using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using ManchkinCore.Enums;
using ManchkinCore.Enums.Accessory;
using ManchkinCore.Implementation;
using ManchkinCore.Interfaces;

namespace ManchkinGame.Windows;

public partial class PlayerWindow
{
    private Player _player;
    public PlayerWindow()
    {
        InitializeComponent();
        
        var name = Application.Current.Resources["USER_NAME"].ToString();
        var sex = Application.Current.Resources["SEX"].ToString() == "мужcкой" ? Genders.MALE : Genders.FEMALE;
        
        _player= new Player(name, sex);
        InstallBaseManchkinParameters();
        
        IncreaseLevelButton.Click += IncreaseLevelButtonClick;
        ReduceLevelButton.Click += ReduceLevelButtonClick;
        
        MoveButton.Click += MoveButtonClick;
        BattleButton.Click += BattleButtonClick;
        
        ChangeGenderButton.Click += ChangeGenderButtonClick;
        
        LostRaceButton.Click += LostRaceButtonClick;
        ChangeRaceButton.Click += ChangeRaceButtonClick;
        
        LostClassButton.Click += LostClassButtonClick;
        ChangeClassButton.Click += ChangeClassButtonClick;
        
        DeathButton.Click += DeathButtonClick;
        
        LostArmorButton.Click += LostArmorButtonClick;
        SellArmorButton.Click += ChangeArmorButtonClick;
        
        
        LostShoesButton.Click += LostShoesButtonCLick;
        SellShoesButton.Click += SellShoesButtonClick;
        
        LostWeaponButton.Click += LostWeaponButtonClick;
        SellWeaponButton.Click += SellWeaponButtonClick;
        
        LostHatButton.Click += LostHatButtonClick;
        SellHatButton.Click += SellHatButtonClick;
        
        DescriptionButton.Click += DescriptionButtonClick;
        SellSmallStuffButton.Click += SellSmallStuffButtonClick;
        LostSmallStuffButton.Click += LostSmallStuffButtonClick;
        SellHugeStuffButton.Click += SellHugeStuffButtonClick;
        LostHugeStuffButton.Click += LostHugeStuffButtonClick;
        ChangeMercenaryButton.Click += ChangeMercenaryButtonClick;
        LostMercenaryButton.Click += LostMercenaryButtonClick;
    }

    #region Level
    private void ReduceLevelButtonClick(object sender, RoutedEventArgs e)
    {
        _player.Manchkin.LostLevel();
        LevelBlock.Text = Intallation.Level(_player);
        DamageBlock.Text = Intallation.Damage(_player);
    }

    private void IncreaseLevelButtonClick(object sender, RoutedEventArgs e)
    {
        _player.Manchkin.GetLevel();
        LevelBlock.Text = Intallation.Level(_player);
        DamageBlock.Text = Intallation.Damage(_player);
    }
    #endregion

    #region Management

    private void BattleButtonClick(object sender, RoutedEventArgs e)
    {
        BattleButton.Content = ReferenceEquals(BattleButton.Content, "БОЙ") ? "НЕ В БОЮ" : "БОЙ";
    }

    private void MoveButtonClick(object sender, RoutedEventArgs e)
    {
        MoveButton.Content = ReferenceEquals(MoveButton.Content, "МОЙ ХОД") ? "ЧУЖОЙ ХОД" : "МОЙ ХОД";
    }

    #endregion
    
    private void ChangeGenderButtonClick(object sender, RoutedEventArgs e)
    {
        _player.Manchkin.ChangeGender();
        Refresh();
    }
    
    #region Race

    private void LostRaceButtonClick(object sender, RoutedEventArgs e)
    {
        if(_player.Manchkin.Race is Human)
            UserMessage.CreateImpossibleLostMessage("расу");
        _player.Manchkin.Race = new Human();
        LostRaceButton.Style = (Style) FindResource("RoundedNotActiveRedButtonStyle");
        if(_player.Manchkin.Descriptions.Count == 0)
            DescriptionButton.Style = (Style) FindResource("RoundedNotActiveRedButtonStyle");
        Refresh();
    }
    
    private void ChangeRaceButtonClick(object sender, RoutedEventArgs e)
    {
        App.Current.Resources["TYPE_OF_VARIANTS"] = "расу";
        App.Current.Resources["CURRENT"] = RaceBlock.Text;
        DialogWindow.Show(new ChooseWindow(), this);

        if (App.Current.Resources["NEW"] == null) return;
        
        var newRace = App.Current.Resources["NEW"] as IRace;
        _player.Manchkin.Race = newRace;
        LostRaceButton.Style = (Style) FindResource("RoundedRedButtonStyle");
        DescriptionButton.Style = (Style) FindResource("RoundedGreenButtonStyle");
        Refresh();
    }

    #endregion

    #region Class
    
    private void LostClassButtonClick(object sender, RoutedEventArgs e)
    {
        if(_player.Manchkin.Class is Nobody)
            UserMessage.CreateImpossibleLostMessage("класс");
        _player.Manchkin.Class = new Nobody();
        LostClassButton.Style = (Style) FindResource("RoundedNotActiveRedButtonStyle");
        if(_player.Manchkin.Descriptions.Count == 0)
            DescriptionButton.Style = (Style) FindResource("RoundedNotActiveRedButtonStyle");
        Refresh();
    }
    
    private void ChangeClassButtonClick(object sender, RoutedEventArgs e)
    {
        App.Current.Resources["TYPE_OF_VARIANTS"] = "класс";
        App.Current.Resources["CURRENT"] = ClassBlock.Text;
        DialogWindow.Show(new ChooseWindow(), this);
        
        if (App.Current.Resources["NEW"] == null) return;
        var newClass = App.Current.Resources["NEW"] as IClass;
        _player.Manchkin.Class = newClass;
        LostClassButton.Style = (Style) FindResource("RoundedRedButtonStyle");
        DescriptionButton.Style = (Style) FindResource("RoundedGreenButtonStyle");
        Refresh();
    }

    #endregion
    
    private void SellHatButtonClick(object sender, RoutedEventArgs e)
    {
        if(_player.Manchkin.WornHat == null)
            UserMessage.CreateEmptyStuffMessage();
    }

    private void SellWeaponButtonClick(object sender, RoutedEventArgs e)
    {
        if(_player.Manchkin.Hands.LeftHand == null && _player.Manchkin.Hands.RightHand == null)
            UserMessage.CreateEmptyStuffMessage();
    }


    private void SellShoesButtonClick(object sender, RoutedEventArgs e)
    {
        if(_player.Manchkin.WornShoes == null)
            UserMessage.CreateEmptyStuffMessage();
    }

    private void ChangeArmorButtonClick(object sender, RoutedEventArgs e)
    {
        if(_player.Manchkin.WornArmor == null)
            UserMessage.CreateEmptyStuffMessage();
    }
    
    private void Refresh()
    {
        RaceBlock.Text = Intallation.Race(_player);
        ClassBlock.Text = Intallation.Class(_player);
        GenderBlock.Text = Intallation.Gender(_player);
        DamageBlock.Text = Intallation.Damage(_player);
        CardCountBlock.Text = Intallation.CardCount(_player);
        FlushingBonusBlock.Text = Intallation.FlushingBonus(_player);
        DoublePriceBlock.Text = Intallation.DoublePrice(_player);
        ArmorBlock.Text = Intallation.Armor(_player);
        ShoesBlock.Text = Intallation.Shoes(_player);
        WeaponBlock.Text = Intallation.Weapon(_player);
        HatBlock.Text = Intallation.Hat(_player);
    }

    private void LostMercenaryButtonClick(object sender, RoutedEventArgs e)
    {
        if (_player.Manchkin.Mercenaries.Count == 0)
            UserMessage.CreateEmptyStuffMessage();
    }

    private void ChangeMercenaryButtonClick(object sender, RoutedEventArgs e)
    {
        if (_player.Manchkin.Mercenaries.Count == 0)
            UserMessage.CreateEmptyStuffMessage();
    }

    private void LostHugeStuffButtonClick(object sender, RoutedEventArgs e)
    {
        if (_player.Manchkin.HugeStuffs.Count == 0)
            UserMessage.CreateEmptyStuffMessage();
    }

    private void SellHugeStuffButtonClick(object sender, RoutedEventArgs e)
    {
        if (_player.Manchkin.HugeStuffs.Count == 0)
            UserMessage.CreateEmptyStuffMessage();
    }

    private void LostSmallStuffButtonClick(object sender, RoutedEventArgs e)
    {
        if (_player.Manchkin.SmallStuffs.Count == 0)
            UserMessage.CreateEmptyStuffMessage();
    }

    private void SellSmallStuffButtonClick(object sender, RoutedEventArgs e)
    {
        if (_player.Manchkin.SmallStuffs.Count == 0)
            UserMessage.CreateEmptyStuffMessage();
    }

    private void DescriptionButtonClick(object sender, RoutedEventArgs e)
    {
        if (_player.Manchkin.Descriptions.Count == 0)
            UserMessage.CreateEmptyStuffMessage();
        else
        {
            DialogWindow.Show(new DescriptionWindow(_player.Manchkin.Descriptions), this);
        }
    }

    private void LostHatButtonClick(object sender, RoutedEventArgs e)
    {
        if(_player.Manchkin.WornHat == null)
            UserMessage.CreateEmptyStuffMessage();
    }

    private void LostWeaponButtonClick(object sender, RoutedEventArgs e)
    {
        if(_player.Manchkin.Hands.LeftHand == null && _player.Manchkin.Hands.RightHand == null)
            UserMessage.CreateEmptyStuffMessage();
    }

    private void LostShoesButtonCLick(object sender, RoutedEventArgs e)
    {
        if(_player.Manchkin.WornShoes == null)
            UserMessage.CreateEmptyStuffMessage();
    }

    private void LostArmorButtonClick(object sender, RoutedEventArgs e)
    {
        if(_player.Manchkin.WornArmor == null)
            UserMessage.CreateEmptyStuffMessage();
    }

    private void DeathButtonClick(object sender, RoutedEventArgs e)
    {
        DeathButton.Content = ReferenceEquals(DeathButton.Content, "ВОСКРЕСНУТЬ") ? "УМЕРЕРТЬ" : "ВОСКРЕСНУТЬ";
        DeathButton.Style  = ReferenceEquals(DeathButton.Content, "ВОСКРЕСНУТЬ") ? 
            (Style)FindResource("RoundedGreenButtonStyle"):
            (Style)FindResource("RoundedRedButtonStyle");
        
    }
    
    private void InstallBaseManchkinParameters()
    {
        NameBlock.Text = _player.Name;
        LevelBlock.Text = Intallation.Level(_player);
        RaceBlock.Text = Intallation.Race(_player);
        ClassBlock.Text = Intallation.Class(_player);
        GenderBlock.Text = Intallation.Gender(_player);
        DamageBlock.Text = Intallation.Damage(_player);
        DeadBlock.Text = Intallation.Life(_player);
        CardCountBlock.Text = Intallation.CardCount(_player);
        FlushingBonusBlock.Text = Intallation.FlushingBonus(_player);
        DoublePriceBlock.Text = Intallation.DoublePrice(_player);
        SupermanchkinBlock.Text = Intallation.SuperManchkin(_player);
        HalfBloodBlock.Text = Intallation.HalfBlood(_player);
        ArmorBlock.Text = Intallation.Armor(_player);
        ShoesBlock.Text = Intallation.Shoes(_player);
        WeaponBlock.Text = Intallation.Weapon(_player);
        HatBlock.Text = Intallation.Hat(_player);
    }
}