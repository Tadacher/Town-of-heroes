using UnityEngine;

namespace Services.Factories
{
    public class WorldCellFactory : MonobehaviourAbstractPoolerFactory<AbstractWorldCell>
    {
        private AbstractWorldCell _cellPrefab;
        public WorldCellFactory(AbstractWorldCell abstractWorldCell) : base()
        {

        }

        protected override AbstractWorldCell CreateNew()
        {
            AbstractWorldCell cell = Object.Instantiate(_cellPrefab, null);
            cell.Initialize();
            return cell;
        }
    }
}