using Core.Towers;
using System;
using UnityEngine;
using WorldCells;

namespace Services.CardGeneration
{
    /// <summary>
    /// contains all tower to worldcell configs
    /// </summary>
    [CreateAssetMenu(fileName = "WorldCellCardsConfig", menuName = "ScriptableObjects/WorldCellCardsConfig", order = 1)]
    public class WorldCellCardsConfig : ScriptableObject
    {
        public TowerToWorldCellPair[] Configs;
    }
    /// <summary>
    /// describes which tower on card can contain which world cell card on other side
    /// </summary>
    [Serializable]
    public class TowerToWorldCellPair
    {
        public AbstractTower Tower;
        public CellProbabilityPair[] CellProbabilityPairs;
    }
    /// <summary>
    /// contains weighted probability of world cell to be spawned on card
    /// </summary>
    [Serializable]
    public class CellProbabilityPair
    {
        public AbstractWorldCell WorldCell;
        public float ProbabilityWeight;
    }
}