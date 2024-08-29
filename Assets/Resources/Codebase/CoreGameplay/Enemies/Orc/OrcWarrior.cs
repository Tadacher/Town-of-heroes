using MovementModules;
using Services;
using UnityEngine;

namespace Enemies
{
    public class OrcWarrior : Orc
    {
        [SerializeField] private int _blockValue;
        public override void Construct(AudioSource audioSource,
                                       DamageTextService damageTextService,
                                       MonsterInfoServiceIngame monsterInfoServiceIngame,
                                       IEnemyReachedReciever enemyReachedReciever,
                                       IWaveNumberProvider waveNumberProvider)
        {
            base.Construct(audioSource, damageTextService, monsterInfoServiceIngame, enemyReachedReciever, waveNumberProvider);
            _abstractDamageRecievingModule = new BlockHealthModule(transform, damageTextService, _blockValue);
        }
    }
}