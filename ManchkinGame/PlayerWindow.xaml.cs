﻿using System;
using System.Linq;
using System.Windows;
using ManchkinGame;
using ManchkinCore;
using ManchkinCore.CardEnums.Accessory;
using ManchkinCore.CardEnums.Aspects;
using ManchkinCore.GameLogic.Implementation.Accessory.Classes;
using ManchkinCore.GameLogic.Implementation.Accessory.Races;
using ManchkinCore.GameLogic.Implementation.MainOutfit.Armor;
using ManchkinCore.GameLogic.Implementation.MainOutfit.Hats;
using ManchkinCore.GameLogic.Implementation.MainOutfit.Shoes;
using ManchkinCore.GameLogic.Implementation.MainOutfit.Weapons;
using ManchkinCore.GameLogic.Interfaces.Accessory;
using ManchkinCore.GameLogic.Interfaces.Stuff;
using ManchkinGame.DialogWindows;

namespace ManchkinGame.Windows;

public partial class PlayerWindow : Window
{
    private Player Player { get; }
    
    public PlayerWindow()
    {
        InitializeComponent();

        var name = Application.Current.Resources["USER_NAME"].ToString();
        var sex = Application.Current.Resources["SEX"].ToString() == "мужcкой" ? Genders.MALE : Genders.FEMALE;

        Player = DITree.MakePlayer(name, sex);
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
        ChangeWeaponButton.Click += ChangeWeaponButtonClick;
        LostWeaponButton.Click += LostWeaponButtonClick;

        HatButton.Click += HatButtonClick;
        ChangeHatButton.Click += ChangeHatButtonClick;
        LostHatButton.Click += LostHatButtonClick;

        DescriptionButton.Click += DescriptionButtonClick;
        ActionsButton.Click += ActionsButtonClick;

        SuperManchkinButton.Click += SuperManchkinButtonClick;
        HalfBloodButton.Click += HalfBloodButtonClick;

        SmallStuffButton.Click += SmallStuffButtonClick;
        GetSmallStuffButton.Click += GetSmallStuffButtonClick;
        LostSmallStuffButton.Click += LostSmallStuffButtonClick;

        HugeStuffButton.Click += HugeStuffButtonClick;
        GetHugeStuffButton.Click += GetHugeStuffButtonClick;
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
        if (Player.Manchkin.IsDead)
            UserMessage.CreateDeathActionMessage();
        else
        {
            Player.Manchkin.LostLevel();
            Refresh();
        }
    }

    private void IncreaseLevelButtonClick(object sender, RoutedEventArgs e)
    {
        if (Player.Manchkin.IsDead)
            UserMessage.CreateDeathActionMessage();
        else
        {
            Player.Manchkin.GetLevel();
            Refresh();
        }
    }

    #endregion

    #region Management
    
    private void BattleButtonClick(object sender, RoutedEventArgs e)
    {
        if (Player.Manchkin.IsDead)
            UserMessage.CreateDeathActionMessage();
        else
        {
            BattleButton.Content = ReferenceEquals(BattleButton.Content, "БОЙ") ? "НЕ В БОЮ" : "БОЙ";
            RefreshPossibilities();
        }
    }

    private void MoveButtonClick(object sender, RoutedEventArgs e)
    {
        MoveButton.Content = ReferenceEquals(MoveButton.Content, "МОЙ ХОД") ? "ЧУЖОЙ ХОД" : "МОЙ ХОД";
        RefreshPossibilities();
        if (Player.Manchkin.Race is Halfling)
            Player.Manchkin.DoublePrice = true;
        
        if (!Player.Manchkin.IsDead) return;
        
        Player.Manchkin.IsDead = false;
        BattleButton.Style = BattleButton.Style = (Style) FindResource("RoundGreenToggleButtonStyle");
        Refresh();
    }

    #endregion

    private void ChangeGenderButtonClick(object sender, RoutedEventArgs e)
    {
        if (Player.Manchkin.IsDead)
            UserMessage.CreateDeathActionMessage();
        else
        {
            var gen = Player.Manchkin.Gender == Genders.MALE ? Genders.FEMALE : Genders.MALE;
            if (!Player.Manchkin.CheckStuffBeforeChanging(gen))
            {
                if (!UserMessage.CreateAskingMessage("пол")) return;
                Player.Manchkin.ChangeGender();
                Refresh();
            }
            else
            {
                Player.Manchkin.ChangeGender();
                Refresh();
            }
        }
    }

    #region Race

    private void LostRaceButtonClick(object sender, RoutedEventArgs e)
    {
        if (Player.Manchkin.IsDead)
            UserMessage.CreateDeathActionMessage();
        else
        {
            if (Player.Manchkin.Race is Human)
                UserMessage.CreateImpossibleLostMessage("расу");
            else
            {
                if (!Player.Manchkin.CheckStuffBeforeChanging(new Human()))
                {
                    if (!UserMessage.CreateAskingMessage("расу")) return;
                    Player.Manchkin.Race = new Human();
                    Refresh();
                }
                else
                {
                    Player.Manchkin.Race = new Human();
                    Refresh();
                }
            }
        }
    }

