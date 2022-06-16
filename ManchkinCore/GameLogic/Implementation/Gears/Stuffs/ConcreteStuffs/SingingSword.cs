using ManchkinCore.Enums.Accessory;
using ManchkinCore.GameLogic.Implementation.Gears.Stuffs;
using ManchkinCore.Interfaces;

namespace ManchkinCore.Implementation.Gears;

public class SingingSword : SmallStuff
{
    public SingingSword()
    {
        Price = 400;
        Damage = 2;
        Weight = Bulkiness.SMALL;
        Fullness = Arms.NO;
        Descriptions = new List<string>();
        FlushingBonus = 0;
        TextRepresentation = "Меч песни и пляски";
    }

    public override bool CanBeUsed(IRace? race) => true;

    public override bool CanBeUsed(IClass? _class) => _class is not Thief || Cheat;

    public override bool CanBeUsed(Genders gender) => true;
}