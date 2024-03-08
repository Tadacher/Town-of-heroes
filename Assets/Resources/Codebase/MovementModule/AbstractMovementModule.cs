using System;
using System.Collections;
using UnityEngine;

namespace MovementModules
{
    public abstract class AbstractMovementModule
    {
        public event Action OnEnemyReached;
        protected Vector3 _targetV3;
        protected float _speed;

        protected Transform _unitTransform;
        protected MonoBehaviour _coroutineProcessor;
        protected Coroutine _movementCoroutine;
        public virtual void StartMovementCoroutine(MonoBehaviour coroutineProcessor) => _movementCoroutine = coroutineProcessor.StartCoroutine(MovementCoroutine());
        public virtual void StopMovementCoroutine(MonoBehaviour coroutineProcessor) => _coroutineProcessor.StopAllCoroutines();
        protected abstract IEnumerator MovementCoroutine();
        protected void EnemyReached() => OnEnemyReached?.Invoke();
    }
}