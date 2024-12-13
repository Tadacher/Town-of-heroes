using MovementModules;
using Services;
using System.Collections.Generic;
using UnityEngine;

namespace Enemies
{
    public class GobboWarrior : Gobbo
    {
        [SerializeField] private int _bubbleShieldCharge;
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
            var abilties = new List<AbstractPassiveDefensiveAbility>()
                    {
                        new BubbleShieldAbility(_bubbleShieldCharge)
                    };
            _abstractDamageRecievingModule = new DefaultHealthModule(transform, damageTextService, passiveDefensiveAbilities: abilties);
        }
    }
}