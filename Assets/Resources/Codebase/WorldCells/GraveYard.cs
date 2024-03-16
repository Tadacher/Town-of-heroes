using Services.GridSystem;
using UnityEngine;

namespace WorldCells
{
    public class GraveYard : AbstractWorldCell
    {
        protected override void AddResources()
        {
            _resourceService.GatherStone(1);
        }

        protected override void CheckForGeneralInteraction()
        {

        }

        protected override void CheckForNeighborInteraction(IGridCellObject gridCellObject, Vector2 pos)
        {
            switch (gridCellObject)
            {
                //place
                case Forest:
                    _worldCellGridService.SpawnAndReplacetCellToWorld(typeof(CursedForest), pos);
                    break;
            }
        }
    }
}