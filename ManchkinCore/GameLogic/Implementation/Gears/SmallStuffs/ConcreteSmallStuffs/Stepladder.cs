using ManchkinCore.Enums.Accessory;
using ManchkinCore.Interfaces;

namespace ManchkinCore.Implementation.Gears;

public class Stepladder : SmallStuff
{
    public Stepladder()
    {
        Price = 400;
        Damage = 3;
        Weight = Bulkiness.HUGE;
        Fullness = Arms.NO;
        Descriptions = new List<string>();
        FlushingBonus = 0;
        TextRepresentation = "Боевая стремянка";
    }

    public override bool CanBeUsed(IRace? race) => race is Halfling || Cheat;

    public override bool CanBeUsed(IClass? _class) => true;

    public override bool CanBeUsed(Genders gender) => true;
}