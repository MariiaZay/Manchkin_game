﻿using ManchkinCore.CardEnums.Accessory;
using ManchkinCore.CardEnums.Aspects;
using ManchkinCore.GameLogic.Interfaces.Accessory;

namespace ManchkinCore.GameLogic.Implementation.MainOutfit.Weapons.ConcreteWeapons;

public class Buckler : SingleHandWeapon
{
    public Buckler()
    {
        Price = 400;
        Damage = 2;
        Weight = Bulkiness.SMALL;
        Fullness = Arms.SINGLE;
        Descriptions = new List<string>();
        FlushingBonus = 0;
        TextRepresentation = "Баклер бахвала";
    }

    public override bool CanBeUsed(IRace? race) => true;
    public override bool CanBeUsed(IClass? _class) => true;
    public override bool CanBeUsed(Genders gender) => true;
}