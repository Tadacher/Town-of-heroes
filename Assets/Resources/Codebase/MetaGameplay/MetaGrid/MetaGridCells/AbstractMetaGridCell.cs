using Metagameplay.Ui;
using Services;
using Services.GlobalMap;
using Services.GridSystem;
using Services.Input;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.EventSystems;
using WorldCells;
using Zenject;

namespace Metagameplay.Buildings
{
    /// <summary>
    /// Base class for every meta building
    /// </summary>
    public abstract class AbstractMetaGridCell : MonoBehaviour, IGridCellObject, IPoolableObject, IMetaGridCellInfoProvider, IPointerDownHandler, ISelectableElement
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
        public bool HideInBuildingMenu => _hideInBuildingMenu;


        [SerializeField] SpriteRenderer _outline;
        [SerializeField] protected PointerFollower _pointerFollower;
        [SerializeField] protected SpriteRenderer _spriteRenderer;
        /// <summary>
        /// Building description shown in menu
        /// </summary>
        [SerializeField] protected MetaBuildingDescriptionParams _description;
        [SerializeField] private bool _hideInBuildingMenu;
        protected MetaGridSevice _gameplayGridSevice;
        private MetaGridCellInfoService _cellInfoService;
        private IObjectPooler _pooler;
        private MetaCityService _cityService;
        private AbstractSoundPlayerService _soundPlayerService;

        [Inject]
        public void Initialize(
            AbstractInputService abstractInputService,
            MetaGridSevice abstractGridService,
            MetaCityService metaCityService,
            MetaGridCellInfoService metaGridCellInfoService,
            AbstractSoundPlayerService abstractSoundPlayerService)

        {
            _cellInfoService = metaGridCellInfoService;
            _cityService = metaCityService;
            _gameplayGridSevice = abstractGridService;
            _pointerFollower.Initialize(abstractInputService, abstractGridService);
            _soundPlayerService = abstractSoundPlayerService;
        }
        public void InjectDependencies(IObjectPooler objectPooler)
        {
            _pooler = objectPooler;
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
        public void OnPointerDown(PointerEventData eventData)
        {
            _cellInfoService.ShowInfo(this, this);
            Select();
        }
        public void DeSelect()
        {
            _outline.gameObject.SetActive(false);
        }
        public void Select()
        {
            _outline.gameObject.SetActive(true);
            _soundPlayerService.PlayOneShot(_description.ClipOnSelect);
        }
        protected abstract void ApplyGridInteractions();
        protected IWorldGridCellObject GetNeighborCell(int xOffset, int yOffset) =>
            _gameplayGridSevice.GetCellObjectFromGridCoords(xOffset, yOffset);
        protected Vector2 GetNeighborWorldCoords(int xOffset, int yOffset) =>
            (Vector2)transform.position + new Vector2(xOffset, yOffset);
        protected bool CanGetNeigborCell(int x, int y) 
            => _gameplayGridSevice.CoordIsInsideGrid(x, y);
    }
}
