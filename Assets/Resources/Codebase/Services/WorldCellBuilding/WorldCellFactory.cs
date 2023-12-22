using Services.GlobalMap;
using Services.Input;
using UnityEngine;

namespace Services.Factories
{
    public class WorldCellFactory : MonobehaviourAbstractPoolerFactory<AbstractWorldCell>
    {
        private AbstractWorldCell _cellPrefab;
        private WorldCellGridService _cellGridService;
        private AbstractInputService _inputService;
        private WorldCellBalanceService _worldCellBalanceService;
        public WorldCellFactory(AbstractWorldCell cellPrefab,
                                WorldCellGridService worldCellGridService,
                                AbstractInputService inputService,
                                WorldCellBalanceService worldCellBalanceService) : base()
        {
            _cellPrefab = cellPrefab;
            _cellGridService = worldCellGridService;
            _inputService = inputService;
            _cellGridService = worldCellGridService;
        }

        protected override AbstractWorldCell CreateNew()
        {
            AbstractWorldCell cell = Object.Instantiate(_cellPrefab, null);
            cell.Initialize(this, _cellGridService, _inputService, _worldCellBalanceService);
            return cell;
        }
    }
}