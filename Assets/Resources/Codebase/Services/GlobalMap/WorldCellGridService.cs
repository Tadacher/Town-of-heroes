using Services.GridSystem;
using UnityEngine;

namespace Services.GlobalMap
{
    public class WorldCellGridService : AbstractGridService
    {
        private static readonly Vector2Int _offset = new(27, 38); //to 72, 60
        private const int _cellCountx = 45;
        private const int _cellCounty = 22;
        public WorldCellGridService() : base(new(0.5f, 0.5f), _offset, 1, _cellCountx, _cellCounty)
        {

        }
    }
}
