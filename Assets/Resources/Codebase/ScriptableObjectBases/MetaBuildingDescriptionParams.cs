using Core.Towers;
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
        public ResourceData Cost;

        public PerLevelEffect[] PerLevelEffects;
        public ResourceData[] UpgradeCostsPerLevel;

        public Sprite Image;
        public AudioClip ClipOnSelect;
    }

    [Serializable]
    public class PerLevelEffect
    {
        public AbstractTower[] AvailableTowers;
    }
}