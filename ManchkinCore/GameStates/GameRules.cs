namespace ManchkinCore.GameStates;

public static class GameRules
{
    private static readonly HashSet<GameActions> BaseActions = 
        new() {GameActions.DropRace, GameActions.DropClass};
    
    private static readonly HashSet<GameActions> NonBattleActions = 
        new() {GameActions.ChangeClass, GameActions.ChangeRace,GameActions.Change, GameActions.Sell,};

    public static IReadOnlySet<GameActions> GetTurnActions(bool isPlayerTurn, bool isInBattle)
    {
        if (!isPlayerTurn)
            return new HashSet<GameActions>()
            {

            };
        if (isInBattle)
            return BaseActions;
        return BaseActions.Union(NonBattleActions).ToHashSet();

    }
}