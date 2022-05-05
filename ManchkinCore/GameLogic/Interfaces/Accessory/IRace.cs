namespace ManchkinCore.Interfaces;

public interface IRace : IDescriptable
{
    public int FlushingBonus { get; }
    public int CardCount { get; }
    public bool CellingByDoublePrice { get; }
}