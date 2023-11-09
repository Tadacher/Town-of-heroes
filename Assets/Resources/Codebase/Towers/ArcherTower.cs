
using UnityEngine;

namespace Core.Towers
{
    public class ArcherTower : AbstractTower
    {
        private void Awake()
        {
            InitializeAttackModule<SimpleTowerAttackModule>();
            InitializeProjectileFactory(_towerStats.ProjectilePrefab);
            RefreshAttackDelay();
        }
        protected override void Update()
        {
            base.Update();
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

        protected override void InitializeProjectileFactory(ProjectileBehaviour projectilePrefab) => 
            _projectileFactory = new SimpleProjectileFactory(projectilePrefab);
    }
}