    private void ChangeRaceButtonClick(object sender, RoutedEventArgs e)
    {
        if (Player.Manchkin.IsDead)
            UserMessage.CreateDeathActionMessage();
        else if (!ReferenceEquals(MoveButton.Content, "МОЙ ХОД"))
            UserMessage.CreateCantDoItNowMessage(MoveButton.Content.ToString());
        else
        {
            Application.Current.Resources["TYPE_OF_VARIANTS"] = "расу";
            Application.Current.Resources["CURRENT"] = RaceBlock.Text;
            Application.Current.Resources["MANCHKIN"] = Player.Manchkin;

            DialogWindow.Show(new ChooseWindow(), this);

            if (Application.Current.Resources["NEW"] == null) return;

            Player.Manchkin.Race = Application.Current.Resources["NEW"] as IRace;
            Refresh();
        }
    }

    #endregion

    #region Class

    private void LostClassButtonClick(object sender, RoutedEventArgs e)
    {
        if (Player.Manchkin.IsDead)
            UserMessage.CreateDeathActionMessage();
        else
        {
            if (Player.Manchkin.Class is Nobody)
                UserMessage.CreateImpossibleLostMessage("класс");
            else
            {
                if (!Player.Manchkin.CheckStuffBeforeChanging(new Nobody()))
                {
                    if (!UserMessage.CreateAskingMessage("класс")) return;
                    Player.Manchkin.Class = new Nobody();
                    Refresh();
                }
                else
                {
                    Player.Manchkin.Class = new Nobody();
                    Refresh();
                }
            }
        }
    }

    private void ChangeClassButtonClick(object sender, RoutedEventArgs e)
    {
        if (Player.Manchkin.IsDead)
            UserMessage.CreateDeathActionMessage();
        else if (!ReferenceEquals(MoveButton.Content, "МОЙ ХОД"))
            UserMessage.CreateCantDoItNowMessage(MoveButton.Content.ToString());
        else
        {
            Application.Current.Resources["TYPE_OF_VARIANTS"] = "класс";
            Application.Current.Resources["CURRENT"] = ClassBlock.Text;
            Application.Current.Resources["MANCHKIN"] = Player.Manchkin;
            DialogWindow.Show(new ChooseWindow(), this);

            if (Application.Current.Resources["NEW"] == null) return;

            Player.Manchkin.Class = Application.Current.Resources["NEW"] as IClass;
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
            if (Player.Manchkin.SmallStuffs.Count != 0 || Player.Manchkin.HugeStuffs.Count != 0)
                Player.Manchkin.LostAllStuffs();
            Player.Manchkin.IsDead = true;
            Refresh();
        }
    }

    #region Armor

    private void ArmorButtonClick(object sender, RoutedEventArgs e)
    {
        if (Player.Manchkin.IsDead)
            UserMessage.CreateDeathWearingMessage();
        else
        {
            if (Player.Manchkin.WornArmor == null)
                UserMessage.CreateEmptyStuffMessage();
            else
                ShowStuff(Player.Manchkin.WornArmor);
        }
    }

    private void ChangeArmorButtonClick(object sender, RoutedEventArgs e)
    {
        if (Player.Manchkin.IsDead)
            UserMessage.CreateDeathActionMessage();
        else if(ReferenceEquals(BattleButton.Content, "БОЙ"))
            UserMessage.CreateCantDoItNowMessage(BattleButton.Content.ToString());
        else
            ChangeStuff("броник", Player.Manchkin.WornArmor);
    }

    private void LostArmorButtonClick(object sender, RoutedEventArgs e)
    {
        if (Player.Manchkin.IsDead)
            UserMessage.CreateDeathActionMessage();
        else if(!CanDoItNow())
            UserMessage.CreateCantDoItNowMessage(BattleButton.Content.ToString());
        else
        {
            if (Player.Manchkin.WornArmor == null)
                UserMessage.CreateEmptyActionStuffMessage();
            else
            {
                Player.Manchkin.LostStuff(Player.Manchkin.WornArmor);
                Refresh();
            }
        }
    }

    #endregion

    #region Shoes

    private void ShoesButtonClick(object sender, RoutedEventArgs e)
    {
        if (Player.Manchkin.IsDead)
            UserMessage.CreateDeathWearingMessage();
        else
        {
            if (Player.Manchkin.WornShoes == null)
                UserMessage.CreateEmptyStuffMessage();
            else
                ShowStuff(Player.Manchkin.WornShoes);
        }
    }

