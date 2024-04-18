using System;
using System.Collections;
using UnityEngine;

namespace MovementModules
{
    public abstract class AbstractMovementModule
    {
        public event Action OnEnemyReached;
        public float Speed {  get; protected set; }
        protected Vector3 _targetV3;

        protected Transform _unitTransform;
        protected MonoBehaviour _coroutineProcessor;
        protected Coroutine _movementCoroutine;

        public virtual void SetSpeed(float speed) => Speed = speed;
        public virtual void StartMovementCoroutine(MonoBehaviour coroutineProcessor) => _movementCoroutine = coroutineProcessor.StartCoroutine(MovementCoroutine());
        public virtual void StopMovementCoroutine(MonoBehaviour coroutineProcessor) => _coroutineProcessor.StopAllCoroutines();
        protected abstract IEnumerator MovementCoroutine();
        protected void EnemyReached() => OnEnemyReached?.Invoke();
    }
}