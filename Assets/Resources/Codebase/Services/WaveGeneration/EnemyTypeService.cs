using System;
using System.Collections.Generic;
using System.Diagnostics;

public class EnemyTypeService
{
    private readonly EnemyTypeToBiomeSettings _enemyTypeToBiomeSettings;
    private readonly EnemyPrefabContainer _enemyPrefabContainer;
    /// <summary>
    /// Dictionary contains EnemyType as key and List<(AbstractEnemy, float) as value
    /// first determines some enemy type like gobbo or greeskin, second contains list of (concrete enemy of that type, weight in gen); 
    /// </summary>
    private Dictionary<EnemyType, List<(AbstractEnemy, float)>> EnemiesByType;
    public EnemyTypeService(EnemyTypeToBiomeSettings enemyTypeToBiomeSettings, EnemyPrefabContainer enemyPrefabContainer)
    {
        _enemyTypeToBiomeSettings = enemyTypeToBiomeSettings;
        _enemyPrefabContainer = enemyPrefabContainer;
        InitializeEnemieesByType();
    }

    private void InitializeEnemieesByType()
    {
        EnemiesByType = new Dictionary<EnemyType, List<(AbstractEnemy, float)>>();
        foreach (AbstractEnemy enemy in _enemyPrefabContainer.Enemies)
        {
            foreach (EnemyStats.WeightToEnemyType tag in enemy.Stats.EnemyTypesAndWeights)
            {
                if (EnemiesByType.ContainsKey(tag.enemyType))
                    EnemiesByType[tag.enemyType].Add((enemy, tag.Weight));
                else
                {
                    EnemiesByType.Add(tag.enemyType, new List<(AbstractEnemy, float)>());
                    EnemiesByType[tag.enemyType].Add((enemy, tag.Weight));
                }
            }
        }
    }

    public EnemyType GetRandomEnemyTypeByBiome(WorldCells.CellBiomeTypes biomeType)
    {
        float maxvalue = 0;
        EnemytypeToWeightPair[] biomeToEnemyTypesPairs = _enemyTypeToBiomeSettings.GetPairByBiomeType(biomeType);
        
        foreach (var pair in biomeToEnemyTypesPairs)
            maxvalue += pair.Weight;

        float pointer = UnityEngine.Random.Range(0, maxvalue);

        foreach (var pair in biomeToEnemyTypesPairs)
        {
            pointer -= pair.Weight;
            if (pointer <= 0)
                return pair.EnemyType;
        }

        UnityEngine.Debug.LogError($"no acceptable intries found while generating enemy type from biome {biomeType}");
        return EnemyType.Goblin;
    }

    /// <summary>
    /// Gets random (weighted) enemy type from a list of enemies of given EnemyType
    /// </summary>
    /// <param name="enemyRaceType">given EnemyType</param>
    /// <returns>Link to prefab</returns>
    public Type GetRandomConcreteEnemyTypeByEnemyRaceType(EnemyType enemyRaceType)
    {
        if(!EnemiesByType.ContainsKey(enemyRaceType))
        {
            UnityEngine.Debug.LogError($"Found no enemies typed as {enemyRaceType}");
            return (EnemiesByType[EnemyType.Goblin])[0].Item1.GetType();
        }
        if (EnemiesByType[enemyRaceType].Count == 0)
            return (EnemiesByType[enemyRaceType])[0].Item1.GetType();

        float maxValue = 0;
        foreach ((AbstractEnemy, float) pair in EnemiesByType[enemyRaceType])
            maxValue += pair.Item2;

        float pointer = UnityEngine.Random.Range(0, maxValue);        

        foreach ((AbstractEnemy, float) pair in EnemiesByType[enemyRaceType])
        {
            pointer -= pair.Item2;
            if (pointer <= 0)
                return pair.Item1.GetType();
        }

        UnityEngine.Debug.LogError($"no acceptable intries found while generating enemy type from type {enemyRaceType}");
        return _enemyPrefabContainer.Enemies[0].GetType();
    }
}
public enum EnemyType
{
    //Common
    Greenskin,
    Undead,

    //Concrete
    Goblin,
    Orc,
    Satyr,
}