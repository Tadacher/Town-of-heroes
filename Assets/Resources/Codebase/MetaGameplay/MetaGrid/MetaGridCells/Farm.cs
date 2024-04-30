using Progress;
using UnityEngine;
using Zenject;

namespace Metagameplay.Buildings
{
    public class Farm : AbstractMetaGridCell
    {
        private MetaBuildingsInstantiationService _instantiationService;
        private ResourceService _resourceService;
        private int _fieldCount;

        [Inject]
        public void InjectDependencies(MetaBuildingsInstantiationService metaBuildingsInstantiationService, ResourceService resourceService)
        {
            _instantiationService = metaBuildingsInstantiationService;
            _resourceService = resourceService;
        }
        public override void ApplyPerWaveEffects(int waveCount)
        {
            CountFields();

            base.ApplyPerWaveEffects(waveCount);
            _resourceService.GatherFood(waveCount * _fieldCount);
        }

        private void CountFields()
        {
            Vector2Int pos = _gridSevice.PosToCellCoords(transform.position);
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

                    if (cell is FarmField) 
                        AddField();
                }
            }
        }

        protected override void ApplyGridInteractions()
        {
            Vector2Int pos = _gridSevice.PosToCellCoords(transform.position);
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
                        field.transform.position = _gridSevice.CelCoordsToPos(placePos);
                        field.InsertSelfToGrid(); 
                        field.StopFollowingPointer();
                        Vector2Int debugpos = _gridSevice.PosToCellCoords(field.transform.position);
                        
                        Debug.Log($"Field inserted at {debugpos.x} {debugpos.y}");
                    }
                    else
                    {
                        Debug.Log(cell);
                    }
                }
            }
        }

        public void AddField()
        {
            _fieldCount++;
        }

        protected override void UnApplyGridInteractions()
        {
            Vector2Int pos = _gridSevice.PosToCellCoords(transform.position);
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

                    if (cell is FarmField)
                        (cell as FarmField).TryReturnToPool();
                }
            }
        }
    }
}