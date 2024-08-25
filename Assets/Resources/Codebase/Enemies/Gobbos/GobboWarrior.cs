using MovementModules;
using Services;
using UnityEngine;

namespace Enemies
{
    public class GobboWarrior : Gobbo
    {
        [SerializeField] private int _bubbleShieldCharge;
        public override void Construct(AudioSource audioSource,
                                       DamageTextService damageTextService,
                                       MonsterInfoServiceIngame monsterInfoServiceIngame,
                                       IEnemyReachedReciever enemyReachedReciever,
                                       IWaveNumberProvider waveNumberProvider)
        {
            base.Construct(audioSource, damageTextService, monsterInfoServiceIngame, enemyReachedReciever, waveNumberProvider);
            _abstractDamageRecievingModule = new BubbleShieldModule(transform, damageTextService, _bubbleShieldCharge);
        }
    }
}