public class EnemyInstantiationService
{
    public AbstractEnemyFactory _gobboFactory;

    public EnemyInstantiationService(EnemyContainer enemyContainer, UnityEngine.AudioSource _audioSource)
    {
        _gobboFactory = new GobboFactory(enemyContainer, _audioSource);
    }
}
