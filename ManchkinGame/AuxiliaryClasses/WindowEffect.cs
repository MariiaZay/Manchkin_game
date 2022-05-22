using System.Windows;

namespace ManchkinGame;

public static class WindowEffect
{
    public static void ApplyEffect(Window win)
    {
        var objBlur = new System.Windows.Media.Effects.BlurEffect
        {
            Radius = 4
        };
        win.Effect = objBlur;
    }
    
    public static void ClearEffect(Window win)
    {
        win.Effect = null;
    }
}