using MovementModules;
using Services;
using System;
using UnityEngine;

public class Gobbo : AbstractEnemy
{
    public override void Initialize(AudioSource audioSource, DamageTextService damageTextService, IObjectPooler objectPooler)
    {
        base.Initialize(audioSource, damageTextService, objectPooler);
        _abstractHealthModule = new DefaultHealthModule(transform, damageTextService);
        _enemyMovementModule = new StraightMovementModule(transform, new Vector3(-1.5f, -11.5f, 0f), this, _speed);
    }
    public override void Heal(int points)
    {
        _hitpoints += points;
        if (_hitpoints > _maxHitpoints)
            _hitpoints = _maxHitpoints;
    }

}