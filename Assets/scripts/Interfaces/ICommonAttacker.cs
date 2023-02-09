using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ICommonAttacker
{
    protected delegate void Attack(AbstractEnemy abstractEnemy, int damage);
    
    public AbstractEnemy FindTarget();
}