    private void ChangeShoesButtonClick(object sender, RoutedEventArgs e)
    {
        if (Player.Manchkin.IsDead)
            UserMessage.CreateDeathActionMessage();
        else  if(ReferenceEquals(BattleButton.Content, "БОЙ"))
            UserMessage.CreateCantDoItNowMessage(BattleButton.Content.ToString());
        else
            ChangeStuff("обувка", Player.Manchkin.WornShoes);
    }

    private void LostShoesButtonClick(object sender, RoutedEventArgs e)
    {
        if (Player.Manchkin.IsDead)
            UserMessage.CreateDeathActionMessage();
        else if(!CanDoItNow())
            UserMessage.CreateCantDoItNowMessage(BattleButton.Content.ToString());
        else
        {
            if (Player.Manchkin.WornShoes == null)
                UserMessage.CreateEmptyActionStuffMessage();
            else
            {
                Player.Manchkin.LostStuff(Player.Manchkin.WornShoes);
                Refresh();
            }
        }
    }

    #endregion

    #region Weapon

    private void WeaponButtonClick(object sender, RoutedEventArgs e)
    {
        if (Player.Manchkin.IsDead)
            UserMessage.CreateDeathWearingMessage();
        else
        {
            switch (Player.Manchkin.Hands.LeftHand)
            {
                case null when Player.Manchkin.Hands.RightHand == null:
                    UserMessage.CreateEmptyStuffMessage();
                    break;
                case {Fullness: Arms.BOTH}:
                    ShowStuff(Player.Manchkin.Hands.LeftHand);
                    break;
                default:
                {
                    if (Player.Manchkin.Hands.LeftHand != null && Player.Manchkin.Hands.RightHand == null)
                        ShowStuff(Player.Manchkin.Hands.LeftHand);
                    else if (Player.Manchkin.Hands.LeftHand == null && Player.Manchkin.Hands.RightHand != null)
                        ShowStuff(Player.Manchkin.Hands.RightHand);
                    else
                    {
                        App.Current.Resources["LEFT"] = Player.Manchkin.Hands.LeftHand;
                        App.Current.Resources["RIGHT"] = Player.Manchkin.Hands.RightHand;
                        DialogWindow.Show(new BothWeaponWindow(), this);
                    }

                    break;
                }
            }
        }
    }

    private void ChangeWeaponButtonClick(object sender, RoutedEventArgs e)
    {
        if (Player.Manchkin.IsDead)
            UserMessage.CreateDeathActionMessage();
        else if(ReferenceEquals(BattleButton.Content, "БОЙ"))
            UserMessage.CreateCantDoItNowMessage(BattleButton.Content.ToString());
        else
        {
            DialogWindow.Show(new AskingChangeWeaponWindow(), this);
            switch (App.Current.Resources["ANSWER"].ToString())
            {
                case "SINGLE":
                    Application.Current.Resources["MANCHKIN"] = Player.Manchkin;
                    Application.Current.Resources["CURRENT_RIGHT_HAND"] = Player.Manchkin.Hands.RightHand;

                    Application.Current.Resources["CURRENT_LEFT_HAND"] = Player.Manchkin.Hands.LeftHand;

                    DialogWindow.Show(new SingleWeaponWindow(), this);

                    break;

                case "BOTH":

                    Application.Current.Resources["MANCHKIN"] = Player.Manchkin;
                    Application.Current.Resources["TYPE_OF_VARIANTS"] = "оружие";

                    if (Player.Manchkin.Hands.LeftHand == null || Player.Manchkin.Hands.RightHand == null ||
                        Player.Manchkin.Hands.LeftHand != Player.Manchkin.Hands.RightHand)
                        Application.Current.Resources["CURRENT"] = "";
                    else
                        Application.Current.Resources["CURRENT"] = Player.Manchkin.Hands.LeftHand.TextRepresentation;

                    DialogWindow.Show(new WearingWindow(), this);
                    break;
            }

            Refresh();
        }
    }

    private void LostWeaponButtonClick(object sender, RoutedEventArgs e)
    {
        if (Player.Manchkin.IsDead)
            UserMessage.CreateDeathActionMessage();
        else if(!CanDoItNow())
            UserMessage.CreateCantDoItNowMessage(BattleButton.Content.ToString());
        else
        {
            if (Player.Manchkin.Hands.LeftHand == null && Player.Manchkin.Hands.RightHand == null)
                UserMessage.CreateEmptyActionStuffMessage();
            else
            {
                if (Player.Manchkin.Hands.LeftHand is {Fullness: Arms.BOTH})
                    Player.Manchkin.LostStuff(Player.Manchkin.Hands.LeftHand);
                else if (Player.Manchkin.Hands.LeftHand != null && Player.Manchkin.Hands.RightHand == null)
                    Player.Manchkin.LostStuff(Player.Manchkin.Hands.LeftHand);
                else if (Player.Manchkin.Hands.LeftHand == null && Player.Manchkin.Hands.RightHand != null)
                    Player.Manchkin.LostStuff(Player.Manchkin.Hands.RightHand);
                else
                {
                    App.Current.Resources["MANCHKIN"] = Player.Manchkin;
                    DialogWindow.Show(new LostSingleWeaponWindow(), this);
                }

                Refresh();
            }
        }
    }

