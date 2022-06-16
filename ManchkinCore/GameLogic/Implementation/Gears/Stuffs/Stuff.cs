using ManchkinCore.CardEnums.Accessory;
using ManchkinCore.CardEnums.Aspects;
using ManchkinCore.GameLogic.Interfaces.Accessory;
using ManchkinCore.GameLogic.Interfaces.Stuff;

namespace ManchkinCore.GameLogic.Implementation.Gears.Stuffs;

public abstract class Stuff : IStuff
{
    public int Price { get; protected set; }
    public int Damage { get; protected set; }
    public Bulkiness Weight { get; protected set; }
    public Arms Fullness { get; protected set; }
    public int FlushingBonus { get; protected set; }
    public bool Cheat { get; set; } = false;

    public List<string> Descriptions { get; protected set; }

    public string TextRepresentation { get; protected set; }

    public abstract bool CanBeUsed(IRace? race);
    public abstract bool CanBeUsed(IClass? _class);
    public abstract bool CanBeUsed(Genders gender);
}