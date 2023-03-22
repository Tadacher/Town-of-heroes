using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading;
using System.Threading.Tasks;

public class WaveGenerator : MonoBehaviour
{
    
    [SerializeField] float interval;
    [SerializeField] float internalDelay;
    [SerializeField] Transform spawn;
    [SerializeField] EnemyContainer enemieContainer;
    float currentInterval;
    float waveCup;

    internal struct Wave
    {
        internal string enemyName;
        internal int count;
        internal Wave(string name, int maxCount)
        {
            
            enemyName = name;
            count = Random.Range(3, maxCount>2?maxCount:2);
        }
    }

    private void Update()
    {
        currentInterval -= Time.deltaTime;
        if(currentInterval<=0)
        {
            currentInterval = interval;
            StartCoroutine(SendWave(new("Basic Gobbo", 5)));
        }
    }
    
    IEnumerator SendWave(Wave wave)
    {
        GameObject enemy = (GameObject)enemieContainer.GetEnemy(wave.enemyName);

        for (int count = 0; count < wave.count; count++)
        {
            Instantiate(enemy, spawn, true);
            yield return new WaitForSeconds(internalDelay);
        }

    }
}
