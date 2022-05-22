using System.Windows;

namespace ManchkinGame;

public static class DialogWindow
{
    public static void Show(Window dialog, Window main)
    {
        dialog.Owner = main;
        WindowEffect.ApplyEffect(main);
        dialog.ShowDialog();
        WindowEffect.ClearEffect(main);
    }
}