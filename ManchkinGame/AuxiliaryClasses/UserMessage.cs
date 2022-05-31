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

    public static void CreateEmptyStuffMessage()
        => MessageBox.Show("У тебя пока что нет этого",
            "Пока пусто",
            MessageBoxButton.OK,
            MessageBoxImage.Information);

    public static void CreateDeathMessage()
        => MessageBox.Show("Ты и так мертв, закончи ход, чтобы воскреснуть",
            "Смэрть",
            MessageBoxButton.OK,
            MessageBoxImage.Information);
    public static void CreateDeathActionMessage()
        => MessageBox.Show("Ты не можешь делать это, так как ты мёртв! Закончи ход, чтобы воскреснуть",
            "Смэрть",
            MessageBoxButton.OK,
            MessageBoxImage.Information);

    public static void CreateDeathWearingMessage()
        => MessageBox.Show("Ты умер и потерял все шмотки! Закончи ход, чтобы воскреснуть",
            "Смэрть",
            MessageBoxButton.OK,
            MessageBoxImage.Information);

    public static void CreateImpossibleTakingStuffMessage()
        => MessageBox.Show("Ты не можешь надеть эту шмотку. Если у тебя есть ЧИТ! Можешь использовать его",
            "ОЙ!",
            MessageBoxButton.OK,
            MessageBoxImage.Information);

    public static bool CreateAskingMessage(string mess)
    {
        var caption = mess switch
        {
            "класс" => "класса",
            "расу" => "расы",
            "пол" => "пола"
        };
        var answer = MessageBox.Show(String.Format("Ты уверен, что хочешь сменить {0}", mess),
            String.Format("Смена {0}", caption), MessageBoxButton.YesNo, MessageBoxImage.Warning);
        return answer == MessageBoxResult.Yes;
    }

    public static MessageBoxResult CreateWeaponAskingMessage()
        => MessageBox.Show("Оружие, которое ты хочень надеть одноручное?",
            "Смена шмотки", MessageBoxButton.YesNo, MessageBoxImage.Warning);
    
    

    public static void CreateImpossibleLostMessage(string mess)
    {
        var pronoun = mess == "класс" ? "его" : "её";
        var caption = mess == "класс" ? "Смена класса" : "Смена расы";
        MessageBox.Show(String.Format("Ты не можешь потерять {0}, так как у тебя {1} нет", mess, pronoun)
            , caption, MessageBoxButton.OK, MessageBoxImage.Information, MessageBoxResult.Yes);
    }

    public static void CreateNotChosenItemMessage(string mess)
    {
        MessageBox.Show(String.Format("Ты не указал {0}", mess), "Недостаточно данных",
            MessageBoxButton.OK,
            MessageBoxImage.Information);
    }
}