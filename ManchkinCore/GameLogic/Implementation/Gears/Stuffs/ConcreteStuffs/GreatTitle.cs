using ManchkinCore.Enums.Accessory;
using ManchkinCore.GameLogic.Implementation.Gears.Stuffs;
using ManchkinCore.Interfaces;

namespace ManchkinCore.Implementation.Gears;

public class GreatTitle : SmallStuff
{
    public GreatTitle()
    {
        Price = 0;
        Damage = 3;
        Weight = Bulkiness.SMALL;
        Fullness = Arms.NO;
        Descriptions = new List<string>();
        FlushingBonus = 0;
        TextRepresentation = "Реально впечатляющий титул";
    }

    public override bool CanBeUsed(IRace? race) => true;

    public override bool CanBeUsed(IClass? _class) => true;

    public override bool CanBeUsed(Genders gender) => true;
}