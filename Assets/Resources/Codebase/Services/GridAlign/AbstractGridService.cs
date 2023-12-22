using UnityEngine;
namespace Services.GridSystem
{
    public class AbstractGridService
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

        public Vector3 PosToGrid(Vector2 pos)
        {
            Debug.Log((Vector2)PosToCellCoords(pos) + " " + _cellOffset);
            return (Vector2)PosToCellCoords(pos) + _cellOffset + _zeroPoint;
        }

        protected virtual void InitCellContent()
        {
           
        }
        protected Vector2Int PosToCellCoords(Vector3 position)
        {
            position -= (Vector3)_zeroPoint;
            int x = (int)(position.x / _cellsize);
            int y = (int)(position.y / _cellsize);
            Debug.Log(x + " " + y);
            return new Vector2Int(x, y);
        }
    }
}