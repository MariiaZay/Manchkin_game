using ManchkinCore.CardEnums.Accessory;
using ManchkinCore.CardEnums.Aspects;
using ManchkinCore.GameLogic.Interfaces.Accessory;
using ManchkinCore.GameLogic.Interfaces.Stuff;

namespace ManchkinCore.GameLogic.Implementation.MainOutfit.Armor;

public abstract class Armor : IStuff
{
    public int Price { get; protected init; }
    public int Damage { get; protected init; }
    public Bulkiness Weight { get; protected init; }
    public Arms Fullness { get; protected init; }
    public int FlushingBonus { get; protected set; }
    public bool Cheat { get; set; } = false;
    public List<string> Descriptions { get; protected set; }
    public string TextRepresentation { get; protected set; }

    public abstract bool CanBeUsed(IRace? race);
    public abstract bool CanBeUsed(IClass? _class);
    public abstract bool CanBeUsed(Genders gender);
}