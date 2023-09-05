using Services;
using UnityEngine;
using Zenject;

namespace BootStrap
{
    public class SceneBootstrapper : MonoBehaviour
    {
        [SerializeField] private AudioSource _audioSource;
        [SerializeField] private EnemySpawnPosMarker _spawnPos;
        [SerializeField] private EnemyPrefabContainer _enemyPrefabContainer;
        private AbstractSoundPlayerService _soundPlayerService;
        private WaveService _waveService;
        private DamageTextService _damageTextService;
        [Inject]
        private void Construct(WaveService waveService)
        {
            _waveService = waveService;
            //_damageTextService = new();
            //_soundPlayerService = new SimpleSoundPlayerService(_audioSource);
            //_waveService = new(_spawnPos, _audioSource, _damageTextService, _enemyPrefabContainer);
            _waveService.StartAsync();
        }

        private void OnDisable() => _waveService.CancelRoutine();

    }
}

