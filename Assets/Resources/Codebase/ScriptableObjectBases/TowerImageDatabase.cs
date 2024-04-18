using System.Collections.Generic;
using UnityEngine;

namespace Services.CardGeneration
{
    /// <summary>
    /// contains all sprites for tower cards
    /// </summary>
    [CreateAssetMenu(fileName = "TowerCardImageDatabase", menuName = "ScriptableObjects/InitializableConfig/TowerCardImageDatabase", order = 1)]

    public class TowerImageDatabase : ScriptableObject, IInitializableConfig
    {
        public Dictionary<string, Sprite> TowerCardSpritesDataBase;

        [SerializeField] private Sprite[] _towerCardSprites;
        void IInitializableConfig.Initialize()
        {
            if (TowerCardSpritesDataBase != null)
                return;

            TowerCardSpritesDataBase = new Dictionary<string, Sprite>();

            for (int index = 0; index < _towerCardSprites.Length; index++)
            {
                Sprite sprite = _towerCardSprites[index];
                TowerCardSpritesDataBase.Add(sprite.name, sprite);
            }
        }
        /// <summary>
        /// get sprite by tower name
        /// </summary>
        /// <param name="name">same as tower type</param>
        /// <returns>sprite for worldcell</returns>
        public Sprite GetSprite(string name)
        {
            if (TowerCardSpritesDataBase.ContainsKey(name))
                return TowerCardSpritesDataBase[name];

            Debug.Log($"No sprites found for {name}");
            return null;
        }
    }
}