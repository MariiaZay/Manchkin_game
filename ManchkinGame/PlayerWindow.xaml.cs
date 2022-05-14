using System.Windows;
using ManchkinCore.Enums.Accessory;

namespace ManchkinGame.Windows;

public partial class PlayerWindow
{
    public PlayerWindow()
    {
        InitializeComponent();
        var name = Application.Current.Resources["USER_NAME"].ToString();
        var sex = Application.Current.Resources["SEX"].ToString() == "мужской" ? Genders.MALE : Genders.FEMALE;

        var player = new Player(name, sex);
    }
}