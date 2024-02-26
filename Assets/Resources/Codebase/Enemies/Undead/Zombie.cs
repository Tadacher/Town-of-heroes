using MovementModules;
using Services;
using UnityEngine;

public class Zombie : AbstractEnemy
{
    [SerializeField] private float _reviveChance;
    [SerializeField] private AudioClip _reviveClip;
    public override void Initialize(AudioSource audioSource, DamageTextService damageTextService, IObjectPooler objectPooler)
    {
        base.Initialize(audioSource, damageTextService, objectPooler);
        _enemyMovementModule = new StraightMovementModule(transform, new Vector3(16.5f, 7.5f, 0f), this, _speed);
        _abstractDamageRecievingModule = new DefaultHealthModule(transform, damageTextService);
    }
    protected override void Die()
    {
        if (Random.Range(0f, 1f) > _reviveChance)
        {
            base.Die();
            return;
        }

        PlayReviveSound();
        StartReviveAnimCoroutine();
        _hitpoints = _maxHitpoints / 2;
    }

    private void StartReviveAnimCoroutine()
    {

    }

    private void PlayReviveSound() => _audioSource.PlayOneShot(_reviveClip);
}
