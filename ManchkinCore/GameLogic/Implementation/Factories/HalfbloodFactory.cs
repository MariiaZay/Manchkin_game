using ManchkinCore.Interfaces;

namespace ManchkinCore.GameLogic.Implementation.Factories;

public class HalfbloodFactory
{
    private IRace _race;
    
    public HalfbloodFactory() {}

    private HalfbloodFactory(IRace race)
    {
        _race = race;
    }

    public HalfbloodFactory SetSecondRace(IRace race) => new HalfbloodFactory(race);

    public HalfbloodFactory ResetSecondRace() => new HalfbloodFactory();

    public Halfblood Build() => _race == null ? new Halfblood() : new Halfblood(_race);
}