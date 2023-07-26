using System;
using UnityEngine;

public class Gobbo : AbstractEnemy
{
    public override void Heal(int points)
    {
        _hitpoints += points;
        if (_hitpoints > _maxHitpoints)
            _hitpoints = _maxHitpoints;
    }

}
