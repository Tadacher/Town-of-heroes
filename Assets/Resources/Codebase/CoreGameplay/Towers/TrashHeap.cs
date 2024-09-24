namespace Core.Towers
{
    public class TrashHeap : AbstractTower
    {
        protected override void Attack()
        {
            _projectileFactory.ReturnProjectile(_towerStats.ProjectileSpeed, _currentEnemy.transform, transform.position, () => { TryDealDamageToCurrentEnemy(); });
            PlayAttackSound();
            RefreshAttackDelay();
        }

        protected override void InitializeAttackModule()
        {
            _attackModule = new SimpleTowerAttackModule();
        }
    }

}