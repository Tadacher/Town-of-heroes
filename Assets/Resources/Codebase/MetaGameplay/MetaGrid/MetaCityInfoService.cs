using Metagameplay.Buildings;
using Metagameplay.Ui;
using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// General city info
/// building count, allowed types and levels, placed building cache
/// </summary>
public class MetaCityInfoService
{
    public event Action<Type> OnAvailableBuilingTypeAdded;

    public Dictionary<Type, int> MetaCellCountByType => _metaCellsCountByType;
    private readonly Dictionary<Type, int> _metaCellsCountByType = new();

    public readonly HashSet<Type> AvailableBuildingTypes = new();
    public readonly Dictionary<Type, int> AvailableBuildingLevels = new();

    private readonly HashSet<AbstractMetaGridCell> _placedCellHash = new();

    private IBuildingContainerProvider _buildingContainerProvider;
    public MetaCityInfoService(IBuildingContainerProvider buildingContainerProvider)
    {
        _buildingContainerProvider = buildingContainerProvider;
        InitDefaultAvailableBuildings();
        InitCellCountDictionary();
    }
    public void ApplyEffects()
    {
        foreach (var cell in _placedCellHash) { cell.ApplyLevelEffects(); }
    }
    public void CountCell(AbstractMetaGridCell cell)
    {
        Type type = cell.GetType();

        if (_metaCellsCountByType.ContainsKey(type))
        {
            _metaCellsCountByType[type]++;
        }
        else
        {
            Debug.LogError("ADDED UNEXPECTED BUILDING TYPE. THIS TYPE WAS NOT PRESET AT INITIALIZATION!");
            _metaCellsCountByType.Add(type, 1);
        }
    }

    public void CachePlacedCell(AbstractMetaGridCell cell)
    {
        _placedCellHash.Add(cell);
    }

    public void ApplyWaveEffects(int waves)
    {
        foreach (var entry in _placedCellHash)
        {
            entry.ApplyPerWaveEffects(waves);
        }
    }

    public void InitContent()
    {
        foreach (var entry in _placedCellHash)
        {
            if (AvailableBuildingLevels.ContainsKey(entry.GetType()))
                continue;

            AvailableBuildingLevels.Add(entry.GetType(), 0);
        }
    }
    public void AddAvailableType(Type type)
    {
        if (AvailableBuildingTypes.Contains(type) == false)
        {
            AvailableBuildingTypes.Add(type);
            OnAvailableBuilingTypeAdded?.Invoke(type);
        }
    }
    private void InitDefaultAvailableBuildings()
    {
        AddAvailableType(typeof(Townhall));
    }

   

    /// <summary>
    /// get all possible buildings and set their count to zero
    /// </summary>
    private void InitCellCountDictionary()
    {
        foreach (var pair in _buildingContainerProvider.BuildingContainer)
        {
            var building = pair.Value;

            InitCellEntryInCountDictionary(building);
        }
    }

    private void InitCellEntryInCountDictionary(AbstractMetaGridCell building)
    {
        Type type = building.GetType();

        if (_metaCellsCountByType.ContainsKey(type))
        {
            Debug.LogError("REPETATIVE BUILDING TYPE INIT ATTEMPT");
        }
        else
            _metaCellsCountByType.Add(type, 0);
    }

    
}

