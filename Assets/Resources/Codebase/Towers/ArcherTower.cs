using UnityEngine;
public class ArcherTower : AbstractTower
{
    private void Awake()
    {
        InitializeBase();
        InitializeAttackModule<SimpleTowerAttackModule>();
        InitializeProjectileFactory(_towerStats.ProjectilePrefab);
        RefreshAttackDelay();
    }
    private void Update()
    {
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

    protected override void InitializeProjectileFactory(ProjectileBehaviour projectilePrefab) => _projectileFactory = new SimpleProjectileFactory(projectilePrefab);
}
