using ManchkinCore.GameLogic.Implementation.Manchkin;
using ManchkinCore.GameLogic.Interfaces.Accessory;

namespace ManchkinCore.GameLogic.Implementation.Factories;

public class HalfbloodFactory
{
    private readonly IRace _race;
    
    public HalfbloodFactory() {}

    private HalfbloodFactory(IRace race)
    {
        _race = race;
    }

    public static HalfbloodFactory SetSecondRace(IRace race) => new HalfbloodFactory(race);

    public static HalfbloodFactory ResetSecondRace() => new HalfbloodFactory();

    public Halfblood Build() => _race == null ? new Halfblood() : new Halfblood(_race);
}