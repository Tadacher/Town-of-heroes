using UnityEngine;
using System.Collections;
using Infrastructure;
using Services.Ui;
using TMPro.EditorUtilities;
using System;

/// <summary>
/// General service for generating and spawning waves
/// </summary>
public class WaveService : IWaveNumberProvider
{
    public int WaveNumber => _waveNumber;

    private float _interval;

    private WaveGenerator _waveGenerator;   
    private ICoroutineRunner _coroutineRunner;
    private GameplayCanvasContainer _gameplayCanvasContainer;
    private Coroutine _waveSender;
    private Coroutine _spawnerOfwawes;
    private int _waveNumber;
    public WaveService(WaveGenerator waveGenerator, ICoroutineRunner coroutineRunner, GameplayCanvasContainer gameplayCanvasContainer)
    {
        _waveGenerator = waveGenerator;
        _coroutineRunner = coroutineRunner;
        _gameplayCanvasContainer = gameplayCanvasContainer;
        _interval = 30f;      
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
        float currentInterval = _interval;
        
        while (true)
        {
            if (currentInterval > 0)
            {
                currentInterval -= Time.deltaTime;
                _gameplayCanvasContainer.TimeLeftText.text = currentInterval.ToString("0.0");
                yield return null;

                continue;
            }
            currentInterval = _interval;
            _spawnerOfwawes = _coroutineRunner.StartCoroutine(SendWaweCoroutine(_waveGenerator.GenerateWave(), () => _waveNumber++)); 
        }
    }

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
