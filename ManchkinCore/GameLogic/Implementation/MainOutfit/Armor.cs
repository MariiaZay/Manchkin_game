using ManchkinCore.Enums.Accessory;
using ManchkinCore.Interfaces;

namespace ManchkinCore.Implementation;

public abstract class Armor : IStuff
{
    public int Price { get; protected init; }
    public int Damage { get; protected init; }
    public bool ActiveCheat { get; protected set; }
    public Bulkiness Weight { get; protected init; }
    public Arms Fullness { get; protected init; }
    
    
    public abstract bool CanBeUsed(IRace race);
    public abstract bool CanBeUsed(IClass _class);
    public abstract bool CanBeUsed(Genders gender);
}

public class MithrilArmor : Armor
{
    public MithrilArmor()
    {
        Price = 600;
        Damage = 3;
        Weight = Bulkiness.HUGE;
        Fullness = Arms.NO;
    }

    public override bool CanBeUsed(IRace race) => true;

    public override bool CanBeUsed(IClass _class) => _class is not Wizard;

    public override bool CanBeUsed(Genders gender) => true;
}

public class DwarfArmor : Armor
{
    public DwarfArmor()
    {
        Price = 400;
        Damage = 3;
        Weight = Bulkiness.SMALL;
        Fullness = Arms.NO;
    }

    public override bool CanBeUsed(IRace race) => race is Dwarf;

    public override bool CanBeUsed(IClass _class) => true;

    public override bool CanBeUsed(Genders gender) => true;
}

public class LeatherArmor : Armor
{
    public LeatherArmor()
    {
        Price = 200;
        Damage = 1;
        Weight = Bulkiness.SMALL;
        Fullness = Arms.NO;
    }

    public override bool CanBeUsed(IRace race) => true;

    public override bool CanBeUsed(IClass _class) => true;

    public override bool CanBeUsed(Genders gender) => true;
}

public class MucousMembrane : Armor
{
    public MucousMembrane()
    {
        Price = 200;
        Damage = 1;
        Weight = Bulkiness.SMALL;
        Fullness = Arms.NO;
    }

    public override bool CanBeUsed(IRace race) => true;

    public override bool CanBeUsed(IClass _class) => true;

    public override bool CanBeUsed(Genders gender) => true;
}

public class FlamingArmor : Armor
{
    public FlamingArmor()
    {
        Price = 400;
        Damage = 2;
        Weight = Bulkiness.SMALL;
        Fullness = Arms.NO;
    }

    public override bool CanBeUsed(IRace race) => true;

    public override bool CanBeUsed(IClass _class) => true;

    public override bool CanBeUsed(Genders gender) => true;
}


