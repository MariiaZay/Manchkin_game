using ManchkinCore.CardEnums;
using ManchkinCore.GameLogic.Interfaces.Accessory;

namespace ManchkinCore.GameLogic.Interfaces.Manchkin;

public interface ISuperManchkin
{
    public HalfTypes HalfType { get; }
    public IClass? SecondClass { get; }
    
}