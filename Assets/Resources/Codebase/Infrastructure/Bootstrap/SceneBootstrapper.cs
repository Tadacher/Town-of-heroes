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
            if (_waveService != null)
                return;

            _waveService = waveService;
            _waveService.StartCoroutine();
        }

        private void OnDisable() => 
            _waveService.CancelRoutine();
    }
}

