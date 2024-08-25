using Progress;
using Services.GlobalMap;
using Services.Input;
using UnityEngine;
using WorldCells;
using Zenject;

namespace Services.Factories
{
    public class WorldCellFactory : MonobehaviourAbstractPoolerFactory<AbstractWorldCell>
    {
        private AbstractWorldCell _cellPrefab;
        public WorldCellFactory(AbstractWorldCell cellPrefab, DiContainer diContainer) : base(diContainer)
        {
            _cellPrefab = cellPrefab;
        }

        protected override AbstractWorldCell CreateNew()
        {
            AbstractWorldCell cell = Object.Instantiate(_cellPrefab, null);
            cell.Initialize(objectPooler: this,
                            worldCellBalanceService: _container.Resolve<WorldCellBalanceService>(),
                            inputService: _container.Resolve<AbstractInputService>(),
                            gridAlignService: _container.Resolve<WorldCellGridService>(),
                            resourceService: _container.Resolve<ResourceService>(),
                            worldCellInfoService: _container.Resolve<WorldCellInfoService>(),
                            abstractInputService: _container.Resolve<AbstractInputService>());
            return cell;
        }
    }
}