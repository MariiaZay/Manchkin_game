using System;
using System.Windows;
using ManchkinCore;
using ManchkinCore.Implementation;
using Microsoft.VisualBasic;

namespace ManchkinGame;

public static class UserMessage
{
    public static bool CreateChangeSingleWeaponMessage(string currentWeapon, string takenWeapon,string hand)
    {
        var answer = MessageBox.Show(
            String.Format("Ты уверен, что хочешь взять {0} в {1}? У тебя уже в ней {2}", 
                takenWeapon, hand, currentWeapon),
            "Смена оружия", MessageBoxButton.YesNo, MessageBoxImage.Warning);
        return answer == MessageBoxResult.Yes;
    }
    
    public static bool CreateLostOrTakeOffMessage()
    {
        var answer = MessageBox.Show( 
            "Если ты теряешь шмотку по принуждению, нажми Yes, иначе No",
            "ЧТО ТЫ ДЕЛАЕШЬ", MessageBoxButton.YesNo, MessageBoxImage.Warning);
        return answer == MessageBoxResult.Yes;
    }

    public static void CreateCantDoItNowMessage(string mess)
        => CreateInfoMessage(String.Format("Ты не можешь сделать это сейчас, потому что сейчас {0}", mess), "ОЙ");
    
    public static bool CreateChangeBothWeaponMessage(string takenWeapon)
    {
        
        var answer = MessageBox.Show(
            String.Format("Ты уверен, что хочешь взять {0} в обе руки? У тебя уже руки заняты", 
                takenWeapon),
            "Смена оружия", MessageBoxButton.YesNo, MessageBoxImage.Warning);
        return answer == MessageBoxResult.Yes;
    }

    public static bool CreateChangeEquipmentMessage(string current, string taken)
    {
        var answer = MessageBox.Show(
            String.Format("Ты уверен, что хочешь взять {0}? У тебя уже есть {1}", 
                taken, current),
            "Смена экипировки", MessageBoxButton.YesNo, MessageBoxImage.Warning);
        return answer == MessageBoxResult.Yes;
    }

    public static void CreateOneWeaponInBothHandsMessage()
        => CreateInfoMessage("Ты не можешь держать одно и то же одноручное оружие в обеих руках",
            "ОЙ");

    public static void CreateEndStuffMessage(string mess)
        => CreateInfoMessage(
            String.Format("Ты больше не можешь надевать {0} шмотки, так как они кончились в колоде", mess),
            "КОНЧИЛИСЬ КАРТЫ");
    
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

    public static void CreateImpossibleTakingStuffMessage(string stuff)
        => CreateInfoMessage(String.Format("Ты не можешь надеть {0}. Если у тебя есть ЧИТ! Можешь использовать его", stuff),
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

    public static void CreateImpossibleLostMessage(string mess)
    {
        var pronoun = mess == "класс" ? "его" : "её";
        var caption = mess == "класс" ? "Смена класса" : "Смена расы";
        CreateInfoMessage(String.Format("Ты не можешь потерять {0}, так как у тебя {1} нет", mess, pronoun),caption);
    }

    public static void CreateNotChosenItemMessage(string mess)
        => CreateInfoMessage(String.Format("Ты не указал {0}", mess), "Недостаточно данных");
    
    public static void CreateAlreadyLostWeapon()
        => CreateInfoMessage("Ты уже потерял оруие в этой руке!", "ОЙ!");
    

    public static void CreateHalfCleanMessage(string mess)
        => CreateInfoMessage(String.Format("Ты уже чистый {0}", mess), mess);

    private static void CreateInfoMessage(string mess, string caption)
        => MessageBox.Show(mess, caption, MessageBoxButton.OK, MessageBoxImage.Information);
}