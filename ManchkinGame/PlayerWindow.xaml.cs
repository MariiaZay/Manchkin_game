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
        ChangeShoesButton.Click += ChangeShoesButtonClick;
        LostShoesButton.Click += LostShoesButtonClick;

        WeaponButton.Click += WeaponButtonClick;
        //TODO: сделать обработчик потери и смены оружия
        LostWeaponButton.Click += LostWeaponButtonClick;

        HatButton.Click += HatButtonClick;
        ChangeHatButton.Click += ChangeHatButtonClick;
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
        if (_player.Manchkin.IsDead)
            UserMessage.CreateDeathActionMessage();
        else
        {
            _player.Manchkin.LostLevel();
            Refresh();
        }
    }

    private void IncreaseLevelButtonClick(object sender, RoutedEventArgs e)
    {
        if (_player.Manchkin.IsDead)
            UserMessage.CreateDeathActionMessage();
        else
        {
            _player.Manchkin.GetLevel();
            Refresh();
        }
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
        if (_player.Manchkin.IsDead)
            UserMessage.CreateDeathActionMessage();
        else
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
    }

    #region Race

    private void LostRaceButtonClick(object sender, RoutedEventArgs e)
    {
        if (_player.Manchkin.IsDead)
            UserMessage.CreateDeathActionMessage();
        else
        {
            if (_player.Manchkin.Race is Human)
                UserMessage.CreateImpossibleLostMessage("расу");
            else
            {
                if (!_player.Manchkin.CheckStuffBeforeChanging(new Human()))
                {
                    if (!UserMessage.CreateAskingMessage("расу")) return;
                    _player.Manchkin.Race = new Human();
                    Refresh();
                }
                else
                {
                    _player.Manchkin.Race = new Human();
                    Refresh();
                }
            }
        }
    }

    private void ChangeRaceButtonClick(object sender, RoutedEventArgs e)
    {
        if (_player.Manchkin.IsDead)
            UserMessage.CreateDeathActionMessage();
        else
        {
            App.Current.Resources["TYPE_OF_VARIANTS"] = "расу";
            App.Current.Resources["CURRENT"] = RaceBlock.Text;
            App.Current.Resources["MANCHKIN"] = _player.Manchkin;

            DialogWindow.Show(new ChooseWindow(), this);

            if (App.Current.Resources["NEW"] == null ||
                App.Current.Resources["NEW"] as IRace == _player.Manchkin.Race) return;

            _player.Manchkin.Race = App.Current.Resources["NEW"] as IRace;
            Refresh();
        }
    }

    #endregion

    #region Class

    private void LostClassButtonClick(object sender, RoutedEventArgs e)
    {
        if (_player.Manchkin.IsDead)
            UserMessage.CreateDeathActionMessage();
        else
        {
            if (!_player.Manchkin.CheckStuffBeforeChanging(new Nobody()))
            {
                if (!UserMessage.CreateAskingMessage("класс")) return;
                _player.Manchkin.Class = new Nobody();
                Refresh();
            }
            else
            {
                _player.Manchkin.Class = new Nobody();
                Refresh();
            }
        }
    }

    private void ChangeClassButtonClick(object sender, RoutedEventArgs e)
    {
        if (_player.Manchkin.IsDead)
            UserMessage.CreateDeathActionMessage();
        else
        {
            App.Current.Resources["TYPE_OF_VARIANTS"] = "класс";
            App.Current.Resources["CURRENT"] = ClassBlock.Text;
            App.Current.Resources["MANCHKIN"] = _player.Manchkin;
            DialogWindow.Show(new ChooseWindow(), this);

            if (App.Current.Resources["NEW"] == null) return;

            _player.Manchkin.Class = App.Current.Resources["NEW"] as IClass;
            //TODO: вынести в отдельный метод
            Refresh();
        }
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
            _player.Manchkin.IsDead = true;
            Refresh();
        }
    }

    #region Armor

    private void ArmorButtonClick(object sender, RoutedEventArgs e)
    {
        if (_player.Manchkin.IsDead)
            UserMessage.CreateDeathWearingMessage();
        else
        {
            if (_player.Manchkin.WornArmor == null)
                UserMessage.CreateEmptyStuffMessage();
        }
    }

    private void ChangeArmorButtonClick(object sender, RoutedEventArgs e)
    {
        if (_player.Manchkin.IsDead)
            UserMessage.CreateDeathActionMessage();
        else
            ChangeStuff("броник", _player.Manchkin.WornArmor);
    }

    private void LostArmorButtonClick(object sender, RoutedEventArgs e)
    {
        if (_player.Manchkin.IsDead)
            UserMessage.CreateDeathActionMessage();
        else
        {
            if (_player.Manchkin.WornArmor == null)
                UserMessage.CreateEmptyActionStuffMessage();
            else
            {
                _player.Manchkin.LostStuff(_player.Manchkin.WornArmor);
                Refresh();
            }
        }
    }

    #endregion

    #region Shoes

    private void ShoesButtonClick(object sender, RoutedEventArgs e)
    {
        if (_player.Manchkin.IsDead)
            UserMessage.CreateDeathWearingMessage();
        else
        {
            if (_player.Manchkin.WornShoes == null)
                UserMessage.CreateEmptyStuffMessage();
        }
    }

    private void ChangeShoesButtonClick(object sender, RoutedEventArgs e)
    {
        if (_player.Manchkin.IsDead)
            UserMessage.CreateDeathActionMessage();
        else
            ChangeStuff("обувка", _player.Manchkin.WornShoes);
    }

    private void LostShoesButtonClick(object sender, RoutedEventArgs e)
    {
        if (_player.Manchkin.IsDead)
            UserMessage.CreateDeathActionMessage();
        else
        {
            if (_player.Manchkin.WornShoes == null)
                UserMessage.CreateEmptyActionStuffMessage();
            else
            {
                _player.Manchkin.LostStuff(_player.Manchkin.WornShoes);
                Refresh();
            }
        }
    }

    #endregion

    #region Weapon

    private void WeaponButtonClick(object sender, RoutedEventArgs e)
    {
        if (_player.Manchkin.IsDead)
            UserMessage.CreateDeathWearingMessage();
        else
        {
            if (_player.Manchkin.Hands.LeftHand == null && _player.Manchkin.Hands.RightHand == null)
                UserMessage.CreateEmptyStuffMessage();
        }
    }

    private void LostWeaponButtonClick(object sender, RoutedEventArgs e)
    {
        if (_player.Manchkin.IsDead)
            UserMessage.CreateDeathWearingMessage();
        else
        {
            if (_player.Manchkin.Hands.LeftHand == null && _player.Manchkin.Hands.RightHand == null)
                UserMessage.CreateEmptyActionStuffMessage();
        }
    }

    #endregion

    #region Hat

    private void HatButtonClick(object sender, RoutedEventArgs e)
    {
        if (_player.Manchkin.IsDead)
            UserMessage.CreateDeathWearingMessage();
        else
        {
            if (_player.Manchkin.WornHat == null)
                UserMessage.CreateEmptyStuffMessage();
        }
    }

    private void ChangeHatButtonClick(object sender, RoutedEventArgs e)
    {
        if (_player.Manchkin.IsDead)
            UserMessage.CreateDeathActionMessage();
        else
            ChangeStuff("головняк", _player.Manchkin.WornHat);
    }

    private void LostHatButtonClick(object sender, RoutedEventArgs e)
    {
        if (_player.Manchkin.IsDead)
            UserMessage.CreateDeathActionMessage();
        else
        {
            if (_player.Manchkin.WornHat == null)
                UserMessage.CreateEmptyActionStuffMessage();
            else
            {
                _player.Manchkin.LostStuff(_player.Manchkin.WornHat);
                Refresh();
            }
        }
    }

    #endregion

    #region Stuff

    private void SmallStuffButtonClick(object sender, RoutedEventArgs e)
    {
        if (_player.Manchkin.IsDead)
            UserMessage.CreateDeathWearingMessage();
        else
        {
            if (_player.Manchkin.SmallStuffs.Count == 0)
                UserMessage.CreateEmptyStuffMessage();
        }
    }

    private void GetSmallStuffButtonClick(object sender, RoutedEventArgs e)
    {
        if (_player.Manchkin.IsDead)
            UserMessage.CreateDeathActionMessage();
    }

    private void LostSmallStuffButtonClick(object sender, RoutedEventArgs e)
    {
        if (_player.Manchkin.IsDead)
            UserMessage.CreateDeathWearingMessage();
        else
        {
            if (_player.Manchkin.SmallStuffs.Count == 0)
                UserMessage.CreateEmptyActionStuffMessage();
        }
    }

    private void HugeStuffButtonClick(object sender, RoutedEventArgs e)
    {
        if (_player.Manchkin.IsDead)
            UserMessage.CreateDeathWearingMessage();
        else
        {
            if (_player.Manchkin.SmallStuffs.Count == 0)
                UserMessage.CreateEmptyStuffMessage();
        }
    }

    private void GetHugeStuffButtonClick(object sender, RoutedEventArgs e)
    {
        if (_player.Manchkin.IsDead)
            UserMessage.CreateDeathActionMessage();
    }

    private void LostHugeStuffButtonClick(object sender, RoutedEventArgs e)
    {
        if (_player.Manchkin.IsDead)
            UserMessage.CreateDeathWearingMessage();
        else
        {
            if (_player.Manchkin.HugeStuffs.Count == 0)
                UserMessage.CreateEmptyStuffMessage();
        }
    }

    private void SellStuffButtonClick(object sender, RoutedEventArgs e)
    {
        if (_player.Manchkin.IsDead)
            UserMessage.CreateDeathWearingMessage();
        else
        {
            if (_player.Manchkin.HugeStuffs.Count == 0 && _player.Manchkin.SmallStuffs.Count == 0)
                UserMessage.CreateEmptyActionStuffMessage();
        }
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
        if (_player.Manchkin.IsDead)
            UserMessage.CreateDeathActionMessage();
    }

    private void LostMercenaryButtonClick(object sender, RoutedEventArgs e)
    {
        if (_player.Manchkin.Mercenaries.Count == 0)
            UserMessage.CreateEmptyActionStuffMessage();
    }

    private void ChangeMercenaryButtonClick(object sender, RoutedEventArgs e)
    {
        if (_player.Manchkin.IsDead)
            UserMessage.CreateDeathWearingMessage();
        else
        {
            if (_player.Manchkin.Mercenaries.Count == 0)
                UserMessage.CreateEmptyActionStuffMessage();
        }
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


    private void ChangeStuff(string variantType, IStuff? currentStuff)
    {
        App.Current.Resources["MANCHKIN"] = _player.Manchkin;
        App.Current.Resources["TYPE_OF_VARIANTS"] = variantType;

        if (currentStuff == null)
            App.Current.Resources["CURRENT"] = "";
        else
            App.Current.Resources["CURRENT"] = currentStuff.TextRepresentation;

        DialogWindow.Show(new WearingWindow(), this);

        if (App.Current.Resources["NEW"] == null) return;

        var stuff = App.Current.Resources["NEW"] as IStuff;
        _player.Manchkin.TakeStuff(stuff);
        Refresh();
    }

    private void ButtonRefresh()
    {
        if (_player.Manchkin.IsDead)
        {
            IncreaseLevelButton.Style = (Style) FindResource("RoundedNotActiveGreenButtonStyle");
            ReduceLevelButton.Style = (Style) FindResource("RoundedNotActiveRedButtonStyle");

            ChangeRaceButton.Style = (Style) FindResource("RoundedNotActiveGreenButtonStyle");
            LostRaceButton.Style = (Style) FindResource("RoundedNotActiveRedButtonStyle");

            ChangeClassButton.Style = (Style) FindResource("RoundedNotActiveGreenButtonStyle");
            LostClassButton.Style = (Style) FindResource("RoundedNotActiveRedButtonStyle");

            ChangeGenderButton.Style = (Style) FindResource("RoundedNotActiveGreenButtonStyle");

            DeathButton.Style = (Style) FindResource("RoundedNotActiveRedButtonStyle");

            ChangeArmorButton.Style = (Style) FindResource("RoundedNotActiveGreenButtonStyle");
            LostArmorButton.Style = (Style) FindResource("RoundedNotActiveRedButtonStyle");

            ChangeShoesButton.Style = (Style) FindResource("RoundedNotActiveGreenButtonStyle");
            LostShoesButton.Style = (Style) FindResource("RoundedNotActiveRedButtonStyle");

            ChangeWeaponButton.Style = (Style) FindResource("RoundedNotActiveGreenButtonStyle");
            LostWeaponButton.Style = (Style) FindResource("RoundedNotActiveRedButtonStyle");

            ChangeHatButton.Style = (Style) FindResource("RoundedNotActiveGreenButtonStyle");
            LostHatButton.Style = (Style) FindResource("RoundedNotActiveRedButtonStyle");

            if (ReferenceEquals(SuperManchkinButton.Content, "ПОЛУЧИТЬ"))
                SuperManchkinButton.Style = (Style) FindResource("RoundedNotActiveGreenButtonStyle");
            else
                SuperManchkinButton.Style = (Style) FindResource("RoundedNotActiveRedButtonStyle");

            if (ReferenceEquals(HalfBloodButton.Content, "ПОЛУЧИТЬ"))
                HalfBloodButton.Style = (Style) FindResource("RoundedNotActiveGreenButtonStyle");
            else
                HalfBloodButton.Style = (Style) FindResource("RoundedNotActiveRedButtonStyle");

            if (_player.Manchkin.Descriptions.Count == 0)
                DescriptionButton.Style = (Style) FindResource("RoundedNotActiveGreenButtonStyle");

            GetSmallStuffButton.Style = (Style) FindResource("RoundedNotActiveGreenButtonStyle");
            LostSmallStuffButton.Style = (Style) FindResource("RoundedNotActiveRedButtonStyle");

            GetHugeStuffButton.Style = (Style) FindResource("RoundedNotActiveGreenButtonStyle");
            LostHugeStuffButton.Style = (Style) FindResource("RoundedNotActiveRedButtonStyle");

            SellStuffButton.Style = (Style) FindResource("RoundedNotActiveGreenButtonStyle");

            GetMercenariesButton.Style = (Style) FindResource("RoundedNotActiveGreenButtonStyle");
            ChangeMercenaryButton.Style = (Style) FindResource("RoundedNotActiveGreenButtonStyle");
            LostMercenaryButton.Style = (Style) FindResource("RoundedNotActiveRedButtonStyle");
        }
        else
        {
            if (_player.Manchkin.Level > 9)
                IncreaseLevelButton.Style = (Style) FindResource("RoundedNotActiveGreenButtonStyle");
            else IncreaseLevelButton.Style = (Style) FindResource("RoundedGreenButtonStyle");

            if (_player.Manchkin.Level < 2)
                ReduceLevelButton.Style = (Style) FindResource("RoundedNotActiveRedButtonStyle");
            else ReduceLevelButton.Style = (Style) FindResource("RoundedRedButtonStyle");


            if (_player.Manchkin.Descriptions.Count == 0)
                DescriptionButton.Style = (Style) FindResource("RoundedNotActiveGreenButtonStyle");
            else
                DescriptionButton.Style = (Style) FindResource("RoundedGreenButtonStyle");

            if (_player.Manchkin.Race is Human)
                LostRaceButton.Style = (Style) FindResource("RoundedNotActiveRedButtonStyle");
            else
                LostRaceButton.Style = (Style) FindResource("RoundedRedButtonStyle");

            if (_player.Manchkin.Class is Nobody)
                LostClassButton.Style = (Style) FindResource("RoundedNotActiveRedButtonStyle");
            else
                LostClassButton.Style = (Style) FindResource("RoundedRedButtonStyle");


            if (_player.Manchkin.WornArmor == null)
            {
                ChangeArmorButton.Content = "НАДЕТЬ";
                LostArmorButton.Style = (Style) FindResource("RoundedNotActiveRedButtonStyle");
            }
            else
            {
                ChangeArmorButton.Content = "СМЕНИТЬ";
                LostArmorButton.Style = (Style) FindResource("RoundedRedButtonStyle");
            }

            if (_player.Manchkin.WornHat == null)
            {
                ChangeHatButton.Content = "НАДЕТЬ";
                LostHatButton.Style = (Style) FindResource("RoundedNotActiveRedButtonStyle");
            }
            else
            {
                ChangeHatButton.Content = "СМЕНИТЬ";
                LostHatButton.Style = (Style) FindResource("RoundedRedButtonStyle");
            }

            if (_player.Manchkin.WornShoes == null)
            {
                ChangeShoesButton.Content = "НАДЕТЬ";
                LostShoesButton.Style = (Style) FindResource("RoundedNotActiveRedButtonStyle");
            }
            else
            {
                ChangeShoesButton.Content = "СМЕНИТЬ";
                LostShoesButton.Style = (Style) FindResource("RoundedRedButtonStyle");
            }

            if (_player.Manchkin.Hands.LeftHand == null && _player.Manchkin.Hands.RightHand == null)
            {
                ChangeWeaponButton.Content = "НАДЕТЬ";
                LostWeaponButton.Style = (Style) FindResource("RoundedNotActiveRedButtonStyle");
            }
            else
            {
                ChangeWeaponButton.Content = "СМЕНИТЬ";
                LostWeaponButton.Style = (Style) FindResource("RoundedRedButtonStyle");
            }

            if (_player.Manchkin.SmallStuffs.Count == 0)
                LostSmallStuffButton.Style = (Style) FindResource("RoundedNotActiveRedButtonStyle");
            else
                LostSmallStuffButton.Style = (Style) FindResource("RoundedRedButtonStyle");


            if (_player.Manchkin.HugeStuffs.Count == 0)
                LostHugeStuffButton.Style = (Style) FindResource("RoundedNotActiveRedButtonStyle");
            else
                LostHugeStuffButton.Style = (Style) FindResource("RoundedRedButtonStyle");


            if (_player.Manchkin.SmallStuffs.Count == 0 && _player.Manchkin.HugeStuffs.Count == 0)
                SellStuffButton.Style = (Style) FindResource("RoundedNotActiveGreenButtonStyle");
            else
                SellStuffButton.Style = (Style) FindResource("RoundedGreenButtonStyle");
        }
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