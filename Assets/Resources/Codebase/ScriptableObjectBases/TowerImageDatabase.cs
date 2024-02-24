using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace Services.CardGeneration
{
    /// <summary>
    /// contains all sprites for tower cards
    /// </summary>
    [CreateAssetMenu(fileName = "TowerCardImageDatabase", menuName = "ScriptableObjects/TowerCardImageDatabase", order = 1)]

    public class TowerImageDatabase : ScriptableObject, IInitializableConfig
    {
        public Dictionary<string, Sprite> TowerCardSpritesDataBase;

        [SerializeField] private Sprite[] _towerCardSprites;
        private const string _towerNameSpace = "Core.Towers.";
        public void Initialize()
        {
            StringBuilder stringBuilder = new StringBuilder();

            if (TowerCardSpritesDataBase != null)
                return;

            TowerCardSpritesDataBase = new Dictionary<string, Sprite>();

            for (int index = 0; index < _towerCardSprites.Length; index++)
            {
                stringBuilder.Clear();
                Sprite sprite = _towerCardSprites[index];
                stringBuilder.Append(_towerNameSpace); //Namespace is needed due to .GetType() returns full type including namespace
                stringBuilder.Append(sprite.name);
                TowerCardSpritesDataBase.Add(_towerNameSpace + sprite.name, sprite);
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