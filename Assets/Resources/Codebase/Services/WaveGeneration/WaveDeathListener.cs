using System;
/// <summary>
/// listens death of every mob in wave, calls event when every mob in wave is dead
/// Preferrable way to get object of this type is via WaveDeathListenerDatory
/// </summary>
public class WaveDeathListener : IMobDeathListener, IPoolableObject
{
    private int _mobCount;
    private IObjectPooler _pooler;

    public WaveDeathListener(IObjectPooler pooler)
    {
        _pooler = pooler;
    }

    public event Action OnWaveDead;

    /// <summary>
    /// reinit after being got from pool
    /// </summary>
    /// <param name="mobCount">new wave mob count</param>
    public void Reinitialize(int mobCount)
    {
        _mobCount = mobCount;
        OnWaveDead = null;
    }
    /// <summary>
    /// returns self to pool
    /// </summary>
    public void ReturnToPool() => _pooler.ReturnToPool(this);

    /// <summary>
    /// recieves signal from dead mob, 
    /// </summary>
    void IMobDeathListener.RecieveDeath()
    {
        _mobCount--;
        if(_mobCount == 0)
        {
            OnWaveDead?.Invoke();
            ReturnToPool();
        }
    }

}
