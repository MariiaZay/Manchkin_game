using System.Windows;

namespace ManchkinGame.Windows;

public partial class PlayerWindow : Window
{
    private PlayerWindowLogic _logic;
    
    public PlayerWindow()
    {
        InitializeComponent();
        _logic = new PlayerWindowLogic(this);
    }
}