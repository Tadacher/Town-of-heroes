using Metagameplay.Ui;
using Services.GridSystem;
using Services.Input;
using UnityEngine;

namespace Metagameplay.Buildings
{
    /// <summary>
    /// Base class for every meta building
    /// </summary>
    public abstract class AbstractMetaGridCell : MonoBehaviour, IGridCellObject, IPoolableObject
    {
        /// <summary>
        /// building name from description
        /// </summary>
        public string Name => _description.Name;
        /// <summary>
        /// building image from description
        /// </summary>
        public Sprite Image => _description.Image;
        /// <summary>
        /// link to building description config
        /// </summary>
        public MetaBuildingDescriptionParams Description => _description;


        [SerializeField] protected PointerFollower _pointerFollower;
        [SerializeField] protected SpriteRenderer _spriteRenderer;
        /// <summary>
        /// Building description shown in menu
        /// </summary>
        [SerializeField] protected MetaBuildingDescriptionParams _description;

        private IObjectPooler _pooler;
        private MetaGridSevice _gameplayGridSevice;
        private MetaCityService _cityService;

        public void Initialize(
            AbstractInputService abstractInputService,
            MetaGridSevice abstractGridService,
            MetaCityService metaCityService,
            IObjectPooler objectPooler)
        {
            _cityService = metaCityService;
            _gameplayGridSevice = abstractGridService;
            _pooler = objectPooler;
            _pointerFollower.Initialize(abstractInputService, abstractGridService);

        }
        /// <summary>
        /// Apply building effect
        /// </summary>
        public abstract void ApplyEffect();
        public abstract void UnApplyEffect();

        public void StartFollowingPointer() =>
            _pointerFollower.enabled = true;
        public void StopFollowingPointer() =>
            _pointerFollower.enabled = false;

        public void ReturnToPool() => _pooler.ReturnToPool(this);


        public AbstractMetaGridCell AsUnGhost() => this;
        public AbstractMetaGridCell AsGhost() => this;

        public void InsertSelfToGrid()
        {
            ApplyGridInteractions();
            _gameplayGridSevice.Insert(this, transform.position);
            _cityService.AddCell(this);
        }
        protected abstract void ApplyGridInteractions();
    }
}