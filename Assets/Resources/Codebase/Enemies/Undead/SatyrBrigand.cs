using Core.Towers;
using MovementModules;
using Services;
using System;
using UnityEngine;

namespace Enemies
{
    public class SatyrBrigand : AbstractEnemy
    {
        [SerializeField] private Type illusionType;
        private int _illusionCount = 1;
        public override void Initialize(AudioSource audioSource, DamageTextService damageTextService, IEnemyReachedReciever enemyReachedReciever, IObjectPooler objectPooler)
        {
            base.Initialize(audioSource, damageTextService, enemyReachedReciever, objectPooler);
            _enemyMovementModule = new StraightMovementModule(transform, new Vector3(16.5f, 7.5f, 0f), this, _speed);
            _abstractDamageRecievingModule = new DefaultHealthModule(transform, damageTextService);

            _enemyMovementModule.OnEnemyReached += () => _coreGameplayService.RecieveEnemyReached(_damage);

        }
        public override void RecieveDamage(int damage, AbstractTower abstractTower)
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