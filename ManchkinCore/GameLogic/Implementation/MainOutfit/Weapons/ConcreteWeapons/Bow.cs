using ManchkinCore.Enums.Accessory;
using ManchkinCore.Interfaces;

namespace ManchkinCore.Implementation;

public class Bow : BothHandWeapon
{
    public Bow()
    {
        Price = 800;
        Damage = 4;
        Weight = Bulkiness.SMALL;
        Fullness = Arms.BOTH;
        Descriptions = new List<string>();
        FlushingBonus = 0;
        TextRepresentation = "Лучок с ленточками";
    }

    public override bool CanBeUsed(IRace? race) => race is Elf || Cheat;

    public override bool CanBeUsed(IClass? _class) => true;

    public override bool CanBeUsed(Genders gender) => true;
}