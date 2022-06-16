using ManchkinCore.CardEnums.Accessory;
using ManchkinCore.CardEnums.Aspects;
using ManchkinCore.GameLogic.Interfaces.Accessory;

namespace ManchkinCore.GameLogic.Interfaces.Stuff;

public interface IStuff : ICardsBaseableDescribable
{
    public int Price { get;}
    public int Damage { get;}
    
    public Bulkiness Weight { get;}
    public Arms Fullness { get;}
    public int FlushingBonus { get; }
    public bool Cheat { get; set; }
    
    public bool CanBeUsed(IRace? race);
    public bool CanBeUsed(IClass? _class);
    public bool CanBeUsed(Genders gender);
}
