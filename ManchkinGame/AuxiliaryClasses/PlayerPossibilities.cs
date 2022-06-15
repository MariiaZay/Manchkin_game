using System.Collections.Generic;

namespace ManchkinGame;

public static class PlayerPossibilities
{
    public static List<string> Always = new()
    {
        "Сбросить класс или рассу",
        "сыграть карту \"Получи уровень\"",
        "Сыграть проклятие"
    };
    
    public static List<string> AlwaysButNotInFight = new()
    {
        "Обменятся шмотками с другим игроком (он не должен быть в бою)",
        "\"Снять\" одни шмотки и \"надеть\" другие",
        "Сыграть только что полученную карту"
    };
    
    public static List<string> AlwaysInFight = new()
    {
        "Сыграть карты, которые можно сыграть в бою"
    };
    
    public static List<string> YourTurn = new()
    {
        "Сыграть новый класс или расу",
        "Сыграть шмотку"
    };
    
    public static List<string> YourTurnButNotInFight = new()
    {
        "Продать шмотки за уровни",
    };
    
    public static List<string> YourTurnInFight = new()
    {
        "Сыграть некоторые разовые шмотки",
    };
}