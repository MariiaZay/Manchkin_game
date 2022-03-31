using ManchkinCore.Enums.Accessory;
using ManchkinCore.Interfaces;

namespace ManchkinCore.Implementation;

public abstract class Hat : IStuff
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

public class HornedHelmet : Hat
{
    public HornedHelmet()
    {
        Price = 600;
        Damage = 1;
        Weight = Bulkiness.SMALL;
        Fullness = Arms.NO;
    }

    public override bool CheckRace(IRaceAndClass race) => race is Elf;

    public override bool CheckClass(IRaceAndClass _class) => true;

    public override bool CheckGender(Genders gender) => true;
}

public class BandanaOfBastartism : Hat
{
    public BandanaOfBastartism()
    {
        Price = 400;
        Damage = 3;
        Weight = Bulkiness.SMALL;
        Fullness = Arms.NO;
    }

    public override bool CheckRace(IRaceAndClass race) => race is Human;

    public override bool CheckClass(IRaceAndClass _class) => true;

    public override bool CheckGender(Genders gender) => true;
}

public class HatOfPower : Hat
{
    public HatOfPower()
    {
        Price = 400;
        Damage = 3;
        Weight = Bulkiness.SMALL;
        Fullness = Arms.NO;
    }

    public override bool CheckRace(IRaceAndClass race) => true;

    public override bool CheckClass(IRaceAndClass _class) => _class is Wizard;

    public override bool CheckGender(Genders gender) => true;
}

public class HelmetOfCourage : Hat
{
    public HelmetOfCourage()
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




