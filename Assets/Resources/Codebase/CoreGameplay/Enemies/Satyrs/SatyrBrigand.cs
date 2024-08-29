using Core.Towers;
using MovementModules;
using Services;
using System;
using UnityEngine;
using Zenject;

namespace Enemies
{

    public class SatyrBrigand : AbstractEnemy
    {
        [SerializeField] private Type illusionType;
        private int _illusionCount = 1;

        [Inject]
        public override void Construct(AudioSource audioSource,
                                       DamageTextService damageTextService,
                                       MonsterInfoServiceIngame monsterInfoServiceIngame,
                                       IEnemyReachedReciever enemyReachedReciever,
                                       IWaveNumberProvider waveNumberProvider)
        {
            base.Construct(audioSource, damageTextService, monsterInfoServiceIngame, enemyReachedReciever, waveNumberProvider);
            _abstractDamageRecievingModule = new DefaultHealthModule(transform, damageTextService);

        }
        protected override void Awake()
        {
            _enemyMovementModule = new StraightMovementModule(transform, new Vector3(16.5f, 7.5f, 0f), this, _speed);
            _enemyMovementModule.OnEnemyReached += OnReachedTargetHandler;
        }
        public override void RecieveDamage(float damage, AbstractTower abstractTower)
        {
            if (_illusionCount > 0)
            {
                _illusionCount--;
                SpawnIllusion();
            }
            else
            {
                base.RecieveDamage(damage, abstractTower);
            }
        }

     

        private void SpawnIllusion()
        {
            
        }
    }
}