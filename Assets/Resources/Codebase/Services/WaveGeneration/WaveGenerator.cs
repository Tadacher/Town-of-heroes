using System;

public class WaveGenerator
{
    private EnemyPrefabContainer _enemyPrefabContainer;

    public WaveGenerator(EnemyPrefabContainer enemyPrefabContainer) => _enemyPrefabContainer = enemyPrefabContainer;


    private Type[] GenerateEnemies()
    {
        Type[] abstractEnemies = new Type[3];
        abstractEnemies[0] = typeof(GobboTrapper);
        abstractEnemies[1] = typeof(Gobbo);
        abstractEnemies[2] = typeof(Gobbo);
        return abstractEnemies;
    }
    public Wave GenerateWave() => new(1f, GenerateEnemies());
}
