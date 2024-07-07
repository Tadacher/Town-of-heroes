using Services.Factories;
using UnityEngine;
using Zenject;

public class EnemyFactory : AbstractPoolerFactory<AbstractEnemy> 
{
    
    protected AbstractEnemy _enemyPrefab;
    protected GameObject _prototype;

    public EnemyFactory(AbstractEnemy enemyPrefab, DiContainer diContainer) : base(diContainer) 
        => _enemyPrefab = enemyPrefab;

    public override AbstractEnemy GetObject() =>
        _pool.Get();

    public override void ReturnToPool(IPoolableObject poolable) => 
        _pool.Release((AbstractEnemy)poolable);

    protected override void ActionOnDestroy(AbstractEnemy poolable) => 
        GameObject.Destroy(poolable.gameObject);

    protected override void ActionOnGet(AbstractEnemy poolable) => 
        poolable.gameObject.SetActive(true);

    protected override void ActionOnRelease(AbstractEnemy poolable) => 
        poolable.gameObject.SetActive(false);

    protected override AbstractEnemy CreateNew()
    {
        AbstractEnemy enemy = _container.InstantiatePrefabForComponent<AbstractEnemy>(prefab: _enemyPrefab, parentTransform: null);
        enemy.Init(this);
        
        return enemy;
    }
}
