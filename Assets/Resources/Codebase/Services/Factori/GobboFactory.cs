using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GobboFactory : AbstractEnemyFactory
{
    private const string _basicGobboKey = "Basic Gobbo";
    private readonly AudioSource _audioSource;
    System.Type[] behaviourVariants = new System.Type[]
    {
        typeof(Gobbo)
    };

    public GobboFactory(EnemyContainer enemyContainer, AudioSource audioSource)
    {
        _enemyContainer = enemyContainer;
        _audioSource = audioSource;
    }

    public override GameObject Construct(Transform spawnpos)
    {
       
        _constructionField = GameObject.Instantiate(_enemyContainer.GetEnemy(_basicGobboKey), spawnpos.position, Quaternion.identity);
        _constructionField.GetComponent<Gobbo>().Inject(_audioSource);
        //_constructionField.AddComponent(GetRandomBehaviour());
        return _constructionField;
    }
    protected System.Type GetRandomBehaviour() => behaviourVariants[Random.Range((int)0, behaviourVariants.Length - 1)];


}
