using Infrastructure;
using Progress;

public class CoreGameplayService : IEnemyReachedReciever
{
    public int CastleHitpoints;

    private readonly CoreGameplayStats _coreGameplayStats;
    private readonly GameStateMachine _gameStateMachine;
    private readonly ResourcesSaveLoader _resourcesSaveLoader;
    private readonly ResourceService _resourceService;
    private readonly CoreGameplaySceneUiService _coreGameplaySceneUiService;

    public CoreGameplayService(CoreGameplayStats coreGameplayStats,
                               GameStateMachine gameStateMachine,
                               ResourcesSaveLoader resourcesSaveLoader,
                               ResourceService resourceService,
                               CoreGameplaySceneUiService coreGameplaySceneUiService)
    {

        _coreGameplayStats = coreGameplayStats;
        _gameStateMachine = gameStateMachine;
        _resourcesSaveLoader = resourcesSaveLoader;
        _resourceService = resourceService;
        _coreGameplaySceneUiService = coreGameplaySceneUiService;
        Init();
        
    }
    private void Init()
    {
        CastleHitpoints =  _coreGameplayStats.CastleHitpoints;
        _coreGameplaySceneUiService.SetCastleHP(CastleHitpoints);
    }

    void IEnemyReachedReciever.RecieveEnemyReached(int enemyDamage) => RecieveDamage(enemyDamage);

    private void RecieveDamage(int damage)
    {
        CastleHitpoints -= damage;
        _coreGameplaySceneUiService.SetCastleHP(CastleHitpoints);
        if(CastleHitpoints <= 0 )
        {
           _resourceService.Save();
            _gameStateMachine.EnterState<LoadInitialLevelState, string>("MetaGameplayScene");
        }
    }
}
