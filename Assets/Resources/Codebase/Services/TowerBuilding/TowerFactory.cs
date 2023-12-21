using Core.Towers;
using Services.Factories;
using Services.GridSystem;
using Services.Input;
using UnityEngine;

public class TowerFactory : MonobehaviourAbstractPoolerFactory<AbstractTower>
{
    protected readonly AbstractTower _towerPrefab;
    protected readonly AbstractInputService _abstractInputService;
    protected readonly BattleGridService _gridAlignService;

    public TowerFactory(AbstractTower towerPrefab, AbstractInputService abstractInputService, BattleGridService gridAlignService) : base()
    {
        _gridAlignService = gridAlignService;
        _towerPrefab = towerPrefab;
        _abstractInputService = abstractInputService;
    }
    protected override AbstractTower CreateNew()
    {
        AbstractTower tower = GameObject.Instantiate(_towerPrefab, null);
        tower.Initialize(this, _abstractInputService, _gridAlignService);
        return tower;
    }
}
