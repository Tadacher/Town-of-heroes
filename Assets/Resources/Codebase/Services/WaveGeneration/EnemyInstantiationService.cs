using Services;
using System;
using System.Collections.Generic;
using UnityEngine;

public class EnemyInstantiationService
{
    private Dictionary<Type, EnemyFactory> _factoryList;
    public EnemyFactory _goblinFactory;
    public EnemyFactory _goblinTrapperFactory;


    public EnemyInstantiationService(EnemyPrefabContainer enemyContainer, AudioSource audioSource, DamageTextService damageTextService)
    {
        _factoryList = new();
        //Goblin
        _factoryList.Add(typeof(Gobbo), new(enemyContainer.Goblin, audioSource, damageTextService));

        //GoblinTrapper
        _factoryList.Add(typeof(GobboTrapper), new(enemyContainer.GoblinTrapper, audioSource, damageTextService));
    }

    public void ReturnObject(Type type, Transform spawnpos)
    {
        _factoryList[type]?.ReturnObject(spawnpos);
    }
}
