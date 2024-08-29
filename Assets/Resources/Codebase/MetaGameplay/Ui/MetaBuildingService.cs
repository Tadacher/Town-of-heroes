using Metagameplay.Buildings;
using Metagameplay.Ui;
using Progress;
using Services.Input;
using System;

/// <summary>
/// Instantiates and controlls lifecycle of meta buildings
/// </summary>
public class MetaBuildingService : IBuildingPlacedEventProvider
{
    public event Action<AbstractMetaGridCell> OnMetaGridCellPlaced;

    IPoolableObject _activeBuilding;
    private readonly MetaBuildingsInstantiationService _metaBuildingsInstantiationService;
    private readonly MetaGridSevice _metaGameplayGridSevice;
    private readonly ResourceService _resourceService;
    private readonly AbstractInputService _inputService;
    private readonly MetaCityInfoService _metaCityService;


    private  AbstractMetaGridCell _activeCell;

    public MetaBuildingService(MetaBuildingsInstantiationService metaBuildingsInstantiationService,
                               MetaGridSevice gameplayGridSevice,
                               AbstractInputService inputInputService,
                               ResourceService resourceService,
                               MetaCityInfoService metaCityService)
    {
        _metaBuildingsInstantiationService = metaBuildingsInstantiationService;
        _metaGameplayGridSevice = gameplayGridSevice;
        _inputService = inputInputService;
        _resourceService = resourceService;
        _metaCityService = metaCityService;
    }

    public bool TryInstantiateBuilding(AbstractMetaGridCell prefab)
    {
        if (CanBuild(prefab))
        {
            _resourceService.SubtractResources(prefab.Description.Cost);
            _activeCell = GetBuildingGhost(prefab.GetType());
            _activeBuilding = _activeCell;
            _activeCell.StartFollowingPointer();

            _inputService.OnPointerDown += TryReleaseActiveCell;
            return true;
        }
        return false;
    }

    private bool CanBuild(AbstractMetaGridCell prefab)
    {
        if (_resourceService.GetResourceData() < prefab.Description.Cost)
        {
            //show warn
            return false;
        }

        if (_metaCityService.MetaCellCountByType[prefab.GetType()] >= prefab.Description.MaxBuildingsOfThisType)
        {
            //show warn
            return false;
        }

        if(!_metaCityService.AvailableBuildingTypes.Contains(prefab.GetType()))
        {
            //show warn
            return false;
        }
        return true;
    }

    private void TryReleaseActiveCell()
    {
        _inputService.OnPointerDown -= TryReleaseActiveCell;
        if (CanBePlacedAtPointer())
        {
            PlaceActiveCell();
        }
        else
        {
            _activeCell.gameObject.SetActive(false);
            _activeCell.StopFollowingPointer();
            _activeCell.ReturnToPool();
        }
    }

    private void PlaceActiveCell()
    {
        _activeCell.AsUnGhost().StopFollowingPointer();
        _activeCell.InsertSelfToGrid();
    }

    private bool CanBePlacedAtPointer() => 
        _metaGameplayGridSevice.CellAvailable(_activeCell.transform.position);

    private AbstractMetaGridCell GetBuildingGhost(Type type) => 
        _metaBuildingsInstantiationService.ReturnObject(type).AsGhost();
}
