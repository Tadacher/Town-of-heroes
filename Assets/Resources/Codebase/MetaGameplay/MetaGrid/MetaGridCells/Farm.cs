using System;
using UnityEngine;
using Zenject;

namespace Metagameplay.Buildings
{
    public class Farm : AbstractMetaGridCell
    {
        private MetaBuildingsInstantiationService _instantiationService;
        [Inject]
        public void InjectDependencies(MetaBuildingsInstantiationService metaBuildingsInstantiationService)
        {
            _instantiationService = metaBuildingsInstantiationService;
        }
        public override void ApplyEffect()
        {

        }

        public override void UnApplyEffect()
        {

        }

        protected override void ApplyGridInteractions()
        {
            Vector2Int pos = _gameplayGridSevice.PosToCellCoords(transform.position);
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
                    if (cell == null) 
                    {
                        AbstractMetaGridCell field = _instantiationService.ReturnObject(typeof(FarmField));
                        _gameplayGridSevice.Insert(field, placePos);
                        field.transform.position = _gameplayGridSevice.CelCoordsToPos(placePos);
                        field.StopFollowingPointer();
                    }
                }
            }
        }
    }
}