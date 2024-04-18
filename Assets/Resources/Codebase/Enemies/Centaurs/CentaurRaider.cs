using Core.Towers;
using MovementModules;
using Services;
using UnityEngine;

namespace Enemies
{
    public class CentaurRaider : AbstractEnemy
    {
        [SerializeField] private float _boostMod;
        private bool _rushActivated;
        public override void Construct(AudioSource audioSource, DamageTextService damageTextService, MonsterInfoServiceIngame monsterInfoServiceIngame, IEnemyReachedReciever coreGameplayService)
        {
            base.Construct(audioSource, damageTextService, monsterInfoServiceIngame, coreGameplayService);
            _abstractDamageRecievingModule = new DefaultHealthModule(transform, damageTextService);
        }

        public override void RecieveDamage(float damage, AbstractTower abstractTower)
        {
            if(!_rushActivated) 
            { 
                _rushActivated = true;
                _enemyMovementModule.SetSpeed(_boostMod * _enemyMovementModule.Speed);
            }
            base.RecieveDamage(damage, abstractTower);
        }

        protected override void Awake()
        {
            _enemyMovementModule = new CentaurMovementModule(transform, this, new Vector3(16.5f, 7.5f, 0f), this, _speed);
            _enemyMovementModule.OnEnemyReached += OnReachedTargetHandler;
        }
    }
}