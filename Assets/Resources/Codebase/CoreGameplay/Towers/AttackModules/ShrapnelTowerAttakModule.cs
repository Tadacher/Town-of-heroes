using Core.Towers;

namespace Towers
{
    public class ShrapnelTowerAttakModule : AbstractTowerAttackModule
    {
        private int _shrapnelCount;

        public int ShrapnelCount { get => _shrapnelCount; set => _shrapnelCount = value; }

        public override void DealDamage(IHitpointOwner target, int damage, AbstractTower damageDealer)
        {
            for (int i = 0; i < ShrapnelCount; i++)
            {
                target.RecieveDamage(damage, damageDealer);
            }
        }
    }
}