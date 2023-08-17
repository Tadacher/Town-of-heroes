using System;

public class WaveGenerator
{
    public Wave NewGobboWave() => new Wave("BasicGobbo", 3, 1);

    internal Wave NewGobboTrapperWave() => new Wave("GobboTrapper", 3, 1);
}
