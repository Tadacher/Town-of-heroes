using System;
using System.Collections.Generic;
using UnityEngine;
using WorldCells;

[CreateAssetMenu(fileName = "EnemyType To Biome Settings", menuName = "ScriptableObjects/EnemyTypeToBiome", order = 1)]
public class EnemyTypeToBiomeSettings : ScriptableObject
{
    public BiomeToEnemyTypesPair[] BiomeToEnemyTypesSettings;
    private Dictionary<CellBiomeTypes, EnemytypeToWeightPair[]> _biomeToEnemyPairs;

    /// <summary>
    /// returns every possible enemy type in biome and its weight
    /// </summary>
    /// <param name="biomeType"></param>
    /// <returns></returns>
    public EnemytypeToWeightPair[] GetPairByBiomeType(CellBiomeTypes biomeType)
    {
        if (_biomeToEnemyPairs == null)
            InitBiomeToEnemyPairs();

        return _biomeToEnemyPairs[biomeType];
    }

    private void InitBiomeToEnemyPairs()
    {
        _biomeToEnemyPairs = new Dictionary<CellBiomeTypes, EnemytypeToWeightPair[]>();
        foreach (var pair in BiomeToEnemyTypesSettings)
        {
            _biomeToEnemyPairs.Add(pair.CellBiomeType, pair.ProbabilityWeightPairs);
        }
    }
}
/// <summary>
/// describes which biome can be cause which enemyTypes to spawn
/// </summary>
[Serializable]
public class BiomeToEnemyTypesPair
{
    public CellBiomeTypes CellBiomeType;
    public EnemytypeToWeightPair[] ProbabilityWeightPairs;
}
/// <summary>
/// describes weight of EnemyType in certain biome 
/// </summary>
[Serializable]
public class EnemytypeToWeightPair
{
    public EnemyType EnemyType;
    public float Weight;
}