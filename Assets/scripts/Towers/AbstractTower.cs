using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbstractTower : MonoBehaviour, ILevelable, ICommonAttacker
{
    [SerializeField]
    protected int attackDamage;
    protected float attackRange;
    [SerializeField]
    protected float attackDelay, currentTimeTillAttack;
    [SerializeField]
    protected string targetTag;
    protected ICommonAttacker.Attack currentAttack;

    public virtual int Experience { get; protected set; }
    public virtual int Level { get; protected set; }
    public virtual int Hp { get; protected set; }

    protected CircleCollider2D circleCollider;
    protected List<GameObject> availableTargets;

    

    public abstract AbstractEnemy FindTarget();
    public abstract void RecieveExperience(int exp);
}
