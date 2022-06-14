using ManchkinCore.Enums;
using ManchkinCore.Interfaces;

namespace ManchkinCore.GameLogic.Implementation.Factories;

public class SuperManchkinFactory
{
    private IClass _secondClass;
    
    public SuperManchkinFactory() {}

    private SuperManchkinFactory(IClass secondClass)
    {
        _secondClass = secondClass;
    }

    public SuperManchkinFactory SetSecondClass(IClass secondClass) => new SuperManchkinFactory(secondClass);

    public SuperManchkinFactory ResetSecondClass() => new SuperManchkinFactory();

    public ISuperManchkin Build() => _secondClass == null 
        ? new SuperManchkin(HalfTypes.SINGLE_CLEAN)
        : new SuperManchkin(HalfTypes.BOTH, _secondClass);
}