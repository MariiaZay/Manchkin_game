using ManchkinCore.Enums.Accessory;
using ManchkinCore.Interfaces;

namespace ManchkinCore.Implementation;

public class CheeseGrater : Weapon
{
    public CheeseGrater()
    {
        Price = 400;
        Damage = 3;
        Weight = Bulkiness.SMALL;
        Fullness = Arms.SINGLE;
        Descriptions = new List<string>();
        FlushingBonus = 0;
        TextRepresentation = "Сыротерка умиротворения";
    }

    public override bool CanBeUsed(IRace? race) => true;
    public override bool CanBeUsed(IClass? _class) => _class is Cleric;
    public override bool CanBeUsed(Genders gender) => true;
}