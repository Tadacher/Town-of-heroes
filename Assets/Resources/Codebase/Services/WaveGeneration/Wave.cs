using System;

public class Wave 
{
    public  Action[] EnemyCreationCommands => enemyCreationCommands;
    public readonly float Delay;

    private Action[] enemyCreationCommands;


    public Wave(float delay, Action[] enemyTypes)
    {
        enemyCreationCommands = enemyTypes;
        Delay = delay;
    }
}