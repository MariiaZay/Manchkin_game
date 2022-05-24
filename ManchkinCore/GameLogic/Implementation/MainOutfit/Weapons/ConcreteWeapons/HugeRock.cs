using ManchkinCore.Enums.Accessory;
using ManchkinCore.Interfaces;

namespace ManchkinCore.Implementation;

public class HugeRock : Weapon
{
    public HugeRock()
    {
        Price = 0;
        Damage = 3;
        Weight = Bulkiness.HUGE;
        Fullness = Arms.BOTH;
        Descriptions = new List<string>();
        FlushingBonus = 0;
        TextRepresentation = "Огромный камень";
    }

    public override bool CanBeUsed(IRace? race) => true;

    public override bool CanBeUsed(IClass? _class) => true;

    public override bool CanBeUsed(Genders gender) => true;
}