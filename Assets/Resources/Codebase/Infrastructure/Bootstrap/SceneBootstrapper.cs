using Services;
using Services.CardGeneration;
using UnityEngine;
using Zenject;

namespace BootStrap
{
    /// <summary>
    /// gameplay scene general bootstrap. For binding look at GameplaySceneInstaller
    /// </summary>
    public class SceneBootstrapper : MonoBehaviour
    {
        private WaveService _waveService;
        private CardGenerationService _cardGenerationService;
        [Inject]
        private void Construct(WaveService waveService, CardGenerationService cardGenerationService)
        {
            if (_waveService != null)
            {
                Debug.LogError("Multiple attempt to init ");
                return;
            }

            _cardGenerationService = cardGenerationService;
            _waveService = waveService;
            _cardGenerationService.GenerateInitialCards();
            _waveService.StartWaveCoroutine();
        }

        private void OnDisable() => 
            _waveService.CancelRoutine();
    }
}

