using MovementModules;
using Services;
using UnityEngine;

namespace Enemies
{
    public class GobboWarrior : Gobbo
    {
        [SerializeField] private int _blockValue;
        public override void Construct(AudioSource audioSource, DamageTextService damageTextService, MonsterInfoServiceIngame monsterInfoServiceIngame, IEnemyReachedReciever enemyReachedReciever)
        {
            base.Construct(audioSource, damageTextService, monsterInfoServiceIngame, enemyReachedReciever);
            _abstractDamageRecievingModule = new BlockHealthModule(transform, damageTextService, _blockValue);
        }
    }
}