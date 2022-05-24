using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using Accessibility;
using ManchkinCore;
using ManchkinCore.Enums;
using ManchkinCore.Enums.Accessory;
using ManchkinCore.Implementation;
using ManchkinCore.Interfaces;
using ManchkinGame.DialogWindows;

namespace ManchkinGame.Windows;

public partial class PlayerWindow
{
    private Player _player;

    public PlayerWindow()
    {
        InitializeComponent();

        var name = Application.Current.Resources["USER_NAME"].ToString();
        var sex = Application.Current.Resources["SEX"].ToString() == "мужcкой" ? Genders.MALE : Genders.FEMALE;

        _player = new Player(name, sex);
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

        ArmorButton.Click += ArmorButtonClick;
        ChangeArmorButton.Click += ChangeArmorButtonClick;
        LostArmorButton.Click += LostArmorButtonClick;

        ShoesButton.Click += ShoesButtonClick;
        LostShoesButton.Click += LostShoesButtonClick;

        WeaponButton.Click += WeaponButtonClick;
        LostWeaponButton.Click += LostWeaponButtonClick;

        HatButton.Click += HatButtonClick;
        LostHatButton.Click += LostHatButtonClick;

        DescriptionButton.Click += DescriptionButtonClick;

        SmallStuffButton.Click += SmallStuffButtonClick;
        GetSmallStuffButton.Click += GetSmallStuffButtonClick;
        GetHugeStuffButton.Click += GetHugeStuffButtonClick;
        LostSmallStuffButton.Click += LostSmallStuffButtonClick;
        HugeStuffButton.Click += HugeStuffButtonClick;
        LostHugeStuffButton.Click += LostHugeStuffButtonClick;
        SellStuffButton.Click += SellStuffButtonClick;

        MercenariesButton.Click += MercenariesButtonClick;
        GetMercenariesButton.Click += GetMersernariesButtonClick;
        ChangeMercenaryButton.Click += ChangeMercenaryButtonClick;
        LostMercenaryButton.Click += LostMercenaryButtonClick;
    }

    #region Level

    private void ReduceLevelButtonClick(object sender, RoutedEventArgs e)
    {
        _player.Manchkin.LostLevel();
        Refresh();
    }

    private void IncreaseLevelButtonClick(object sender, RoutedEventArgs e)
    {
        _player.Manchkin.GetLevel();
        Refresh();
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
        var gen = _player.Manchkin.Gender == Genders.MALE ? Genders.FEMALE : Genders.MALE;
        if (!_player.Manchkin.CheckStuffBeforeChanging(gen))
        {
            if (!UserMessage.CreateAskingMessage("пол")) return;
            _player.Manchkin.ChangeGender();
            Refresh();
        }
        else
        {
            _player.Manchkin.ChangeGender();
            Refresh();
        }
    }

    #region Race

    private void LostRaceButtonClick(object sender, RoutedEventArgs e)
    {
        if (_player.Manchkin.Race is Human)
            UserMessage.CreateImpossibleLostMessage("расу");
        else
        {
            if (!_player.Manchkin.CheckStuffBeforeChanging(new Human()))
            {
                if (!UserMessage.CreateAskingMessage("расу")) return;
                _player.Manchkin.Race = new Human();
                
                LostRaceButton.Style = (Style) FindResource("RoundedNotActiveRedButtonStyle");
                
                if (_player.Manchkin.Descriptions.Count == 0)
                    DescriptionButton.Style = (Style) FindResource("RoundedNotActiveRedButtonStyle");
                Refresh();
            }
            else
            {
                _player.Manchkin.Race = new Human();
                LostRaceButton.Style = (Style) FindResource("RoundedNotActiveRedButtonStyle");
                if (_player.Manchkin.Descriptions.Count == 0)
                    DescriptionButton.Style = (Style) FindResource("RoundedNotActiveRedButtonStyle");
                Refresh();
            }
        }
    }

