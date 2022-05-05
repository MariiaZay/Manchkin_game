using ManchkinCore.Enums.Accessory;
using ManchkinCore.Interfaces;

namespace ManchkinCore.Implementation;

public abstract class Weapon : IStuff
{
    public int Price { get; protected init; }
    public int Damage { get; protected init; }
    public bool ActiveCheat { get; set; }
    public Bulkiness Weight { get; protected init; }
    public Arms Fullness { get; protected init; }
    public int FlushingBonus { get; protected set; }
    public bool Cheat { get; set; } = false;

    public abstract bool CanBeUsed(IRace race);
    public abstract bool CanBeUsed(IClass _class);
    public abstract bool CanBeUsed(Genders gender);
    public List<string> Descriptions { get; protected set; }
}

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
    }

    public override bool CanBeUsed(IRace race) => true;
    public override bool CanBeUsed(IClass _class) => true;
    public override bool CanBeUsed(Genders gender) => true;
}

public class Hammer : Weapon
{
    public Hammer()
    {
        Price = 600;
        Damage = 4;
        Weight = Bulkiness.SMALL;
        Fullness = Arms.SINGLE;
        Descriptions = new List<string>();
        FlushingBonus = 0;
    }

    public override bool CanBeUsed(IRace race) => race is Dwarf || ActiveCheat;
    public override bool CanBeUsed(IClass _class) => true;
    public override bool CanBeUsed(Genders gender) => true;
}

public class ProgressiveSword : Weapon
{
    public ProgressiveSword()
    {
        Price = 400;
        Damage = 3;
        Weight = Bulkiness.SMALL;
        Fullness = Arms.SINGLE;
        Descriptions = new List<string>();
        FlushingBonus = 0;
    }

    public override bool CanBeUsed(IRace race) => true;
    public override bool CanBeUsed(IClass _class) => true;
    public override bool CanBeUsed(Genders gender) => gender is Genders.FEMALE || ActiveCheat;
}

public class BastardSword : Weapon
{
    public BastardSword()
    {
        Price = 400;
        Damage = 2;
        Weight = Bulkiness.SMALL;
        Fullness = Arms.SINGLE;
        Descriptions = new List<string>();
        FlushingBonus = 0;
    }

    public override bool CanBeUsed(IRace race) => true;
    public override bool CanBeUsed(IClass _class) => true;
    public override bool CanBeUsed(Genders gender) => true;
}

public class CheeseGrater : Weapon
{
    public CheeseGrater()
    {
        Price = 400;
        Damage = 3;
        Weight = Bulkiness.SMALL;
        Fullness = Arms.SINGLE;
        Descriptions = new List<string>();
        FlushingBonus = 0;
    }

    public override bool CanBeUsed(IRace race) => true;
    public override bool CanBeUsed(IClass _class) => true;
    public override bool CanBeUsed(Genders gender) => true;
}

public class NapalmStuff : Weapon
{
    public NapalmStuff()
    {
        Price = 800;
        Damage = 5;
        Weight = Bulkiness.SMALL;
        Fullness = Arms.SINGLE;
        Descriptions = new List<string>();
        FlushingBonus = 0;
    }

    public override bool CanBeUsed(IRace race) => true;

    public override bool CanBeUsed(IClass _class) => _class is Wizard || ActiveCheat;

    public override bool CanBeUsed(Genders gender) => true;
}

public class Rapier : Weapon
{
    public Rapier()
    {
        Price = 600;
        Damage = 3;
        Weight = Bulkiness.SMALL;
        Fullness = Arms.SINGLE;
        Descriptions = new List<string>();
        FlushingBonus = 0;
    }

    public override bool CanBeUsed(IRace race) => true;

    public override bool CanBeUsed(IClass _class) => true;

    public override bool CanBeUsed(Genders gender) => true;
}

public class Dagger : Weapon
{
    public Dagger()
    {
        Price = 400;
        Damage = 3;
        Weight = Bulkiness.SMALL;
        Fullness = Arms.SINGLE;
        Descriptions = new List<string>();
        FlushingBonus = 0;
    }

    public override bool CanBeUsed(IRace race) => true;

    public override bool CanBeUsed(IClass _class) => _class is Thief || ActiveCheat;

    public override bool CanBeUsed(Genders gender) => true;
}

public class Club : Weapon
{
    public Club()
    {
        Price = 600;
        Damage = 4;
        Weight = Bulkiness.SMALL;
        Fullness = Arms.SINGLE;
        Descriptions = new List<string>();
        FlushingBonus = 0;
    }

    public override bool CanBeUsed(IRace race) => true;

