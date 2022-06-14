using System.Collections.Generic;

namespace ManchkinGame;

public static class PlayerPossibilities
{
    public static List<string> Always = new List<string>()
    {
        "Сбросить класс или рассу",
        "сыграть карту \"Получи уровень\"",
        "Сыграть проклятие"
    };
    
    public static List<string> AlwaysButNotInFight = new List<string>()
    {
        "Обменятся шмотками с другим игроком (он не должен быть в бою)",
        "\"Снять\" одни шмотки и \"надеть\" другие",
        "Сыграть только что полученную карту"
    };
    
    public static List<string> AlwaysInFight = new List<string>()
    {
        "Сыграть карты, которые можно сыграть в бою"
    };
    
    public static List<string> YourTurn = new List<string>()
    {
        "Сыграть новый класс или расу",
        "Сыграть шмотку"
    };
    
    public static List<string> YourTurnButNotInFight = new List<string>()
    {
        "Продать шмотки за уровни",
    };
    
    public static List<string> YourTurnInFight = new List<string>()
    {
        "Сыграть некоторые разовые шмотки",
    };
}