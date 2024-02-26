using Services.GridSystem;
using UnityEngine;

namespace WorldCells
{
    public class Forest : AbstractWorldCell
    {
        protected override void CheckForGeneralInteraction()
        {
            
        }

        protected override void CheckForNeighborInteraction(IGridCellObject gridCellObject, Vector2 pos)
        {
            switch (gridCellObject)
            {
                //place
                case GraveYard graveYard:
                    _worldCellGridService.SpawnAndInjectCellToWorld(typeof(CursedForest), transform.position);
                    RemoveSelfFromGrid();
                    PoolSelf();
                    break;
            }
        }
    }
}