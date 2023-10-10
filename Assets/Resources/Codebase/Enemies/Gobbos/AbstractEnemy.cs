using MovementModules;
using Services;
using UnityEngine;
public abstract class AbstractEnemy : MonoBehaviour, IHitpointOwner, IMowementModuleOwner
{
    public string EnemyName;

    [SerializeField]
    protected EnemyStats _stats;

    protected int _hitpoints;
    protected float _speed;
    protected float _expForKill;
    protected int _maxHitpoints;
    protected int _damage;



    //Audio
    protected AudioClip _deathClip;
    protected AudioSource _audioSource;
    //Modules
    protected AbstractMovementModule _enemyMovementModule;
    protected AbstractHealthModule _abstractHealthModule;

    protected DamageTextService _damageTextService;

    public virtual void Initialize(AudioSource audioSource, DamageTextService damageTextService)
    {
        _audioSource = audioSource;
        _damageTextService = damageTextService;
        InitializeStats();
        
    }


    public abstract void Heal(int points);

    public virtual void RecieveDamage(int damage, AbstractTower abstractTower)
    {
        
        if (_hitpoints < 0)
            return;

        _hitpoints -= _abstractHealthModule.RecieveDamage(damage);

        _damageTextService.ReturnDamageText(damage, transform.position);
        
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
        ReturnToPool();
    }

    private void ReturnToPool() => gameObject.SetActive(false);

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
}


   