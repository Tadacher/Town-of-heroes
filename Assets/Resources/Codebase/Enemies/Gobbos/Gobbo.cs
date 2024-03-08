using MovementModules;
using Services;
using UnityEngine;

namespace Enemies
{
    public class Gobbo : AbstractEnemy
    {
        public override void Initialize(AudioSource audioSource, DamageTextService damageTextService, IEnemyReachedReciever coreGameplayService, IObjectPooler objectPooler)
        {
            base.Initialize(audioSource, damageTextService, coreGameplayService, objectPooler);
            _abstractDamageRecievingModule = new DefaultHealthModule(transform, damageTextService);
            _enemyMovementModule = new StraightMovementModule(transform, new Vector3(16.5f, 7.5f, 0f), this, _speed);

            _enemyMovementModule.OnEnemyReached += OnReachedTarget;
        }

        private void OnReachedTarget()
        {
            _coreGameplayService.RecieveEnemyReached(_damage);
            ReturnToPool();
        }
    }
}