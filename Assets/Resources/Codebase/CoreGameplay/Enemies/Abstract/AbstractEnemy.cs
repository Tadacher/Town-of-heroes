using Core.Towers;
using MovementModules;
using Services;
using System;
using UnityEngine;
using UnityEngine.EventSystems;
using Zenject;
/// <summary>
/// base class for all enemies
/// </summary>
public abstract class AbstractEnemy : MonoBehaviour, IHitpointOwner, IMowementModuleOwner, IPoolableObject, IHitpointInfoProvider, IPointerDownHandler
{
    public string EnemyName;
    public EnemyStats Stats => _stats;

    float IHitpointInfoProvider.CurrentHealth => Hitpoints;
    float IHitpointInfoProvider.MaxHealth => Stats.HitPoints;

    protected float Hitpoints 
    { 
        get => _hitpoints; 
        set
        {
            _hitpoints = value;
            OnHealthChanged?.Invoke();
        }
    }

    [SerializeField]
    protected EnemyStats _stats;

    private float _hitpoints;
    protected float _speed;
    protected float _expForKill;
    protected float _maxHitpoints;
    protected int _damage;


    protected IObjectPooler _pooler;
    protected bool _pooled = false;
    //Audio
    protected AudioClip _deathClip;
    protected AudioSource _audioSource;
    //Modules
    protected AbstractMovementModule _enemyMovementModule;
    protected AbstractDamageRecievingModule _abstractDamageRecievingModule;

    //other
    protected IEnemyReachedReciever _coreGameplayService;
    protected IMobDeathListener _deathListener;
    protected IWaveNumberProvider _waveNumberProvider;
    protected DamageTextService _damageTextService;

    //linked ui interactions
    private MonsterInfoServiceIngame _monsterInfoService;

    public event Action OnHealthChanged;
    
    [Inject]
    public virtual void Construct(AudioSource audioSource,
                                   DamageTextService damageTextService,
                                   MonsterInfoServiceIngame monsterInfoServiceIngame,
                                   IEnemyReachedReciever coreGameplayService,
                                   IWaveNumberProvider waveNumberProvider)
    {
        _audioSource = audioSource;
        _damageTextService = damageTextService;
        _coreGameplayService = coreGameplayService;
        _monsterInfoService = monsterInfoServiceIngame;
        _waveNumberProvider = waveNumberProvider;
        InitializeStats();      
    }
    public virtual void Init(IObjectPooler objectPooler)
    {
        _pooler = objectPooler;
    }
    public void OnPointerDown(PointerEventData eventData)
    {
        _monsterInfoService.Show(this, this);
    }

    public void ActionOnGet()
    {
        _pooled = false;
    }

    protected abstract void Awake();
    public virtual void Heal(float points)
    {
        Hitpoints += points;
        if (Hitpoints > _maxHitpoints)
            Hitpoints = _maxHitpoints;
    }

    public virtual void RecieveDamage(float damage, AbstractTower abstractTower)
    {
        if (Hitpoints < 0)
            return;

        Hitpoints -= _abstractDamageRecievingModule.CalculateRecievedDamage(damage);
        
        if (Hitpoints <= 0)
        {
            abstractTower.RecieveExperience(_expForKill);
            Die();
        }
    }
    public virtual AbstractMovementModule MovementModule() => _enemyMovementModule;

    public virtual void ReInitialize(Vector3 position)
    {
        InitializeStats();
        transform.position = position;
        Hitpoints = _maxHitpoints;
        _enemyMovementModule.SetSpeed(_speed);
        _enemyMovementModule.StartMovementCoroutine(this);
        _abstractDamageRecievingModule.ReInit();
    }
    protected virtual void PlayDeathSound() => _audioSource.PlayOneShot(_deathClip);
    protected virtual void Die()
    {
        _enemyMovementModule.StopMovementCoroutine(this);
        PlayDeathSound();
        _deathListener.RecieveDeath();
        ReturnToPool();
    }

    protected void ReturnToPool()
    {
        if (_pooled)
            return;

        _pooled = true;
        gameObject.SetActive(false);
        _pooler.ReturnToPool(this);
    }

    private void InitializeStats()
    {
        EnemyName = _stats.Name;
        SetMaxHp();
        Hitpoints = _maxHitpoints;
        SetDamage();
        SetSpeed();

        _deathClip = _stats.DeathSound;
        _expForKill = _stats.ExpPerKill;
    }

    private void SetSpeed() => _speed = _stats.Speed + _stats.SpeedPerLevel * _waveNumberProvider.WaveNumber;
    private void SetDamage() => _damage = _stats.Damage + _stats.DamagePerLevel * _waveNumberProvider.WaveNumber;
    private void SetMaxHp() => _maxHitpoints = _stats.HitPoints + _stats.HitPointsPerLevel * _waveNumberProvider.WaveNumber;

    void IPoolableObject.ReturnToPool()
    {
        gameObject.SetActive(false);
        _pooler.ReturnToPool(this);
    }

    public AbstractEnemy WithWaveDeathListener(IMobDeathListener waveDeathListener)
    {
        _deathListener = waveDeathListener;
        return this;
    }

    protected void OnReachedTargetHandler()
    {
        _coreGameplayService.RecieveEnemyReached(_damage);
        ReturnToPool();
    }

    
}