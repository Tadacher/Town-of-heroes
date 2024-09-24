using UnityEngine;

namespace WorldCells
{
    public class Forest : AbstractWorldCell
    {
        protected override void AddResources()
        {
            _resourceService.GatherWood(1);
        }

        protected override void CheckNeighborCells()
        {
            int nearbyForestCells = 0;
            for (int x = -1; x <= 1; x++)
            {
                for (int y = -1; y <= 1; y++)
                {
                    if(y == x && y == 0)
                        continue;
                    
                    IWorldGridCellObject gridCellObject = GetNeighborCell(x, y);

                    if (IsForestCell(gridCellObject))
                        nearbyForestCells++;



                    CheckForNeighborInteraction(gridCellObject, GetNeighborWorldCoords(x, y));

                    if (gridCellObject != null)
                        gridCellObject.CheckForNeighborInteraction(this, _worldCellGridService.PosToGrid(transform.position));
                }
            }
            if (nearbyForestCells == 8)
                ReplaceSelfWith(typeof(ImpenetrableForest));
        }

        private bool IsForestCell(IWorldGridCellObject gridCellObject)
        {
           
            return
                gridCellObject is Forest ||
                gridCellObject is ImpenetrableForest;
        }

        protected override void CheckForGeneralInteraction()
        {
                    
        }

        public override void CheckForNeighborInteraction(IWorldGridCellObject gridCellObject, Vector2 pos)
        {
            switch (gridCellObject)
            {
                //place
                case GraveYard graveYard:
                    ReplaceSelfWith(typeof(CursedForest));
                    break;

            }
        }
    }
}