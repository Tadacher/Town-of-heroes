using Services.GridSystem;
using UnityEngine;

namespace WorldCells
{
    public interface IWorldGridCellObject : IGridCellObject
    {
        /// <summary>
        /// Check all possible interactions cell can perform with targeted cell and perfom them
        /// It is called twice - when cell is placed - for every cell around and each time some cell is placed around it will call this for every neighbor
        /// </summary>
        /// <param name="gridCellObject">targeted neigbor cell</param>
        /// <param name="pos">targeted neigbor cell GRID position</param>
        public void CheckForInteraction(IWorldGridCellObject gridCellObject, Vector2 pos);

        /// <summary>
        /// Checks nearby interaction without recursive call for neighbors
        /// </summary>
        public void CheckForNearbyInteractionsUnrecursive();
    }
}