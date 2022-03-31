using ManchkinCore.Enums.Accessory;
using ManchkinCore.Enums.Gears;
using ManchkinCore.Interfaces;

namespace ManchkinCore.Implementation.Gears;

public abstract class SmallStuff : IStuff
{
    public int Price { get; protected set; }
    public int Damage { get; protected set; }
    public bool ActiveCheat { get; private set; }
    public IMercenary? Mercenary { get; private set; }
    public Bulkiness Weight { get; protected set; }
    public Arms Fullness { get; protected set; }

    public abstract bool CheckRace(IRaceAndClass race);

    public abstract bool CheckClass(IRaceAndClass _class);

    public abstract bool CheckGender(Genders gender);

    public void SetMercenary(IMercenary? mercenary) => Mercenary = mercenary;
    public void RemoveMercenary() => Mercenary = null;
    public void SetCheat() => ActiveCheat = true;
    public void RemoveCheat() => ActiveCheat = false;
}

public class Stepladder : SmallStuff
{
    public Stepladder()
    {
        Price = 400;
        Damage = 3;
        Weight = Bulkiness.HUGE;
        Fullness = Arms.NO;
    }

    public override bool CheckRace(IRaceAndClass race) => race is Halfling;

    public override bool CheckClass(IRaceAndClass _class) => true;

    public override bool CheckGender(Genders gender) => true;
}

public class GreatTitle : SmallStuff
{
    public GreatTitle()
    {
        Price = 0;
        Damage = 3;
        Weight = Bulkiness.SMALL;
        Fullness = Arms.NO;
    }

    public override bool CheckRace(IRaceAndClass race) => true;

    public override bool CheckClass(IRaceAndClass _class) => true;

    public override bool CheckGender(Genders gender) => true;
}

public class SpikedKnees : SmallStuff
{
    public SpikedKnees()
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

public class SingingSword : SmallStuff
{
    public SingingSword()
    {
        Price = 400;
        Damage = 2;
        Weight = Bulkiness.SMALL;
        Fullness = Arms.NO;
    }

    public override bool CheckRace(IRaceAndClass race) => true;

    public override bool CheckClass(IRaceAndClass _class) => _class is not Thief;

    public override bool CheckGender(Genders gender) => true;
}

public class Sandwich : SmallStuff
{
    public Sandwich()
    {
        Price = 400;
        Damage = 3;
        Weight = Bulkiness.SMALL;
        Fullness = Arms.NO;
    }

    public override bool CheckRace(IRaceAndClass race) => race is Halfling;

    public override bool CheckClass(IRaceAndClass _class) => true;

    public override bool CheckGender(Genders gender) => true;
}

public class Cloack : SmallStuff
{
    public Cloack()
    {
        Price = 600;
        Damage = 4;
        Weight = Bulkiness.SMALL;
        Fullness = Arms.NO;
    }

    public override bool CheckRace(IRaceAndClass race) => true;

    public override bool CheckClass(IRaceAndClass _class) => _class is Thief;

    public override bool CheckGender(Genders gender) => true;
}

public class Pantyhose : SmallStuff
{
    public Pantyhose()
    {
        Price = 600;
        Damage = 3;
        Weight = Bulkiness.SMALL;
        Fullness = Arms.NO;
    }

    public override bool CheckRace(IRaceAndClass race) => true;

    public override bool CheckClass(IRaceAndClass _class) => _class is not Warrior;

    public override bool CheckGender(Genders gender) => true;
}

//TODO: придумать, как написать наколенники развода