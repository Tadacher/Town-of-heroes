using Services.GridSystem;
using UnityEngine;

namespace WorldCells
{
    public class River : AbstractWorldCell
    {
        protected override void AddResources() => _resourceService.GatherFood(4);

        protected override void CheckForGeneralInteraction()
        {
           
        }

        public override void CheckForInteraction(IWorldGridCellObject gridCellObject, Vector2 pos)
        {
            
        }

        public override void CheckForNearbyInteractionsUnrecursive()
        {

        }
    }
}