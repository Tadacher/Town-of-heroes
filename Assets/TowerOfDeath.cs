using UnityEngine;

namespace Core.Towers
{
    public class TowerOfDeath : AbstractTower
    {
        protected override void Update()
        {
            base.Update();

            if (_isGhost)
                return;

            Debug.Log(_currentEnemy);
            FinClosestTargetIfNeeded();
            CountAttackDelay();
            TryToAttack();
        }
        protected override void Attack()
        {
            _projectileFactory.ReturnProjectile(_towerStats.ProjectileSpeed, _currentEnemy.transform, transform.position, () => { TryDealDamageToCurrentEnemy(); });
            PlayAttackSound();
            RefreshAttackDelay();
        }
    }
}