using Services;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GobboFactory : AbstractEnemyFactory
{
    private const string _basicGobboKey = "Basic Gobbo";
    
    private readonly AudioSource _audioSource;
    private readonly DamageTextService _damageTextService;

    public GobboFactory(EnemyContainer enemyContainer, AudioSource audioSource, Services.DamageTextService damageTextService)
    {
        _objectPoolList = new();
        _enemyContainer = enemyContainer;
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
        AbstractEnemy newObject = GameObject.Instantiate(_enemyContainer.GetEnemy(_basicGobboKey), spawnpos.position, Quaternion.identity).GetComponent<AbstractEnemy>();
        newObject.Initialize(_audioSource, _damageTextService);
        _objectPoolList.Add(newObject);
        return newObject;
    }
}
