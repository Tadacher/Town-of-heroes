using MovementModules;
using Services;
using UnityEngine;

public class Gobbo : AbstractEnemy
{
    public override void Initialize(AudioSource audioSource, DamageTextService damageTextService, IObjectPooler objectPooler)
    {
        base.Initialize(audioSource, damageTextService, objectPooler);
        _abstractHealthModule = new DefaultHealthModule(transform, damageTextService);
        _enemyMovementModule = new StraightMovementModule(transform, new Vector3(16.5f, 7.5f, 0f), this, _speed);
    }
}
