using MovementModules;
using Services;
using UnityEngine;

namespace Enemies
{
    public class GobboTrapper : Gobbo
    {
        [SerializeField] private float _dodgeValue;
        public override void Construct(AudioSource audioSource,
                                       DamageTextService damageTextService,
                                       MonsterInfoServiceIngame monsterInfoServiceIngame,
                                       IEnemyReachedReciever enemyReachedReciever,
                                       IWaveNumberProvider waveNumberProvider)
        {
            base.Construct(audioSource, damageTextService, monsterInfoServiceIngame, enemyReachedReciever, waveNumberProvider);
            _abstractDamageRecievingModule = new DodgeDamageRecievingModule(_dodgeValue, transform, damageTextService);
        }
        public override void Heal(float points)
        {
            Hitpoints += points;
            if (Hitpoints > _maxHitpoints)
                Hitpoints = _maxHitpoints;
        }
    }
}