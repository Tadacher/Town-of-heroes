using MovementModules;
using Services;
using UnityEngine;
using Zenject;

namespace Enemies
{
    public class Zombie : AbstractEnemy
    {
        [SerializeField] private float _reviveChance;
        [SerializeField] private AudioClip _reviveClip;

        [Inject]
        public override void Construct(AudioSource audioSource,
                                       DamageTextService damageTextService,
                                       MonsterInfoServiceIngame monsterInfoServiceIngame,
                                       IEnemyReachedReciever coreGameplayService,
                                       IWaveNumberProvider waveNumberProvider)
        {
            base.Construct(audioSource,
                           damageTextService,
                           monsterInfoServiceIngame,
                           coreGameplayService,
                           waveNumberProvider);
            _abstractDamageRecievingModule = new DefaultHealthModule(transform, damageTextService, null);
        }
        protected override void Awake()
        {
            _enemyMovementModule = new StraightMovementModule(transform, new Vector3(16.5f, 7.5f, 0f), this, _speed);
            _enemyMovementModule.OnEnemyReached += OnReachedTargetHandler;
        }
        protected override void Die()
        {
            if (Random.Range(0f, 1f) > _reviveChance)
            {
                base.Die();
                return;
            }

            PlayReviveSound();
            StartReviveAnimCoroutine();
            Hitpoints = _maxHitpoints / 2;
        }

        private void StartReviveAnimCoroutine()
        {

        }

        private void PlayReviveSound() => _audioSource.PlayOneShot(_reviveClip);

        
    }
}