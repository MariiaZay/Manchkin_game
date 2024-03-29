﻿using ManchkinCore.CardEnums.Accessory;
using ManchkinCore.CardEnums.Aspects;
using ManchkinCore.GameLogic.Interfaces.Accessory;
using ManchkinCore.GameLogic.Interfaces.Manchkin;

namespace ManchkinCore.GameLogic.Implementation.Gears.Stuffs.ConcreteStuffs;

public class KneepadsOfAllure : SmallStuff, IDescriptable
{
    public KneepadsOfAllure()
    {
        Price = 600;
        Damage = 0;
        Weight = Bulkiness.SMALL;
        Fullness = Arms.NO;
        Descriptions = new List<string>
        {
            "Ни один игрок, чей уровень выше твоего, не может отказать тебе помочь в бою и не требует за это награды." +
            " Ты не можешь получить победнй уровень за бой, в котором твой помощник разведен наколенниками."
        };
        FlushingBonus = 0;
        TextRepresentation = "Наколенники развода";
    }

    public override bool CanBeUsed(IRace? race) => true;

    public override bool CanBeUsed(IClass? _class) => true;

    public override bool CanBeUsed(Genders gender) => true;
}