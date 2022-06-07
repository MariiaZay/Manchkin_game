using ManchkinCore.Enums.Accessory;
using ManchkinCore.Interfaces;

namespace ManchkinCore.Implementation;

public class MithrilArmor : Armor
{
    public MithrilArmor()
    {
        Price = 600;
        Damage = 3;
        Weight = Bulkiness.HUGE;
        Fullness = Arms.NO;
        Descriptions = new List<string>();
        FlushingBonus = 0;
        TextRepresentation = "Мифрильная броня";
    }

    public override bool CanBeUsed(IRace? race) => true;

    public override bool CanBeUsed(IClass? _class) => _class is not Wizard || Cheat;

    public override bool CanBeUsed(Genders gender) => true;
}