using Services.Factories;
using Services.GridSystem;
using System;
using UnityEngine;
using WorldCells;

namespace Services.GlobalMap
{
    public class WorldCellGridService : AbstractGridService
    {
        private static readonly Vector2Int _offset = new(27, 38); //to 72, 60
        private const int _cellCountx = 45;
        private const int _cellCounty = 22;
        public readonly static float Cellsize = 0.5f;
        private readonly WorldCellInstantiationService _worldCellInstantiationService;

        public WorldCellGridService(WorldCellInstantiationService worldCellInstantiationService) : base(new(Cellsize, Cellsize), _offset, 1, _cellCountx, _cellCounty)
        {
            _worldCellInstantiationService = worldCellInstantiationService;
        }
        public IGridCellObject GetCellObjectFromWorldCoords(Vector2 coords)
        {
            Vector2Int coord = PosToCellCoords(coords);
            return _cells[coord.x, coord.y];
        }

        protected override void InitCellContent()
        {
            //place castle and initial cells
        }
        public void SpawnAndInjectCellToWorld(Type type, Vector2 coords)
        {
            AbstractWorldCell cell = _worldCellInstantiationService.ReturnObject(type).AsUnGhost();
            cell.transform.position = coords;
            cell.InsertSelfToGrid();
        }

        public void SpawnAndReplacetCellToWorld(Type type, Vector3 coords)
        {
            var prevCell = (AbstractWorldCell)GetCellObjectFromWorldCoords(coords);
            prevCell.RemoveSelfFromGrid();
            prevCell.ReturnToPool();

            AbstractWorldCell cell = _worldCellInstantiationService.ReturnObject(type).AsUnGhost();
            cell.transform.position = coords;
            cell.InsertSelfToGrid();
        }
    }
}
