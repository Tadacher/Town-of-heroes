using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

/// <summary>
/// a collection of fabric exemplars paramerized with child classes of Tproduction
/// </summary>

public abstract class AbstractInstantiationService<TProduction> where TProduction : MonoBehaviour
{
    protected readonly DiContainer _container;
    protected Dictionary<Type, IFactory<TProduction>> _factories;
    protected string _productionPrefabPath;

    protected AbstractInstantiationService(string productionPrefabPath, DiContainer diContainer)
    {
        _container = diContainer;
        _factories = new();
        _productionPrefabPath = productionPrefabPath;
    }

    public abstract TProduction ReturnObject(Type type);

    protected abstract void AddNewFactory(Type type);
    protected abstract IFactory<TProduction> GetNewFactory(Type productType);

    protected TProduction LoadProductPrefab(Type productType)
    {
        TProduction tenemyType = Resources.Load<TProduction>(_productionPrefabPath + productType.Name);
        return tenemyType;
    }

}