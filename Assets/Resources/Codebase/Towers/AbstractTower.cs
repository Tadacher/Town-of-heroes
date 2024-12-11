using Enemies;
using Services.GridSystem;
using Services.Input;
using Towers;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Core.Towers
{   
    /// <summary>
    /// General tower logic
    /// </summary>
    public abstract partial class AbstractTower : MonoBehaviour, IExpReciever, ICommonAttacker, IPoolableObject, IGridCellObject, IPointerDownHandler, ITowerInfoProvider
    {
        public virtual float Experience { get; protected private set; }
        public virtual int Hp { get; protected private set; }

        #region TowerInfoProvider
        public virtual int Level { get; protected private set; }
        float ITowerInfoProvider.Damage => _attackDamage;
        float ITowerInfoProvider.Range => _attackRange;
        float ITowerInfoProvider.Delay => _attackDelay;
        string ITowerInfoProvider.Name => GetType().Name;
        #endregion


        protected const string _targetTag = "Enemy";
        private const string EnemyLayerName = "Enemies";

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
        protected BattleGridService _gridAlignService;
        protected readonly Color _ghostColor = new Color(0, 0, 0, 0.4f);
        //

        //dependencies
        protected IObjectPooler _objectPooler;

        protected virtual void Awake()
        {
            InitializeAttackModule();
            InitializeProjectileFactory(_towerStats.ProjectilePrefab);
            RefreshAttackDelay();
        }
        public AbstractTower AsGhost()
        {  
            MakeGhost();
            return this;
        }
        public AbstractTower AsUnGhost()
        {
            MakeUnGhost();
            return this;
        }
        public virtual void Initialize(IObjectPooler objectPooler,
                                       AbstractInputService abstractInputService,
                                       BattleGridService gridAlignService,
                                       TowerInfoServiceIngame towerInfoService)
        {
            //AbstractTower.Infopanel
            _towerInfoService = towerInfoService;
            _abstractInputService = abstractInputService;
            //

            _objectPooler = objectPooler;
            _gridAlignService = gridAlignService;
            _pointerFollower.Initialize(abstractInputService, gridAlignService);
            _availableEnemies = new Collider2D[20];
            _attackDamage = _towerStats.AttackDamage;
            _attackRange = _towerStats.AttackRange;
            _attackDelay = _towerStats.AttackDelay;
        }

        protected virtual void Update()
        {
           
        }

        public virtual AbstractEnemy FindClosestTarget()
        {
            int layerMask = LayerMask.NameToLayer(EnemyLayerName);
            _availableEnemies = Physics2D.OverlapCircleAll(transform.position, _towerStats.AttackRange, 1<<layerMask);

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
            _pointerFollower.enabled = false;

        public void InsertSelfToGrid() => _gridAlignService.Insert(this, transform.position);

        protected abstract void Attack();
        protected virtual void InitializeProjectileFactory(ProjectileBehaviour projectilePrefab) =>
             _projectileFactory = new SimpleProjectileFactory(projectilePrefab);

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
            if (_currentEnemy == null)
                return;

            if (Vector3.Distance(transform.position, _currentEnemy.transform.position) <= _attackRange)
                _attackModule.DealDamage(_currentEnemy, _attackDamage, this);

            else
                _currentEnemy = null;
        }




        protected void RefreshAttackDelay() => _currentTimeTillAttack = _attackDelay;
        protected void PlayAttackSound() => _audioSource.PlayOneShot(_towerStats.AttackClip);
        protected abstract void InitializeAttackModule();

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
            if (_currentEnemy == null || _currentEnemy.gameObject.activeSelf == false)
                _currentEnemy = FindClosestTarget();
            else if (!TargetInRange())
                _currentEnemy = FindClosestTarget();
        }

        private void MakeGhost()
        {
            _isGhost = true;
            _spriteRenderer.color -= _ghostColor;
        }

        private void MakeUnGhost()
        {
            _isGhost = false;
            _spriteRenderer.color += _ghostColor;
        }

    }
}