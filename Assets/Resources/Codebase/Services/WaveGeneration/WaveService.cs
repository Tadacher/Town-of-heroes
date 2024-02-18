using UnityEngine;
using System.Threading;
using System.Collections;
using Infrastructure;
using WorldCells;
using Zenject;

/// <summary>
/// 
/// </summary>
public class WaveService
{
    private float _interval;

    private WaveGenerator _waveGenerator;
    private EnemyInstantiationService _enemyInstantiationService;
    private CancellationTokenSource _cancellationTokenSource;
   
    private ICoroutineRunner _coroutineRunner;

    private Coroutine _waveSender;
    private Coroutine _spawnerOfwawes;
  
    private DiContainer _container;
    public WaveService(DiContainer diContainer, ICoroutineRunner coroutineRunner)
    {
        _container = diContainer;
        _coroutineRunner = coroutineRunner;
        ConstructEnemtInstantiationService();
        ConstructWaveGenerator();
        _interval = 5f;      
    }
    private void ConstructEnemtInstantiationService()
    {
        _enemyInstantiationService = new(
                    diContainer: _container,
                    enemySpawnPosMarker: _container.Resolve<EnemySpawnPosMarker>());
    }
    private void ConstructWaveGenerator()
    {
        _waveGenerator = new(
                    nemyInstantiationService: _enemyInstantiationService,
                    enemyTypeService: _container.Resolve<EnemyTypeService>(),
                    enemyPrefabContainer: _container.Resolve<EnemyPrefabContainer>(),
                    worldCellBalanceService: _container.Resolve<WorldCellBalanceService>());
    }

    public void StartWaveCoroutine() 
        => _waveSender = _coroutineRunner.StartCoroutine(WaveSending());

    private IEnumerator WaveSending()
    {
        while (true)
        {
            //   Debug.Log("NewWave");
            _spawnerOfwawes = _coroutineRunner.StartCoroutine(SendWaweCoroutine(_waveGenerator.GenerateWave())); 
            yield return new WaitForSeconds(_interval);         
        }
    }

    public void CancelRoutine()
    {
        Debug.Log("spawning routine canceled");
        _cancellationTokenSource?.Cancel();
    }

    private IEnumerator SendWaweCoroutine(Wave wave)
    {
        for (int count = 0; count < wave.EnemyCreationCommands.Length; count++)
        {
            wave.EnemyCreationCommands[count].Invoke();
           yield return new WaitForSeconds(wave.Delay);
        }
    }
}
