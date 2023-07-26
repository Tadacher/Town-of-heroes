using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbstractEnemyFactory
{
    protected EnemyContainer _enemyContainer;
    protected GameObject _prototype;
    protected List<AbstractEnemy> _objectPoolList;
    public abstract AbstractEnemy ReturnObject(Transform spawnpos);
    
    protected virtual bool TryGetObjectFromList(out AbstractEnemy result)
    {
        if (_objectPoolList.Count > 0)
        {
            foreach (AbstractEnemy abstractEnemy in _objectPoolList)
            {
                if (!abstractEnemy.gameObject.activeSelf)
                {
                    abstractEnemy.gameObject.SetActive(true);
                    result = abstractEnemy;
                    return true;
                }
            }
        }
        result = null;
        return false;
    }
}
