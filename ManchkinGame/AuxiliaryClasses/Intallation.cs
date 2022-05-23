using ManchkinCore.Enums;
using ManchkinCore.Enums.Accessory;
using ManchkinCore.Implementation;
using System.Windows;

namespace ManchkinGame;

public static class Intallation
{
    #region Base parameters

    public static string Race(Player player)
        => player.Manchkin.Race.TextRepresentation;

    
    public static string Class(Player player)
        => player.Manchkin.Class.TextRepresentation;

    public static string Gender(Player player)
        => player.Manchkin.Gender == Genders.MALE ? "мужчина" : "женщина";

    public static string Damage(Player player) => player.Manchkin.Damage.ToString();
    public static string Life(Player player) =>  player.Manchkin.IsDead  ? "мертв" : "жив";

    public static string Level(Player player) => player.Manchkin.Level.ToString();

    #endregion

    #region Addition Parameters

    public static string CardCount(Player player)
        => player.Manchkin.CardsCount.ToString();

    public static string FlushingBonus(Player player)
        => player.Manchkin.FlushingBonus.ToString();

    public static string DoublePrice(Player player)
        =>  player.Manchkin.DoublePrice ? "активно" : "неактивно";

    public static string HalfBlood(Player player)
    {
        var halfBlood = "";
        if (!player.Manchkin.IsHalfBlood)
            halfBlood = "неактивно";
        else
            halfBlood = player.Manchkin.HalfBlood.HalfType == HalfTypes.BOTH
                ? player.Manchkin.HalfBlood.SecondRace.ToString()
                : "чистая раса";
        
        return halfBlood;
    }

    public static string SuperManchkin(Player player)
    {
        var superManchkin = "";
        if (!player.Manchkin.IsSuperManchkin)
            superManchkin = "неактивно";
        else
            superManchkin = player.Manchkin.SuperManchkin.HalfType == HalfTypes.BOTH
                ? player.Manchkin.SuperManchkin.SecondClass.ToString()
                : "чистый класс";
        return superManchkin;
    }
    #endregion

    #region Stuff

    public static string Hat(Player player)
        => player.Manchkin.WornHat == null ? "нет" : "есть";
    
    public static string Shoes(Player player)
        => player.Manchkin.WornShoes == null ? "нет" : "есть";
    
    public static string Armor(Player player)
        => player.Manchkin.WornArmor == null ? "нет" : "есть";

    public static string Weapon(Player player)
    {
        var weapon = "";
        if (player.Manchkin.Hands.LeftHand == null && player.Manchkin.Hands.LeftHand == null)
            weapon = "нет";
        else
        {
            if (player.Manchkin.Hands.LeftHand != null && player.Manchkin.Hands.LeftHand == null)
                weapon = "левая";
            else
            {
                if (player.Manchkin.Hands.LeftHand == null && player.Manchkin.Hands.LeftHand != null)
                    weapon = "правая";
                else weapon = "обе";
            }
        }
        return weapon;
    }

    public static string SmallStuff(Player player)
        => player.Manchkin.SmallStuffs.Count == 0 ? "нет" : "есть";
    
    public static string HugeStuff(Player player)
        => player.Manchkin.HugeStuffs.Count == 0 ? "нет" : "есть";

    #endregion
}