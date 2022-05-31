using System;
using System.Windows;
using Microsoft.VisualBasic;

namespace ManchkinGame;

public static class UserMessage
{
    public static void CreateOneWeaponInBothHandsMessage()
        => CreateInfoMessage("Ты не можешь держать одно и то же одноручное оружие в обеих руках",
            "ОЙ");
    public static void CreateEmptyActionStuffMessage()
        => CreateInfoMessage("У тебя пока что нет этого, чтобы с этим что-то делать", "Пока пусто");

    public static void CreateEmptyStuffMessage()
        => CreateInfoMessage("У тебя пока что нет этого", "Пока пусто");

    public static void CreateDeathMessage()
        => CreateInfoMessage("Ты и так мертв, закончи ход, чтобы воскреснуть", "Смэрть");

    public static void CreateDeathActionMessage()
        => CreateInfoMessage("Ты не можешь делать это, так как ты мёртв! Закончи ход, чтобы воскреснуть", 
            "Смэрть");

    public static void CreateDeathWearingMessage()
        => CreateInfoMessage("Ты умер и потерял все шмотки! Закончи ход, чтобы воскреснуть",
            "Смэрть");

    public static void CreateImpossibleTakingStuffMessage()
        => CreateInfoMessage("Ты не можешь надеть эту шмотку. Если у тебя есть ЧИТ! Можешь использовать его",
            "ОЙ!");

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
        => MessageBox.Show("Оружие, которое ты хочень надеть, одноручное?",
            "Смена шмотки", MessageBoxButton.YesNo, MessageBoxImage.Warning);
    
    public static void CreateImpossibleLostMessage(string mess)
    {
        var pronoun = mess == "класс" ? "его" : "её";
        var caption = mess == "класс" ? "Смена класса" : "Смена расы";
        CreateInfoMessage(String.Format("Ты не можешь потерять {0}, так как у тебя {1} нет", mess, pronoun),caption);
    }

    public static void CreateNotChosenItemMessage(string mess)
        => CreateInfoMessage(String.Format("Ты не указал {0}", mess), "Недостаточно данных");

    private static void CreateInfoMessage(string mess, string caption)
        => MessageBox.Show(mess, caption, MessageBoxButton.OK, MessageBoxImage.Information);
}