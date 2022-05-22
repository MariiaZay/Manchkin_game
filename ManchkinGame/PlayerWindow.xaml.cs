using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using ManchkinCore.Enums;
using ManchkinCore.Enums.Accessory;
using ManchkinCore.Implementation;

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
        LostShoesButton.Click += LostShoesButtonCLick;
        LostWeaponButton.Click += LostWeaponButtonClick;
        LostHatButton.Click += LostHatButtonClick;
        FeaturesButton.Click += FeaturesButtonClick;
        SellSmallStuffButton.Click += SellSmallStuffButtonClick;
        LostSmallStuffButton.Click += LostSmallStuffButtonClick;
        SellHugeStuffButton.Click += SellHugeStuffButtonClick;
        LostHugeStuffButton.Click += LostHugeStuffButtonClick;
        ChangeMercenaryButton.Click += ChangeMercenaryButtonClick;
        LostMercenaryButton.Click += LostMercenaryButtonClick;
    }

    private void ChangeClassButtonClick(object sender, RoutedEventArgs e)
    {
        App.Current.Resources["TYPE_OF_VARIANTS"] = "класс";
        App.Current.Resources["CURRENT"] = ClassBlock.Text;
        DialogWindow.Show(new ChooseWindow(), this);
    }
    
    private void ChangeRaceButtonClick(object sender, RoutedEventArgs e)
    {
        App.Current.Resources["TYPE_OF_VARIANTS"] = "расу";
        App.Current.Resources["CURRENT"] = RaceBlock.Text;
        DialogWindow.Show(new ChooseWindow(), this);
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

    private void FeaturesButtonClick(object sender, RoutedEventArgs e)
    {
        if (_player.Manchkin.Descriptions.Count == 0)
            UserMessage.CreateEmptyStuffMessage();
    }

    private void LostHatButtonClick(object sender, RoutedEventArgs e)
    {
        if(_player.Manchkin.WornHat == null)
            UserMessage.CreateImpossibleLostEquipmentMessage();
    }

    private void LostWeaponButtonClick(object sender, RoutedEventArgs e)
    {
        if(_player.Manchkin.Hands.LeftHand == null && _player.Manchkin.Hands.RightHand == null)
            UserMessage.CreateImpossibleLostEquipmentMessage();
    }

    private void LostShoesButtonCLick(object sender, RoutedEventArgs e)
    {
        if(_player.Manchkin.WornShoes == null)
            UserMessage.CreateImpossibleLostEquipmentMessage();
    }

    private void LostArmorButtonClick(object sender, RoutedEventArgs e)
    {
        if(_player.Manchkin.WornArmor == null)
            UserMessage.CreateImpossibleLostEquipmentMessage();
    }

    private void DeathButtonClick(object sender, RoutedEventArgs e)
    {
        DeathButton.Content = ReferenceEquals(DeathButton.Content, "ВОСКРЕСНУТЬ") ? "УМЕРЕРТЬ" : "ВОСКРЕСНУТЬ";
        DeathButton.Style  = ReferenceEquals(DeathButton.Content, "ВОСКРЕСНУТЬ") ? 
            (Style)FindResource("RoundedGreenButtonStyle"):
            (Style)FindResource("RoundedRedButtonStyle");
        
    }

    private void LostClassButtonClick(object sender, RoutedEventArgs e)
    {
        if(_player.Manchkin.Class is Nobody)
            UserMessage.CreateImpossibleLostMessage("класс");
    }

    private void LostRaceButtonClick(object sender, RoutedEventArgs e)
    {
        if(_player.Manchkin.Race is Human)
            UserMessage.CreateImpossibleLostMessage("расу");
    }

    private void ChangeGenderButtonClick(object sender, RoutedEventArgs e)
    {
        _player.Manchkin.ChangeGender();
        GenderBlock.Text = Intallation.Gender(_player);
    }

    private void BattleButtonClick(object sender, RoutedEventArgs e)
    {
        BattleButton.Content = ReferenceEquals(BattleButton.Content, "БОЙ") ? "НЕ В БОЮ" : "БОЙ";
    }

    private void MoveButtonClick(object sender, RoutedEventArgs e)
    {
        MoveButton.Content = ReferenceEquals(MoveButton.Content, "МОЙ ХОД") ? "ЧУЖОЙ ХОД" : "МОЙ ХОД";
    }

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