using ManchkinCore.CardEnums.Accessory;
using ManchkinCore.CardEnums.Aspects;
using ManchkinCore.GameLogic.Implementation.Accessory.Races;
using ManchkinCore.GameLogic.Interfaces.Accessory;

namespace ManchkinCore.GameLogic.Implementation.MainOutfit.Weapons.ConcreteWeapons;

public class Hammer : SingleHandWeapon
{
    public Hammer()
    {
        Price = 600;
        Damage = 4;
        Weight = Bulkiness.SMALL;
        Fullness = Arms.SINGLE;
        Descriptions = new List<string>();
        FlushingBonus = 0;
        TextRepresentation = "Коленоотбойный молот";
    }

    public override bool CanBeUsed(IRace? race) => race is Dwarf || Cheat;
    public override bool CanBeUsed(IClass? _class) => true;
    public override bool CanBeUsed(Genders gender) => true;
}