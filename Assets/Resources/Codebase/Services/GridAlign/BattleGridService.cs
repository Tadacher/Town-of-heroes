using UnityEngine;

namespace Services.GridSystem
{
    public class BattleGridService : AbstractGridService
    {
        protected const float _battleCellSize = 1f;
        protected const int _battleGridSizeX = 32;
        protected const int _battleGridSizeY = 32;
        
        public BattleGridService() : base(new Vector2(0.5f, 0.5f), new Vector2(0f, 0f), _battleCellSize, _battleGridSizeX, _battleGridSizeY)
        {
            _cells = new IGridCellObject[_gridSizeX, _gridSizeY];
            InitCellContent();
        }
        protected override void InitCellContent()
        {
            for (int i = 0; i < _gridSizeY - 1; i++)
                _cells[6, i] = new RoadCell();
        }
    }
}