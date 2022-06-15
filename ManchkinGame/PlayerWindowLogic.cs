using System.Linq;
using System.Windows;
using ManchkinCore.Enums.Accessory;
using ManchkinCore.Implementation;
using ManchkinCore.Interfaces;
using ManchkinGame.DialogWindows;
using ManchkinGame.Windows;
using ManchkinCore;

namespace ManchkinGame;

public class PlayerWindowLogic
{
    private PlayerWindow _window;
    private Player Player { get; }

    public PlayerWindowLogic(PlayerWindow window)
    {
        _window = window;
        
        var name = Application.Current.Resources["USER_NAME"].ToString();
        var sex = Application.Current.Resources["SEX"].ToString() == "мужcкой" ? Genders.MALE : Genders.FEMALE;

        Player = DITree.MakePlayer(name, sex);
        
        InstallBaseManchkinParameters();
        BindEventHandlers();
    }

    private void BindEventHandlers()
    {
        //TODO: доработать возможности смены всего во врем боя и тд
        _window.IncreaseLevelButton.Click += IncreaseLevelButtonClick;
        _window.ReduceLevelButton.Click += ReduceLevelButtonClick;

        _window.MoveButton.Click += MoveButtonClick;
        _window.BattleButton.Click += BattleButtonClick;

        _window.ChangeGenderButton.Click += ChangeGenderButtonClick;

        _window.LostRaceButton.Click += LostRaceButtonClick;
        _window.ChangeRaceButton.Click += ChangeRaceButtonClick;

        _window.LostClassButton.Click += LostClassButtonClick;
        _window.ChangeClassButton.Click += ChangeClassButtonClick;

        _window.DeathButton.Click += DeathButtonClick;

        _window.ArmorButton.Click += ArmorButtonClick;
        _window.ChangeArmorButton.Click += ChangeArmorButtonClick;
        _window.LostArmorButton.Click += LostArmorButtonClick;

        _window.ShoesButton.Click += ShoesButtonClick;
        _window.ChangeShoesButton.Click += ChangeShoesButtonClick;
        _window.LostShoesButton.Click += LostShoesButtonClick;

        _window.WeaponButton.Click += WeaponButtonClick;
        _window.ChangeWeaponButton.Click += ChangeWeaponButtonClick;
        _window.LostWeaponButton.Click += LostWeaponButtonClick;

        _window.HatButton.Click += HatButtonClick;
        _window.ChangeHatButton.Click += ChangeHatButtonClick;
        _window.LostHatButton.Click += LostHatButtonClick;

        _window.DescriptionButton.Click += DescriptionButtonClick;
        _window.ActionsButton.Click += ActionsButtonClick;

        _window.SuperManchkinButton.Click += SuperManchkinButtonClick;
        _window.HalfBloodButton.Click += HalfBloodButtonClick;

        _window.SmallStuffButton.Click += SmallStuffButtonClick;
        _window.GetSmallStuffButton.Click += GetSmallStuffButtonClick;
        _window.LostSmallStuffButton.Click += LostSmallStuffButtonClick;

        _window.HugeStuffButton.Click += HugeStuffButtonClick;
        _window.GetHugeStuffButton.Click += GetHugeStuffButtonClick;
        _window.LostHugeStuffButton.Click += LostHugeStuffButtonClick;

        _window.SellStuffButton.Click += SellStuffButtonClick;

        _window.MercenariesButton.Click += MercenariesButtonClick;
        _window.GetMercenariesButton.Click += GetMersernariesButtonClick;
        _window.ChangeMercenaryButton.Click += ChangeMercenaryButtonClick;
        _window.LostMercenaryButton.Click += LostMercenaryButtonClick;
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
            _window.BattleButton.Content = ReferenceEquals(_window.BattleButton.Content, "БОЙ") ? "НЕ В БОЮ" : "БОЙ";
            RefreshPossibilities();
        }
    }

    private void MoveButtonClick(object sender, RoutedEventArgs e)
    {
        _window.MoveButton.Content = ReferenceEquals(_window.MoveButton.Content, "МОЙ ХОД") ? "ЧУЖОЙ ХОД" : "МОЙ ХОД";
        RefreshPossibilities();
        if (Player.Manchkin.Race is Halfling)
            Player.Manchkin.DoublePrice = true;
        
        if (!Player.Manchkin.IsDead) return;
        
        Player.Manchkin.IsDead = false;
        _window.BattleButton.Style 
            = _window.BattleButton.Style 
                = (Style) _window.FindResource("RoundGreenToggleButtonStyle");
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
        else
        {
            Application.Current.Resources["TYPE_OF_VARIANTS"] = "расу";
            Application.Current.Resources["CURRENT"] = _window.RaceBlock.Text;
            Application.Current.Resources["MANCHKIN"] = Player.Manchkin;

            DialogWindow.Show(new ChooseWindow(), _window);

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
        else
        {
            Application.Current.Resources["TYPE_OF_VARIANTS"] = "класс";
            Application.Current.Resources["CURRENT"] = _window.ClassBlock.Text;
            Application.Current.Resources["MANCHKIN"] = Player.Manchkin;
            DialogWindow.Show(new ChooseWindow(), _window);

            if (Application.Current.Resources["NEW"] == null) return;

            Player.Manchkin.Class = Application.Current.Resources["NEW"] as IClass;
            //TODO: вынести в отдельный метод
            Refresh();
        }
    }

    #endregion

    private void DeathButtonClick(object sender, RoutedEventArgs e)
    {
        if (ReferenceEquals(_window.DeathButton.Content, "MЁРТВ"))
            UserMessage.CreateDeathMessage();

        else
        {
            _window.DeathButton.Content = ReferenceEquals(_window.DeathButton.Content, "MЁРТВ") ? "УМЕРЕРТЬ" : "MЁРТВ";
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
        else
            ChangeStuff("броник", Player.Manchkin.WornArmor);
    }

    private void LostArmorButtonClick(object sender, RoutedEventArgs e)
    {
        if (Player.Manchkin.IsDead)
            UserMessage.CreateDeathActionMessage();
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
        else
            ChangeStuff("обувка", Player.Manchkin.WornShoes);
    }

    private void LostShoesButtonClick(object sender, RoutedEventArgs e)
    {
        if (Player.Manchkin.IsDead)
            UserMessage.CreateDeathActionMessage();
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
                        DialogWindow.Show(new BothWeaponWindow(), _window);
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
        else
        {
            DialogWindow.Show(new AskingChangeWeaponWindow(), _window);
            switch (App.Current.Resources["ANSWER"].ToString())
            {
                case "SINGLE":
                    Application.Current.Resources["MANCHKIN"] = Player.Manchkin;
                    Application.Current.Resources["CURRENT_RIGHT_HAND"] = Player.Manchkin.Hands.RightHand;

                    Application.Current.Resources["CURRENT_LEFT_HAND"] = Player.Manchkin.Hands.LeftHand;

                    DialogWindow.Show(new SingleWeaponWindow(), _window);

                    break;

                case "BOTH":

                    Application.Current.Resources["MANCHKIN"] = Player.Manchkin;
                    Application.Current.Resources["TYPE_OF_VARIANTS"] = "оружие";

                    if (Player.Manchkin.Hands.LeftHand == null || Player.Manchkin.Hands.RightHand == null ||
                        Player.Manchkin.Hands.LeftHand != Player.Manchkin.Hands.RightHand)
                        Application.Current.Resources["CURRENT"] = "";
                    else
                        Application.Current.Resources["CURRENT"] = Player.Manchkin.Hands.LeftHand.TextRepresentation;

                    DialogWindow.Show(new WearingWindow(), _window);
                    break;
            }

            Refresh();
        }
    }

    private void LostWeaponButtonClick(object sender, RoutedEventArgs e)
    {
        if (Player.Manchkin.IsDead)
            UserMessage.CreateDeathWearingMessage();
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
                    DialogWindow.Show(new LostSingleWeaponWindow(), _window);
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
        else
            ChangeStuff("головняк", Player.Manchkin.WornHat);
    }

    private void LostHatButtonClick(object sender, RoutedEventArgs e)
    {
        if (Player.Manchkin.IsDead)
            UserMessage.CreateDeathActionMessage();
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

        else
            switch (_window.SupermanchkinBlock.Text)
            {
                case "неактивно":
                    App.Current.Resources["EXTRA_TYPE"] = "super";
                    App.Current.Resources["MANCHKIN"] = Player.Manchkin;
                    DialogWindow.Show(new AskingExtraWindow(), _window);
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
        else if (_window.HalfBloodBlock.Text == "неактивно")
        {
            App.Current.Resources["EXTRA_TYPE"] = "halfblood";
            App.Current.Resources["MANCHKIN"] = Player.Manchkin;
            DialogWindow.Show(new AskingExtraWindow(), _window);
            Refresh();
        }
        else if (_window.HalfBloodBlock.Text == "чистый")
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
            DialogWindow.Show(new DescriptionWindow(Player.Manchkin.Descriptions), _window);
        }
    }
    
    private void ActionsButtonClick(object sender, RoutedEventArgs e)
    {
        App.Current.Resources["TITLE"] = "Что можно сейчас сделать";
        DialogWindow.Show(new DescriptionWindow(Player.CurrentFeatures), _window);
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
                DialogWindow.Show(new ShowStuffWindow(), _window);
            }
        }
    }

    private void GetSmallStuffButtonClick(object sender, RoutedEventArgs e)
    {
        if (Player.Manchkin.IsDead)
            UserMessage.CreateDeathActionMessage();
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
            UserMessage.CreateDeathWearingMessage();
        else
        {
            if (Player.Manchkin.SmallStuffs.Count == 0)
                UserMessage.CreateEmptyActionStuffMessage();
            else
            {
                App.Current.Resources["MANCHKIN"] = Player.Manchkin;
                App.Current.Resources["TYPE_OF_VARIANTS"] = "мелкие";
                DialogWindow.Show(new LostStuffWindow(), _window);
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
                DialogWindow.Show(new ShowStuffWindow(), _window);
            }
        }
    }

    private void GetHugeStuffButtonClick(object sender, RoutedEventArgs e)
    {
        if (Player.Manchkin.IsDead)
            UserMessage.CreateDeathActionMessage();
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
            UserMessage.CreateDeathWearingMessage();
        else
        {
            if (Player.Manchkin.HugeStuffs.Count == 0)
                UserMessage.CreateEmptyStuffMessage();
            else
            {
                App.Current.Resources["MANCHKIN"] = Player.Manchkin;
                App.Current.Resources["TYPE_OF_VARIANTS"] = "крупные";
                DialogWindow.Show(new LostStuffWindow(), _window);
                Refresh();
            }
        }
    }

    private void SellStuffButtonClick(object sender, RoutedEventArgs e)
    {
        if (Player.Manchkin.IsDead)
            UserMessage.CreateDeathWearingMessage();
        else
        {
            if (Player.Manchkin.HugeStuffs.Count == 0 && Player.Manchkin.SmallStuffs.Count == 0)
                UserMessage.CreateEmptyActionStuffMessage();
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

    private void RefreshPossibilities()
    {
        if (ReferenceEquals(_window.MoveButton.Content, "МОЙ ХОД"))
        {
            Player.AddFeatures(PlayerPossibilities.YourTurn);
                
            if (ReferenceEquals(_window.BattleButton.Content, "БОЙ"))
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
                
            if (ReferenceEquals(_window.BattleButton.Content, "БОЙ"))
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
        DialogWindow.Show(new StuffWindow(), _window);
    }

    private void ChangeStuff(string variantType, IStuff? currentStuff)
    {
        App.Current.Resources["MANCHKIN"] = Player.Manchkin;
        App.Current.Resources["TYPE_OF_VARIANTS"] = variantType;

        if (currentStuff == null)
            App.Current.Resources["CURRENT"] = "";
        else
            App.Current.Resources["CURRENT"] = currentStuff.TextRepresentation;

        DialogWindow.Show(new WearingWindow(), _window);

        Refresh();
    }

    private void ButtonRefresh()
    {
        if (Player.Manchkin.IsDead)
        {
            _window.BattleButton.Content = "НЕ В БОЮ";
            _window.BattleButton.IsChecked = false;
            _window.BattleButton.Style = (Style) _window.FindResource("RoundNotActiveGreenToggleButtonStyle");

            _window.IncreaseLevelButton.Style = (Style) _window.FindResource("RoundedNotActiveGreenButtonStyle");
            _window.ReduceLevelButton.Style = (Style) _window.FindResource("RoundedNotActiveRedButtonStyle");

            _window.ChangeRaceButton.Style = (Style) _window.FindResource("RoundedNotActiveGreenButtonStyle");
            _window.LostRaceButton.Style = (Style) _window.FindResource("RoundedNotActiveRedButtonStyle");

            _window.ChangeClassButton.Style = (Style) _window.FindResource("RoundedNotActiveGreenButtonStyle");
            _window.LostClassButton.Style = (Style) _window.FindResource("RoundedNotActiveRedButtonStyle");

            _window.ChangeGenderButton.Style = (Style) _window.FindResource("RoundedNotActiveGreenButtonStyle");

            _window.DeathButton.Style = (Style) _window.FindResource("RoundedNotActiveRedButtonStyle");

            _window.ChangeArmorButton.Style = (Style) _window.FindResource("RoundedNotActiveGreenButtonStyle");
            _window.LostArmorButton.Style = (Style) _window.FindResource("RoundedNotActiveRedButtonStyle");

            _window.ChangeShoesButton.Style = (Style) _window.FindResource("RoundedNotActiveGreenButtonStyle");
            _window.LostShoesButton.Style = (Style) _window.FindResource("RoundedNotActiveRedButtonStyle");

            _window.ChangeWeaponButton.Style = (Style) _window.FindResource("RoundedNotActiveGreenButtonStyle");
            _window.LostWeaponButton.Style = (Style) _window.FindResource("RoundedNotActiveRedButtonStyle");

            _window.ChangeHatButton.Style = (Style) _window.FindResource("RoundedNotActiveGreenButtonStyle");
            _window.LostHatButton.Style = (Style) _window.FindResource("RoundedNotActiveRedButtonStyle");


            _window.SuperManchkinButton.Style = ReferenceEquals(_window.SuperManchkinButton.Content, "ПОЛУЧИТЬ")
                ? (Style) _window.FindResource("RoundedNotActiveGreenButtonStyle")
                : (Style) _window.FindResource("RoundedNotActiveRedButtonStyle");


            _window.HalfBloodButton.Style = ReferenceEquals(_window.HalfBloodButton.Content, "ПОЛУЧИТЬ")
                ? (Style) _window.FindResource("RoundedNotActiveGreenButtonStyle")
                : (Style) _window.FindResource("RoundedNotActiveRedButtonStyle");

            if (Player.Manchkin.Descriptions.Count == 0)
                _window.DescriptionButton.Style = (Style) _window.FindResource("RoundedNotActiveGreenButtonStyle");

            _window.GetSmallStuffButton.Style = (Style) _window.FindResource("RoundedNotActiveGreenButtonStyle");
            _window.LostSmallStuffButton.Style = (Style) _window.FindResource("RoundedNotActiveRedButtonStyle");

            _window.GetHugeStuffButton.Style = (Style) _window.FindResource("RoundedNotActiveGreenButtonStyle");
            _window.LostHugeStuffButton.Style = (Style) _window.FindResource("RoundedNotActiveRedButtonStyle");

            _window.SellStuffButton.Style = (Style) _window.FindResource("RoundedNotActiveGreenButtonStyle");

            _window.GetMercenariesButton.Style = (Style) _window.FindResource("RoundedNotActiveGreenButtonStyle");
            _window.ChangeMercenaryButton.Style = (Style) _window.FindResource("RoundedNotActiveGreenButtonStyle");
            _window.LostMercenaryButton.Style = (Style) _window.FindResource("RoundedNotActiveRedButtonStyle");
        }
        else
        {
            _window.IncreaseLevelButton.Style = Player.Manchkin.Level > 9
                ? (Style) _window.FindResource("RoundedNotActiveGreenButtonStyle")
                : (Style) _window.FindResource("RoundedGreenButtonStyle");


            _window.ReduceLevelButton.Style = Player.Manchkin.Level < 2
                ? (Style) _window.FindResource("RoundedNotActiveRedButtonStyle")
                : (Style) _window.FindResource("RoundedRedButtonStyle");


            _window.DescriptionButton.Style = Player.Manchkin.Descriptions.Count == 0
                ? (Style) _window.FindResource("RoundedNotActiveGreenButtonStyle")
                : (Style) _window.FindResource("RoundedGreenButtonStyle");

            _window.ChangeRaceButton.Style = (Style) _window.FindResource("RoundedGreenButtonStyle");

            _window.LostRaceButton.Style = Player.Manchkin.Race is Human
                ? (Style) _window.FindResource("RoundedNotActiveRedButtonStyle")
                : (Style) _window.FindResource("RoundedRedButtonStyle");

            _window.ChangeClassButton.Style 
                = _window.ChangeRaceButton.Style 
                    = (Style) _window.FindResource("RoundedGreenButtonStyle");
            _window.DeathButton.Content = "УМЕРЕТЬ";
            _window.DeathButton.Style = (Style) _window.FindResource("RoundedRedButtonStyle");

            _window.LostClassButton.Style = Player.Manchkin.Class is Nobody
                ? (Style) _window.FindResource("RoundedNotActiveRedButtonStyle")
                : (Style) _window.FindResource("RoundedRedButtonStyle");

            _window.ChangeGenderButton.Style = (Style) _window.FindResource("RoundedGreenButtonStyle");
            _window.DeathButton.Style = (Style) _window.FindResource("RoundedRedButtonStyle");

            _window.ChangeArmorButton.Style = (Style) _window.FindResource("RoundedGreenButtonStyle");
            if (Player.Manchkin.WornArmor == null)
            {
                _window.ChangeArmorButton.Content = "НАДЕТЬ";
                _window.LostArmorButton.Style = (Style) _window.FindResource("RoundedNotActiveRedButtonStyle");
            }
            else
            {
                _window.ChangeArmorButton.Content = "СМЕНИТЬ";
                _window.LostArmorButton.Style = (Style) _window.FindResource("RoundedRedButtonStyle");
            }

            _window.ChangeHatButton.Style = (Style) _window.FindResource("RoundedGreenButtonStyle");
            if (Player.Manchkin.WornHat == null)
            {
                _window.ChangeHatButton.Content = "НАДЕТЬ";
                _window.LostHatButton.Style = (Style) _window.FindResource("RoundedNotActiveRedButtonStyle");
            }
            else
            {
                _window.ChangeHatButton.Content = "СМЕНИТЬ";
                _window.LostHatButton.Style = (Style) _window.FindResource("RoundedRedButtonStyle");
            }

            _window.ChangeShoesButton.Style = (Style) _window.FindResource("RoundedGreenButtonStyle");
            if (Player.Manchkin.WornShoes == null)
            {
                _window.ChangeShoesButton.Content = "НАДЕТЬ";
                _window.LostShoesButton.Style = (Style) _window.FindResource("RoundedNotActiveRedButtonStyle");
            }
            else
            {
                _window.ChangeShoesButton.Content = "СМЕНИТЬ";
                _window.LostShoesButton.Style = (Style) _window.FindResource("RoundedRedButtonStyle");
            }

            _window.ChangeWeaponButton.Style = (Style) _window.FindResource("RoundedGreenButtonStyle");
            if (Player.Manchkin.Hands.LeftHand == null && Player.Manchkin.Hands.RightHand == null)
            {
                _window.ChangeWeaponButton.Content = "НАДЕТЬ";
                _window.LostWeaponButton.Style = (Style) _window.FindResource("RoundedNotActiveRedButtonStyle");
            }
            else
            {
                _window.ChangeWeaponButton.Content = "СМЕНИТЬ";
                _window.LostWeaponButton.Style = (Style) _window.FindResource("RoundedRedButtonStyle");
            }

            if (ReferenceEquals(_window.HalfBloodBlock.Text, "неактивно") 
                || ReferenceEquals(_window.HalfBloodBlock.Text, "чистый"))
            {
                _window.HalfBloodButton.Content = "ПОЛУЧИТЬ";
                _window.HalfBloodButton.Style = ReferenceEquals(_window.HalfBloodBlock.Text, "неактивно")
                    ? (Style) _window.FindResource("RoundedGreenButtonStyle")
                    : (Style) _window.FindResource("RoundedNotActiveGreenButtonStyle");
            }
            else
            {
                _window.HalfBloodButton.Content = "ПОТЕРЯТЬ";
                _window.HalfBloodButton.Style = (Style) _window.FindResource("RoundedRedButtonStyle");
            }

            if (ReferenceEquals(_window.SupermanchkinBlock.Text, "неактивно")
                || ReferenceEquals(_window.SupermanchkinBlock.Text, "чистый"))
            {
                _window.SuperManchkinButton.Content = "ПОЛУЧИТЬ";
                _window.SuperManchkinButton.Style = ReferenceEquals(_window.SupermanchkinBlock.Text, "неактивно")
                    ? (Style) _window.FindResource("RoundedGreenButtonStyle")
                    : (Style) _window.FindResource("RoundedNotActiveGreenButtonStyle");
            }
            else
            {
                _window.SuperManchkinButton.Content = "ПОТЕРЯТЬ";
                _window.SuperManchkinButton.Style = (Style) _window.FindResource("RoundedRedButtonStyle");
            }

            _window.GetSmallStuffButton.Style = (Style) _window.FindResource("RoundedGreenButtonStyle");

            _window.LostSmallStuffButton.Style = Player.Manchkin.SmallStuffs.Count == 0
                ? (Style) _window.FindResource("RoundedNotActiveRedButtonStyle")
                : (Style) _window.FindResource("RoundedRedButtonStyle");


            _window.GetHugeStuffButton.Style = (Style) _window.FindResource("RoundedGreenButtonStyle");
            _window.LostHugeStuffButton.Style = Player.Manchkin.HugeStuffs.Count == 0
                ? (Style) _window.FindResource("RoundedNotActiveRedButtonStyle")
                : _window.LostHugeStuffButton.Style = (Style) _window.FindResource("RoundedRedButtonStyle");

            _window.SellStuffButton.Style = Player.Manchkin.SmallStuffs.Count == 0 && Player.Manchkin.HugeStuffs.Count == 0
                ? (Style) _window.FindResource("RoundedNotActiveGreenButtonStyle")
                : (Style) _window.FindResource("RoundedGreenButtonStyle");

            _window.GetMercenariesButton.Style 
                = _window.GetHugeStuffButton.Style 
                    = (Style) _window.FindResource("RoundedGreenButtonStyle");
            _window.ChangeMercenaryButton.Style = Player.Manchkin.HasMercenary
                ? (Style) _window.FindResource("RoundedGreenButtonStyle")
                : (Style) _window.FindResource("RoundedNotActiveGreenButtonStyle");
            
            _window.LostMercenaryButton.Style = Player.Manchkin.HasMercenary
                ? (Style) _window.FindResource("RoundedRedButtonStyle")
                : (Style) _window.FindResource("RoundedNotActiveRedButtonStyle");
            //TODO: занться рефакорнгом
        }
    }

    private void Refresh()
    {
        _window.LevelBlock.Text = Intallation.Level(Player);
        _window.RaceBlock.Text = Intallation.Race(Player);
        _window.ClassBlock.Text = Intallation.Class(Player);
        _window.GenderBlock.Text = Intallation.Gender(Player);
        _window.DamageBlock.Text = Intallation.Damage(Player);
        _window.CardCountBlock.Text = Intallation.CardCount(Player);
        _window.FlushingBonusBlock.Text = Intallation.FlushingBonus(Player);
        _window.DoublePriceBlock.Text = Intallation.DoublePrice(Player);
        _window.ArmorBlock.Text = Intallation.Armor(Player);
        _window.ShoesBlock.Text = Intallation.Shoes(Player);
        _window.WeaponBlock.Text = Intallation.Weapon(Player);
        _window.HatBlock.Text = Intallation.Hat(Player);
        _window.HalfBloodBlock.Text = Intallation.HalfBlood(Player);
        _window.SupermanchkinBlock.Text = Intallation.SuperManchkin(Player);
        _window.SmallStuffBlock.Text = Intallation.SmallStuff(Player);
        _window.HugeStuffBlock.Text = Intallation.HugeStuff(Player);
        ButtonRefresh();
    }

    private void InstallBaseManchkinParameters()
    {
        _window.NameBlock.Text = Player.Name;

        _window.LevelBlock.Text = Intallation.Level(Player);
        _window.RaceBlock.Text = Intallation.Race(Player);
        _window.ClassBlock.Text = Intallation.Class(Player);
        _window.GenderBlock.Text = Intallation.Gender(Player);
        _window.DamageBlock.Text = Intallation.Damage(Player);
        _window.DeadBlock.Text = Intallation.Life(Player);

        _window.CardCountBlock.Text = Intallation.CardCount(Player);
        _window.FlushingBonusBlock.Text = Intallation.FlushingBonus(Player);
        _window.DoublePriceBlock.Text = Intallation.DoublePrice(Player);
        _window.SupermanchkinBlock.Text = Intallation.SuperManchkin(Player);
        _window.HalfBloodBlock.Text = Intallation.HalfBlood(Player);

        _window.ArmorBlock.Text = Intallation.Armor(Player);
        _window.ShoesBlock.Text = Intallation.Shoes(Player);
        _window.WeaponBlock.Text = Intallation.Weapon(Player);
        _window.HatBlock.Text = Intallation.Hat(Player);

        _window.SmallStuffBlock.Text = Intallation.SmallStuff(Player);
        _window.HugeStuffBlock.Text = Intallation.HugeStuff(Player);
    }

    #endregion
}