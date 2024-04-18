using MovementModules;
using Services;
using UnityEngine;
using Zenject;

namespace Enemies
{
    public class TreeSpider : AbstractEnemy
    {
        [SerializeField] private int SpawnCount; 

        private EnemyInstantiationService _enemyInstantiationService;
        [Inject]
        public override void Construct(AudioSource audioSource,
                                        DamageTextService damageTextService,
                                        MonsterInfoServiceIngame monsterInfoServiceIngame,
                                        IEnemyReachedReciever coreGameplayService)
        {
            base.Construct(audioSource, damageTextService, monsterInfoServiceIngame, coreGameplayService);

            _abstractDamageRecievingModule = new DefaultHealthModule(transform, damageTextService);
        }

        [Inject]
        public void InjectDependencies(EnemyInstantiationService enemyInstantiationService)
        {
            _enemyInstantiationService = enemyInstantiationService;
        }

        protected override void Awake()
        {
            _enemyMovementModule = new StraightMovementModule(transform, new Vector3(16.5f, 7.5f, 0f), this, _speed);
            _enemyMovementModule.OnEnemyReached += OnReachedTargetHandler;
        }

        protected override void Die()
        {
            base.Die();
            for (int i = 0; i < SpawnCount; i++)
                _enemyInstantiationService.ReturnObject(typeof(Spiderling), transform.position);
        }
    }
}