    private void ChangeRaceButtonClick(object sender, RoutedEventArgs e)
    {
        App.Current.Resources["TYPE_OF_VARIANTS"] = "расу";
        App.Current.Resources["CURRENT"] = RaceBlock.Text;
        App.Current.Resources["MANCHKIN"] = _player.Manchkin;
        
        DialogWindow.Show(new ChooseWindow(), this);

        if (App.Current.Resources["NEW"] == null || App.Current.Resources["NEW"] as IRace == _player.Manchkin.Race) return;

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
        if (_player.Manchkin.Class is Nobody)
            UserMessage.CreateImpossibleLostMessage("класс");
        
        _player.Manchkin.Class = new Nobody();
        
        Refresh();
    }

    private void ChangeClassButtonClick(object sender, RoutedEventArgs e)
    {
        App.Current.Resources["TYPE_OF_VARIANTS"] = "класс";
        App.Current.Resources["CURRENT"] = ClassBlock.Text;
        App.Current.Resources["MANCHKIN"] = _player.Manchkin;
        DialogWindow.Show(new ChooseWindow(), this);

        if (App.Current.Resources["NEW"] == null) return;
        _player.Manchkin.Class = App.Current.Resources["NEW"] as IClass;
        LostClassButton.Style = (Style) FindResource("RoundedRedButtonStyle");
        DescriptionButton.Style = (Style) FindResource("RoundedGreenButtonStyle");
        Refresh();
        
    }

    #endregion

    private void DeathButtonClick(object sender, RoutedEventArgs e)
    {
        if (ReferenceEquals(DeathButton.Content, "MЁРТВ"))
            UserMessage.CreateDeathMessage();

        else
        {
            DeathButton.Content = ReferenceEquals(DeathButton.Content, "MЁРТВ") ? "УМЕРЕРТЬ" : "MЁРТВ";
            if (_player.Manchkin.SmallStuffs.Count is not (0 and 0))
                _player.Manchkin.LostAllStuffs();
            
            ChangeArmorButton.Content = "НАДЕТЬ";
            ChangeShoesButton.Content = "НАДЕТЬ";
            ChangeWeaponButton.Content = "НАДЕТЬ";
            ChangeHatButton.Content = "НАДЕТЬ";
            
            ChangeArmorButton.Style = (Style) FindResource("RoundedNotActiveGreenButtonStyle");
            ChangeShoesButton.Style = (Style) FindResource("RoundedNotActiveGreenButtonStyle");
            ChangeWeaponButton.Style = (Style) FindResource("RoundedNotActiveGreenButtonStyle");
            ChangeHatButton.Style = (Style) FindResource("RoundedNotActiveGreenButtonStyle");
            
            LostArmorButton.Style = (Style) FindResource("RoundedNotActiveRedButtonStyle");
            LostShoesButton.Style = (Style) FindResource("RoundedNotActiveRedButtonStyle");
            LostWeaponButton.Style = (Style) FindResource("RoundedNotActiveRedButtonStyle");
            LostHatButton.Style = (Style) FindResource("RoundedNotActiveRedButtonStyle");
            
            LostSmallStuffButton.Style = (Style) FindResource("RoundedNotActiveRedButtonStyle");
            LostHugeStuffButton.Style = (Style) FindResource("RoundedNotActiveRedButtonStyle");
            SellStuffButton.Style = (Style) FindResource("RoundedNotActiveGreenButtonStyle");
            GetSmallStuffButton.Style = (Style) FindResource("RoundedNotActiveGreenButtonStyle");
            GetMercenariesButton.Style = (Style) FindResource("RoundedNotActiveGreenButtonStyle");
            GetHugeStuffButton.Style = (Style) FindResource("RoundedNotActiveGreenButtonStyle");
            
            DeathButton.Style = (Style) FindResource("RoundedNotActiveRedButtonStyle");
            Refresh();
        }
    }

    #region Armor

    private void ArmorButtonClick(object sender, RoutedEventArgs e)
    {
        if (_player.Manchkin.WornArmor == null)
            UserMessage.CreateEmptyStuffMessage();
    }

