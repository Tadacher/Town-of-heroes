using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading;
using System.Threading.Tasks;
using System;

public class WaveLauncher
{

    private float _interval;

    private EnemyContainer _enemyContainer;
    private WaveGenerator _waveGenerator;
    private EnemyInstantiationService _enemyInstantiationService;
    private CancellationTokenSource _cancellationTokenSource;
    private Transform _spawnPosition;
   
    private Task _waveSender;
    private Task _waveSpawner;

    public WaveLauncher(Transform spawnPosition, EnemyContainer enemyContainer, AudioSource _audioSource)
    {
        _cancellationTokenSource = new();
        _enemyInstantiationService = new(enemyContainer, _audioSource);
        _waveGenerator = new();

        _interval = 5f;

        _spawnPosition = spawnPosition;
        _enemyContainer = enemyContainer;


    }
    public async void StartAsync()
    {
        _waveSender = WaveSendingAsync(_cancellationTokenSource.Token);
        await _waveSender;
    }

    private async Task WaveSendingAsync(CancellationToken token)
    {
        while(true)
        {
            Debug.Log("Wawesending...");
            _waveSpawner = SpawnWaveAsync(_waveGenerator.NewGobboWave(), token);
            await Task.Delay((int)(_interval * 1000), token);
        }
    }

    public void CancelRoutine()
    {
        Debug.Log("spawning routine canceled");
        _cancellationTokenSource.Cancel();
    }

    private async Task SpawnWaveAsync(Wave wave, CancellationToken token)
    {
        Debug.Log("sendwawe");
        for (int count = 0; count < wave._count; count++)
        {
            Debug.Log("spawn");
            _enemyInstantiationService._gobboFactory.Construct(_spawnPosition);

            await Task.Delay((int)(wave._delay * 1000), token);
        }
    }
}
