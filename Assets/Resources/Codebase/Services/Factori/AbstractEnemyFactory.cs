using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbstractEnemyFactory
{
    protected EnemyContainer _enemyContainer;
    protected GameObject _prototype;
    protected GameObject _constructionField;

    public abstract GameObject Construct(Transform spawnpos);
}
