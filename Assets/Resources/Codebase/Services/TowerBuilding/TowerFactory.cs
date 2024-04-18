using Core.Towers;
using Services.Factories;
using Services.GridSystem;
using Services.Input;
using UnityEngine;
using Zenject;

public class TowerFactory : MonobehaviourAbstractPoolerFactory<AbstractTower>
{
    protected readonly AbstractTower _towerPrefab;

    public TowerFactory(AbstractTower towerPrefab, DiContainer diContainer) : base(diContainer)
    {
        _towerPrefab = towerPrefab;
    }
    protected override AbstractTower CreateNew()
    {
        AbstractTower tower = GameObject.Instantiate(_towerPrefab, null);
        tower.Initialize(
            objectPooler: this,
            abstractInputService: _container.Resolve<AbstractInputService>(),
            gridAlignService: _container.Resolve<BattleGridService>(),
            towerInfoService: _container.Resolve<TowerInfoServiceIngame>());

        return tower;
    }
}
