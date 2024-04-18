using Core.Towers;
using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "AbstractTowerPrefabContainer", menuName = "ScriptableObjects/InitializableConfig/AbstractTowerPrefabContainer", order = 1)]
public class AbstractTowerPrefabContainer : ScriptableObject, IInitializableConfig
{
    private const string _towersPrefabPath = "Prefabs/Towers";
    private AbstractTower[] _towers;
    public AbstractTower[] Towers => _towers;
    public Dictionary<Type, AbstractTower> TowerDictionary { get; private set; }

    void IInitializableConfig.Initialize()
    {
        _towers = Resources.LoadAll<AbstractTower>(_towersPrefabPath);
        
        TowerDictionary = new Dictionary<Type, AbstractTower>();

        foreach (var tower in _towers) 
            TowerDictionary.Add(tower.GetType(), tower);
    }
}