using Services.GridSystem;
using UnityEngine;

namespace WorldCells
{
    public class GrassMeadows : AbstractWorldCell
    {
        protected override void AddResources()
        {
            _resourceService.GatherFood(1);
        }

        protected override void CheckForGeneralInteraction()
        {

        }

        protected override void CheckForNeighborInteraction(IGridCellObject gridCellObject, Vector2 pos)
        {

        }
    }
}