using Progress;
using Services.CardGeneration;
using Services.GlobalMap;
using Services.Input;
using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace WorldCells
{

    /// <summary>
    /// base class for all world cells
    /// it is mandatory for it to be in WorldCells namespace for propper card generation
    /// </summary>
    public abstract class AbstractWorldCell : MonoBehaviour, IPoolableObject, IWorldGridCellObject, IPointerDownHandler, IWorldCellInfoProvider
    {
        public string Name => _cellStats.Name;
        public string Description => _cellStats.Description;
        public CellBiomeTypes[] CellTypes => _cellStats.CellTags;


        //external
        [SerializeField] protected PointerFollower _pointerFollower;
        [SerializeField] protected SpriteRenderer _spriteRenderer;
        [SerializeField] protected CellStats _cellStats;
        //dependencies
        protected IObjectPooler _objectPooler;
        protected WorldCellGridService _worldCellGridService;
        protected WorldCellBalanceService _worldCellBalanceService;
        protected ResourceService _resourceService;

        private AbstractInputService _abstractInputService;
        private WorldCellInfoService _worldCellInfoService;

        private bool _pooled;
        public void Initialize(IObjectPooler objectPooler,
                               WorldCellGridService gridAlignService,
                               AbstractInputService inputService,
                               WorldCellBalanceService worldCellBalanceService,
                               ResourceService resourceService,
                               AbstractInputService abstractInputService,
                               WorldCellInfoService worldCellInfoService)
        {
            _resourceService = resourceService;
            _objectPooler = objectPooler;
            _worldCellGridService = gridAlignService;
            _worldCellBalanceService = worldCellBalanceService;
            _pointerFollower.Initialize(inputService, gridAlignService);
            _abstractInputService = abstractInputService;
            _worldCellInfoService = worldCellInfoService;
        }
        public void OnPointerDown(PointerEventData eventData)
        {
            if (_abstractInputService.RightMouseDown())
                _worldCellInfoService.Show(this);
        }

        public void ActionOnGet() => _pooled = false;
        public void ReturnToPool()
        {
            if (_pooled)
                return;
            _pooled = true;
            _objectPooler.ReturnToPool(this);
        }

        public void StartFollowPointer() =>
            _pointerFollower.enabled = true;
        public void StopFollowingPointer() =>
            _pointerFollower.enabled = false;
        public virtual void InsertSelfToGrid()
        {
            _worldCellGridService.Insert(this, transform.position);
            _pointerFollower.enabled = false;
            _worldCellBalanceService.Count(_cellStats.CellTags);

            AddResources();
            CheckForGeneralInteraction();
            CheckNeighborCells();
        }

        /// <summary>
        /// Add resources on grid placement
        /// </summary>
        protected abstract void AddResources();

        /// <summary>
        /// Use this to completely remove cell from grid and subtract tag weights. Remove it from world with PoolSelf()
        /// </summary>
        public virtual void RemoveSelfFromGrid()
        {
            _worldCellGridService.Remove(transform.position);
            _worldCellBalanceService.UnCount(_cellStats.CellTags);
        }
        /// <summary>
        /// this method calls all scripted interactions between cells on this cell and on every cell around
        /// </summary>
        protected virtual void CheckNeighborCells()
        {
            for (int x = -1; x <= 1; x++)
            {
                for (int y = -1; y <= 1; y++)
                {
                    IWorldGridCellObject gridCellObject = GetNeighborCell(x, y);

                    CheckForInteraction(gridCellObject, GetNeighborWorldCoords(x, y));

                    if(gridCellObject != null)
                        gridCellObject.CheckForInteraction(this, _worldCellGridService.PosToGrid(transform.position));
                }
            }
        }
        public abstract void CheckForNearbyInteractionsUnrecursive();
        /// <summary>
        /// Returns neighborCell. Calculates coordinates by adding cellsize * given offset
        /// </summary>
        /// <param name="xOffset">offset x</param>
        /// <param name="yOffset">offset y</param>
        /// <returns>neighborCell</returns>
        protected IWorldGridCellObject GetNeighborCell(int xOffset, int yOffset) =>
            _worldCellGridService.GetCellObjectFromWorldCoords(GetNeighborWorldCoords(xOffset, yOffset));

        /// <summary>
        /// gets world position of a neighborCell by original cell world position and grid position offset
        /// </summary>
        /// <param name="xOffset">grid offset x</param>
        /// <param name="yOffset">grid offset y</param>
        /// <returns></returns>
        protected Vector2 GetNeighborWorldCoords(int xOffset, int yOffset) => 
            (Vector2)transform.position + new Vector2(xOffset, yOffset) * _worldCellGridService.GetCellSize;


        public abstract void CheckForInteraction(IWorldGridCellObject gridCellObject, Vector2 pos);

        /// <summary>
        /// checks for such interaction triggers as count of cells in world, kiled enemies, placed towers, plot events and such
        /// </summary>
        protected abstract void CheckForGeneralInteraction();
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
        protected void PoolSelf()
        {
            if (_pooled)
                return;
            _pooled = true;
            _objectPooler.ReturnToPool(this);
        }

        protected void ReplaceSelfWith(Type celltype)
        {
            RemoveSelfFromGrid();
            _worldCellGridService.SpawnAndInjectCellToWorld(celltype, transform.position);
            PoolSelf();
        }

       
    }
}