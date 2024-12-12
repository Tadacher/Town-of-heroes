using UnityEngine;

namespace WorldCells
{
    public class Hill : AbstractWorldCell
    {
        protected override void CheckNeighborCells()
        {
            int nearbyForestCells = 0;
            for (int x = -1; x <= 1; x++)
            {
                for (int y = -1; y <= 1; y++)
                {
                    if (y == x && y == 0)
                        continue;

                    IWorldGridCellObject gridCellObject = GetNeighborCell(x, y);

                    if (IsHillCell(gridCellObject))
                        nearbyForestCells++;



                    CheckForInteraction(gridCellObject, GetNeighborWorldCoords(x, y));

                    if (gridCellObject != null)
                        gridCellObject.CheckForInteraction(this, _worldCellGridService.PosToGrid(transform.position));
                }
            }
            if (nearbyForestCells == 8)
                ReplaceSelfWith(typeof(ImpenetrableForest));
        }

        private bool IsHillCell(IWorldGridCellObject gridCellObject)
        {
            return
               gridCellObject is Hill;
        }

        public override void CheckForInteraction(IWorldGridCellObject gridCellObject, Vector2 pos)
        {
            return;
        }

        protected override void AddResources()
        {
            _resourceService.GatherStone(3);
        }

        protected override void CheckForGeneralInteraction()
        {
            
        }

        public override void CheckForNearbyInteractionsUnrecursive()
        {
            throw new System.NotImplementedException();
        }
    }
}