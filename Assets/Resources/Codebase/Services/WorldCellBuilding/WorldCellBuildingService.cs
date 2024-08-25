using Services.GlobalMap;
using Services.Input;
using System;
using UnityEngine;
using WorldCells;

public class WorldCellBuildingService
{
    protected IPoolableObject _activeCard;
    protected WorldCellInstantiationService _instantiatingService;
    protected WorldCellGridService _alignerService;

    protected AbstractInputService _inputService;


    protected AbstractWorldCell _activeCell;

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
        _inputService.OnPointerUp -= TryReleaseActiveCell;
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
    }

    public void InstantiateWorldCellFromCard(TowerCard towerCard, Type worldCellType)
    {
        _activeCell = GetCellGhost(worldCellType);
        _activeCard = towerCard;

        _activeCell.StartFollowPointer();
        _inputService.OnPointerUp += TryReleaseActiveCell;
    }
    private void PlaceActiveCell()
    {
        _activeCell.AsUnGhost().StopFollowingPointer();
        _activeCell.InsertSelfToGrid();
    }

    private bool CanBePlacedAtPointer() =>
        _alignerService.CellAvailable(_activeCell.transform.position);

    private AbstractWorldCell GetCellGhost(Type type) =>
        _instantiatingService.ReturnObject(type).AsGhost();
}