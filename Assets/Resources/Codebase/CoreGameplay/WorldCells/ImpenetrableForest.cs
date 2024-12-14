using UnityEngine;

namespace WorldCells
{
    public class ImpenetrableForest : AbstractWorldCell
    {
        public override void CheckForInteraction(IWorldGridCellObject gridCellObject, Vector2 pos)
        {
            
        }

        public override void CheckForNearbyInteractionsUnrecursive()
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