using ManchkinCore.Enums;
using ManchkinCore.Interfaces;

namespace ManchkinCore.GameLogic.Implementation;

public class Halfblood: IHulfblood
{
    public HalfTypes HalfType { get; }
    public IRace? SecondRace { get; }

    public Halfblood(HalfTypes halfType, IRace? race)
    {
        HalfType = halfType;
        SecondRace = race;
    }

    public Halfblood(HalfTypes halfType)
    {
        HalfType = halfType;
        SecondRace = null;
    }
}