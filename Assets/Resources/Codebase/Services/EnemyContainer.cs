using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyContainer : MonoBehaviour
{ 
    bool initialized;
    public bool Initialized { get; private set; }
    [SerializeField] GameObject[] enemiePrefabs;
    internal Hashtable enemieHashTable;

    public void InitializeSelf()
    {
        if (!initialized)
        {
            enemieHashTable = new();
            for (int i = 0; i < enemiePrefabs.Length; i++)
            {
                if (enemieHashTable.ContainsKey(enemiePrefabs[i].name)) Debug.LogWarning("Name " + enemiePrefabs[i].name + " already exists!");
                else
                {
                    enemieHashTable.Add(enemiePrefabs[i].name, enemiePrefabs[i]);
                    Debug.Log(enemiePrefabs[i].name);
                }
            }
            initialized = true;
        }
        else Debug.LogWarning("EnemyContainer is already initialized");
    }
    public GameObject GetEnemy(string key)
    {
        if (initialized)
        {
            if (enemieHashTable.ContainsKey(key)) return (GameObject)enemieHashTable[key];
            else
            {
                Debug.LogError("Key is not exists, returning null!");
                return null;
            }
        }
        else
        {
            Debug.LogError("EnemyContainer is not initialized, returning null");
            return null;
        }
    }
}
