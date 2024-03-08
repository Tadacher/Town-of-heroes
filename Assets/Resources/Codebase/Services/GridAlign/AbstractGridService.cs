using System;
using UnityEngine;
namespace Services.GridSystem
{
    public abstract class AbstractGridService
    {
        protected readonly float _cellsize;
        protected readonly int _gridSizeX;
        protected readonly int _gridSizeY;

        protected readonly Vector2 _cellOffset;
        protected readonly Vector2 _zeroPoint;

        protected IGridCellObject[,] _cells;

        public AbstractGridService(Vector2 cellOffset, Vector2 zeroPoint, float cellsize, int gridsizex, int gridsizey)
        {
            _zeroPoint = zeroPoint;
            _cellsize = cellsize;
            _gridSizeX = gridsizex;
            _gridSizeY = gridsizey;
            _cellOffset = cellOffset;
            _cells = new IGridCellObject[gridsizex, gridsizey];
        }
        public bool CellAvailable(Vector3 position)
        {
            Vector2Int cellCoords = PosToCellCoords(position);
            return Equals(_cells[cellCoords.x, cellCoords.y], null);
        }
        public void Insert(IGridCellObject insertable, Vector3 position)
        {
            Vector2Int cellCoords = PosToCellCoords(position);
            _cells[cellCoords.x, cellCoords.y] = insertable;
        }
        /// <summary>
        /// removes cell at gicen coordinates, but only in grid, ypu must remove it in world by yourself
        /// </summary>
        /// <param name="position">cell pos</param>
        public void Remove(Vector3 position)
        {
            Vector2Int cellCoords = PosToCellCoords(position);
            _cells[cellCoords.x, cellCoords.y] = null;
        }
        /// <summary>
        /// Converts world position to nearest grid position
        /// </summary>
        /// <param name="pos">world position</param>
        /// <returns>nearest grid position</returns>
        public Vector3 PosToGrid(Vector2 pos) =>
            PosToCellCoords(pos) + _cellOffset + _zeroPoint;

        public Vector3 AlignToGrid(Vector3 position) =>
            ((Vector3)(Vector3Int)PosToCellCoords(position) * _cellsize) + (Vector3)_zeroPoint + (Vector3)_cellOffset;
        /// <summary>
        /// converts position in grid to positiob in world
        /// </summary>
        /// <param name="coords">grid coords</param>
        /// <returns>positiob in world</returns>
        public Vector3 CelCoordsToPos(Vector2 coords) => 
            (coords * _cellsize) + _zeroPoint + _cellOffset;

        /// <summary>
        /// initialize starting cells, that are placed before game starts
        /// </summary>
        protected abstract void InitCellContent();
       
        protected Vector2Int PosToCellCoords(Vector3 position)
        {
            position -= (Vector3)_zeroPoint;
            int x = (int)(position.x / _cellsize);
            int y = (int)(position.y / _cellsize);
            // Debug.Log($"{x}.{y}");
            return new Vector2Int(x, y);
        }  
    }
}