using UnityEngine;
using System.Collections;
using Infrastructure;

/// <summary>
/// General service for generating and spawning waves
/// </summary>
public class WaveService
{
    private float _interval;

    private WaveGenerator _waveGenerator;   
    private ICoroutineRunner _coroutineRunner;

    private Coroutine _waveSender;
    private Coroutine _spawnerOfwawes;

    public WaveService(WaveGenerator waveGenerator, ICoroutineRunner coroutineRunner)
    {
        _waveGenerator = waveGenerator;
        _coroutineRunner = coroutineRunner;
        _interval = 15f;      
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
        while (true)
        {
            _spawnerOfwawes = _coroutineRunner.StartCoroutine(SendWaweCoroutine(_waveGenerator.GenerateWave())); 
            yield return new WaitForSeconds(_interval);         
        }
    }

    /// <summary>
    /// Cancels all active spawn and wave routines
    /// </summary>
    public void CancelRoutine()
    {
        Debug.Log("Spawn routine canceled");
        _coroutineRunner.StopCoroutine(_waveSender);
        _coroutineRunner.StopCoroutine(_spawnerOfwawes);
    }

    /// <summary>
    /// Coroutine that directly spawns enemies from generated spawn instructions in wave object
    /// </summary>
    /// <param name="wave">wave object</param>
    /// <returns></returns>
    private IEnumerator SendWaweCoroutine(Wave wave)
    {
        for (int pointer = 0; pointer < wave.EnemyCreationCommands.Length; pointer++)
        {
            wave.EnemyCreationCommands[pointer].Invoke();
            yield return new WaitForSeconds(wave.Delay);
        }
    }
}
