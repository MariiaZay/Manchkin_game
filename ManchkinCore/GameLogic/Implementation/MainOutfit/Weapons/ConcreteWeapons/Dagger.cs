using ManchkinCore.Enums.Accessory;
using ManchkinCore.GameLogic.Implementation.MainOutfit.Weapons;
using ManchkinCore.Interfaces;

namespace ManchkinCore.Implementation;

public class Dagger : SingleHandWeapon
{
    public Dagger()
    {
        Price = 400;
        Damage = 3;
        Weight = Bulkiness.SMALL;
        Fullness = Arms.SINGLE;
        Descriptions = new List<string>();
        FlushingBonus = 0;
        TextRepresentation = "Кинжал измены";
    }

    public override bool CanBeUsed(IRace? race) => true;

    public override bool CanBeUsed(IClass? _class) => _class is Thief || Cheat;

    public override bool CanBeUsed(Genders gender) => true;
}