using ManchkinCore.Enums.Accessory;
using ManchkinCore.GameLogic.Implementation.Gears.Stuffs;
using ManchkinCore.Interfaces;

namespace ManchkinCore.Implementation.Gears;

public class Sandwich : SmallStuff
{
    public Sandwich()
    {
        Price = 400;
        Damage = 3;
        Weight = Bulkiness.SMALL;
        Fullness = Arms.NO;
        Descriptions = new List<string>();
        FlushingBonus = 0;
        TextRepresentation = "Сэндвич \"Душитая смерть\"";
    }

    public override bool CanBeUsed(IRace? race) => race is Halfling || Cheat;

    public override bool CanBeUsed(IClass? _class) => true;

    public override bool CanBeUsed(Genders gender) => true;
}