using System;
using System.Collections.Generic;

public partial class WaveGenerator
{
    public class EnemyGenerationCostService
    {
        private Dictionary<Type, int> EnemyCosts = new();
        public EnemyGenerationCostService(EnemyPrefabContainer enemyPrefabContainer)
        {
            foreach (var enemy in enemyPrefabContainer.Enemies)
            {
                EnemyCosts.Add(enemy.GetType(), enemy.Stats.GenerationCost);
            }
        }
        public int GetCostByType(Type type) => EnemyCosts[type];
    }
}
