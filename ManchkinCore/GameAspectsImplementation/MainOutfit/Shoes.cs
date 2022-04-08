using ManchkinCore.Enums.Accessory;
using ManchkinCore.Interfaces;

namespace ManchkinCore.Implementation;

public abstract class Shoes : IStuff
{
    public int Price { get; protected init; }
    public int Damage { get; protected init; }
    public bool ActiveCheat { get; protected set; }
    public Bulkiness Weight { get; protected init; }
    public Arms Fullness { get; protected init; }

    public abstract bool CanBeUsed(IRace _class);
    public abstract bool CanBeUsed(IClass race);
    public abstract bool CanBeUsed(Genders gender);
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
    }
}

//TODO: Дописать сандалеты-протекторы
//TODO: Дописать башмаки реального бега