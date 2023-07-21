using System;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbstractProjectileFactory : IObjectPool<ProjectileBehaviour>
{
    protected List<ProjectileBehaviour> _projectilePool;
    protected ProjectileBehaviour _projectilePrefab;

    protected AbstractProjectileFactory(ProjectileBehaviour projectilePrefab) => _projectilePrefab = projectilePrefab;

    public ProjectileBehaviour ReturnProjectile(float speed, Transform target, Vector3 initialPos, Action ontargetReached)
    {
        if (_projectilePool.Count > 0)
            return GetObjectFromList(target, initialPos, ontargetReached);
        else
            return CreateObject(speed, target, initialPos, ontargetReached);
    }

    public void ReturnToPool(ProjectileBehaviour returnable) => _projectilePool.Add(returnable);

    private ProjectileBehaviour CreateObject(float speed, Transform target, Vector3 initialPos, Action ontargetReached)
    {
        ProjectileBehaviour projectileBehaviour = GameObject.Instantiate(_projectilePrefab, Vector3.zero, Quaternion.identity);
        projectileBehaviour.Initialize(speed, target, initialPos, ontargetReached, this);
        return projectileBehaviour;
        
    }

    private ProjectileBehaviour GetObjectFromList(Transform target, Vector3 initialPos, Action ontargetReached)
    {
       foreach (ProjectileBehaviour projectileBehaviour in _projectilePool)
       {
            if(projectileBehaviour != null)
            {
                _projectilePool.Remove(projectileBehaviour);
                projectileBehaviour.ReInitialize(target, initialPos, ontargetReached);
                return projectileBehaviour;
            }
       }
        
        Debug.LogError("Zero behaviours found in pool, returning new");

        return null;
    }
    protected void TryInitializePool()
    {
        if (_projectilePool == null)
            _projectilePool = new();
    }
}
