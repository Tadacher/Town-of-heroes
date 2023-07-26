using MovementModule;
using Services;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public abstract class AbstractEnemy : MonoBehaviour, IHitpointOwner, IMowementModuleOwner
{
    public string EnemyName;

    [SerializeField]
    protected EnemyStats _stats;

    protected int _hitpoints;
    protected float _speed;
    protected float _expForKill;
    protected AudioClip _deathClip;

    protected AudioSource _audioSource;

    protected int _maxHitpoints;
    protected int _damage;
   
    protected private AbstractMovementModule _enemyMovementModule;
    protected DamageTextService _damageTextService;

    public abstract void Heal(int points);

    public virtual void RecieveDamage(int damage, AbstractTower abstractTower)
    {
        _damageTextService.ReturnDamageText(damage, transform.position);
        if (_hitpoints < 0)
            return;

        _hitpoints -= damage;
        
        if (_hitpoints <= 0)
        {
            abstractTower.RecieveExperience(_expForKill);
            Die();
        }
    }
    public virtual AbstractMovementModule MovementModule() => _enemyMovementModule;


    public virtual void Initialize(AudioSource audioSource, Services.DamageTextService damageTextService)
    {
        _audioSource = audioSource;
        _damageTextService = damageTextService;
        InitializeStats();

        _enemyMovementModule = new StraightMovementModule(transform, new Vector3(-1.5f, -11.5f, 0f), this, _speed);
    }

   

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


   