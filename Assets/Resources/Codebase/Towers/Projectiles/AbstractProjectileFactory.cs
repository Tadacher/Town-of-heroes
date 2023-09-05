using System;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbstractProjectileFactory 
{
    protected List<ProjectileBehaviour> _projectilePool;
    protected ProjectileBehaviour _projectilePrefab;

    protected AbstractProjectileFactory(ProjectileBehaviour projectilePrefab) => _projectilePrefab = projectilePrefab;

    public ProjectileBehaviour ReturnProjectile(float speed, Transform target, Vector3 initialPos, Action ontargetReached)
    {
        Debug.Log(_projectilePool.Count);
        foreach (ProjectileBehaviour projectileBehaviour in _projectilePool)
        {
            if (!projectileBehaviour.gameObject.activeSelf)
            {
                projectileBehaviour.gameObject.SetActive(true);
                projectileBehaviour.ReInitialize(target, initialPos, ontargetReached);
                return projectileBehaviour;
            }
        }
        ProjectileBehaviour newProjectileBehaviour = CreateObject(speed, target, initialPos, ontargetReached);
        _projectilePool.Add(newProjectileBehaviour);
        return newProjectileBehaviour;
    }


    private ProjectileBehaviour CreateObject(float speed, Transform target, Vector3 initialPos, Action ontargetReached)
    {
        ProjectileBehaviour projectileBehaviour = GameObject.Instantiate(_projectilePrefab, Vector3.zero, Quaternion.identity);
        projectileBehaviour.Initialize(speed, target, initialPos, ontargetReached);
        return projectileBehaviour;
        
    }

    
    protected void TryInitializePool()
    {
        if (_projectilePool == null)
            _projectilePool = new();
    }
}
