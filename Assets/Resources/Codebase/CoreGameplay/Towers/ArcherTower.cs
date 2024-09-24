namespace Core.Towers
{
    public class ArcherTower : AbstractTower
    {
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

        protected override void InitializeAttackModule() => _attackModule = new SimpleTowerAttackModule();
    }

}