using ManchkinCore.Enums.Accessory;
using ManchkinCore.GameLogic.Implementation.MainOutfit.Weapons;
using ManchkinCore.Interfaces;

namespace ManchkinCore.Implementation;

public class BastardSword : SingleHandWeapon
{
    public BastardSword()
    {
        Price = 400;
        Damage = 2;
        Weight = Bulkiness.SMALL;
        Fullness = Arms.SINGLE;
        Descriptions = new List<string>();
        FlushingBonus = 0;
        TextRepresentation = "Меч коварного ублюдка";
    }

    public override bool CanBeUsed(IRace? race) => true;
    public override bool CanBeUsed(IClass? _class) => true;
    public override bool CanBeUsed(Genders gender) => true;
}