using UnityEngine;
using System.Threading;
using System.Threading.Tasks;

public class WaveService
{

    private float _interval;

    private EnemyPrefabContainer _enemyPrefabContainer;
    private WaveGenerator _waveGenerator;
    private EnemyInstantiationService _enemyInstantiationService;
    private CancellationTokenSource _cancellationTokenSource;
    private Transform _spawnPosition;
   
    private Task _waveSender;
    private Task _spawnerOfwawes;
  
    public WaveService(Transform spawnPosition,  AudioSource _audioSource, Services.DamageTextService _damageTextService, EnemyPrefabContainer enemyPrefabContainer)
    {
        _cancellationTokenSource = new();
        _enemyInstantiationService = new(enemyPrefabContainer, _audioSource, _damageTextService);
        _waveGenerator = new(enemyPrefabContainer);

        
        _interval = 5f;

        _spawnPosition = spawnPosition;
        _enemyPrefabContainer = enemyPrefabContainer;
    }
    public async void StartAsync()
    {
        _waveSender = WaveSendingAsync(_cancellationTokenSource.Token);
        await _waveSender;
    }

    private async Task WaveSendingAsync(CancellationToken token)
    {
        bool flag = false;
        while (!flag)
        {
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
        for (int count = 0; count < wave.Enemies.Length; count++)
        {
            _enemyInstantiationService.ReturnObject(wave.Enemies[0], _spawnPosition);
            await Task.Delay((int)(wave.Delay * 1000));
        }
    }
    
}
