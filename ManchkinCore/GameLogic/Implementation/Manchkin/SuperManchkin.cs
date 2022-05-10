using ManchkinCore.Enums;
using ManchkinCore.Interfaces;

namespace ManchkinCore.GameLogic.Implementation;

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