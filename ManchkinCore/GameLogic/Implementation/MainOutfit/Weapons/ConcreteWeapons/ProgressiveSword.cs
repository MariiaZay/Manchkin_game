using ManchkinCore.Enums.Accessory;
using ManchkinCore.Interfaces;

namespace ManchkinCore.Implementation;

public class ProgressiveSword : SingleHandWeapon
{
    public ProgressiveSword()
    {
        Price = 400;
        Damage = 3;
        Weight = Bulkiness.SMALL;
        Fullness = Arms.SINGLE;
        Descriptions = new List<string>();
        FlushingBonus = 0;
        TextRepresentation = "Меч широты взглядов";
    }

    public override bool CanBeUsed(IRace? race) => true;
    public override bool CanBeUsed(IClass? _class) => true;
    public override bool CanBeUsed(Genders gender) => gender is Genders.FEMALE || Cheat;
}