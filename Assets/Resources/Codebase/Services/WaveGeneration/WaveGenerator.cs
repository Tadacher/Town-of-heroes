using System;
using UnityEngine;
using WorldCells;
using System.Collections.Generic;
using Services.CardGeneration;

public class WaveGenerator
{
    private EnemyInstantiationService _enemyInstantiationService;
    private WorldCellBalanceService _cellBalanceService;
    private EnemyTypeService _enemyTypeService;
    private readonly WaveDeathListenerFactory _waveDeathListenerFactory;
    private readonly CardGenerationService _cardGenerationService;

    public WaveGenerator(EnemyInstantiationService nemyInstantiationService,
                         WorldCellBalanceService worldCellBalanceService,
                         EnemyTypeService enemyTypeService,
                         WaveDeathListenerFactory waveDeathListenerFactory,
                         CardGenerationService cardGenerationService)
    {
        _enemyInstantiationService = nemyInstantiationService;
        _cellBalanceService = worldCellBalanceService;
        _enemyTypeService = enemyTypeService;
        _waveDeathListenerFactory = waveDeathListenerFactory;
        _cardGenerationService = cardGenerationService;
    }
    /// <summary>
    /// Generate new wave
    /// </summary>
    /// <returns></returns>
    public Wave GenerateWave() => 
        new(1f, GenerateSpawnCommands(4));
    /// <summary>
    /// Generate array of spawn commands for a wave
    /// </summary>
    /// <returns>array of spawn commands for a wave</returns>
    private Action[] GenerateSpawnCommands(int waveSize)
    {
        CellBiomeTypes enemyBiomeType = GetEnemyBiomeByCountWeighted();
        Action[] abstractEnemies = new Action[waveSize];
        WaveDeathListener waveDeathListener = _waveDeathListenerFactory.GetObject();
        waveDeathListener.Reinitialize(waveSize);
        waveDeathListener.OnWaveDead += _cardGenerationService.DraftCard;
        for (int i = 0; i < waveSize; i++)
        {
            abstractEnemies[i] = () => SpawnEnemy(GetEnemyTypeByBiome(enemyBiomeType), waveDeathListener);
        }
        return abstractEnemies;
    }

    private EnemyType GetEnemyTypeByBiome(CellBiomeTypes enemyBiomeType) => 
        _enemyTypeService.GetRandomEnemyTypeByBiome(enemyBiomeType);

    /// <summary>
    /// spawns enemy at spawn point
    /// </summary>
    /// <param name="enemyType">Type of an enemy</param>
    /// <param name="waveDeathListener">look at class description</param>
    /// <returns></returns>
    private AbstractEnemy SpawnEnemy(EnemyType enemyType, WaveDeathListener waveDeathListener)
    {
        AbstractEnemy type = _enemyTypeService.GetRandomEnemyByType(enemyType);
        return _enemyInstantiationService.ReturnObject(type.GetType()).WithWaveDeathListener(waveDeathListener);
    }

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
            return CellBiomeTypes.Green;
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

        return CellBiomeTypes.Green;
    }
}
