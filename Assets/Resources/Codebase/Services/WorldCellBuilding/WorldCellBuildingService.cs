using Core.Towers;
using Services.GridSystem;
using Services.Input;
using System;
using UnityEngine;

public class WorldCellBuildingService
{
    protected IPoolableObject _activeCard;
    private WorldCellInstantiationService _towerInstantiatingService;
    private BattleGridService _alignerService;

    private AbstractInputService _inputService;


    private AbstractWorldCell _activeCell;

    public WorldCellBuildingService(WorldCellInstantiationService towerInstantiatingService, BattleGridService alignerService, AbstractInputService inputService)
    {
        _towerInstantiatingService = towerInstantiatingService;
        _alignerService = alignerService;
        _inputService = inputService;
    }

    public void InstantiateWorldCellFromCard(TowerCard towerCard, Type worldCellType)
    {
        Debug.Log("world cell");
    }
}