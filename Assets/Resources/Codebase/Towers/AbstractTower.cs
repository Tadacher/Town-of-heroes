using Services.Input;
using Towers;
using UnityEngine;

namespace Core.Towers
{
    public abstract class AbstractTower : MonoBehaviour, IExpReciever, ICommonAttacker, IPoolableObject
    {
        public virtual float Experience { get; protected private set; }
        public virtual int Level { get; protected private set; }
        public virtual int Hp { get; protected private set; }

        protected const string _targetTag = "Enemy";
        //external
        [SerializeField] protected TowerStats _towerStats;
        [SerializeField] protected AudioSource _audioSource;
        [SerializeField] private SpriteRenderer _spriteRenderer;
        [SerializeField] private PointerFollower _pointerFollower;
        //

        //stats
        protected int _attackDamage;
        protected float _attackRange;
        protected float _attackDelay;
        protected float _currentTimeTillAttack;
        protected bool _isGhost;
        //

        //internal
        protected AbstractEnemy _currentEnemy;
        protected AbstractProjectileFactory _projectileFactory;
        protected AbstractTowerAttackModule _attackModule;
        protected Collider2D[] _availableEnemies;
        //

        //dependencies
        protected IObjectPooler _objectPooler;

        public AbstractTower AsGhost()
        {
            
            MakeGhost();
            return this;
        }
        protected virtual void Initialize(IObjectPooler objectPooler, AbstractInputService abstractInputService)
        {
            _pointerFollower.Initialize(abstractInputService);
            _availableEnemies = new Collider2D[20];
            _attackDamage = _towerStats.AttackDamage;
            _attackRange = _towerStats.AttackRange;
            _attackDelay = _towerStats.AttackDelay;
        }

        protected virtual void Update()
        {
            if (_isGhost)
                return;
        }

        public virtual AbstractEnemy FindClosestTarget()
        {
            _availableEnemies = Physics2D.OverlapCircleAll(transform.position, _towerStats.AttackRange);

            if (_availableEnemies.Length == 0)
                return null;

            float maxdistance = Mathf.Infinity;
            Collider2D closestTarget = null;

            foreach (Collider2D target in _availableEnemies)
            {
                float distance = Vector3.Distance(transform.position, target.transform.position);
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
        public void ReturnToPool() =>
            _objectPooler.ReturnToPool(this);

        public void StartFollowPointer() =>
            _pointerFollower.enabled = true;

        public void StopFollowingPointer() =>
            _pointerFollower.enabled = true;

        protected abstract void Attack();
        protected abstract void InitializeProjectileFactory(ProjectileBehaviour projectileBehaviour);

        protected virtual void TryLevelup()
        {
            if (Experience >= _towerStats.ExpPerLevel)
                _audioSource.PlayOneShot(_towerStats.LevelupClip);
            while (Experience >= _towerStats.ExpPerLevel)
            {
                Experience -= _towerStats.ExpPerLevel;
                Levelup();
            }
        }
        protected virtual void Levelup()
        {
            Level++;
            _attackDamage += _towerStats.AttackDamagePerLevel;
            _attackRange += _towerStats.AttackRangePerLevel;
            _attackDelay -= _towerStats.AttackDelayPerLevel;
        }

        protected virtual void TryDealDamageToCurrentEnemy()
        {
            if (Vector3.Distance(transform.position, _currentEnemy.transform.position) <= _attackRange)
            {
                _attackModule.DealDamage(_currentEnemy, _attackDamage, this);
            }
            else
                _currentEnemy = null;
        }




        protected void RefreshAttackDelay() => _currentTimeTillAttack = _attackDelay;
        protected void PlayAttackSound() => _audioSource.PlayOneShot(_towerStats.AttackClip);
        protected void InitializeAttackModule<TAttackModule>() where TAttackModule : AbstractTowerAttackModule, new() => _attackModule = new TAttackModule();

        protected bool TargetInRange() => Vector2.Distance(transform.position, _currentEnemy.transform.position) < _attackRange;

        protected void CountAttackDelay()
        {
            if (_currentTimeTillAttack >= 0)
                _currentTimeTillAttack -= Time.deltaTime;
        }

        protected void TryToAttack()
        {
            if (_currentEnemy != null && _currentEnemy.gameObject.activeSelf && TargetInRange() && _currentTimeTillAttack <= 0)
                Attack();
        }

        protected void FinClosestTargetIfNeeded()
        {
            if (_currentEnemy == null)
                _currentEnemy = FindClosestTarget();
            else if (!TargetInRange())
                _currentEnemy = FindClosestTarget();
        }

        private void MakeGhost()
        {
            _isGhost = true;
            _spriteRenderer.color -= new Color(0, 0, 0, 0.4f);
        }
    }
}