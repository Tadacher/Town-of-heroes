using MovementModules;
using Services;
using System.Collections.Generic;
using UnityEngine;

namespace Enemies
{
    public class GobboTrapper : Gobbo
    {
        [SerializeField] private float _dodgeValue;

        public override void Construct(AudioSource audioSource, DamageTextService damageTextService, MonsterInfoServiceIngame monsterInfoServiceIngame, IEnemyReachedReciever enemyReachedReciever)
        {
            base.Construct(audioSource, damageTextService, monsterInfoServiceIngame, enemyReachedReciever);
            var abilties = new List<AbstractPassiveDefensiveAbility>()
                    {
                        new DodgeAbility(_dodgeValue, damageTextService, transform)
                    };
            _abstractDamageRecievingModule = new DefaultHealthModule(transform, damageTextService, passiveDefensiveAbilities: abilties);
        }
        public override void Heal(float points)
        {
            Hitpoints += points;
            if (Hitpoints > _maxHitpoints)
                Hitpoints = _maxHitpoints;
        }
    }
}