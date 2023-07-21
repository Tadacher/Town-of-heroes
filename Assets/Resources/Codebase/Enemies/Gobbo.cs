using System;
using UnityEngine;

public class Gobbo : AbstractEnemy
{
    public void Awake()
    {
        Initialize();
    }

    protected override void Initialize()
    {
        base.Initialize();
    }
    public override void Heal(int points)
    {
        _hitpoints += points;
        if (_hitpoints > _maxHitpoints)
            _hitpoints = _maxHitpoints;
    }

    internal void Inject(AudioSource audioSource) => _audioSource = audioSource;
}
