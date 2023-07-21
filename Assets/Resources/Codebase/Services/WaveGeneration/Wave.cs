
using System;

public struct Wave
{
    public string enemyName;
    public int _count;
    public float _delay;
    internal Wave(string name, int count, float delay)
    {
        enemyName = name;
        _count = count;
        _delay = delay;
    }
}