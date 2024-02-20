using System.Collections.Generic;
using UnityEngine;

namespace TowerCards
{
    public class CardDatabase : ScriptableObject
    {
        /// <summary>
        /// All Tower cards
        /// </summary>
        [SerializeField] private TowerCard[] _towerCards;
        private Dictionary<string, TowerCard> _cards;

        public void Initialize()
        {
            foreach (var card in _towerCards)
            {
                
            }
        }
    }
}
