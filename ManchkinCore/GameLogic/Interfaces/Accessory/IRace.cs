namespace ManchkinCore.GameLogic.Interfaces.Accessory;

public interface IRace : ICardsBaseableDescribable
{
    public int FlushingBonus { get; }
    public int CardCount { get; }
    public bool CellingByDoublePrice { get; }
}