using Core.Towers;
using Metagameplay.Buildings;
using Progress;
using System;
using UnityEngine;

namespace Metagameplay.Ui
{
    [CreateAssetMenu(fileName = "BuildingDescruptionParams", menuName = "ScriptableObjects/BuildingDescruptionParams", order = 1)]
    public class MetaBuildingDescriptionParams : ScriptableObject
    {
        public string Name;
        public string Description;
        public Sprite Image;
        public AudioClip ClipOnSelect;

        [Space]
        [Header("Stats")]
        public int MaxBuildingsOfThisType = 1;
        public ResourceData Cost;
        public PerLevelEffect[] PerLevelEffects;
        public ResourceData[] UpgradeCostsPerLevel;

    }

    [Serializable]
    public class PerLevelEffect
    {
        public AbstractTower[] AvailableTowers;
        public AbstractMetaGridCell[] AvailableBuildings;
        public AvailableBuildingLevel[] AvailableBuldingLevels;
    }
    [Serializable]
    public class AvailableBuildingLevel
    {
        public AbstractMetaGridCell Building;
        public int NewMaxAvailableLevel;
    }
}