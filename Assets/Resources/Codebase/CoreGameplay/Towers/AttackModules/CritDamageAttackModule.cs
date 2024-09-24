using Core.Towers;
using UnityEngine;

namespace Towers
{
    public class CritDamageAttackModule : AbstractTowerAttackModule
    {
        /// <summary>
        /// from 0 to 1 inclusive
        /// </summary>
        private float _critChance;
        /// <summary>
        /// from 1 to whatever
        /// </summary>
        private float _critDamage;
        public CritDamageAttackModule(float critChance, float critDamage)
        {
            _critChance = critChance;
            _critDamage = critDamage;
        }

        public override void DealDamage(IHitpointOwner target, int damage, AbstractTower damageDealer)
        {
            if (Random.Range(0f, 1f) <= _critChance)
                DealCritDamage(target, damage, damageDealer);
            else
                DealNormalDamage(target, damage, damageDealer);
        }

        private void DealNormalDamage(IHitpointOwner target, int damage, AbstractTower damageDealer) => target.RecieveDamage(damage, damageDealer);

        private void DealCritDamage(IHitpointOwner target, int damage, AbstractTower damageDealer) => target.RecieveDamage(damage * _critDamage, damageDealer);
    }
}