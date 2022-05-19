using System.Windows;
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
        var sex = Application.Current.Resources["SEX"].ToString() == "мужской" ? Genders.MALE : Genders.FEMALE;

        _player= new Player(name, sex);
        InstallBaseManchkinParameters();
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