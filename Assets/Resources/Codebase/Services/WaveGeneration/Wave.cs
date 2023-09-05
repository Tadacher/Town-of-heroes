
using System;

public struct Wave 
{
    public Type[] Enemies { get => enemies; }
    public float Delay;

    private Type[] enemies;


    public Wave(float delay, Type[] enemyTypes)
    {
        enemies = enemyTypes;
        Delay = delay;
    }
}