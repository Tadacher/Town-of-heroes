using System;
using UnityEngine;
using WorldCells;
using System.Collections.Generic;

public class WaveGenerator
{
    private EnemyInstantiationService _enemyInstantiationService;
    private WorldCellBalanceService _cellBalanceService;
    private EnemyTypeService _enemyTypeService;

    private Dictionary<Type, AbstractEnemy> _enemies;
    public WaveGenerator(EnemyInstantiationService nemyInstantiationService,
                         WorldCellBalanceService worldCellBalanceService,
                         EnemyPrefabContainer enemyPrefabContainer,
                         EnemyTypeService enemyTypeService)
    {
        _enemyInstantiationService = nemyInstantiationService;
        _cellBalanceService = worldCellBalanceService;
        _enemyTypeService = enemyTypeService;
    }
    public Wave GenerateWave() => 
        new(1f, GenerateSpawnCommands());

    private Action[] GenerateSpawnCommands()
    {
        CellBiomeTypes enemyBiomeType = GetEnemyBiomeType();
        Action[] abstractEnemies = new Action[] {
        () => SpawnEnemy(GetEnemyTypeByBiome(enemyBiomeType)),
        () => _enemyInstantiationService.ReturnObject(typeof(Gobbo)),
        () => _enemyInstantiationService.ReturnObject(typeof(Gobbo))};
        return abstractEnemies;
    }

    private EnemyTypes GetEnemyTypeByBiome(CellBiomeTypes enemyBiomeType) => 
        _enemyTypeService.GetRandomEnemyTypeFromBiome(enemyBiomeType);

    private AbstractEnemy SpawnEnemy(EnemyTypes enemyType)
    {
        AbstractEnemy type = _enemyTypeService.GetRandomEnemyByType(enemyType);
        return _enemyInstantiationService.ReturnObject(type.GetType());
    }

    private CellBiomeTypes GetEnemyBiomeType()
    {
        Dictionary<CellBiomeTypes, int> cells = _cellBalanceService.CellCountByTag;
        float maxValue = 0;

        foreach (KeyValuePair<CellBiomeTypes, int> cell in cells)
            maxValue += cell.Value;

        if (maxValue == 0)
        {
            Debug.LogWarning($"No cell placed yet, sending green");
            return CellBiomeTypes.Green;
        }

        float randomNumber = UnityEngine.Random.Range(0, maxValue);
        foreach (var  cell in cells)
        {
            randomNumber -= cell.Value;
            if (randomNumber <= 0)
                return cell.Key;
        }

        Debug.LogError($"No acceptable entries found for enemy biome type generation ");

        return CellBiomeTypes.Green;
    }
}
