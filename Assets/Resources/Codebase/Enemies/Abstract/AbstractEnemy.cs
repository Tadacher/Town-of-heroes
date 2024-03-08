using Core.Towers;
using MovementModules;
using Services;
using System;
using UnityEngine;
/// <summary>
/// base class for all enemies
/// </summary>
public abstract class AbstractEnemy : MonoBehaviour, IHitpointOwner, IMowementModuleOwner, IPoolableObject
{
    public string EnemyName;
    public EnemyStats Stats => _stats;

    [SerializeField]
    protected EnemyStats _stats;

    protected int _hitpoints;
    protected float _speed;
    protected float _expForKill;
    protected int _maxHitpoints;
    protected int _damage;


    protected IObjectPooler _pooler;
    //Audio
    protected AudioClip _deathClip;
    protected AudioSource _audioSource;
    //Modules
    protected AbstractMovementModule _enemyMovementModule;
    protected AbstractDamageRecievingModule _abstractDamageRecievingModule;

    //other
    protected IEnemyReachedReciever _coreGameplayService;
    protected IMobDeathListener _deathListener;
    protected DamageTextService _damageTextService;

    public virtual void Initialize(AudioSource audioSource,
                                   DamageTextService damageTextService,
                                   IEnemyReachedReciever coreGameplayService,
                                   IObjectPooler objectPooler)
    {
        _pooler = objectPooler;
        _audioSource = audioSource;
        _damageTextService = damageTextService;
        _coreGameplayService = coreGameplayService;

        InitializeStats();      
    }


    public virtual void Heal(int points)
    {
        _hitpoints += points;
        if (_hitpoints > _maxHitpoints)
            _hitpoints = _maxHitpoints;
    }

    public virtual void RecieveDamage(int damage, AbstractTower abstractTower)
    {
        
        if (_hitpoints < 0)
            return;

        _hitpoints -= _abstractDamageRecievingModule.CalculateRecievedDamage(damage);
        
        if (_hitpoints <= 0)
        {
            abstractTower.RecieveExperience(_expForKill);
            Die();
        }
    }
    public virtual AbstractMovementModule MovementModule() => _enemyMovementModule;

    public virtual void ReInitialize(Vector3 position)
    {
        transform.position = position;
        _hitpoints = _maxHitpoints;
        _enemyMovementModule.StartMovementCoroutine(this);
    }
    protected virtual void PlayDeathSound() => _audioSource.PlayOneShot(_deathClip);
    protected virtual void Die()
    {
        _enemyMovementModule.StopMovementCoroutine(this);
        PlayDeathSound();
        _deathListener.RecieveDeath();
        ReturnToPool();
    }

    protected void ReturnToPool() => gameObject.SetActive(false);

    private void InitializeStats()
    {
        EnemyName = _stats.Name;
        _maxHitpoints = _stats.HitPoints;
        _hitpoints = _maxHitpoints;
        _damage = _stats.Damage;
        _speed = _stats.Speed;
        _deathClip = _stats.DeathSound;
        _expForKill = _stats.ExpPerKill;
    }

    void IPoolableObject.ReturnToPool() => 
        _pooler.ReturnToPool(this);

    public AbstractEnemy WithWaveDeathListener(IMobDeathListener waveDeathListener)
    {
        _deathListener = waveDeathListener;
        return this;
    }
}


   