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

        public override void CheckForInteraction(IWorldGridCellObject gridCellObject, Vector2 pos)
        {
            switch (gridCellObject)
            {
                case River:
                    ReplaceSelfWith(typeof(FloweringMeadows));
                    break;
            }
        }

        public override void CheckForNearbyInteractionsUnrecursive()
        {

        }
    }
}