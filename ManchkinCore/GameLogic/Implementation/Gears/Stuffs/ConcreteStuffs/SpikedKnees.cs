using ManchkinCore.Enums.Accessory;
using ManchkinCore.GameLogic.Implementation.Gears.Stuffs;
using ManchkinCore.Interfaces;

namespace ManchkinCore.Implementation.Gears;

public class SpikedKnees : SmallStuff
{
    public SpikedKnees()
    {
        Price = 200;
        Damage = 1;
        Weight = Bulkiness.SMALL;
        Fullness = Arms.NO;
        Descriptions = new List<string>();
        FlushingBonus = 0;
        TextRepresentation = "Шипастые коленки";
    }

    public override bool CanBeUsed(IRace? race) => true;

    public override bool CanBeUsed(IClass? _class) => true;

    public override bool CanBeUsed(Genders gender) => true;
}