using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public abstract class AbstractEnemy : MonoBehaviour, IHitpoint
{
    [SerializeField]
    protected int hitpoints;
    [SerializeField]
    protected int blockAmmount;

    public string enemyName;
    public IHitpoint.RecieveDamage recieveDamage;
    protected abstract int BlockDamage(int blockAmmount, int damage);
    public abstract void Heal(int points);
}
