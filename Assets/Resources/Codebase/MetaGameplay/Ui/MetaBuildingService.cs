using Metagameplay.Buildings;
using Services.Input;
using System;
using UnityEngine;

/// <summary>
/// Instantiates and controlls lifecycle of meta buildings
/// </summary>
public class MetaBuildingService
{
    IPoolableObject _activeBuilding;
    private MetaBuildingsInstantiationService _metaBuildingsInstantiationService;
    private MetaGridSevice _metaGameplayGridSevice;

    private AbstractInputService _inputService;
    private AbstractMetaGridCell _activeCell;

    public MetaBuildingService(MetaBuildingsInstantiationService metaBuildingsInstantiationService,
                               MetaGridSevice gameplayGridSevice,
                               AbstractInputService inputInputService)
    {
        _metaBuildingsInstantiationService = metaBuildingsInstantiationService;
        _metaGameplayGridSevice = gameplayGridSevice;
        _inputService = inputInputService;
    }

    public void InstantiateBuilding(Type type)
    {
        _activeCell = GetBuildingGhost(type);
        _activeBuilding = _activeCell;
        _activeCell.StartFollowingPointer();

        _inputService.OnPointerDown += TryReleaseActiveCell;
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
