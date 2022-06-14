using ManchkinCore.GameLogic.Interfaces;

namespace ManchkinCore.Interfaces;

public interface IRace : ICardsBaseableDescribable
{
    public int FlushingBonus { get; }
    public int CardCount { get; }
    public bool CellingByDoublePrice { get; }
}