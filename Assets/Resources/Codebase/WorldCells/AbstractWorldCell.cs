using Services.CardGeneration;
using Services.GlobalMap;
using Services.GridSystem;
using Services.Input;
using System;
using UnityEngine;

namespace WorldCells
{/// <summary>
/// base class for all world cells
/// it is mandatory for it to be in WorldCells namespace for propper card generation
/// </summary>
    public abstract class AbstractWorldCell : MonoBehaviour, IPoolableObject, IGridCellObject
    {
        //external
        [SerializeField] protected PointerFollower _pointerFollower;
        [SerializeField] protected SpriteRenderer _spriteRenderer;
        [SerializeField] protected CellStats _cellStats;
        //dependencies
        protected IObjectPooler _objectPooler;
        protected WorldCellGridService _worldCellGridService;
        protected WorldCellBalanceService _worldCellBalanceService;

        public void Initialize(IObjectPooler objectPooler,
                               WorldCellGridService gridAlignService,
                               AbstractInputService inputService,
                               WorldCellBalanceService worldCellBalanceService)
        {
            _objectPooler = objectPooler;
            _worldCellGridService = gridAlignService;
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
            _worldCellGridService.Insert(this, transform.position);
            _pointerFollower.enabled = false;
            _worldCellBalanceService.Count(_cellStats.CellTags);

            CheckNeighborCells();
            CheckForGeneralInteraction();
        }

        /// <summary>
        /// Use this to completely remove cell from grid and subtract tag weights 
        /// </summary>
        public virtual void RemoveSelfFromGrid()
        {
            _worldCellGridService.Remove(transform.position);
            _worldCellBalanceService.UnCount(_cellStats.CellTags);
        }

        protected void CheckNeighborCells()
        {
            for (int x = -1; x <= 1; x++)
            {
                for (int y = -1; y <= 1; y++)
                {
                    CheckForNeighborInteraction(GetNeighborCell(x, y), GetNeighborWorldCoords(x,y));
                }
            }
        }

        /// <summary>
        /// Returns neighborCell. Calculates coordinates by adding cellsize * given offset
        /// </summary>
        /// <param name="xOffset">offset x</param>
        /// <param name="yOffset">offset y</param>
        /// <returns>neighborCell</returns>
        protected IGridCellObject GetNeighborCell(int xOffset, int yOffset) =>
            _worldCellGridService.GetCellObjectFromWorldCoords(
                GetNeighborWorldCoords(xOffset, yOffset));

        private Vector2 GetNeighborWorldCoords(int xOffset, int yOffset)
        {
            return (Vector2)transform.position +
                            (new Vector2(xOffset, yOffset));
        }

        /// <summary>
        /// Check all possible interactions cell can perform with targeted cell and perfom them
        /// </summary>
        /// <param name="gridCellObject">targeted neigbor cell</param>
        /// <param name="pos">targeted neigbor cell position</param>
        protected abstract void CheckForNeighborInteraction(IGridCellObject gridCellObject, Vector2 pos);
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
        protected void PoolSelf() => _objectPooler.ReturnToPool(this);
    }
}