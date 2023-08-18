using Services;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFactory : AbstractAbstractEnemyFactory
{
    
    private readonly AudioSource _audioSource;
    private readonly DamageTextService _damageTextService;

    public EnemyFactory(AbstractEnemy enemyPrefab, AudioSource audioSource, DamageTextService damageTextService)
    {
        _objectPoolList = new();
        _enemyPrefab = enemyPrefab;
        _audioSource = audioSource;
        _damageTextService = damageTextService;
    }

    public override AbstractEnemy ReturnObject(Transform spawnpos)
    {
        if (TryGetObjectFromList(out AbstractEnemy result))
            result.ReInitialize(spawnpos.position);
        else
            result = ConstructAndPoolNew(spawnpos);
        return result;
    }

    private AbstractEnemy ConstructAndPoolNew(Transform spawnpos)
    {
        AbstractEnemy newObject = GameObject.Instantiate(_enemyPrefab, spawnpos.position, Quaternion.identity);
        newObject.Initialize(_audioSource, _damageTextService);
        _objectPoolList.Add(newObject);
        return newObject;
    }
}