    private void ChangeArmorButtonClick(object sender, RoutedEventArgs e)
    {
        if ((string) DeathButton.Content == "MЁРТВ")
            UserMessage.CreateImpossibleWearingMessage();
        else
        {
            App.Current.Resources["MANCHKIN"] = _player.Manchkin;
            App.Current.Resources["TYPE_OF_VARIANTS"] = "броник";
            if (_player.Manchkin.WornArmor == null)
                App.Current.Resources["CURRENT"] = "";
            else
                App.Current.Resources["CURRENT"] = _player.Manchkin.WornArmor.TextRepresentation;

            DialogWindow.Show(new WearingWindow(), this);
            
            if(App.Current.Resources["NEW"] == null) return;
            
            var stuff = App.Current.Resources["NEW"] as IStuff;
            _player.Manchkin.TakeStuff(stuff);

            LostArmorButton.Style = (Style) FindResource("RoundedRedButtonStyle");
            ChangeArmorButton.Content = "СМЕНИТЬ";

            if (stuff.Weight == Bulkiness.HUGE)
                LostHugeStuffButton.Style = (Style) FindResource("RoundedRedButtonStyle");
            else
                LostSmallStuffButton.Style = (Style) FindResource("RoundedRedButtonStyle");
            SellStuffButton.Style = (Style) FindResource("RoundedGreenButtonStyle");
            Refresh();
            
        }
    }


    private void LostArmorButtonClick(object sender, RoutedEventArgs e)
    {
        if (_player.Manchkin.WornArmor == null)
            UserMessage.CreateEmptyActionStuffMessage();
        
        _player.Manchkin.LostStuff(_player.Manchkin.WornArmor);
        Refresh();
    }

    #endregion

    #region Shoes

    private void ShoesButtonClick(object sender, RoutedEventArgs e)
    {
        if (_player.Manchkin.WornShoes == null)
            UserMessage.CreateEmptyStuffMessage();
    }

    private void LostShoesButtonClick(object sender, RoutedEventArgs e)
    {
        if (_player.Manchkin.WornShoes == null)
            UserMessage.CreateEmptyActionStuffMessage();
    }

    #endregion

    #region Weapon

    private void WeaponButtonClick(object sender, RoutedEventArgs e)
    {
        if (_player.Manchkin.Hands.LeftHand == null && _player.Manchkin.Hands.RightHand == null)
            UserMessage.CreateEmptyStuffMessage();
    }

    private void LostWeaponButtonClick(object sender, RoutedEventArgs e)
    {
        if (_player.Manchkin.Hands.LeftHand == null && _player.Manchkin.Hands.RightHand == null)
            UserMessage.CreateEmptyActionStuffMessage();
    }

    #endregion

    #region Hat

    private void HatButtonClick(object sender, RoutedEventArgs e)
    {
        if (_player.Manchkin.WornHat == null)
            UserMessage.CreateEmptyStuffMessage();
    }

    private void LostHatButtonClick(object sender, RoutedEventArgs e)
    {
        if (_player.Manchkin.WornHat == null)
            UserMessage.CreateEmptyActionStuffMessage();
    }

    #endregion

    #region Stuff

    private void SmallStuffButtonClick(object sender, RoutedEventArgs e)
    {
        if (_player.Manchkin.SmallStuffs.Count == 0)
            UserMessage.CreateEmptyStuffMessage();
    }

    private void GetSmallStuffButtonClick(object sender, RoutedEventArgs e)
    {
        if ((string) DeathButton.Content == "MЁРТВ")
            UserMessage.CreateImpossibleWearingMessage();
    }

    private void LostSmallStuffButtonClick(object sender, RoutedEventArgs e)
    {
        if (_player.Manchkin.SmallStuffs.Count == 0)
            UserMessage.CreateEmptyActionStuffMessage();
    }

    private void HugeStuffButtonClick(object sender, RoutedEventArgs e)
    {
        if (_player.Manchkin.SmallStuffs.Count == 0)
            UserMessage.CreateEmptyActionStuffMessage();
    }
    
    private void GetHugeStuffButtonClick(object sender, RoutedEventArgs e)
    {
        if ((string) DeathButton.Content == "MЁРТВ")
            UserMessage.CreateImpossibleWearingMessage();
    }

    private void LostHugeStuffButtonClick(object sender, RoutedEventArgs e)
    {
        if (_player.Manchkin.HugeStuffs.Count == 0)
            UserMessage.CreateEmptyStuffMessage();
    }

    private void SellStuffButtonClick(object sender, RoutedEventArgs e)
    {
        if (_player.Manchkin.HugeStuffs.Count == 0 && _player.Manchkin.SmallStuffs.Count == 0)
            UserMessage.CreateEmptyActionStuffMessage();
    }

