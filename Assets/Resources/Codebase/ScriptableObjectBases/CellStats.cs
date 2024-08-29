using System;
using UnityEngine;
using WorldCells;

namespace Services.CardGeneration
{
    [CreateAssetMenu(fileName = "CellStats", menuName = "ScriptableObjects/CellStats", order = 1)]
    public class CellStats : ScriptableObject
    {
        public string Name;
        public string Description;
        public CellBiomeTypes[] CellTags;
       
        public TowerBuffPair[] TowerBuffs;
        public EnemyBuffPair[] EnemyBuffs;
        [Serializable]
        public class TowerBuffPair
        {
            public TowerBuffType Type;
            public float Value;
        }
        [Serializable]
        public class EnemyBuffPair
        {
            public EnemyBuffType Type;
            public float Value;
        }
    }

    public enum TowerBuffType
    {
        Damage,
        AttackSpeed,
        Range,
    }
    public enum EnemyBuffType
    {
        Health,
        Speed,
        PoolSize,
    }
}