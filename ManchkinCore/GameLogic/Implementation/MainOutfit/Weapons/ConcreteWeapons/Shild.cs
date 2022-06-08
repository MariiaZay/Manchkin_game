using ManchkinCore.Enums.Accessory;
using ManchkinCore.Interfaces;

namespace ManchkinCore.Implementation;

public class Shild : Weapon
{
    public Shild()
    {
        Price = 600;
        Damage = 4;
        Weight = Bulkiness.HUGE;
        Fullness = Arms.SINGLE;
        Descriptions = new List<string>();
        FlushingBonus = 0;
        TextRepresentation = "Вездешний щит";
    }

    public override bool CanBeUsed(IRace? race) => true;

    public override bool CanBeUsed(IClass? _class) => true;

    public override bool CanBeUsed(Genders gender) => true;
}