using Infrastructure;

public class CoreGameplayService : IEnemyReachedReciever
{
    private CoreGameplayStats _coreGameplayStats;
    private readonly GameStateMachine _gameStateMachine;
    public int CastleHitpoints;

    public CoreGameplayService(CoreGameplayStats coreGameplayStats, GameStateMachine gameStateMachine)
    {
        _coreGameplayStats = coreGameplayStats;
        _gameStateMachine = gameStateMachine;
        Init();
    }

    private void Init()
    {
        CastleHitpoints =  _coreGameplayStats.CastleHitpoints;
    }

    void IEnemyReachedReciever.RecieveEnemyReached(int enemyDamage) => RecieveDamage(enemyDamage);
    private void RecieveDamage(int damage)
    {
        CastleHitpoints -= damage;
        if(CastleHitpoints <= 0 )
        {
            _gameStateMachine.EnterState<LoadInitialLevelState, string>("MetaGameplayScene");
        }
    }
}
