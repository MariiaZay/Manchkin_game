using ManchkinCore.CardEnums.Accessory;
using ManchkinCore.CardEnums.Aspects;
using ManchkinCore.GameLogic.Interfaces.Accessory;

namespace ManchkinCore.GameLogic.Implementation.MainOutfit.Weapons.ConcreteWeapons;

public class TubeOfCharm : SingleHandWeapon
{
    public TubeOfCharm()
    {
        Price = 300;
        Damage = 0;
        Weight = Bulkiness.HUGE;
        Fullness = Arms.SINGLE;
        Descriptions = new List<string> { FirstFeature };
        FlushingBonus = 3;
        TextRepresentation = "Чарующая дуда";
    }

    private const string FirstFeature = "Успешно смывшись, можешь взять одно сокровище в закрытую (один раз в ход)";
    public override bool CanBeUsed(IRace? race) => true;

    public override bool CanBeUsed(IClass? _class) => true;

    public override bool CanBeUsed(Genders gender) => true;
}