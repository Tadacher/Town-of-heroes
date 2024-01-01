using UnityEngine;
using System.Threading;
using System.Collections;
using Infrastructure;
using WorldCells;

public class WaveService
{

    private float _interval;

    private WaveGenerator _waveGenerator;
    private EnemyInstantiationService _enemyInstantiationService;
    private CancellationTokenSource _cancellationTokenSource;
    private Transform _spawnPosition;
   
    private ICoroutineRunner _coroutineRunner;

    private Coroutine _waveSender;
    private Coroutine _spawnerOfwawes;
  
    public WaveService(
        EnemySpawnPosMarker spawnPosition,
        AudioSource _audioSource,
        Services.DamageTextService _damageTextService,
        WorldCellBalanceService worldCellBalanceService,
        EnemyPrefabContainer enemyPrefabContainer,
        EnemyTypeService enemyTypeService,
        ICoroutineRunner coroutineRunner)
    {
        _coroutineRunner = coroutineRunner;
        _cancellationTokenSource = new();
        _enemyInstantiationService = new(_audioSource, _damageTextService, spawnPosition);
        _waveGenerator = new(_enemyInstantiationService, worldCellBalanceService, enemyPrefabContainer, enemyTypeService);

        
        _interval = 5f;

        _spawnPosition = spawnPosition.transform;
    }
    public void StartCoroutine() 
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
        _cancellationTokenSource.Cancel();
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
