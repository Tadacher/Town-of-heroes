using System;
using System.Collections.Generic;
using System.Diagnostics;

public class EnemyTypeService
{
    private readonly EnemyTypeToBiomeSettings _enemyTypeToBiomeSettings;
    private readonly EnemyPrefabContainer _enemyPrefabContainer;
    private Dictionary<EnemyTypes, List<AbstractEnemy>> EnemiesByType;
    public EnemyTypeService(EnemyTypeToBiomeSettings enemyTypeToBiomeSettings, EnemyPrefabContainer enemyPrefabContainer)
    {
        _enemyTypeToBiomeSettings = enemyTypeToBiomeSettings;
        _enemyPrefabContainer = enemyPrefabContainer;
        InitializeEnemieesByType();
    }

    private void InitializeEnemieesByType()
    {
        EnemiesByType = new Dictionary<EnemyTypes, List<AbstractEnemy>>();
        foreach (AbstractEnemy enemy in _enemyPrefabContainer.Enemies)
        {
            foreach (EnemyTypes tag in enemy.Stats.EnemyTypes)
            {
                if (EnemiesByType.ContainsKey(tag))
                    EnemiesByType[tag].Add(enemy);
                else
                {
                    EnemiesByType.Add(tag, new List<AbstractEnemy>());
                    EnemiesByType[tag].Add(enemy);
                }
            }
        }
    }

    public EnemyTypes GetRandomEnemyTypeFromBiome(WorldCells.CellBiomeTypes biomeType)
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
        return EnemyTypes.Goblin;
    }

    public AbstractEnemy GetRandomEnemyByType(EnemyTypes enemyBiomeType)
    {
        List<AbstractEnemy> EnemiesByBiome;

        if (EnemiesByType.ContainsKey(enemyBiomeType))
            EnemiesByBiome = EnemiesByType[enemyBiomeType];
        else
            EnemiesByBiome = EnemiesByType[EnemyTypes.Goblin];
        
        return EnemiesByBiome[UnityEngine.Random.Range(0, EnemiesByBiome.Count)];
    }
}
public enum EnemyTypes
{
    Goblin,
    Orc,
    Greenskin,
    Undead,
}