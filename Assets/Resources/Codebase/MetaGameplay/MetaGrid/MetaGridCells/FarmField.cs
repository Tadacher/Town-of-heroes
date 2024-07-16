using System;
using UnityEngine;

namespace Metagameplay.Buildings
{
    public class FarmField : AbstractMetaGridCell
    {
        public override void UnApplyEffect()
        {
           
            
        }

        protected override void ApplyGridInteractions()
        {
            
        }

        protected override void UnApplyGridInteractions()
        {

        }

        public void TryReturnToPool()
        {
            Vector2Int pos = _gridSevice.PosToCellCoords(transform.position);
            int farmcount = 0;
            for (int x = -1; x <= 1; x++)
            {
                for (int y = -1; y <= 1; y++)
                {
                    Vector2Int placePos = pos;
                    placePos.x += x;
                    placePos.y += y;

                    if (y == x && y == 0)
                        continue;
                    if (!CanGetNeigborCell(placePos.x, placePos.y))
                        continue;

                    var cell = GetNeighborCell(placePos.x, placePos.y);

                    if (cell is Farm)
                       farmcount++;
                }
            }
            if (farmcount == 1)
                ReturnToPool();
            else if (farmcount == 0) 
                Debug.LogError("somehow no farms nearby, probably a bug!");
        }
    }
}