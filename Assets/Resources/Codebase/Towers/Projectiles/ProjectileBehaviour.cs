using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileBehaviour : MonoBehaviour
{
    private float _speed;
    private Transform _target;
    private Action _onTagetReached;
    private IObjectPool<ProjectileBehaviour> _objectPool;
    void Update()
    {
        transform.position += _speed * Time.deltaTime * (_target.position - transform.position).normalized;
        if (Vector3.Distance(transform.position, _target.position) < 0.1)
        {
            _onTagetReached.Invoke();
            DeInitializeAndPool();
        }
    } 

    public void Initialize(float speed, Transform target, Vector3 initialPos, Action onTargetReached, IObjectPool<ProjectileBehaviour> objectPool)
    {
        transform.position = initialPos;
        _objectPool = objectPool;
        _speed = speed;
        _target = target;
        _onTagetReached += onTargetReached;
    }
    public void ReInitialize(Transform target, Vector3 initialPos, Action onTargetReached)
    {
        transform.position = initialPos;
        _target = target;
        _onTagetReached += onTargetReached;
    }
    private void ResetSelf() => _onTagetReached = null;
    
    private void DeInitializeAndPool()
    {
        gameObject.SetActive(false);
        ResetSelf();
        _objectPool.ReturnToPool(this);
    }
}
