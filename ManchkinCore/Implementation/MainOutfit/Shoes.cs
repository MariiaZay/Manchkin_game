using ManchkinCore.Enums.Accessory;
using ManchkinCore.Interfaces;

namespace ManchkinCore.Implementation;

public abstract class Shoes : IStuff
{
    public int Price { get; protected init; }
    public int Damage { get; protected init; }
    public bool ActiveCheat { get; private set; }
    public IMercenary? Mercenary { get; private set; }
    public Bulkiness Weight { get; protected init; }
    public Arms Fullness { get; protected init; }

    public abstract bool CheckRace(IRaceAndClass _class);
    public abstract bool CheckClass(IRaceAndClass race);
    public abstract bool CheckGender(Genders gender);
    public void SetMercenary(IMercenary? mercenary) => Mercenary = mercenary;
    public void RemoveMercenary() => Mercenary = null;
    public void SetCheat() => ActiveCheat = true;
    public void RemoveCheat() =>ActiveCheat = false;
}

public class MightyShoes : Shoes
{
    public override bool CheckRace(IRaceAndClass race) => true;

    public override bool CheckClass(IRaceAndClass _class) => true;

    public override bool CheckGender(Genders gender) => true;

    public MightyShoes()
    {
        Price = 400;
        Damage = 2;
        Weight = Bulkiness.SMALL;
        Fullness = Arms.NO;
    }
}

//TODO: Дописать сандалеты-протекторы
//TODO: Дописать башмаки реального бега

