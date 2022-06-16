using ManchkinCore.CardEnums;
using ManchkinCore.GameLogic.Interfaces.Accessory;
using ManchkinCore.GameLogic.Interfaces.Manchkin;

namespace ManchkinCore.GameLogic.Implementation.Manchkin;

public class SuperManchkin : ISuperManchkin
{
    public HalfTypes HalfType { get; }
    public IClass? SecondClass { get; }

    public SuperManchkin(HalfTypes halfType, IClass _class)
    {
        HalfType = halfType;
        SecondClass = _class;
    }
    
    public SuperManchkin(HalfTypes halfType)
    {
        HalfType = halfType;
        SecondClass = null;
    }
}