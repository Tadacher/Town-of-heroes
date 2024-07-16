using Services;
using System;
using UnityEngine;
using UnityEngine.UIElements;
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
        AbstractEnemy returnable = _factories[type].GetObject();
        returnable.transform.position = _spawnPosition.position;
        returnable.ReInitialize(_spawnPosition.position);
        return returnable;
    }
    public AbstractEnemy ReturnObject(Type type, Vector3 position)
    {
        if (!_factories.ContainsKey(type))
        {
            AddNewFactory(type);
        }
        var returnable = _factories[type].GetObject();
        returnable.transform.position = position;
        returnable.ReInitialize(position);
        return returnable;
    }

    protected override void AddNewFactory(Type type) => 
        _factories.Add(type, GetNewFactory(type));

    protected override IFactory<AbstractEnemy> GetNewFactory(Type productType) =>
        new EnemyFactory(LoadProductPrefab(productType), _container);

}
