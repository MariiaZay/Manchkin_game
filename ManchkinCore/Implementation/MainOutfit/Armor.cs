using ManchkinCore.Enums.Accessory;
using ManchkinCore.Interfaces;

namespace ManchkinCore.Implementation;

public abstract class Armor : IStuff
{
    public int Price { get; protected init; }
    public int Damage { get; protected init; }
    public bool ActiveCheat { get; private set; }
    public IMercenary? Mercenary { get; private set; }
    public Bulkiness Weight { get; protected init; }
    public Arms Fullness { get; protected init; }
    
    
    public abstract bool CheckRace(IRaceAndClass race);
    public abstract bool CheckClass(IRaceAndClass _class);
    public abstract bool CheckGender(Genders gender);
    
    public void SetMercenary(IMercenary? mercenary) => Mercenary = mercenary;
    public void RemoveMercenary() => Mercenary = null;
    public void SetCheat() => ActiveCheat = true;
    public void RemoveCheat() =>ActiveCheat = false;
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

    public override bool CheckRace(IRaceAndClass race) => true;

    public override bool CheckClass(IRaceAndClass _class) => _class is not Wizard;

    public override bool CheckGender(Genders gender) => true;
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

    public override bool CheckRace(IRaceAndClass race) => race is Dwarf;

    public override bool CheckClass(IRaceAndClass _class) => true;

    public override bool CheckGender(Genders gender) => true;
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

    public override bool CheckRace(IRaceAndClass race) => true;

    public override bool CheckClass(IRaceAndClass _class) => true;

    public override bool CheckGender(Genders gender) => true;
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

    public override bool CheckRace(IRaceAndClass race) => true;

    public override bool CheckClass(IRaceAndClass _class) => true;

    public override bool CheckGender(Genders gender) => true;
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

    public override bool CheckRace(IRaceAndClass race) => true;

    public override bool CheckClass(IRaceAndClass _class) => true;

    public override bool CheckGender(Genders gender) => true;
}


