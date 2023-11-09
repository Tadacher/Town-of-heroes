using Core.Towers;
using System;
using UnityEngine;

namespace Services.GridSystem
{
    public class GridAlignService
    {
        private const float _gridsize = 1f;
        private const int _gridSizeX = 32;
        private const int _gridSizeY = 32;

        private static Vector2 _offset = new Vector2(0.5f, 0.5f);
        private IGridCellObject[,] _cells;

        public GridAlignService()
        {
            _cells = new IGridCellObject[_gridSizeX, _gridSizeY];
            InitCellContent();
        }
        public bool CellAvailable(Vector3 position)
        {
            Vector2Int cellCoords = PosToCellCoords(position);
            return Equals(_cells[cellCoords.x, cellCoords.y], null);
        }

        public Vector3 PosToGrid(Vector2 pos) => 
            (Vector2)PosToCellCoords(pos) + _offset;

        public void Insert(IGridCellObject insertable, Vector3 position)
        {
            Vector2Int cellCoords = PosToCellCoords(position);
            _cells[cellCoords.x, cellCoords.y] = insertable;
        }

        private void InitCellContent()
        {
            //place road Cells
            for (int i = 0; i < _gridSizeY - 1; i++)
                _cells[6, i] = new RoadCell();
        }


        private Vector2Int PosToCellCoords(Vector3 position)
        {
            int x = (int)(position.x / _gridsize);
            int y = (int)(position.y / _gridsize);
            Debug.Log(x + " " + y);
            return new Vector2Int(x, y);
        }

    }
}