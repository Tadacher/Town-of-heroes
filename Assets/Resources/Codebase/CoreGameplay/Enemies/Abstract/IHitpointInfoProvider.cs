using System;

public interface IHitpointInfoProvider
{
    public float CurrentHealth { get; }
    public float MaxHealth { get; }
    public event Action OnHealthChanged;

}