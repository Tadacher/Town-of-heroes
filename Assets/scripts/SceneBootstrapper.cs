using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneBootstrapper : MonoBehaviour
{
    [SerializeField] AbstractEnemyFactory Factories;
    private void Awake()
    {
        InstantiatePhase();
        InitPhase();
    }

    private void InstantiatePhase()
    {
        throw new NotImplementedException();
    }

    private void InitPhase()
    {
        throw new NotImplementedException();
    }
}
