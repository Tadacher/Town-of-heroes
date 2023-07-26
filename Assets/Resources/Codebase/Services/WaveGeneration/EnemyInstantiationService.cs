public class EnemyInstantiationService
{
    public AbstractEnemyFactory _gobboFactory;

    public EnemyInstantiationService(EnemyContainer enemyContainer, UnityEngine.AudioSource _audioSource, Services.DamageTextService _damageTextService)
    {
        _gobboFactory = new GobboFactory(enemyContainer, _audioSource, _damageTextService);
    }
}
