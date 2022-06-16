using ManchkinCore.GameLogic.Implementation.Gears;
using ManchkinCore.GameLogic.Interfaces.Stuff;

namespace ManchkinCore.GameLogic.Implementation.Factories;

public class MercenaryFactory
{
    private readonly IStuff _stuff;
    
    public MercenaryFactory() {}

    private MercenaryFactory(IStuff stuff)
    {
        _stuff = stuff;
    }

    public static MercenaryFactory SetStuff(IStuff stuff) => new MercenaryFactory(stuff);

    public static MercenaryFactory ResetStuff() => new MercenaryFactory();

    public IMercenary Build() => _stuff == null ? new Mercenary() : new Mercenary(_stuff);
}