    #endregion

    #region Mercenary

    private void MercenariesButtonClick(object sender, RoutedEventArgs e)
    {
        if (_player.Manchkin.Mercenaries.Count == 0)
            UserMessage.CreateEmptyStuffMessage();
    }
    
    private void GetMersernariesButtonClick(object sender, RoutedEventArgs e)
    {
        if ((string) DeathButton.Content == "MЁРТВ")
            UserMessage.CreateImpossibleWearingMessage();
    }
    
    private void LostMercenaryButtonClick(object sender, RoutedEventArgs e)
    {
        if (_player.Manchkin.Mercenaries.Count == 0)
            UserMessage.CreateEmptyActionStuffMessage();
    }

    private void ChangeMercenaryButtonClick(object sender, RoutedEventArgs e)
    {
        if (_player.Manchkin.Mercenaries.Count == 0)
            UserMessage.CreateEmptyActionStuffMessage();
    }

    #endregion

    private void DescriptionButtonClick(object sender, RoutedEventArgs e)
    {
        if (_player.Manchkin.Descriptions.Count == 0)
            UserMessage.CreateEmptyActionStuffMessage();
        else
        {
            DialogWindow.Show(new DescriptionWindow(_player.Manchkin.Descriptions), this);
        }
    }

    private void ButtonRefresh()
    {
        if(_player.Manchkin.Level > 9)
            IncreaseLevelButton.Style = (Style) FindResource("RoundedNotActiveGreenButtonStyle");
        else IncreaseLevelButton.Style = (Style) FindResource("RoundedGreenButtonStyle");
        
        if(_player.Manchkin.Level < 2)
            ReduceLevelButton.Style = (Style) FindResource("RoundedNotActiveRedButtonStyle");
        else ReduceLevelButton.Style = (Style) FindResource("RoundedRedButtonStyle");
        
        if (_player.Manchkin.WornArmor == null)
        {
            ChangeArmorButton.Content = "НАДЕТЬ";
            LostArmorButton.Style = (Style) FindResource("RoundedNotActiveRedButtonStyle");
        }
        
        if (_player.Manchkin.WornHat == null)
        {
            ChangeHatButton.Content = "НАДЕТЬ";
            LostHatButton.Style = (Style) FindResource("RoundedNotActiveRedButtonStyle");
        }
        
        if (_player.Manchkin.WornShoes == null)
        {
            ChangeShoesButton.Content = "НАДЕТЬ";
            LostShoesButton.Style = (Style) FindResource("RoundedNotActiveRedButtonStyle");
        }

        if (_player.Manchkin.Hands.LeftHand == null && _player.Manchkin.Hands.RightHand == null)
        {
            ChangeWeaponButton.Content = "НАДЕТЬ";
            LostWeaponButton.Style = (Style) FindResource("RoundedNotActiveRedButtonStyle");
        }
        
        if(_player.Manchkin.Descriptions.Count == 0)
            DescriptionButton.Style = (Style) FindResource("RoundedNotActiveGreenButtonStyle");

        if (_player.Manchkin.SmallStuffs.Count == 0)
        {
            GetSmallStuffButton.Content = "НАДЕТЬ";
            LostSmallStuffButton.Style = (Style) FindResource("RoundedNotActiveRedButtonStyle");
        }
        
        if (_player.Manchkin.HugeStuffs.Count == 0)
        {
            GetHugeStuffButton.Content = "НАДЕТЬ";
            LostHugeStuffButton.Style = (Style) FindResource("RoundedNotActiveRedButtonStyle");
        }
        
        if(_player.Manchkin.SmallStuffs.Count == 0 && _player.Manchkin.HugeStuffs.Count == 0)
            SellStuffButton.Style = (Style) FindResource("RoundedNotActiveGreenButtonStyle");
    }

    private void Refresh()
    {
        ButtonRefresh();
        LevelBlock.Text = Intallation.Level(_player);
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
        SmallStuffBlock.Text = Intallation.SmallStuff(_player);
        HugeStuffBlock.Text = Intallation.HugeStuff(_player);
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

        SmallStuffBlock.Text = Intallation.SmallStuff(_player);
        HugeStuffBlock.Text = Intallation.HugeStuff(_player);
    }
}