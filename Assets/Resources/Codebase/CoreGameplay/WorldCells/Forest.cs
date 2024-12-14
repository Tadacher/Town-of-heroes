using System;
using UnityEngine;

namespace WorldCells
{
    public class Forest : AbstractWorldCell
    {
        public override void CheckForNearbyInteractionsUnrecursive()
        {
            int nearbyForestCells = GetNearbyForestCells();
            if (nearbyForestCells == 8)
                ReplaceSelfWith(typeof(ImpenetrableForest));
        }
        public override void CheckForInteraction(IWorldGridCellObject gridCellObject, Vector2 pos)
        {
            switch (gridCellObject)
            {
                //place
                case GraveYard graveYard:
                    ReplaceSelfWith(typeof(CursedForest));
                    break;

            }
        }

        protected override void AddResources()
        {
            _resourceService.GatherWood(1);
        }

        protected override void CheckNeighborCells()
        {
            int nearbyForestCells = GetNearbyForestCells();
            if (nearbyForestCells == 8)
                    ReplaceSelfWith(typeof(ImpenetrableForest));
           
            for (int x = -1; x <= 1; x++)
            {
                for (int y = -1; y <= 1; y++)
                {
                    if (y == x && y == 0)
                        continue;

                    IWorldGridCellObject gridCellObject = GetNeighborCell(x, y);

                    if (IsForestCell(gridCellObject))
                    {
                        gridCellObject.CheckForNearbyInteractionsUnrecursive();
                    }

                    CheckNeighborInteractionsWithTargetedCell(x, y, gridCellObject);
                }
            }
            if (nearbyForestCells == 8)
                ReplaceSelfWith(typeof(ImpenetrableForest));
        }


        private int GetNearbyForestCells()
        {
            int cellCount = 0;
            for (int x = -1; x <= 1; x++)
            {
                for (int y = -1; y <= 1; y++)
                {
                    if (y == x && y == 0)
                        continue;

                    IWorldGridCellObject gridCellObject = GetNeighborCell(x, y);
                    if (IsForestCell(gridCellObject)) //в теории можно оптимиировать добавив сюда прогон на вызов проверок для сседних клеток,
                        cellCount++;                          //чтобы два раза is Forest не вызывать, но тогда как избавиться от рекурсии
                }
            }
            return cellCount;
        }


        /// <summary>
        /// calls interaction check from this cell to target and vice versa
        /// </summary>
        /// <param name="x">x grid offset</param>
        /// <param name="y">y grid offset</param>
        /// <param name="gridCellObject"></param>
        private void CheckNeighborInteractionsWithTargetedCell(int x, int y, IWorldGridCellObject gridCellObject)
        {
            //from cell to target check
            CheckForInteraction(gridCellObject, GetNeighborWorldCoords(x, y));

            //from target to cell check
            if (gridCellObject != null)
                gridCellObject.CheckForInteraction(this, _worldCellGridService.PosToGrid(transform.position));
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
    }
}