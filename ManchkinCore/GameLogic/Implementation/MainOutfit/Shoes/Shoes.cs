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

    public abstract bool CanBeUsed(IRace? _class);
    public abstract bool CanBeUsed(IClass? race);
    public abstract bool CanBeUsed(Genders gender);
    public List<string> Descriptions { get; protected set; }
    public string TextRepresentation { get; protected set; }
}