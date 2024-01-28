using System.Collections.Generic;
using UnityEngine;

namespace WorldCellBuilding.CardImage
{
    [CreateAssetMenu(fileName = "CardImageDatabase", menuName = "ScriptableObjects/CardImageDatabase", order = 1)]
    public class CardImageDatabase : ScriptableObject
    {
        [SerializeField] private Sprite[] WorldCellCardSprites;
        public Dictionary<string, Sprite> WorldCellCardSpritesDataBase;

        public void Initialize()
        {
            if (WorldCellCardSpritesDataBase != null)
                return;
           
            WorldCellCardSpritesDataBase = new Dictionary<string, Sprite>();

            foreach (var sprite in WorldCellCardSprites)
                WorldCellCardSpritesDataBase.Add("WorldCells." + sprite.name, sprite);
        }
        public Sprite GetSprite(string name)
        {
            if(WorldCellCardSpritesDataBase.ContainsKey(name))
                return WorldCellCardSpritesDataBase[name];

            Debug.Log($"No sprites found for {name}");
            return null;
        }
    }
}
