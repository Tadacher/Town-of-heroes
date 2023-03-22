using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbstractEnemyFactory : MonoBehaviour
{

    [SerializeField] protected GameObject prototype;
    protected GameObject constructionField;

    internal abstract GameObject Construct(Transform spawnpos);
    internal abstract void Init();
}
