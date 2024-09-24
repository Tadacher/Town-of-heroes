using Towers;
using UnityEngine;

namespace Core.Towers
{
    public class SniperNest : AbstractTower
    {
        [SerializeField] private float _critChance;
        [SerializeField] private float _critDamage;
        protected override void Awake()
        {
            InitializeAttackModule();

            InitializeProjectileFactory(_towerStats.ProjectilePrefab);
            RefreshAttackDelay();
        }
        protected override void Update()
        {
            base.Update();

            if (_isGhost)
                return;
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

        protected override void InitializeAttackModule() => _attackModule = new CritDamageAttackModule(_critChance, _critDamage);
    }
}