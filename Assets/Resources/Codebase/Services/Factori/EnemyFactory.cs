using Services;
using UnityEngine;

public class EnemyFactory : AbstractPoolerFactory<AbstractEnemy> 
{
    private readonly AudioSource _audioSource;
    private readonly DamageTextService _damageTextService;
    protected AbstractEnemy _enemyPrefab;
    protected GameObject _prototype;

    public EnemyFactory(AudioSource audioSource, DamageTextService damageTextService, AbstractEnemy enemyPrefab) : base()
    {
        _audioSource = audioSource;
        _damageTextService = damageTextService;
        _enemyPrefab = enemyPrefab;
    }

    public override AbstractEnemy GetObject() =>
        _pool.Get();

    public override void ReturnToPool(IPoolableObject poolable) => 
        _pool.Release((AbstractEnemy)poolable);

    protected override void ActionOnDestroy(AbstractEnemy poolable) => 
        GameObject.Destroy(poolable.gameObject);

    protected override void ActionOnGet(AbstractEnemy poolable) => 
        poolable.gameObject.SetActive(true);

    protected override void ActionOnRelease(AbstractEnemy poolable) => 
        poolable.gameObject.SetActive(false);

    protected override AbstractEnemy CreateNew()
    {
        AbstractEnemy enemy = GameObject.Instantiate(_enemyPrefab, null);
        enemy.Initialize(_audioSource, _damageTextService, this);
        return enemy;
    }
}
