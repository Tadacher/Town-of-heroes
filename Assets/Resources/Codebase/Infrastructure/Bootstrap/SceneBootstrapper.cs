using Services;
using UnityEngine;
using Zenject;

namespace BootStrap
{
    public class SceneBootstrapper : MonoBehaviour
    {
        private WaveService _waveService;
        [Inject]
        private void Construct(WaveService waveService)
        {
            _waveService = waveService;
            _waveService.StartAsync();
        }

        private void OnDisable() => _waveService.CancelRoutine();

    }
}