    #endregion

    #region Hat

    private void HatButtonClick(object sender, RoutedEventArgs e)
    {
        if (Player.Manchkin.IsDead)
            UserMessage.CreateDeathWearingMessage();
        else
        {
            if (Player.Manchkin.WornHat == null)
                UserMessage.CreateEmptyStuffMessage();
            else
                ShowStuff(Player.Manchkin.WornHat);
        }
    }

    private void ChangeHatButtonClick(object sender, RoutedEventArgs e)
    {
        if (Player.Manchkin.IsDead)
            UserMessage.CreateDeathActionMessage();
        else if(ReferenceEquals(BattleButton.Content, "БОЙ"))
            UserMessage.CreateCantDoItNowMessage(BattleButton.Content.ToString());
        else
            ChangeStuff("головняк", Player.Manchkin.WornHat);
    }

    private void LostHatButtonClick(object sender, RoutedEventArgs e)
    {
        if (Player.Manchkin.IsDead)
            UserMessage.CreateDeathActionMessage();
        else if(!CanDoItNow())
            UserMessage.CreateCantDoItNowMessage(BattleButton.Content.ToString());
        else
        {
            if (Player.Manchkin.WornHat == null)
                UserMessage.CreateEmptyActionStuffMessage();
            else
            {
                Player.Manchkin.LostStuff(Player.Manchkin.WornHat);
                Refresh();
            }
        }
    }

    #endregion

    private void SuperManchkinButtonClick(object sender, RoutedEventArgs e)
    {
        if (Player.Manchkin.IsDead)
            UserMessage.CreateDeathActionMessage();
        else if (!ReferenceEquals(MoveButton.Content, "МОЙ ХОД"))
            UserMessage.CreateCantDoItNowMessage(MoveButton.Content.ToString());
        else
            switch (SupermanchkinBlock.Text)
            {
                case "неактивно":
                    App.Current.Resources["EXTRA_TYPE"] = "super";
                    App.Current.Resources["MANCHKIN"] = Player.Manchkin;
                    DialogWindow.Show(new AskingExtraWindow(), this);
                    Refresh();
                    break;
                case "чистый":
                    UserMessage.CreateHalfCleanMessage("Суперманчкин");
                    break;
                default:
                {
                    if (!Player.Manchkin.CheckStuffBeforeChangingSuperManchkin())
                    {
                        if (!UserMessage.CreateAskingMessage("класс")) return;
                        Player.Manchkin.RefuseSuperManchkin();
                        Player.Manchkin.RecalculateParameters();
                        Refresh();
                    }
                    else
                    {
                        Player.Manchkin.RefuseSuperManchkin();
                        Player.Manchkin.RecalculateParameters();
                        Refresh();
                    }

                    break;
                }
            }
    }

    private void HalfBloodButtonClick(object sender, RoutedEventArgs e)
    {
        if (Player.Manchkin.IsDead)
            UserMessage.CreateDeathActionMessage();
        else if (!ReferenceEquals(MoveButton.Content, "МОЙ ХОД"))
            UserMessage.CreateCantDoItNowMessage(MoveButton.Content.ToString());
        else if (HalfBloodBlock.Text == "неактивно")
        {
            App.Current.Resources["EXTRA_TYPE"] = "halfblood";
            App.Current.Resources["MANCHKIN"] = Player.Manchkin;
            DialogWindow.Show(new AskingExtraWindow(), this);
            Refresh();
        }
        else if (HalfBloodBlock.Text == "чистый")
        {
            UserMessage.CreateHalfCleanMessage("Полукровка");
        }
        else
        {
            if (!Player.Manchkin.CheckStuffBeforeChangingHalfblood())
            {
                if (!UserMessage.CreateAskingMessage("расу")) return;
                Player.Manchkin.RefuseHalfblood();
                Player.Manchkin.RecalculateParameters();
                Refresh();
            }
            else
            {
                Player.Manchkin.RefuseHalfblood();
                Player.Manchkin.RecalculateParameters();
                Refresh();
            }
        }
    }

    private void DescriptionButtonClick(object sender, RoutedEventArgs e)
    {
        if (Player.Manchkin.Descriptions.Count == 0)
            UserMessage.CreateEmptyActionStuffMessage();
        else
        {
            App.Current.Resources["TITLE"] = "Мои свойства";
            DialogWindow.Show(new DescriptionWindow(Player.Manchkin.Descriptions), this);
        }
    }
    
