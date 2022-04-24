using ManchkinCore.Automate.Enums;

namespace ManchkinCore.Automate.Interfaces;

public interface IGameState
{
    // информация о текущем положении игры 

    public GameState GameState { get; }

    // кол-во игроков и их статистика
}