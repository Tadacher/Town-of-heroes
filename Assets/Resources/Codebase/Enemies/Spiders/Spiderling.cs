using MovementModules;
using Services;
using UnityEngine;

namespace Enemies
{
    public class Spiderling : AbstractEnemy
    {
        public override void Construct(AudioSource audioSource, DamageTextService damageTextService, MonsterInfoServiceIngame monsterInfoServiceIngame, IEnemyReachedReciever coreGameplayService)
        {
            base.Construct(audioSource, damageTextService, monsterInfoServiceIngame, coreGameplayService);
            
            _abstractDamageRecievingModule = new DefaultHealthModule(transform, damageTextService);
        }

        protected override void Awake()
        {
            _enemyMovementModule = new StraightMovementModule(transform, new Vector3(16.5f, 7.5f, 0f), this, _speed);
            _enemyMovementModule.OnEnemyReached += OnReachedTargetHandler;
        }
        protected override void Die()
        {
            _enemyMovementModule.StopMovementCoroutine(this);
            PlayDeathSound();
            _deathListener.RecieveDeath();
            ReturnToPool();
        }
    }
}