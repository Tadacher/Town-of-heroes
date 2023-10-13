using Services;
using UnityEngine;

public class EnemyFactory<TEnemy> : AbstractPoolerFactory<TEnemy> where TEnemy : AbstractEnemy
{
    private readonly AudioSource _audioSource;
    private readonly DamageTextService _damageTextService;
    protected TEnemy _enemyPrefab;
    protected GameObject _prototype;

    public EnemyFactory(AudioSource audioSource, DamageTextService damageTextService, TEnemy enemyPrefab) : base()
    {
        _audioSource = audioSource;
        _damageTextService = damageTextService;
        _enemyPrefab = enemyPrefab;
    }

    public override TEnemy GetObject() =>
        _pool.Get();

    public override void ReturnToPool(IPoolableObject poolable) => 
        _pool.Release((TEnemy)poolable);

    protected override void ActionOnDestroy(TEnemy poolable) => 
        GameObject.Destroy(poolable.gameObject);

    protected override void ActionOnGet(TEnemy poolable) => 
        poolable.gameObject.SetActive(true);

    protected override void ActionOnRelease(TEnemy poolable) => 
        poolable.gameObject.SetActive(false);

    protected override TEnemy CreateNew()
    {
        TEnemy enemy = GameObject.Instantiate(_enemyPrefab, null);
        enemy.Initialize(_audioSource, _damageTextService, this);
        return enemy;
    }
}
