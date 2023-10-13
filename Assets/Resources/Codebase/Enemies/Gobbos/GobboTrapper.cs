using MovementModules;
using Services;
using UnityEngine;

public class GobboTrapper : Gobbo
{
    [SerializeField] private float _dodgeValue;
    public override void Initialize(AudioSource audioSource, DamageTextService damageTextService, IObjectPooler objectPooler)
    {
        base.Initialize(audioSource, damageTextService, _pooler);
        _abstractHealthModule = new DodgeHealthModule(_dodgeValue, transform, damageTextService);
        _enemyMovementModule = new StraightMovementModule(transform, new Vector3(-1.5f, -11.5f, 0f), this, _speed);
    }
    public override void Heal(int points)
    {
        _hitpoints += points;
        if (_hitpoints > _maxHitpoints)
            _hitpoints = _maxHitpoints;
    }
}
