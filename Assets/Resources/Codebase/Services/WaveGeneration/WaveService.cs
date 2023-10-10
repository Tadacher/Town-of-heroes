using UnityEngine;
using System.Threading;
using System.Threading.Tasks;

public class WaveService
{

    private float _interval;

    private WaveGenerator _waveGenerator;
    private EnemyInstantiationService _enemyInstantiationService;
    private CancellationTokenSource _cancellationTokenSource;
    private Transform _spawnPosition;
   
    private Task _waveSender;
    private Task _spawnerOfwawes;
  
    public WaveService(EnemySpawnPosMarker spawnPosition,  AudioSource _audioSource, Services.DamageTextService _damageTextService, EnemyPrefabContainer enemyPrefabContainer)
    {
        _cancellationTokenSource = new();
        _enemyInstantiationService = new(enemyPrefabContainer, _audioSource, _damageTextService);
        _waveGenerator = new(_enemyInstantiationService);

        
        _interval = 5f;

        _spawnPosition = spawnPosition.transform;
    }
    public async void StartAsync()
    {
        _waveSender = WaveSendingAsync(_cancellationTokenSource.Token);
        await _waveSender;
    }

    private async Task WaveSendingAsync(CancellationToken token)
    {
        while (true)
        {
         //   Debug.Log("NewWave");
           // _spawnerOfwawes = SendAWaweAsync(_waveGenerator.GenerateWave(), token);
            _spawnerOfwawes = SendAWaweAsync(_waveGenerator.GenerateWave(), token);
            await Task.Delay((int)(_interval*1000), token);         
        }
    }

    public void CancelRoutine()
    {
        Debug.Log("spawning routine canceled");
        _cancellationTokenSource.Cancel();
    }

    private async Task SendAWaweAsync(Wave wave, CancellationToken token)
    {
        for (int count = 0; count < wave.EnemyCreationCommands.Length; count++)
        {
            Debug.Log(wave.EnemyCreationCommands[count].ToString());

            wave.EnemyCreationCommands[count].Invoke();

            Debug.Log(wave.EnemyCreationCommands[count].ToString());
            await Task.Delay((int)(wave.Delay * 1000));
        }
    }
}
