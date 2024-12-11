using System;
using UnityEngine;
using WorldCells;
using System.Collections.Generic;
using Services.CardGeneration;
using Progress;
using Enemies;
/// <summary>
/// Generates waves with all linked stuff
/// First - enemy type is picked
/// Second - it generates list of enemies of type
/// </summary>
public partial class WaveGenerator
{
    private EnemyInstantiationService _enemyInstantiationService;
    private WorldCellBalanceService _cellBalanceService;
    private EnemyTypeService _enemyTypeService;
    private readonly WaveDeathListenerFactory _waveDeathListenerFactory;
    private readonly CardGenerationService _cardGenerationService;
    private readonly EnemyGenerationCostService _enemyGenerationCostService;
    private readonly ResourceService _resourceService;
    private float _currentWaveCost = 40;
    private float _waveMaxCost = 40;

    public WaveGenerator(EnemyInstantiationService nemyInstantiationService,
                         WorldCellBalanceService worldCellBalanceService,
                         EnemyTypeService enemyTypeService,
                         WaveDeathListenerFactory waveDeathListenerFactory,
                         CardGenerationService cardGenerationService,
                         EnemyGenerationCostService enemyGenerationCostService,
                         ResourceService resourceService)
    {
        _enemyInstantiationService = nemyInstantiationService;
        _cellBalanceService = worldCellBalanceService;
        _enemyTypeService = enemyTypeService;
        _waveDeathListenerFactory = waveDeathListenerFactory;
        _cardGenerationService = cardGenerationService;
        _enemyGenerationCostService = enemyGenerationCostService;
        _resourceService = resourceService;
    }
    /// <summary>
    /// Generate new wave
    /// </summary>
    /// <returns></returns>
    public Wave GenerateWave() => new(1f, GenerateSpawnCommands());
    /// <summary>
    /// Generate array of spawn commands for a wave
    /// </summary>
    /// <returns>array of spawn commands for a wave</returns>
    private Action[] GenerateSpawnCommands()
    {
        CellBiomeTypes enemyBiomeType = GetEnemyBiomeByCountWeighted();
        List<Action> abstractEnemies = new ();
        WaveDeathListener waveDeathListener = _waveDeathListenerFactory.GetObject();
        while (_currentWaveCost > 0 )
        {
            Type enemyConcreteType = _enemyTypeService.GetRandomConcreteEnemyTypeByEnemyRaceType(GetEnemyRaceTypeByBiome(enemyBiomeType));
            _currentWaveCost -= _enemyGenerationCostService.GetCostByType(enemyConcreteType);
            abstractEnemies.Add(() => SpawnEnemy(enemyConcreteType, waveDeathListener));
        }
        waveDeathListener.Reinitialize(abstractEnemies.Count);
        waveDeathListener.OnWaveDead += _cardGenerationService.DraftCard;
        waveDeathListener.OnWaveDead += _resourceService.CountWave;
        IncreaseAndRestoreWaveCost();
        return abstractEnemies.ToArray();
    }

    private void IncreaseAndRestoreWaveCost()
    {
        _waveMaxCost *= 1.1f;
        _currentWaveCost = +_waveMaxCost;
    }

    /// <summary>
    /// gets rand, enemy from biome type from enemyTypeService
    /// </summary>
    /// <param name="enemyBiomeType">BiomeType</param>
    /// <returns>enemy</returns>
    private EnemyType GetEnemyRaceTypeByBiome(CellBiomeTypes enemyBiomeType) => 
        _enemyTypeService.GetRandomEnemyTypeByBiome(enemyBiomeType);

    /// <summary>
    /// spawns enemy at spawn point
    /// </summary>
    /// <param name="enemyRaceType">Type of an enemy</param>
    /// <param name="waveDeathListener">look at class description</param>
    /// <returns></returns>
    private AbstractEnemy SpawnEnemy(Type concreteEnemyType, WaveDeathListener waveDeathListener) =>
        _enemyInstantiationService.ReturnObject(concreteEnemyType).WithWaveDeathListener(waveDeathListener);

    /// <summary>
    /// selects random biome from all biomes presented on world map by weighted random method
    /// </summary>
    /// <returns> Cell biome type </returns>
    private CellBiomeTypes GetEnemyBiomeByCountWeighted()
    {
        Dictionary<CellBiomeTypes, int> cells = _cellBalanceService.CellCountByTag;
        float maxValue = 0;

        foreach (KeyValuePair<CellBiomeTypes, int> cell in cells)
            maxValue += cell.Value;

        if (maxValue == 0)
        {
            Debug.LogWarning($"No cell placed yet, sending green");
            return CellBiomeTypes.Forest;
        }

        float randomNumber = UnityEngine.Random.Range(0, maxValue);
        foreach (KeyValuePair<CellBiomeTypes, int> cell in cells)
        {
            randomNumber -= cell.Value;
            if (randomNumber <= 0)
            {
                Debug.Log($"picked {cell.Key} biome");
                return cell.Key;
            }
        }

        Debug.LogError($"No acceptable entries found for enemy biome type generation ");

        return CellBiomeTypes.Forest;
    }
}
