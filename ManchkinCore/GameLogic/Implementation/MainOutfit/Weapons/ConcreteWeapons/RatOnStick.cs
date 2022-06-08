using ManchkinCore.Enums.Accessory;
using ManchkinCore.Interfaces;

namespace ManchkinCore.Implementation;

public class RatOnStick : Weapon
{
    public RatOnStick()
    {
        Price = 0;
        Damage = 1;
        Weight = Bulkiness.SMALL;
        Fullness = Arms.SINGLE;
        Descriptions = new List<string> { FirstFeature };
        FlushingBonus = 0;
        TextRepresentation = "Крыса на палочке";
    }

    private const string FirstFeature = "Лучше, чем ничего! Можешь сбросить крысу, даже если не используешь ее, "
                                        + "чтобы автоматически смыться от любого монстра не выше 8 уровня";

    public override bool CanBeUsed(IRace? race) => true;

    public override bool CanBeUsed(IClass? _class) => true;

    public override bool CanBeUsed(Genders gender) => true;
}