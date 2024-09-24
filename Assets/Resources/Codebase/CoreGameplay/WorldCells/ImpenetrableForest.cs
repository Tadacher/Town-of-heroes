using UnityEngine;

namespace WorldCells
{
    public class ImpenetrableForest : AbstractWorldCell
    {
        public override void CheckForNeighborInteraction(IWorldGridCellObject gridCellObject, Vector2 pos)
        {
            
        }

        protected override void AddResources()
        {
            _resourceService.GatherFood(1);
        }

        protected override void CheckForGeneralInteraction()
        {
            
        }
    }
}