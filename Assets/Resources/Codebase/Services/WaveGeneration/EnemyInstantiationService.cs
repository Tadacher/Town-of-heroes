using Services;
using System;
using System.Collections.Generic;
using UnityEngine;

public class EnemyInstantiationService : AbstractInstantiationService<AbstractEnemy>
{
    private AudioSource _audioSource;
    private DamageTextService _damageTextService;
    private Transform _spawnPosition;

    private const string _enemyPrefabPath = "Prefabs/Enemies/";

    public EnemyInstantiationService(AudioSource audioSource, DamageTextService damageTextService, EnemySpawnPosMarker enemySpawnPosMarker) : base()
    {
        _spawnPosition = enemySpawnPosMarker.transform;
        
        _audioSource = audioSource;
        _damageTextService = damageTextService;
    }

    public override TEnemyType ReturnObject<TEnemyType>()
    {
        if (!_factories.ContainsKey(typeof(TEnemyType)))
        {
            AddNewFactory<TEnemyType>();
        }
        var returnable = ((IFactory<TEnemyType>)_factories[typeof(TEnemyType)]).GetObject();
        returnable.transform.position = _spawnPosition.position;
        return returnable;
    }

    private void AddNewFactory<TEnemyType>() where TEnemyType : AbstractEnemy => 
        _factories.Add(typeof(TEnemyType), CreateNewEnemyFactory<TEnemyType>());

    private EnemyFactory<TEnemyType> CreateNewEnemyFactory<TEnemyType>() where TEnemyType : AbstractEnemy => 
        new EnemyFactory<TEnemyType>(_audioSource, _damageTextService, LoadEnemyPrefab<TEnemyType>());

    private TenemyType LoadEnemyPrefab<TenemyType>() where TenemyType : AbstractEnemy
    {
        TenemyType tenemyType = Resources.Load<TenemyType>(_enemyPrefabPath + typeof(TenemyType).Name);
        return tenemyType;
    }
}
