using Services.GridSystem;
using UnityEngine;

namespace WorldCells
{
    public interface IWorldGridCellObject : IGridCellObject
    {
        public void CheckForNeighborInteraction(IWorldGridCellObject gridCellObject, Vector2 pos);
    }
}