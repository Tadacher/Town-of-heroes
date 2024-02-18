using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using WorldCells;

namespace Services.CardGeneration
{
    public class WorldCellCardGenerator
    {
        private WorldCellCardsConfig _worldCellCardsConfig;
        /// <summary>
        /// tower type, probability of each cell type
        /// </summary>
        private Dictionary<Type, CellProbabilityPair[]> _towerCellPairs; 

        public WorldCellCardGenerator(WorldCellCardsConfig worldCellCardsConfig)
        {
            _towerCellPairs = new Dictionary<Type, CellProbabilityPair[]>();
            _worldCellCardsConfig = worldCellCardsConfig;
            for (int index = 0; index < _worldCellCardsConfig.Configs.Length; index++)
            {
                TowerToWorldCellPair entry = _worldCellCardsConfig.Configs[index];
                Type key = entry.Tower.GetType();

                if (_towerCellPairs.ContainsKey(key))
                {
                    Debug.Log($"multiple entries found for {key}");
                    continue;
                }
                _towerCellPairs.Add(key, entry.CellProbabilityPairs);
            }
        }

        public Type GetWorldCellType(Type towerType)
        {
            CellProbabilityPair[] pairs = _towerCellPairs[towerType];
            float sum = pairs.Sum(p => p.ProbabilityWeight);
            float randomNumber = UnityEngine.Random.Range(0, sum);
            for (int i = 0; i < pairs.Length; i++)
            {
                randomNumber -= pairs[i].ProbabilityWeight;
                if(randomNumber <= 0)
                    return pairs[i].WorldCell.GetType();
            }

            Debug.LogError($"found no acceptable entries on  world cell card generation for {towerType}");
            return typeof(GrassMeadows); 
        }
    }
}