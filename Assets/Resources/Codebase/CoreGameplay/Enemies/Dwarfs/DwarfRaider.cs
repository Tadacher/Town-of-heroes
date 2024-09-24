using MovementModules;
using Services;
using UnityEngine;
using Zenject;

public class DwarfRaider : AbstractEnemy
{
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
}