    public override bool CanBeUsed(IClass _class) => _class is Cleric || ActiveCheat;

    public override bool CanBeUsed(Genders gender) => true;
}

public class Shild : Weapon
{
    public Shild()
    {
        Price = 600;
        Damage = 4;
        Weight = Bulkiness.HUGE;
        Fullness = Arms.SINGLE;
        Descriptions = new List<string>();
        FlushingBonus = 0;
    }

    public override bool CanBeUsed(IRace race) => true;

    public override bool CanBeUsed(IClass _class) => true;

    public override bool CanBeUsed(Genders gender) => true;
}

public class Oak : Weapon
{
    public Oak()
    {
        Price = 400;
        Damage = 3;
        Weight = Bulkiness.SMALL;
        Fullness = Arms.SINGLE;
        Descriptions = new List<string>();
        FlushingBonus = 0;
    }

    public override bool CanBeUsed(IRace race) => true;

    public override bool CanBeUsed(IClass _class) => true;

    public override bool CanBeUsed(Genders gender) => gender is Genders.MALE || ActiveCheat;
}

public class Chainsaw : Weapon
{
    public Chainsaw()
    {
        Price = 600;
        Damage = 3;
        Weight = Bulkiness.HUGE;
        Fullness = Arms.BOTH;
        Descriptions = new List<string>();
        FlushingBonus = 0;
    }

    public override bool CanBeUsed(IRace race) => true;

    public override bool CanBeUsed(IClass _class) => true;

    public override bool CanBeUsed(Genders gender) => true;
}

public class Halberd : Weapon
{
    public Halberd()
    {
        Price = 600;
        Damage = 4;
        Weight = Bulkiness.HUGE;
        Fullness = Arms.BOTH;
        Descriptions = new List<string>();
        FlushingBonus = 0;
    }

    public override bool CanBeUsed(IRace race) => true;

    public override bool CanBeUsed(IClass _class) => true;

    public override bool CanBeUsed(Genders gender) => true;
}

public class HugeRock : Weapon
{
    public HugeRock()
    {
        Price = 0;
        Damage = 3;
        Weight = Bulkiness.HUGE;
        Fullness = Arms.BOTH;
        Descriptions = new List<string>();
        FlushingBonus = 0;
    }

    public override bool CanBeUsed(IRace race) => true;

    public override bool CanBeUsed(IClass _class) => true;

    public override bool CanBeUsed(Genders gender) => true;
}

public class Pole : Weapon
{
    public Pole()
    {
        Price = 200;
        Damage = 1;
        Weight = Bulkiness.SMALL;
        Fullness = Arms.BOTH;
        Descriptions = new List<string>();
        FlushingBonus = 0;
    }

    public override bool CanBeUsed(IRace race) => true;

    public override bool CanBeUsed(IClass _class) => true;

    public override bool CanBeUsed(Genders gender) => true;
}

public class Bow : Weapon
{
    public Bow()
    {
        Price = 800;
        Damage = 4;
        Weight = Bulkiness.SMALL;
        Fullness = Arms.BOTH;
        Descriptions = new List<string>();
        FlushingBonus = 0;
    }

    public override bool CanBeUsed(IRace race) => race is Elf || ActiveCheat;

    public override bool CanBeUsed(IClass _class) => true;

    public override bool CanBeUsed(Genders gender) => true;
}

public class RatOnStick : Weapon
{
    public RatOnStick()
    {
        Price = 0;
        Damage = 1;
        Weight = Bulkiness.SMALL;
        Fullness = Arms.SINGLE;
        Descriptions = new List<string> {FirstFeature};
        FlushingBonus = 0;
    }

    private const string FirstFeature = "Лучше, чем ничего! Можешь сбросить крысу, даже если не используешь ее, "
                                        + "чтобы автоматически смыться от любого монстра не выше 8 уровня";

    public override bool CanBeUsed(IRace race) => true;

    public override bool CanBeUsed(IClass _class) => true;

    public override bool CanBeUsed(Genders gender) => true;
}

public class TubeOfCharm : Weapon
{
    public TubeOfCharm()
    {
        Price = 300;
        Damage = 0;
        Weight = Bulkiness.HUGE;
        Fullness = Arms.SINGLE;
        Descriptions = new List<string> {FirstFeature};
        FlushingBonus = 3;
    }

    private const string FirstFeature = "Успешно смывшись, можешь взять одно сокровище в закрытую (один раз в ход)";
    public override bool CanBeUsed(IRace race) => true;

    public override bool CanBeUsed(IClass _class) => true;

    public override bool CanBeUsed(Genders gender) => true;
}
