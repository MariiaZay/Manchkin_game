﻿using ManchkinCore.CardEnums;
using ManchkinCore.GameLogic.Implementation.Manchkin;
using ManchkinCore.GameLogic.Interfaces.Accessory;
using ManchkinCore.GameLogic.Interfaces.Manchkin;

namespace ManchkinCore.GameLogic.Implementation.Factories;

public class SuperManchkinFactory
{
    private readonly IClass _secondClass;
    
    public SuperManchkinFactory() {}

    private SuperManchkinFactory(IClass secondClass)
    {
        _secondClass = secondClass;
    }

    public static SuperManchkinFactory SetSecondClass(IClass secondClass) => new SuperManchkinFactory(secondClass);

    public static SuperManchkinFactory ResetSecondClass() => new SuperManchkinFactory();

    public ISuperManchkin Build() => _secondClass == null 
        ? new SuperManchkin(HalfTypes.SINGLE_CLEAN)
        : new SuperManchkin(HalfTypes.BOTH, _secondClass);
}