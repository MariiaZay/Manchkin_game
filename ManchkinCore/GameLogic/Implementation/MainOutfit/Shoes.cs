using ManchkinCore.Enums.Accessory;
using ManchkinCore.Interfaces;

namespace ManchkinCore.Implementation;

public abstract class Shoes : IStuff
{
    public int Price { get; protected init; }
    public int Damage { get; protected init; }
    public bool ActiveCheat { get; set; }
    public Bulkiness Weight { get; protected init; }
    public Arms Fullness { get; protected init; }
    public int FlushingBonus { get; protected set; }
    public bool Cheat { get; set; } = false;

    public abstract bool CanBeUsed(IRace _class);
    public abstract bool CanBeUsed(IClass race);
    public abstract bool CanBeUsed(Genders gender);
    public List<string> Descriptions { get; protected set; }
    public string TextRepresentation { get; protected set; }
}

public class MightyShoes : Shoes
{
    public override bool CanBeUsed(IRace race) => true;

    public override bool CanBeUsed(IClass _class) => true;

    public override bool CanBeUsed(Genders gender) => true;

    public MightyShoes()
    {
        Price = 400;
        Damage = 2;
        Weight = Bulkiness.SMALL;
        Fullness = Arms.NO;
        Descriptions = new List<string>();
        FlushingBonus = 0;
        TextRepresentation = "Башмаки могучего пенделя";
    }
}

public class ReallyFastBoots : Shoes
{
    public ReallyFastBoots()
    {
        Price = 400;
        Damage = 0;
        Weight = Bulkiness.SMALL;
        Fullness = Arms.NO;
        Descriptions = new List<string>();
        FlushingBonus = 2;
        TextRepresentation = "Башмаки реально быстрого бега";
    }

    public override bool CanBeUsed(IRace _class) => true;

    public override bool CanBeUsed(IClass race) => true;

    public override bool CanBeUsed(Genders gender) => true;
}

public class SandalsOfProtection : Shoes
{
    public SandalsOfProtection()
    {
        Price = 700;
        Damage = 0;
        Weight = Bulkiness.SMALL;
        Fullness = Arms.NO;
        Descriptions = new List<string> {FirstFeature};
        FlushingBonus = 0;
        TextRepresentation = "Сандалеты-протекторы";
    }

    private const string FirstFeature = "Защищают тебя от проклятий, которые ты вытягиваешь, вышибая двери. Не спасут "
                                        + "от проклятий, сыгранных на тебя другими игроками";

    public override bool CanBeUsed(IRace _class) => true;

    public override bool CanBeUsed(IClass race) => true;

    public override bool CanBeUsed(Genders gender) => true;
}