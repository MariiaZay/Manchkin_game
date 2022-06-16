using ManchkinCore.CardEnums;
using ManchkinCore.GameLogic.Interfaces.Accessory;
using ManchkinCore.GameLogic.Interfaces.Manchkin;

namespace ManchkinCore.GameLogic.Implementation.Manchkin;

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