using System;
using System.Windows;
using Microsoft.VisualBasic;

namespace ManchkinGame;

public static class UserMessage
{
    public static void CreateEmptyActionStuffMessage()
        => MessageBox.Show("У тебя пока что нет этого, чтобы с этим что-то делать",
            "Пока пусто",
            MessageBoxButton.OK,
            MessageBoxImage.Information);

    public static void CreateEmptyStuffmessage()
        => MessageBox.Show("У тебя пока что нет этого",
            "Пока пусто",
            MessageBoxButton.OK,
            MessageBoxImage.Information);

        public static void CreateImpossibleLostMessage(string mess)
    {
        var pronoun = mess == "класс" ? "его" : "её";
        var caption = mess == "класс" ? "Смена класса" : "Смена расы";
        MessageBox.Show( String.Format("Ты не можешь потерять {0}, так как у тебя {1} нет", mess, pronoun)
            , caption, MessageBoxButton.OK, MessageBoxImage.Information, MessageBoxResult.Yes);
    }

    public static void CreateNotChosenItemMessage(string mess)
    {
        MessageBox.Show(String.Format("Ты не указал {0}", mess), "Недостаточно данных",
            MessageBoxButton.OK,
            MessageBoxImage.Information);
    }
        
}