using ManchkinCore.Implementation;
using ManchkinCore.Implementation.Gears;
using ManchkinCore.Interfaces;
using Ninject;
using Ninject.Extensions.Conventions;

namespace ManchkinCore.GameLogic;

public static class CardsBase
{
    public static List<IDescriptable> Races;
    public static List<IDescriptable> Classes;
    public static List<IDescriptable> SmallStuffs;
    public static List<IDescriptable> HugeStuffs;
    public static List<IDescriptable> Armors;
    public static List<IDescriptable> Hats;
    public static List<IDescriptable> Shoeses;
    public static List<IDescriptable> SingleHandWeapons;
    public static List<IDescriptable> BothHandWeapons;

    static CardsBase()
    {
        var kernel = new StandardKernel();

        kernel.Bind(x => x.FromThisAssembly()
            .SelectAllClasses()
            .InheritedFrom<IDescriptable>()
            .BindAllInterfaces()
            .Configure(x => x.InSingletonScope()));
        kernel.Bind(x => x.FromThisAssembly()
            .SelectAllClasses()
            .InheritedFrom<IDescriptable>()
            .BindAllBaseClasses()
            .Configure(x => x.InSingletonScope()));
        
        Races = kernel
            .GetAll<IRace>()
            .Select(x => (IDescriptable) x)
            .ToList();
        
        Classes = kernel
            .GetAll<IClass>()
            .Select(x => (IDescriptable) x)
            .ToList();
        
        HugeStuffs = kernel
            .GetAll<HugeStuff>()
            .Select(x => (IDescriptable) x)
            .ToList();
        
        SmallStuffs = kernel
            .GetAll<SmallStuff>()
            .Select(x => (IDescriptable) x)
            .ToList();
        
        Armors = kernel
            .GetAll<Armor>()
            .Select(x => (IDescriptable) x)
            .ToList();
        
        Hats = kernel
            .GetAll<Hat>()
            .Select(x => (IDescriptable) x)
            .ToList();
        
        Shoeses = kernel
            .GetAll<Shoes>()
            .Select(x => (IDescriptable) x)
            .ToList();
        
        SingleHandWeapons = kernel
            .GetAll<SingleHandWeapon>()
            .Select(x => (IDescriptable) x)
            .ToList();
        
        BothHandWeapons = kernel
            .GetAll<BothHandWeapon>()
            .Select(x => (IDescriptable) x)
            .ToList();
    }
}