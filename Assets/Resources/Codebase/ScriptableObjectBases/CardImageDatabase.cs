using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace WorldCellBuilding.CardImage
{
    /// <summary>
    /// contains all images for world cell cards
    /// </summary>
    [CreateAssetMenu(fileName = "CardImageDatabase", menuName = "ScriptableObjects/CardImageDatabase", order = 1)]
    
    public class CardImageDatabase : ScriptableObject
    {
        private const string _worldCellsNameSpace = "WorldCells.";
        [SerializeField] private Sprite[] WorldCellCardSprites;
        /// <summary>
        /// where key is "Worldcells." + sprite.name and value is sprite itself
        /// </summary>
        public Dictionary<string, Sprite> WorldCellCardSpritesDataBase;

        public void Initialize()
        {
            StringBuilder stringBuilder = new StringBuilder();

            if (WorldCellCardSpritesDataBase != null)
                return;
           
            WorldCellCardSpritesDataBase = new Dictionary<string, Sprite>();

            for (int index = 0; index < WorldCellCardSprites.Length; index++)
            {
                stringBuilder.Clear();
                Sprite sprite = WorldCellCardSprites[index];
                stringBuilder.Append(_worldCellsNameSpace); //Namespace is needed due to .GetType() returns full type including namespace
                stringBuilder.Append(sprite.name);
                WorldCellCardSpritesDataBase.Add("WorldCells." + sprite.name, sprite);
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
