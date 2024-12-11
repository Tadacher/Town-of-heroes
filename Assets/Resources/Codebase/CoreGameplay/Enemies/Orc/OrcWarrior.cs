using MovementModules;
using Services;
using System.Collections.Generic;
using UnityEngine;

namespace Enemies
{
    public class OrcWarrior : Orc
    {
        [SerializeField] private int _blockValue;
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
                        new BlockDefenciveAbility(_blockValue)
                    };
            _abstractDamageRecievingModule = new DefaultHealthModule(transform, damageTextService, passiveDefensiveAbilities: abilties);
        }
    }
}