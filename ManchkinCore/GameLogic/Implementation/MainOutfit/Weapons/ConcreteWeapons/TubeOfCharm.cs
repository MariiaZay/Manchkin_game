using ManchkinCore.Enums.Accessory;
using ManchkinCore.Interfaces;

namespace ManchkinCore.Implementation;

public class TubeOfCharm : Weapon
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