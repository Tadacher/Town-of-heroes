using Core.Towers;
using System;
using UnityEngine;
using WorldCells;

namespace Services.CardGeneration
{
    [CreateAssetMenu(fileName = "WorldCellCardsConfig", menuName = "ScriptableObjects/WorldCellCardsConfig", order = 1)]
    public class WorldCellCardsConfig : ScriptableObject
    {
        public TowerToWorldCellPair[] Configs;
    }

    [Serializable]
    public class TowerToWorldCellPair
    {
        public AbstractTower Tower;
        public CellProbabilityPair[] CellProbabilityPairs;
    }
    [Serializable]
    public class CellProbabilityPair
    {
        public AbstractWorldCell WorldCell;
        public float ProbabilityWeight;
    }
}