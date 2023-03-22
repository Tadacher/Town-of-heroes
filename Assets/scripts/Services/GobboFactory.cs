using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GobboFactory : AbstractEnemyFactory
{
    System.Type[] behaviourVariants = new System.Type[]
    {
        typeof(Gobbo)
    };





    internal override void Init()
    {
       
    }
    private void Awake()
    {
        Construct(transform);
        Debug.Log("gobbo is ready");
    }
    internal override GameObject Construct(Transform spawnpos)
    {
        
        constructionField = Instantiate(prototype, spawnpos, true);
        constructionField.AddComponent(GetRandomBehaviour());
        return constructionField;
    }
    protected System.Type GetRandomBehaviour()
    {
        return behaviourVariants[Random.Range((int)0, behaviourVariants.Length - 1)];
    }

    
}