    private void ActionsButtonClick(object sender, RoutedEventArgs e)
    {
        App.Current.Resources["TITLE"] = "Что можно сейчас сделать";
        DialogWindow.Show(new DescriptionWindow(Player.CurrentFeatures), this);
    }

    #region Stuff

    private void SmallStuffButtonClick(object sender, RoutedEventArgs e)
    {
        if (Player.Manchkin.IsDead)
            UserMessage.CreateDeathWearingMessage();
        else
        {
            if (Player.Manchkin.SmallStuffs.Count == 0)
                UserMessage.CreateEmptyStuffMessage();
            else
            {
                App.Current.Resources["MANCHKIN"] = Player.Manchkin;
                App.Current.Resources["TYPE_OF_VARIANTS"] = "мелкие";
                DialogWindow.Show(new ShowStuffWindow(), this);
            }
        }
    }

    private void GetSmallStuffButtonClick(object sender, RoutedEventArgs e)
    {
        if (Player.Manchkin.IsDead)
            UserMessage.CreateDeathActionMessage();
        else if(ReferenceEquals(BattleButton.Content, "БОЙ"))
            UserMessage.CreateCantDoItNowMessage(BattleButton.Content.ToString());
        else
        {
            if (CheckCardsBaseStuff("мелкие"))
                ChangeStuff("мелкие шмотки", null);
            else
                UserMessage.CreateEndStuffMessage("мелкие");
        }
    }

    private void LostSmallStuffButtonClick(object sender, RoutedEventArgs e)
    {
        if (Player.Manchkin.IsDead)
            UserMessage.CreateDeathActionMessage();
        else if(!CanDoItNow())
            UserMessage.CreateCantDoItNowMessage(BattleButton.Content.ToString());
        else
        {
            if (Player.Manchkin.SmallStuffs.Count == 0)
                UserMessage.CreateEmptyActionStuffMessage();
            else
            {
                App.Current.Resources["MANCHKIN"] = Player.Manchkin;
                App.Current.Resources["TYPE_OF_VARIANTS"] = "мелкие";
                DialogWindow.Show(new LostStuffWindow(), this);
                Refresh();
            }
        }
    }

    private void HugeStuffButtonClick(object sender, RoutedEventArgs e)
    {
        if (Player.Manchkin.IsDead)
            UserMessage.CreateDeathWearingMessage();
        else
        {
            if (Player.Manchkin.HugeStuffs.Count == 0)
                UserMessage.CreateEmptyStuffMessage();
            else
            {
                App.Current.Resources["MANCHKIN"] = Player.Manchkin;
                App.Current.Resources["TYPE_OF_VARIANTS"] = "крупные";
                DialogWindow.Show(new ShowStuffWindow(), this);
            }
        }
    }

    private void GetHugeStuffButtonClick(object sender, RoutedEventArgs e)
    {
        if (Player.Manchkin.IsDead)
            UserMessage.CreateDeathActionMessage();
        else if(ReferenceEquals(BattleButton.Content, "БОЙ"))
            UserMessage.CreateCantDoItNowMessage(BattleButton.Content.ToString());
        else
        {
            if (CheckCardsBaseStuff("крупные"))
                ChangeStuff("крупные шмотки", null);
            else
                UserMessage.CreateEndStuffMessage("крупные");
        }
    }

    private void LostHugeStuffButtonClick(object sender, RoutedEventArgs e)
    {
        if (Player.Manchkin.IsDead)
            UserMessage.CreateDeathActionMessage();
        else if(!CanDoItNow())
            UserMessage.CreateCantDoItNowMessage(BattleButton.Content.ToString());
        else
        {
            if (Player.Manchkin.HugeStuffs.Count == 0)
                UserMessage.CreateEmptyStuffMessage();
            else
            {
                App.Current.Resources["MANCHKIN"] = Player.Manchkin;
                App.Current.Resources["TYPE_OF_VARIANTS"] = "крупные";
                DialogWindow.Show(new LostStuffWindow(), this);
                Refresh();
            }
        }
    }

    private void SellStuffButtonClick(object sender, RoutedEventArgs e)
    {
        if (Player.Manchkin.IsDead)
            UserMessage.CreateDeathWearingMessage();
        else if(!ReferenceEquals(MoveButton.Content, "МОЙ ХОД"))
            UserMessage.CreateCantDoItNowMessage(MoveButton.Content.ToString());
        else if (ReferenceEquals(BattleButton.Content, "БОЙ"))
            UserMessage.CreateCantDoItNowMessage(BattleButton.Content.ToString());
        else if (Player.Manchkin.HugeStuffs.Count == 0 && Player.Manchkin.SmallStuffs.Count == 0)
                UserMessage.CreateEmptyActionStuffMessage();
        else
        {
            App.Current.Resources["MANCHKIN"] = Player.Manchkin;
            DialogWindow.Show(new SellWindow(), this);
            Refresh();
        }
    }

