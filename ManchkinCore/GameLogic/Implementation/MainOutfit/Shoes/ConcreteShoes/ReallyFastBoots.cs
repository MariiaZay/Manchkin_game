﻿using ManchkinCore.CardEnums.Accessory;
using ManchkinCore.CardEnums.Aspects;
using ManchkinCore.GameLogic.Interfaces.Accessory;

namespace ManchkinCore.GameLogic.Implementation.MainOutfit.Shoes.ConcreteShoes;

public class ReallyFastBoots : Shoes
{
    public ReallyFastBoots()
    {
        Price = 400;
        Damage = 0;
        Weight = Bulkiness.SMALL;
        Fullness = Arms.NO;
        Descriptions = new List<string>();
        FlushingBonus = 2;
        TextRepresentation = "Башмаки реально быстрого бега";
    }

    public override bool CanBeUsed(IRace? _class) => true;

    public override bool CanBeUsed(IClass? race) => true;

    public override bool CanBeUsed(Genders gender) => true;
}