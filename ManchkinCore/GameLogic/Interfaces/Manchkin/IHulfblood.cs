using ManchkinCore.Enums;

namespace ManchkinCore.Interfaces;

public interface IHulfblood
{
    public HalfTypes HalfType { get; }
    public IRace SecondRace { get; }
}