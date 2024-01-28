using Services;
using System;
using UnityEngine;
using Zenject;

public class EnemyInstantiationService : AbstractInstantiationService<AbstractEnemy>
{
    private Transform _spawnPosition;
    private const string _enemyPrefabPath = "Prefabs/Enemies/";

    public EnemyInstantiationService(EnemySpawnPosMarker enemySpawnPosMarker,
                                     DiContainer diContainer) : base(_enemyPrefabPath, diContainer) 
        => _spawnPosition = enemySpawnPosMarker.transform;

    public override AbstractEnemy ReturnObject(Type type)
    {
        if (!_factories.ContainsKey(type))
        {
            AddNewFactory(type);
        }
        var returnable = _factories[type].GetObject();
        returnable.transform.position = _spawnPosition.position;
        return returnable;
    }

    protected override void AddNewFactory(Type type) => 
        _factories.Add(type, GetNewFactory(type));

    protected override IFactory<AbstractEnemy> GetNewFactory(Type productType) =>
        new EnemyFactory(LoadProductPrefab(productType), _container);

}
