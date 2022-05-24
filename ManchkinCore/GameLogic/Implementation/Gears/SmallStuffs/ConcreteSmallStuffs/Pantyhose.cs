using ManchkinCore.Enums.Accessory;
using ManchkinCore.Interfaces;

namespace ManchkinCore.Implementation.Gears;

public class Pantyhose : SmallStuff
{
    public Pantyhose()
    {
        Price = 600;
        Damage = 3;
        Weight = Bulkiness.SMALL;
        Fullness = Arms.NO;
        Descriptions = new List<string>();
        FlushingBonus = 0;
        TextRepresentation = "Колготы великанской силы";
    }

    public override bool CanBeUsed(IRace? race) => true;

    public override bool CanBeUsed(IClass? _class) => _class is not Warrior || Cheat;

    public override bool CanBeUsed(Genders gender) => true;
}