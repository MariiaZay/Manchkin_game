namespace ManchkinCore.Automate.Interfaces;

public interface IAutomate
{
    IGameState Transit(IGameState state, IPlayerAction playerAction);
}