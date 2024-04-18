using Core.Towers;
using System;

public interface IHitpointOwner
{
    public void RecieveDamage(float damage, AbstractTower damageDealer);
    public void Heal(float points);

}
