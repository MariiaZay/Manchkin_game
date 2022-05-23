using ManchkinCore.Enums;
using ManchkinCore.Enums.Accessory;
using ManchkinCore.Implementation;

namespace ManchkinCore.Interfaces;

public interface IStuff : IDescriptable
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
