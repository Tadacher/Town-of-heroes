namespace Core.Towers
{
    public class LightingSpire : AbstractTower
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
            TryDealDamageToCurrentEnemy();
            DrawAttackRay();
            PlayAttackSound();
            RefreshAttackDelay();
        }

        private void DrawAttackRay()
        {
            
        }

        protected override void InitializeAttackModule() => _attackModule = new SimpleTowerAttackModule();
    }

}