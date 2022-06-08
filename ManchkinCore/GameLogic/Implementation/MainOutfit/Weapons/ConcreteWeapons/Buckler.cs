using ManchkinCore.Enums.Accessory;
using ManchkinCore.Interfaces;

namespace ManchkinCore.Implementation;

public class Buckler : Weapon
{
    public Buckler()
    {
        Price = 400;
        Damage = 2;
        Weight = Bulkiness.SMALL;
        Fullness = Arms.SINGLE;
        Descriptions = new List<string>();
        FlushingBonus = 0;
        TextRepresentation = "Баклер бахвала";
    }

    public override bool CanBeUsed(IRace? race) => true;
    public override bool CanBeUsed(IClass? _class) => true;
    public override bool CanBeUsed(Genders gender) => true;
}