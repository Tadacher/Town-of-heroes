using MovementModules;
using Services;
using UnityEngine;
using Zenject;

namespace Enemies
{
    public abstract class AbstractMurloc : AbstractEnemy
    {
        [SerializeField]  protected int _murlocBlockAmmount = 5;

        [Inject]
        public override void Construct(AudioSource audioSource,
                                       DamageTextService damageTextService,
                                       MonsterInfoServiceIngame monsterInfoServiceIngame,
                                       IEnemyReachedReciever coreGameplayService,
                                       IWaveNumberProvider waveNumberProvider)
        {
            base.Construct(audioSource, damageTextService, monsterInfoServiceIngame, coreGameplayService, waveNumberProvider);

            _enemyMovementModule = new StraightMovementModule(transform, new Vector3(16.5f, 7.5f, 0f), this, _speed);
            _abstractDamageRecievingModule = new BlockHealthModule(transform, damageTextService, _murlocBlockAmmount); 
        }
    }
}