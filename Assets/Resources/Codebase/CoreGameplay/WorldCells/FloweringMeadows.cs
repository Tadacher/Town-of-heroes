using UnityEngine;

namespace WorldCells
{
    public class FloweringMeadows : AbstractWorldCell
    {
        public override void CheckForInteraction(IWorldGridCellObject gridCellObject, Vector2 pos)
        {
            
        }

        public override void CheckForNearbyInteractionsUnrecursive()
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