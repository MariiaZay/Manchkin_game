using System.Linq;
using ManchkinCore.Enums.Accessory;
using ManchkinCore.GameLogic;
using ManchkinCore.GameLogic.Implementation.Factories;
using ManchkinCore.GameLogic.Interfaces;
using ManchkinCore.Implementation;
using ManchkinCore.Interfaces;
using Ninject;
using Ninject.Extensions.Conventions;

namespace ManchkinGame;

public static class DITree
{
    private static StandardKernel kernel = new();
    
    static DITree()
    {
        kernel.Bind<IHands>().To<Hands>();

        var manchkinInitialClass = kernel.Get<Nobody>();
        var manchkinInitialRace = kernel.Get<Human>();

        kernel.Bind<ManchkinFactory>().ToMethod(
            c => new ManchkinFactory(manchkinInitialClass, manchkinInitialRace,
                c.Kernel.Get<Hands>(),
                c.Kernel.Get<MercenaryFactory>(),
                c.Kernel.Get<HalfbloodFactory>(),
                c.Kernel.Get<SuperManchkinFactory>()))
            .InSingletonScope();
        
        kernel.Bind(x => x.FromAssemblyContaining<ICardsBaseableDescribable>()
            .SelectAllClasses()
            .InheritedFrom<ICardsBaseableDescribable>()
            .BindAllInterfaces()
             .Configure(x => x.InSingletonScope()));

        var descriptables = kernel
            .GetAll<ICardsBaseableDescribable>()
            .Select(x => (IDescriptable) x)
            .ToList();
        kernel.Bind<CardsBase>()
            .ToMethod(c => new CardsBase(descriptables))
            .InSingletonScope();
    }

    public static Player MakePlayer(string name, Genders gender)
    {
        var manchkin = kernel
            .Get<ManchkinFactory>()
            .SetGender(gender)
            .Build();
        return new Player(name, manchkin);
    }

    public static CardsBase CardsBase => kernel.Get<CardsBase>();
}