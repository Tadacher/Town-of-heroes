using Core.Towers;
using Towers;

public class SimpleTowerAttackModule : AbstractTowerAttackModule
{
    public override void DealDamage(IHitpointOwner target, int damage, AbstractTower damageDealer) => target.RecieveDamage(damage, damageDealer);
}