    #endregion

    #region Mercenary
    private void MercenariesButtonClick(object sender, RoutedEventArgs e)
    {
        if (Player.Manchkin.Mercenaries.Count == 0)
            UserMessage.CreateEmptyStuffMessage();
    }

    private void GetMersernariesButtonClick(object sender, RoutedEventArgs e)
    {
        if (Player.Manchkin.IsDead)
            UserMessage.CreateDeathActionMessage();
    }

    private void LostMercenaryButtonClick(object sender, RoutedEventArgs e)
    {
        if (Player.Manchkin.Mercenaries.Count == 0)
            UserMessage.CreateEmptyActionStuffMessage();
    }

    private void ChangeMercenaryButtonClick(object sender, RoutedEventArgs e)
    {
        if (Player.Manchkin.IsDead)
            UserMessage.CreateDeathWearingMessage();
        else
        {
            if (Player.Manchkin.Mercenaries.Count == 0)
                UserMessage.CreateEmptyActionStuffMessage();
        }
    }

    #endregion

    #region Auxiliary methods

    private bool CanDoItNow()
        => !ReferenceEquals(BattleButton.Content, "БОЙ") || UserMessage.CreateLostOrTakeOffMessage();
    private void RefreshPossibilities()
    {
        if (ReferenceEquals(MoveButton.Content, "МОЙ ХОД"))
        {
            Player.AddFeatures(PlayerPossibilities.YourTurn);
                
            if (ReferenceEquals(BattleButton.Content, "БОЙ"))
            {
                Player.RemoveFeatures(PlayerPossibilities.AlwaysButNotInFight);
                Player.AddFeatures(PlayerPossibilities.AlwaysInFight);
                
                Player.RemoveFeatures(PlayerPossibilities.YourTurnButNotInFight);
                Player.AddFeatures(PlayerPossibilities.YourTurnInFight);
            }
            else
            {
                Player.AddFeatures(PlayerPossibilities.AlwaysButNotInFight);
                Player.RemoveFeatures(PlayerPossibilities.AlwaysInFight);
                
                Player.AddFeatures(PlayerPossibilities.YourTurnButNotInFight);
                Player.RemoveFeatures(PlayerPossibilities.YourTurnInFight);
            }
        }
        else
        {
            Player.RemoveFeatures(PlayerPossibilities.YourTurn);
            Player.RemoveFeatures(PlayerPossibilities.YourTurnInFight);
            Player.RemoveFeatures(PlayerPossibilities.YourTurnButNotInFight);
                
            if (ReferenceEquals(BattleButton.Content, "БОЙ"))
            {
                Player.RemoveFeatures(PlayerPossibilities.AlwaysButNotInFight);
                Player.AddFeatures(PlayerPossibilities.AlwaysInFight);
            }
            else
            {
                Player.AddFeatures(PlayerPossibilities.AlwaysButNotInFight);
                Player.RemoveFeatures(PlayerPossibilities.AlwaysInFight);
            }
        }
    }

    private bool CheckCardsBaseStuff(string mess)
    {
        var list = mess == "мелкие" ? DITree.CardsBase.SmallStuffs : DITree.CardsBase.HugeStuffs;
        var manStuff = mess == "мелкие" ? Player.Manchkin.SmallStuffs : Player.Manchkin.HugeStuffs;
        return list.Select(stuff => stuff as IStuff).Any(s => !manStuff.Contains(s));
    }

    private void ShowStuff(IStuff stuff)
    {
        Application.Current.Resources["STUFF"] = stuff;
        Application.Current.Resources["STUFF_TYPE"] = stuff switch
        {
            Armor => "броник",
            Hat => "головняк",
            Shoes => "обувка",
            Weapon => "оружие",
            _ => "просто шмотка"
        };
        DialogWindow.Show(new StuffWindow(), this);
    }

    private void ChangeStuff(string variantType, IStuff? currentStuff)
    {
        App.Current.Resources["MANCHKIN"] = Player.Manchkin;
        App.Current.Resources["TYPE_OF_VARIANTS"] = variantType;

        if (currentStuff == null)
            App.Current.Resources["CURRENT"] = "";
        else
            App.Current.Resources["CURRENT"] = currentStuff.TextRepresentation;

        DialogWindow.Show(new WearingWindow(), this);

        Refresh();
    }

