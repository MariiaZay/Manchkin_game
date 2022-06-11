using ManchkinCore.Enums.Accessory;
using ManchkinCore.Interfaces;

namespace ManchkinCore.Implementation;

public abstract class Weapon : IStuff
{
    public int Price { get; protected init; }
    public int Damage { get; protected init; }
    public Bulkiness Weight { get; protected init; }
    public Arms Fullness { get; protected init; }
    public int FlushingBonus { get; protected set; }
    public bool Cheat { get; set; } = false;

    public abstract bool CanBeUsed(IRace? race);
    public abstract bool CanBeUsed(IClass? _class);
    public abstract bool CanBeUsed(Genders gender);
    public List<string> Descriptions { get; protected set; }
    public string TextRepresentation { get; protected set; }
}

public abstract class BothHandWeapon: Weapon
{}
public abstract class SingleHandWeapon : Weapon
{}