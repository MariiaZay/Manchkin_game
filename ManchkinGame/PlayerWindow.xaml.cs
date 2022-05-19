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
    }

    private void LostRaceButtonClick(object sender, RoutedEventArgs e)
    {
        if(_player.Manchkin.Race is Human)
            CreateMessageForUser("Тебе нечего терять, ты и так человек!","Смена расы", MessageBoxButton.OK,
                MessageBoxImage.Information);
            
    }
    
    private void CreateMessageForUser(string mess, string caption,
        MessageBoxButton button, MessageBoxImage icon)
        => MessageBox.Show(mess, caption, button, icon, MessageBoxResult.Yes);

    private void ChangeGenderButtonClick(object sender, RoutedEventArgs e)
    {
        _player.Manchkin.ChangeGender();
        GenderBlock.Text = Intallation.Gender(_player);
    }

    private void BattleButtonClick(object sender, RoutedEventArgs e)
    {
        BattleButton.Content = ReferenceEquals(BattleButton.Content, "НЕ В БОЮ") ? "БОЙ" : "НЕ В БОЮ";
    }

    private void MoveButtonClick(object sender, RoutedEventArgs e)
    {
        MoveButton.Content = ReferenceEquals(MoveButton.Content, "ЧУЖОЙ ХОД") ? "МОЙ ХОД" : "ЧУЖОЙ ХОД";

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