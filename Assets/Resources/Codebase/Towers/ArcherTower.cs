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
        if(_currentEnemy == null)
           _currentEnemy = FindClosestTarget();

        if (_currentTimeTillAttack >= 0)
            _currentTimeTillAttack -= Time.deltaTime;

        else if (_currentEnemy != null)
            Attack();                
    }

    protected override void Attack()
    {
        _projectileFactory.ReturnProjectile(_towerStats.ProjectileSpeed, _currentEnemy.transform, transform.position, () => { TryDealDamageToCurrentEnemy(); });     
    }

    protected override void InitializeProjectileFactory(ProjectileBehaviour projectileBehaviour) => _projectileFactory = new ArrowProjectileFactory(projectileBehaviour);
}
