﻿using Core.Towers;

namespace Towers
{
    public abstract class AbstractTowerAttackModule
    {
        public abstract void DealDamage(IHitpointOwner target, int damage, AbstractTower damageDealer);
    }
}