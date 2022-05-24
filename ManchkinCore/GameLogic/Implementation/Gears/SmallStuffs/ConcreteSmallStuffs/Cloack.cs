using ManchkinCore.Enums.Accessory;
using ManchkinCore.Interfaces;

namespace ManchkinCore.Implementation.Gears;

public class Cloack : SmallStuff
{
    public Cloack()
    {
        Price = 600;
        Damage = 4;
        Weight = Bulkiness.SMALL;
        Fullness = Arms.NO;
        Descriptions = new List<string>();
        FlushingBonus = 0;
        TextRepresentation = "Плащ замутнености";
    }

    public override bool CanBeUsed(IRace? race) => true;

    public override bool CanBeUsed(IClass? _class) => _class is Thief || Cheat;

    public override bool CanBeUsed(Genders gender) => true;
}