using System;
using System.Windows;

namespace ManchkinGame;

public static class UserMessage
{
    public static void CreateEmptyStuffMessage()
        => MessageBox.Show("У тебя пока что нет этого, чтобы с этим что-то делать",
            "Пока пусто",
            MessageBoxButton.OK,
            MessageBoxImage.Information);
    
    public static void CreateImpossibleLostEquipmentMessage()
        => MessageBox.Show("Ты не можешь это потерять, так как у тебя этого нет",
            "Потеря шмотки",
            MessageBoxButton.OK,
            MessageBoxImage.Information);

    public static void CreateImpossibleLostMessage(string mess)
    {
        var pronoun = mess == "класс" ? "его" : "её";
        var caption = mess == "класс" ? "Смена класса" : "Смена расы";
        MessageBox.Show( String.Format("Ты не можешь потерять {0}, так как у тебя {1} нет", mess, pronoun)
            , caption, MessageBoxButton.OK, MessageBoxImage.Information, MessageBoxResult.Yes);
    }
        
}