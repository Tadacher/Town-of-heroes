using Services.Factories;
using UnityEngine;
using Zenject;

public class EnemyFactory : MonobehaviourAbstractPoolerFactory<AbstractEnemy> 
{
    
    protected AbstractEnemy _enemyPrefab;
    protected GameObject _prototype;

    public EnemyFactory(AbstractEnemy enemyPrefab, DiContainer diContainer) : base(diContainer) 
        => _enemyPrefab = enemyPrefab;

    protected override AbstractEnemy CreateNew()
    {
        AbstractEnemy enemy = _container.InstantiatePrefabForComponent<AbstractEnemy>(prefab: _enemyPrefab, parentTransform: null);
        enemy.Init(this);
        
        return enemy;
    }
}
