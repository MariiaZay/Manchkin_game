using ManchkinCore.CardEnums.Accessory;
using ManchkinCore.CardEnums.Aspects;
using ManchkinCore.GameLogic.Interfaces.Accessory;

namespace ManchkinCore.GameLogic.Implementation.MainOutfit.Shoes.ConcreteShoes;

public class SandalsOfProtection : Shoes
{
    public SandalsOfProtection()
    {
        Price = 700;
        Damage = 0;
        Weight = Bulkiness.SMALL;
        Fullness = Arms.NO;
        Descriptions = new List<string> { FirstFeature };
        FlushingBonus = 0;
        TextRepresentation = "Сандалеты-протекторы";
    }

    private const string FirstFeature = "Защищают тебя от проклятий, которые ты вытягиваешь, вышибая двери. Не спасут "
                                        + "от проклятий, сыгранных на тебя другими игроками";

    public override bool CanBeUsed(IRace? _class) => true;

    public override bool CanBeUsed(IClass? race) => true;

    public override bool CanBeUsed(Genders gender) => true;
}