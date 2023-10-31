using Services;
using System;
using UnityEngine;

public class EnemyInstantiationService : AbstractInstantiationService<AbstractEnemy>
{
    private AudioSource _audioSource;
    private DamageTextService _damageTextService;
    private Transform _spawnPosition;

    private const string _enemyPrefabPath = "Prefabs/Enemies/";

    public EnemyInstantiationService(AudioSource audioSource, DamageTextService damageTextService, EnemySpawnPosMarker enemySpawnPosMarker) : base(_enemyPrefabPath)
    {
        _spawnPosition = enemySpawnPosMarker.transform;
        
        _audioSource = audioSource;
        _damageTextService = damageTextService;
    }

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
        new EnemyFactory(_audioSource, _damageTextService, LoadProductPrefab(productType));

}
