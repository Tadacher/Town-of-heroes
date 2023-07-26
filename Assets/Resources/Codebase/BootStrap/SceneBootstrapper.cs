using Services;
using UnityEngine;

namespace BootStrap
{
    public class SceneBootstrapper : MonoBehaviour
    {
        [SerializeField] private AudioSource _audioSource;
        [SerializeField] private Transform _spawnPos;
        [SerializeField] private EnemyContainer _enemyContainer;

        private AbstractSoundPlayerService _soundPlayerService;
        private WaveLauncher _waveLauncher;
        private DamageTextService _damageTextService;
        private void Awake()
        {
            _damageTextService = new();
            _enemyContainer.InitializeSelf();
            _soundPlayerService = new SimpleSoundPlayerService(_audioSource);
            _waveLauncher = new(_spawnPos, _enemyContainer, _audioSource, _damageTextService);
            _waveLauncher.StartAsync();
        }

        private void OnDisable() => _waveLauncher.CancelRoutine();

    }
}

