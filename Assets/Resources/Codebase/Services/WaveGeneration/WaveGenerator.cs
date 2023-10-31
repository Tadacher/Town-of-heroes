using System;

public class WaveGenerator
{
    private EnemyInstantiationService _enemyInstantiationService;

    public WaveGenerator(EnemyInstantiationService nemyInstantiationService)
    {
        _enemyInstantiationService = nemyInstantiationService;
    }

    private Action[] GenerateSpawnCommands()
    {
        Action[] abstractEnemies = new Action[] {
        () => _enemyInstantiationService.ReturnObject(typeof(Gobbo)),
        () => _enemyInstantiationService.ReturnObject(typeof(Gobbo)),
        () => _enemyInstantiationService.ReturnObject(typeof(Gobbo))};
        return abstractEnemies;
    }
    public Wave GenerateWave() => 
        new(1f, GenerateSpawnCommands());
}