    private void ButtonRefresh()
    {
        if (Player.Manchkin.IsDead)
        {
            BattleButton.Content = "НЕ В БОЮ";
            BattleButton.IsChecked = false;
            BattleButton.Style = (Style) FindResource("RoundNotActiveGreenToggleButtonStyle");

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


            SuperManchkinButton.Style = ReferenceEquals(SuperManchkinButton.Content, "ПОЛУЧИТЬ")
                ? (Style) FindResource("RoundedNotActiveGreenButtonStyle")
                : (Style) FindResource("RoundedNotActiveRedButtonStyle");


            HalfBloodButton.Style = ReferenceEquals(HalfBloodButton.Content, "ПОЛУЧИТЬ")
                ? (Style) FindResource("RoundedNotActiveGreenButtonStyle")
                : (Style) FindResource("RoundedNotActiveRedButtonStyle");

            if (Player.Manchkin.Descriptions.Count == 0)
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
            IncreaseLevelButton.Style = Player.Manchkin.Level > 9
                ? (Style) FindResource("RoundedNotActiveGreenButtonStyle")
                : (Style) FindResource("RoundedGreenButtonStyle");


            ReduceLevelButton.Style = Player.Manchkin.Level < 2
                ? (Style) FindResource("RoundedNotActiveRedButtonStyle")
                : (Style) FindResource("RoundedRedButtonStyle");


            DescriptionButton.Style = Player.Manchkin.Descriptions.Count == 0
                ? (Style) FindResource("RoundedNotActiveGreenButtonStyle")
                : (Style) FindResource("RoundedGreenButtonStyle");

            ChangeRaceButton.Style = (Style) FindResource("RoundedGreenButtonStyle");

            LostRaceButton.Style = Player.Manchkin.Race is Human
                ? (Style) FindResource("RoundedNotActiveRedButtonStyle")
                : (Style) FindResource("RoundedRedButtonStyle");

            ChangeClassButton.Style = ChangeRaceButton.Style = (Style) FindResource("RoundedGreenButtonStyle");
            DeathButton.Content = "УМЕРЕТЬ";
            DeathButton.Style = (Style) FindResource("RoundedRedButtonStyle");

            LostClassButton.Style = Player.Manchkin.Class is Nobody
                ? (Style) FindResource("RoundedNotActiveRedButtonStyle")
                : (Style) FindResource("RoundedRedButtonStyle");

            ChangeGenderButton.Style = (Style) FindResource("RoundedGreenButtonStyle");
            DeathButton.Style = (Style) FindResource("RoundedRedButtonStyle");

            ChangeArmorButton.Style = (Style) FindResource("RoundedGreenButtonStyle");
            if (Player.Manchkin.WornArmor == null)
            {
                ChangeArmorButton.Content = "НАДЕТЬ";
                LostArmorButton.Style = (Style) FindResource("RoundedNotActiveRedButtonStyle");
            }
            else
            {
                ChangeArmorButton.Content = "СМЕНИТЬ";
                LostArmorButton.Style = (Style) FindResource("RoundedRedButtonStyle");
            }

            ChangeHatButton.Style = (Style) FindResource("RoundedGreenButtonStyle");
            if (Player.Manchkin.WornHat == null)
            {
                ChangeHatButton.Content = "НАДЕТЬ";
                LostHatButton.Style = (Style) FindResource("RoundedNotActiveRedButtonStyle");
            }
            else
            {
                ChangeHatButton.Content = "СМЕНИТЬ";
                LostHatButton.Style = (Style) FindResource("RoundedRedButtonStyle");
            }

            ChangeShoesButton.Style = (Style) FindResource("RoundedGreenButtonStyle");
            if (Player.Manchkin.WornShoes == null)
            {
                ChangeShoesButton.Content = "НАДЕТЬ";
                LostShoesButton.Style = (Style) FindResource("RoundedNotActiveRedButtonStyle");
            }
            else
            {
                ChangeShoesButton.Content = "СМЕНИТЬ";
                LostShoesButton.Style = (Style) FindResource("RoundedRedButtonStyle");
            }

            ChangeWeaponButton.Style = (Style) FindResource("RoundedGreenButtonStyle");
            if (Player.Manchkin.Hands.LeftHand == null && Player.Manchkin.Hands.RightHand == null)
            {
                ChangeWeaponButton.Content = "НАДЕТЬ";
                LostWeaponButton.Style = (Style) FindResource("RoundedNotActiveRedButtonStyle");
            }
            else
            {
                ChangeWeaponButton.Content = "СМЕНИТЬ";
                LostWeaponButton.Style = (Style) FindResource("RoundedRedButtonStyle");
            }

            if (ReferenceEquals(HalfBloodBlock.Text, "неактивно") || ReferenceEquals(HalfBloodBlock.Text, "чистый"))
            {
                HalfBloodButton.Content = "ПОЛУЧИТЬ";
                HalfBloodButton.Style = ReferenceEquals(HalfBloodBlock.Text, "неактивно")
                    ? (Style) FindResource("RoundedGreenButtonStyle")
                    : (Style) FindResource("RoundedNotActiveGreenButtonStyle");
            }
            else
            {
                HalfBloodButton.Content = "ПОТЕРЯТЬ";
                HalfBloodButton.Style = (Style) FindResource("RoundedRedButtonStyle");
            }

            if (ReferenceEquals(SupermanchkinBlock.Text, "неактивно") ||
                ReferenceEquals(SupermanchkinBlock.Text, "чистый"))
            {
                SuperManchkinButton.Content = "ПОЛУЧИТЬ";
                SuperManchkinButton.Style = ReferenceEquals(SupermanchkinBlock.Text, "неактивно")
                    ? (Style) FindResource("RoundedGreenButtonStyle")
                    : (Style) FindResource("RoundedNotActiveGreenButtonStyle");
            }
            else
            {
                SuperManchkinButton.Content = "ПОТЕРЯТЬ";
                SuperManchkinButton.Style = (Style) FindResource("RoundedRedButtonStyle");
            }

            GetSmallStuffButton.Style = (Style) FindResource("RoundedGreenButtonStyle");

            LostSmallStuffButton.Style = Player.Manchkin.SmallStuffs.Count == 0
                ? (Style) FindResource("RoundedNotActiveRedButtonStyle")
                : (Style) FindResource("RoundedRedButtonStyle");


            GetHugeStuffButton.Style = (Style) FindResource("RoundedGreenButtonStyle");
            LostHugeStuffButton.Style = Player.Manchkin.HugeStuffs.Count == 0
                ? (Style) FindResource("RoundedNotActiveRedButtonStyle")
                : LostHugeStuffButton.Style = (Style) FindResource("RoundedRedButtonStyle");

            SellStuffButton.Style = Player.Manchkin.SmallStuffs.Count == 0 && Player.Manchkin.HugeStuffs.Count == 0
                ? (Style) FindResource("RoundedNotActiveGreenButtonStyle")
                : (Style) FindResource("RoundedGreenButtonStyle");

            GetMercenariesButton.Style = GetHugeStuffButton.Style = (Style) FindResource("RoundedGreenButtonStyle");
            ChangeMercenaryButton.Style = Player.Manchkin.HasMercenary
                ? (Style) FindResource("RoundedGreenButtonStyle")
                : (Style) FindResource("RoundedNotActiveGreenButtonStyle");
            
            LostMercenaryButton.Style = Player.Manchkin.HasMercenary
                ? (Style) FindResource("RoundedRedButtonStyle")
                : (Style) FindResource("RoundedNotActiveRedButtonStyle");
        }
    }

