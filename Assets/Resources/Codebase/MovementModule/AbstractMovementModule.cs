using System.Collections;
using UnityEngine;

namespace MovementModule
{
    public abstract class AbstractMovementModule
    {
        protected Transform _unitTransform;
        protected Vector3 _targetV3;
        protected MonoBehaviour _coroutineProcessor;
        protected Coroutine _movementCoroutine;
        protected float _speed;
        public virtual void StartMovementCoroutine(MonoBehaviour coroutineProcessor) => _movementCoroutine = coroutineProcessor.StartCoroutine(MovementCoroutine());
        public virtual void StopMovementCoroutine(MonoBehaviour coroutineProcessor) => _coroutineProcessor.StopAllCoroutines();

        protected abstract IEnumerator MovementCoroutine();
    }
}