using ManchkinCore.Enums.Accessory;

using ManchkinCore.Interfaces;

namespace ManchkinCore.Implementation.Gears;

public abstract class SmallStuff : IStuff, IDescriptable
{
    public int Price { get; protected set; }
    public int Damage { get; protected set; }
    public bool ActiveCheat { get; set; }
    public Bulkiness Weight { get; protected set; }
    public Arms Fullness { get; protected set; }
    public int FlushingBonus { get; protected set; }
    public bool Cheat { get; set; } = false;

    public List<string> Descriptions { get; protected set; }

    public abstract bool CanBeUsed(IRace race);
    public abstract bool CanBeUsed(IClass _class);
    public abstract bool CanBeUsed(Genders gender);
    
}

public class Stepladder : SmallStuff
{
    public Stepladder()
    {
        Price = 400;
        Damage = 3;
        Weight = Bulkiness.HUGE;
        Fullness = Arms.NO;
        Descriptions = new List<string>();
        FlushingBonus = 0;
    }

    public override bool CanBeUsed(IRace race) => race is Halfling || ActiveCheat;

    public override bool CanBeUsed(IClass _class) => true;

    public override bool CanBeUsed(Genders gender) => true;
}

public class GreatTitle : SmallStuff
{
    public GreatTitle()
    {
        Price = 0;
        Damage = 3;
        Weight = Bulkiness.SMALL;
        Fullness = Arms.NO;
        Descriptions = new List<string>();
        FlushingBonus = 0;
    }

    public override bool CanBeUsed(IRace race) => true;

    public override bool CanBeUsed(IClass _class) => true;

    public override bool CanBeUsed(Genders gender) => true;
}

public class SpikedKnees : SmallStuff
{
    public SpikedKnees()
    {
        Price = 200;
        Damage = 1;
        Weight = Bulkiness.SMALL;
        Fullness = Arms.NO;
        Descriptions = new List<string>();
        FlushingBonus = 0;
    }

    public override bool CanBeUsed(IRace race) => true;

    public override bool CanBeUsed(IClass _class) => true;

    public override bool CanBeUsed(Genders gender) => true;
}

public class SingingSword : SmallStuff
{
    public SingingSword()
    {
        Price = 400;
        Damage = 2;
        Weight = Bulkiness.SMALL;
        Fullness = Arms.NO;
        Descriptions = new List<string>();
        FlushingBonus = 0;
    }

    public override bool CanBeUsed(IRace race) => true;

    public override bool CanBeUsed(IClass _class) => _class is not Thief || ActiveCheat;

    public override bool CanBeUsed(Genders gender) => true;
}

public class Sandwich : SmallStuff
{
    public Sandwich()
    {
        Price = 400;
        Damage = 3;
        Weight = Bulkiness.SMALL;
        Fullness = Arms.NO;
        Descriptions = new List<string>();
        FlushingBonus = 0;
    }

    public override bool CanBeUsed(IRace race) => race is Halfling || ActiveCheat;

    public override bool CanBeUsed(IClass _class) => true;

    public override bool CanBeUsed(Genders gender) => true;
}

public class Cloack : SmallStuff
{
    public Cloack()
    {
        Price = 600;
        Damage = 4;
        Weight = Bulkiness.SMALL;
        Fullness = Arms.NO;
        Descriptions = new List<string>();
        FlushingBonus = 0;
    }

    public override bool CanBeUsed(IRace race) => true;

    public override bool CanBeUsed(IClass _class) => _class is Thief || ActiveCheat;

    public override bool CanBeUsed(Genders gender) => true;
}

public class Pantyhose : SmallStuff
{
    public Pantyhose()
    {
        Price = 600;
        Damage = 3;
        Weight = Bulkiness.SMALL;
        Fullness = Arms.NO;
        Descriptions = new List<string>();
        FlushingBonus = 0;
    }

    public override bool CanBeUsed(IRace race) => true;

    public override bool CanBeUsed(IClass _class) => _class is not Warrior || ActiveCheat;

    public override bool CanBeUsed(Genders gender) => true;
}

public class KneepadsOfAllure : SmallStuff, IDescriptable
{
    public KneepadsOfAllure()
    {
        Price = 600;
        Damage = 0;
        Weight = Bulkiness.SMALL;
        Fullness = Arms.NO;
        Descriptions = new List<string>() {
            "Ни один игрок, чей уровень выше твоего, не может отказать тебе помочь в бою и не требует за это награды." +
            " Ты не можешь получить победнй уровень за бой, в котором твой помощник разведен наколенниками."
        };
        FlushingBonus = 0;
    }

    public override bool CanBeUsed(IRace race) => true;

    public override bool CanBeUsed(IClass _class) => true;

    public override bool CanBeUsed(Genders gender) => true;
}
