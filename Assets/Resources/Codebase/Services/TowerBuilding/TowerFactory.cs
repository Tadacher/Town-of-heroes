using Core.Towers;
using Services.GridSystem;
using Services.Input;
using UnityEngine;

public class TowerFactory : AbstractPoolerFactory<AbstractTower>
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

    public override AbstractTower GetObject() 
        => _pool.Get();

    public override void ReturnToPool(IPoolableObject returnable) => 
        _pool.Release((AbstractTower)returnable);

    protected override void ActionOnDestroy(AbstractTower poolable) => 
        Object.Destroy(poolable.gameObject);

    protected override void ActionOnGet(AbstractTower poolable) =>
        poolable.gameObject.SetActive(true);

    protected override void ActionOnRelease(AbstractTower type) => 
        type.gameObject.SetActive(false);

    protected override AbstractTower CreateNew()
    {
        AbstractTower tower = GameObject.Instantiate(_towerPrefab, null);
        tower.Initialize(this, _abstractInputService, _gridAlignService);
        return tower;
    }
}
