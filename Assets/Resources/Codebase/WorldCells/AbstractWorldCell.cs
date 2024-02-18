using Services.CardGeneration;
using Services.GlobalMap;
using Services.GridSystem;
using Services.Input;
using UnityEngine;

namespace WorldCells
{/// <summary>
/// base class for all world cells
/// it is mandatory for it to be in WorldCells namespace for propper card generation
/// </summary>
    public class AbstractWorldCell : MonoBehaviour, IPoolableObject, IGridCellObject
    {
        //external
        [SerializeField] protected PointerFollower _pointerFollower;
        [SerializeField] protected SpriteRenderer _spriteRenderer;
        [SerializeField] protected CellStats _cellStats;
        //dependencies
        protected IObjectPooler _objectPooler;
        protected WorldCellGridService _gridAlignService;
        protected WorldCellBalanceService _worldCellBalanceService;

        public void Initialize(IObjectPooler objectPooler,
                               WorldCellGridService gridAlignService,
                               AbstractInputService inputService,
                               WorldCellBalanceService worldCellBalanceService)
        {
            _objectPooler = objectPooler;
            _gridAlignService = gridAlignService;
            _worldCellBalanceService = worldCellBalanceService;
            _pointerFollower.Initialize(inputService, gridAlignService);
        }

        public void ReturnToPool() =>
               _objectPooler.ReturnToPool(this);

        public void StartFollowPointer() =>
            _pointerFollower.enabled = true;
        public void StopFollowingPointer() =>
            _pointerFollower.enabled = false;
        public virtual void InsertSelfToGrid()
        {
            _gridAlignService.Insert(this, transform.position);
            _pointerFollower.enabled = false;
            _worldCellBalanceService.Count(_cellStats.CellTags);
        }
        public virtual void RemoveSelfFromGrid()
        {
            _gridAlignService.Remove(transform.position);
            _worldCellBalanceService.Clear(_cellStats.CellTags);
        }

        public AbstractWorldCell AsGhost()
        {
            MakeGhost();
            return this;
        }

        public AbstractWorldCell AsUnGhost()
        {
            MakeUnGhost();
            return this;
        }

        protected void MakeUnGhost()
        {

        }

        protected void MakeGhost()
        {

        }
    }
}