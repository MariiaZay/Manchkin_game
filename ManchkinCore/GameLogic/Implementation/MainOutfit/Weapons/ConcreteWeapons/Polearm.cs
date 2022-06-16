using ManchkinCore.Enums.Accessory;
using ManchkinCore.GameLogic.Implementation.MainOutfit.Weapons;
using ManchkinCore.Interfaces;

namespace ManchkinCore.Implementation;

public class Polearm : BothHandWeapon
{
    public Polearm()
    {
        Price = 600;
        Damage = 4;
        Weight = Bulkiness.HUGE;
        Fullness = Arms.BOTH;
        Descriptions = new List<string>();
        FlushingBonus = 0;
        TextRepresentation = "Швейцарская армейская алебарда";
    }

    public override bool CanBeUsed(IRace? race) => race is Human;

    public override bool CanBeUsed(IClass? _class) => true;

    public override bool CanBeUsed(Genders gender) => true;
}