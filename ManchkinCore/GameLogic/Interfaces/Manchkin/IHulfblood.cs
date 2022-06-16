using ManchkinCore.CardEnums;
using ManchkinCore.GameLogic.Interfaces.Accessory;

namespace ManchkinCore.GameLogic.Interfaces.Manchkin;

public interface IHulfblood
{
    public HalfTypes HalfType { get; }
    public IRace? SecondRace { get; }
}