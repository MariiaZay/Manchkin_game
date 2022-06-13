using ManchkinCore.Enums;
using ManchkinCore.Interfaces;

namespace ManchkinCore.GameLogic.Implementation;

public class Halfblood: IHulfblood
{
    public HalfTypes HalfType { get; }
    public IRace? SecondRace { get; }

    public Halfblood(IRace? race)
    {
        HalfType = HalfTypes.BOTH;
        SecondRace = race;
    }

    public Halfblood()
    {
        HalfType = HalfTypes.SINGLE_CLEAN;
        SecondRace = null;
    }
}