    private void Refresh()
    {
        LevelBlock.Text = Intallation.Level(Player);
        RaceBlock.Text = Intallation.Race(Player);
        ClassBlock.Text = Intallation.Class(Player);
        GenderBlock.Text = Intallation.Gender(Player);
        DamageBlock.Text = Intallation.Damage(Player);
        CardCountBlock.Text = Intallation.CardCount(Player);
        FlushingBonusBlock.Text = Intallation.FlushingBonus(Player);
        DoublePriceBlock.Text = Intallation.DoublePrice(Player);
        ArmorBlock.Text = Intallation.Armor(Player);
        ShoesBlock.Text = Intallation.Shoes(Player);
        WeaponBlock.Text = Intallation.Weapon(Player);
        HatBlock.Text = Intallation.Hat(Player);
        HalfBloodBlock.Text = Intallation.HalfBlood(Player);
        SupermanchkinBlock.Text = Intallation.SuperManchkin(Player);
        SmallStuffBlock.Text = Intallation.SmallStuff(Player);
        HugeStuffBlock.Text = Intallation.HugeStuff(Player);
        ButtonRefresh();
    }

    private void InstallBaseManchkinParameters()
    {
        NameBlock.Text = Player.Name;

        LevelBlock.Text = Intallation.Level(Player);
        RaceBlock.Text = Intallation.Race(Player);
        ClassBlock.Text = Intallation.Class(Player);
        GenderBlock.Text = Intallation.Gender(Player);
        DamageBlock.Text = Intallation.Damage(Player);
        DeadBlock.Text = Intallation.Life(Player);

        CardCountBlock.Text = Intallation.CardCount(Player);
        FlushingBonusBlock.Text = Intallation.FlushingBonus(Player);
        DoublePriceBlock.Text = Intallation.DoublePrice(Player);
        SupermanchkinBlock.Text = Intallation.SuperManchkin(Player);
        HalfBloodBlock.Text = Intallation.HalfBlood(Player);

        ArmorBlock.Text = Intallation.Armor(Player);
        ShoesBlock.Text = Intallation.Shoes(Player);
        WeaponBlock.Text = Intallation.Weapon(Player);
        HatBlock.Text = Intallation.Hat(Player);

        SmallStuffBlock.Text = Intallation.SmallStuff(Player);
        HugeStuffBlock.Text = Intallation.HugeStuff(Player);
    }

    #endregion
}