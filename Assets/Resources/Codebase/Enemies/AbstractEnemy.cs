using MovementModule;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public abstract class AbstractEnemy : MonoBehaviour, IHitpointOwner, IMowementModuleOwner
{
    public string EnemyName;

    [SerializeField]
    protected int _hitpoints;
    [SerializeField]
    protected float _speed;
    [SerializeField]
    protected float expForKill;
    [SerializeField]
    protected AudioClip _deathClip;
    [SerializeField]
    protected AudioSource _audioSource;

    protected int _maxHitpoints;
    protected int _damage;
    protected private AbstractMovementModule _enemyMovementModule;


    public abstract void Heal(int points);

    public virtual void RecieveDamage(int damage, AbstractTower abstractTower)
    {
        _hitpoints -= damage;
        if (_hitpoints <= 0)
        {
            abstractTower.RecieveExperience(expForKill);
            Die();
        }
    }
    public virtual AbstractMovementModule MovementModule() => _enemyMovementModule;


    protected virtual void Initialize()
    {
        InitializeBase(10, "Gobbo");
    }
    protected virtual void PlayDeathSound() => _audioSource.PlayOneShot(_deathClip);
    protected virtual void Die()
    {
        PlayDeathSound();
        Destroy(gameObject);
    }
    protected void InitializeBase(int maxHp, string name)
    {
        EnemyName = name;
        _maxHitpoints = maxHp;
        _hitpoints = maxHp;
        _damage = 1;
        _enemyMovementModule = new StraightMovementModule(transform, new Vector3(-1.5f, -11.5f, 0f), this, _speed);
    }
}


   