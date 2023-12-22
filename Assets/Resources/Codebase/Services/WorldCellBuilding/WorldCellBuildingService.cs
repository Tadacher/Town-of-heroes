using Core.Towers;
using Services.GlobalMap;
using Services.GridSystem;
using Services.Input;
using System;
using UnityEngine;

public class WorldCellBuildingService
{
    protected IPoolableObject _activeCard;
    private WorldCellInstantiationService _instantiatingService;
    private WorldCellGridService _alignerService;

    private AbstractInputService _inputService;


    private AbstractWorldCell _activeCell;

    public WorldCellBuildingService(WorldCellInstantiationService towerInstantiatingService,
                                    WorldCellGridService alignerService,
                                    AbstractInputService inputService)
    {
        _instantiatingService = towerInstantiatingService;
        _alignerService = alignerService;
        _inputService = inputService;

        
    }

    private void TryReleaseActiveCell()
    {
        if (CanBePlacedAtPointer())
        {
            PlaceActiveCell();
            _activeCard.ReturnToPool();
        }
        else
        {
            ((MonoBehaviour)_activeCard).gameObject.SetActive(true);
            _activeCell.StopFollowingPointer();
            _activeCell.ReturnToPool();
        }
        _inputService.OnPointerUp -= TryReleaseActiveCell;
    }

    public void InstantiateWorldCellFromCard(TowerCard towerCard, Type worldCellType)
    {
        _activeCell = GetCellGhost(worldCellType);
        _activeCard = towerCard;

        _activeCell.StartFollowPointer();
        _inputService.OnPointerUp += TryReleaseActiveCell;
    }
    private void PlaceActiveCell() => _activeCell.AsUnGhost().StopFollowingPointer();

    private bool CanBePlacedAtPointer() =>
        _alignerService.CellAvailable(_activeCell.transform.position);

    private AbstractWorldCell GetCellGhost(Type type) =>
        _instantiatingService.ReturnObject(type).AsGhost();
}