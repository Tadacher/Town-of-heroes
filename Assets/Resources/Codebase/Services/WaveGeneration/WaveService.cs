using UnityEngine;
using System.Collections;
using Infrastructure;
using Services.Ui;
using System;

/// <summary>
/// General service for generating and spawning waves
/// </summary>
public class WaveService : IWaveNumberProvider
{
    public int WaveNumber => _waveNumber;

    private readonly float _interval = 30f;
    private float _currentInterval;

    private WaveGenerator _waveGenerator;   
    private ICoroutineRunner _coroutineRunner;
    private GameplayCanvasContainer _gameplayCanvasContainer;
    private Coroutine _waveSender;
    private Coroutine _spawnerOfwawes;
    private int _waveNumber = -1;
    public WaveService(WaveGenerator waveGenerator,
                       ICoroutineRunner coroutineRunner,
                       GameplayCanvasContainer gameplayCanvasContainer)
    {
        _waveGenerator = waveGenerator;
        _coroutineRunner = coroutineRunner;
        _gameplayCanvasContainer = gameplayCanvasContainer;

        _gameplayCanvasContainer.ForceWaveButton.onClick.AddListener(ForceWaveHandler);
    }


    /// <summary>
    /// Starts general coroutine that starts wavesending and waits for an interval betwee waves
    /// </summary>
    public void StartWaveCoroutine() 
        => _waveSender = _coroutineRunner.StartCoroutine(WaveSending());

    /// <summary>
    /// General coroutine that starts wavesending and waits for an interval betwee waves
    /// </summary>
    /// <returns></returns>
    private IEnumerator WaveSending()
    {
        _currentInterval = _interval;
        
        while (true)
        {
            if (_currentInterval > 0)
            {
                _currentInterval -= Time.deltaTime;
                _gameplayCanvasContainer.TimeLeftText.text = _currentInterval.ToString("0.0");
                yield return null;

                continue;
            }
            _waveNumber++;
            _currentInterval = _interval;
            _spawnerOfwawes = _coroutineRunner.StartCoroutine(SendWaweCoroutine(_waveGenerator.GenerateWave())); 
        }
    }
    private void ForceWaveHandler() => _currentInterval = 0;

    /// <summary>
    /// Cancels all active spawn and wave routines
    /// </summary>
    public void CancelRoutine()
    {
        Debug.Log("Spawn routine canceled");
        _coroutineRunner?.StopCoroutine(_waveSender);
        _coroutineRunner?.StopCoroutine(_spawnerOfwawes);
    }

    /// <summary>
    /// Coroutine that directly spawns enemies from generated spawn instructions in wave object
    /// </summary>
    /// <param name="wave">wave object</param>
    /// <returns></returns>
    private IEnumerator SendWaweCoroutine(Wave wave, Action callback = null)
    {
        for (int pointer = 0; pointer < wave.EnemyCreationCommands.Length; pointer++)
        {
            wave.EnemyCreationCommands[pointer].Invoke();
            yield return new WaitForSeconds(wave.Delay);
        }
        callback?.Invoke();
    }
}
