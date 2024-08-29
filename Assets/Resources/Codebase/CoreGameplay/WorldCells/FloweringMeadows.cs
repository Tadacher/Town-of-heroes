using UnityEngine;

namespace WorldCells
{
    public class FloweringMeadows : AbstractWorldCell
    {
        public override void CheckForNeighborInteraction(IWorldGridCellObject gridCellObject, Vector2 pos)
        {
            
        }

        protected override void AddResources()
        {
            _resourceService.GatherFood(5);
        }

        protected override void CheckForGeneralInteraction()
        {
            
        }
    }
}