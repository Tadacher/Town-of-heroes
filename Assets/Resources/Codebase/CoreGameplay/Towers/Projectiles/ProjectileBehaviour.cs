using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileBehaviour : MonoBehaviour
{
    private float _speed;
    private Transform _target;
    private Action _onTagetReached;
    void Update()
    {
        if (_target == null || !_target.gameObject.activeSelf)
        {
            ReturnToPool();
            return;
        }
        

        transform.position += _speed * Time.deltaTime * (_target.position - transform.position).normalized;
        LookAtTarget();
        if (Vector3.Distance(transform.position, _target.position) < 0.1)
        {
            _onTagetReached.Invoke();
            ReturnToPool();
        }
    }

    private void LookAtTarget()
    {
        Vector3 look = transform.InverseTransformPoint(_target.position);
        float angle = Mathf.Atan2(look.y, look.x) * Mathf.Rad2Deg - 90;
        transform.Rotate(0, 0, angle);
    }

    public void Initialize(float speed, Transform target, Vector3 initialPos, Action onTargetReached)
    {
        transform.position = initialPos;
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
    
    private void ReturnToPool()
    {
        ResetSelf();
        gameObject.SetActive(false);
    }
}
