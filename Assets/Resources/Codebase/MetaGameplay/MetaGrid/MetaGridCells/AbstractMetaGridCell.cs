using Metagameplay.Ui;
using Progress;
using Services;
using Services.GridSystem;
using Services.Input;
using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
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
        /// <summary>
        /// Show in building menu or not, marks available for player buildings 
        /// </summary>
        public bool HideInBuildingMenu => _hideInBuildingMenu;
        public bool ShouldNotSave => _shouldNotSave;
        public int Level => _level;


        [SerializeField] SpriteRenderer _outline;
        [SerializeField] protected PointerFollower _pointerFollower;
        [SerializeField] protected SpriteRenderer _spriteRenderer;
        /// <summary>
        /// Building description shown in menu and stats
        /// </summary>
        [SerializeField] protected MetaBuildingDescriptionParams _description;
        [SerializeField] private bool _hideInBuildingMenu;
        [SerializeField] private bool _shouldNotSave;
        [SerializeField] private bool _canBeDestroyed;

        
        protected MetaGridSevice _gridSevice;
        protected CardAvalilabilityService _cardAvalilabilityService;
        protected int _level;

        private IObjectPooler _pooler;
        private ResourceService _resourceService;
        private MetaGridCellInfoService _cellInfoService;
        private MetaCityInfoService _cityService;
        private AbstractSoundPlayerService _soundPlayerService;
        private Button _upgradeButton; 
        private Button _destroyButton;

        [Inject]
        public void Initialize(
            AbstractInputService abstractInputService,
            MetaGridSevice abstractGridService,
            MetaCityInfoService metaCityService,
            MetaGridCellInfoService metaGridCellInfoService,
            AbstractSoundPlayerService abstractSoundPlayerService,
            CardAvalilabilityService cardAvalilabilityService,
            MetaUiContainer metaUiContainer,
            ResourceService resourceService)

        {
            _cellInfoService = metaGridCellInfoService;
            _cityService = metaCityService;
            _gridSevice = abstractGridService;
            _pointerFollower.Initialize(abstractInputService, abstractGridService);
            _soundPlayerService = abstractSoundPlayerService;
            _cardAvalilabilityService = cardAvalilabilityService;
            _resourceService = resourceService;
            _upgradeButton = metaUiContainer.UpgradeBtn;
            _destroyButton = metaUiContainer.DestroyBtn;    
        }
        public void InjectDependencies(IObjectPooler objectPooler)
        {
            _pooler = objectPooler;
        }
        public void OnPointerDown(PointerEventData eventData)
        {
            _cellInfoService.ShowInfo(this, this);
            Select();
        }
        public void ActionOnGet()
        {
           
        }
        /// <summary>
        /// Apply building effect
        /// </summary>
        public void InsertSelfToGrid()
        {
            ApplyGridInteractions();
            ApplyLevelEffects();
            _gridSevice.Insert(this, transform.position);
            _cityService.CountCell(this);
            _cityService.CachePlacedCell(this);
        }

        /// <summary>
        /// scroll per effect array and apply if building level is high enough
        /// </summary>
        public virtual void ApplyLevelEffects()
        {
            if (_description == null)
                return;

            if (_description.PerLevelEffects == null || Description.PerLevelEffects.Length == 0)
                return;

            for (int i = 0; i <= _level; i++)
                ApplyLevelEffect(i);
        }      

        protected void ApplyLevelEffect(PerLevelEffect perLevelEffect)
        {
            foreach (var tower in perLevelEffect.AvailableTowers)
            {
                if (tower == null)
                    continue;

                _cardAvalilabilityService.AddAllowedType(tower.GetType());
            }

            foreach (var building in perLevelEffect.AvailableBuildings)
            {
                if (_cityService.AvailableBuildingTypes.Contains(building.GetType()))
                    continue;

                _cityService.AddAvailableType(building.GetType());
            }

            foreach (var level in perLevelEffect.AvailableBuldingLevels)
            {
                if (!_cityService.AvailableBuildingLevels.ContainsKey(level.Building.GetType()))
                    continue;

                var count = _cityService.AvailableBuildingLevels[level.Building.GetType()];
                if (count > level.NewMaxAvailableLevel)
                {
                    _cityService.AvailableBuildingLevels[level.Building.GetType()] = count;
                }
            }
        }
        protected void ApplyLevelEffect(int level)
        {
            Debug.Log("Levelup " + level);

            if (_description.PerLevelEffects.Length <= level)
                return;
            if (_description.PerLevelEffects[level] == null)
                return;

            ApplyLevelEffect(_description.PerLevelEffects[level]);
        }
        public virtual void UnApplyEffect()
        {
            for (int i = 0; i <= _level; i++)
            {
                if (_description.PerLevelEffects.Length == i)
                    break;
                if (_description.PerLevelEffects[i] == null)
                    continue;

                foreach (var tower in _description.PerLevelEffects[i].AvailableTowers)
                {
                    if (tower == null)
                        continue;
                    _cardAvalilabilityService.RemoveAllowedType(tower.GetType());
                }
            }
        }

        public virtual void ApplyPerWaveEffects(int waveCount)
        {

        }

        public void StartFollowingPointer() =>
            _pointerFollower.enabled = true;
        public void StopFollowingPointer() =>
            _pointerFollower.enabled = false;
        
        public void ReturnToPool() => 
            _pooler.ReturnToPool(this);
       
        
        public AbstractMetaGridCell AsUnGhost() => this;
        public AbstractMetaGridCell AsGhost() => this;

        public void SetLevel(int level) 
            => _level = level;

        public void DeSelect()
        {
            _outline.gameObject.SetActive(false);
            _upgradeButton.onClick.RemoveAllListeners();
            _destroyButton.onClick.RemoveAllListeners();
        }
        public void Select()
        {
            _outline.gameObject.SetActive(true);
            SetUpgradeButtoninteractions();
            SetDestroyButtonInteractions();
            _soundPlayerService.PlayOneShot(_description.ClipOnSelect);
        }

        private void SetDestroyButtonInteractions()
        {
            if (_canBeDestroyed)
            {
                _destroyButton.gameObject.SetActive(true);
                _destroyButton.onClick.AddListener(DestroyBuiding);
            }
            else
            {
                _destroyButton.gameObject.SetActive(false);
            }
        }

        private void DestroyBuiding()
        {
            _destroyButton.onClick.RemoveAllListeners();
            _destroyButton.gameObject.SetActive(false);
            UnApplyGridInteractions();
            ReturnToPool();
        }

        private void SetUpgradeButtoninteractions()
        {
            if (CanBeUpgraded())
            {
                _upgradeButton.gameObject.SetActive(true);
                _upgradeButton.onClick.AddListener(Upgrade);
            }
            else
                _upgradeButton.gameObject.SetActive(false);
        }

        protected virtual void Upgrade()
        {
            _level++;
            ApplyLevelEffect(_level);
            _upgradeButton.onClick.RemoveListener(Upgrade);
            SetUpgradeButtoninteractions();
        }

        private bool CanBeUpgraded()
        {
            if(Description.UpgradeCostsPerLevel == null)
                return false;
            if (Description.UpgradeCostsPerLevel.Length <= Level + 1) //if no levels to upgrade
                return false;

            return Description.UpgradeCostsPerLevel[Level + 1] <= _resourceService.GetResourceData(); //if is enough resources
        }

        protected abstract void ApplyGridInteractions();
        protected abstract void UnApplyGridInteractions();

        protected IGridCellObject GetNeighborCell(int xOffset, int yOffset) =>
            _gridSevice.GetCellObjectFromGridCoords(xOffset, yOffset);
        protected Vector2 GetNeighborWorldCoords(int xOffset, int yOffset) =>
            (Vector2)transform.position + new Vector2(xOffset, yOffset);
        protected bool CanGetNeigborCell(int x, int y) 
            => _gridSevice.CoordIsInsideGrid(x, y);
    }
}
