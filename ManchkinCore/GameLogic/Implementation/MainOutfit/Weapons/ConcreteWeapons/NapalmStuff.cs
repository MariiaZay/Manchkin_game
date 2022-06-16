using ManchkinCore.Enums.Accessory;
using ManchkinCore.GameLogic.Implementation.MainOutfit.Weapons;
using ManchkinCore.Interfaces;

namespace ManchkinCore.Implementation;

public class NapalmStuff : SingleHandWeapon
{
    public NapalmStuff()
    {
        Price = 800;
        Damage = 5;
        Weight = Bulkiness.SMALL;
        Fullness = Arms.SINGLE;
        Descriptions = new List<string>();
        FlushingBonus = 0;
        TextRepresentation = "Посох напалма";
    }

    public override bool CanBeUsed(IRace? race) => true;

    public override bool CanBeUsed(IClass? _class) => _class is Wizard || Cheat;

    public override bool CanBeUsed(Genders gender) => true;
}