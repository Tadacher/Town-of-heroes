using System.Collections;
using UnityEngine;
using Utilities;

public abstract  class AbstractTower : MonoBehaviour, IExpReciever, ICommonAttacker
{ 
    public virtual float Experience { get; protected private set; }
    public virtual int Level { get; protected private set; }
    public virtual int Hp { get; protected private set; }

    protected const string _targetTag = "Enemy";

    [SerializeField] protected TowerStats _towerStats;
    [SerializeField] protected AudioSource _audioSource;

    protected int _attackDamage;
    protected float _attackRange;
    protected float _attackDelay;
    protected float _currentTimeTillAttack;

   
    protected AbstractEnemy _currentEnemy;
    protected AbstractProjectileFactory _projectileFactory;
    protected AbstractTowerAttackModule _attackModule;
    protected Collider2D[] _availableEnemies;
    
    
   
    protected abstract void Attack();
    protected abstract void InitializeProjectileFactory(ProjectileBehaviour projectileBehaviour);

    public virtual AbstractEnemy FindClosestTarget()
    {
        _availableEnemies = Physics2D.OverlapCircleAll(transform.position, _towerStats.AttackRange);

        if (_availableEnemies.Length == 0)
            return null;

        float maxdistance = Mathf.Infinity;
        Collider2D closestTarget = null;

        foreach (Collider2D target in _availableEnemies)
        {
            float distance = Mat.DistanceBetweenPointsV3(transform.position, target.transform.position);
            if (distance < maxdistance)
            {
                maxdistance = distance;
                closestTarget = target;
            }
        }
        return closestTarget.GetComponent<AbstractEnemy>();
    }
    public virtual void RecieveExperience(float exp)
    {
        Experience += exp;
        TryLevelup();
    }

   
    
    protected virtual void TryLevelup()
    {
        while (Experience >= _towerStats.ExpPerLevel)
        {
            Experience -= _towerStats.ExpPerLevel;
            Levelup();
        }
    }
    protected virtual void Levelup()
    {
        Level++;
        _audioSource.PlayOneShot(_towerStats.LevelupClip);
        _attackDamage += _towerStats.AttackDamagePerLevel;
        _attackRange += _towerStats.AttackRangePerLevel;
        _attackDelay += _towerStats.AttackDelayPerLevel;
    }

    protected virtual void TryDealDamageToCurrentEnemy()
    {
        if (Mat.DistanceBetweenPointsV3(transform.position, _currentEnemy.transform.position) <= _attackRange)
        {
            _attackModule.DealDamage(_currentEnemy, _attackDamage, this);
            PlayAttackSound();
            RefreshAttackDelay();
        }
        else
            _currentEnemy = null;
    }



    protected void InitializeBase()
    {
        _availableEnemies = new Collider2D[20];
        _attackDamage = _towerStats.AttackDamage;
        _attackRange = _towerStats.AttackRange;
        _attackDelay = _towerStats.AttackDelay;
    }

    protected void RefreshAttackDelay() => _currentTimeTillAttack = _attackDelay;
    protected void PlayAttackSound() => _audioSource.PlayOneShot(_towerStats.AttackClip);
    protected void InitializeAttackModule<TAttackModule>() where TAttackModule : AbstractTowerAttackModule, new() => _attackModule = new TAttackModule();
   
}
