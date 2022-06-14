using ManchkinCore.Interfaces;

namespace ManchkinCore.Implementation;

public class MercenaryFactory
{
    private IStuff _stuff;
    
    public MercenaryFactory() {}

    private MercenaryFactory(IStuff stuff)
    {
        _stuff = stuff;
    }

    public MercenaryFactory SetStuff(IStuff stuff) => new MercenaryFactory(stuff);

    public MercenaryFactory ResetStuff() => new MercenaryFactory();

    public IMercenary Build() => _stuff == null ? new Mercenary() : new Mercenary(_stuff);
}