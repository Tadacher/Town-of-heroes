using System.Collections.Generic;
using UnityEngine;

namespace WorldCellBuilding.CardImage
{
    /// <summary>
    /// contains all images for world cell cards
    /// </summary>
    [CreateAssetMenu(fileName = "CardImageDatabase", menuName = "ScriptableObjects/CardImageDatabase", order = 1)]
    
    public class CardImageDatabase : ScriptableObject, IInitializableConfig
    {
        [SerializeField] private Sprite[] WorldCellCardSprites;
        /// <summary>
        /// where key is "Worldcells." + sprite.name and value is sprite itself
        /// </summary>
        public Dictionary<string, Sprite> WorldCellCardSpritesDataBase;

        public void Initialize()
        {
            if (WorldCellCardSpritesDataBase != null)
                return;
           
            WorldCellCardSpritesDataBase = new Dictionary<string, Sprite>();

            for (int index = 0; index < WorldCellCardSprites.Length; index++)
            {
                Sprite sprite = WorldCellCardSprites[index];
                WorldCellCardSpritesDataBase.Add(sprite.name, sprite);
            }
        }
        /// <summary>
        /// get sprite by world cell name
        /// </summary>
        /// <param name="name">same as world cell type</param>
        /// <returns>sprite for worldcell</returns>
        public Sprite GetSprite(string name)
        {
            if(WorldCellCardSpritesDataBase.ContainsKey(name))
                return WorldCellCardSpritesDataBase[name];

            Debug.Log($"No sprites found for {name}");
            return null;
        }
    }
}
