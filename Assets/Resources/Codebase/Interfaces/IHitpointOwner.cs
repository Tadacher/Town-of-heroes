using Core.Towers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IHitpointOwner
{
    public void RecieveDamage(int damage, AbstractTower damageDealer);
    public void Heal(int points);

}
