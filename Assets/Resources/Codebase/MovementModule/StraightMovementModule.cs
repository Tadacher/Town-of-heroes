using System.Collections;
using UnityEngine;

namespace MovementModule
{
    public class StraightMovementModule : AbstractMovementModule
    {
        private Transform _unitTransform;
        private Vector3 _targetV3;
        private MonoBehaviour _coroutineProcessor;
        private Coroutine _movementCoroutine;
        private float _speed;

        public StraightMovementModule(Transform unitTransform, Vector3 target, MonoBehaviour coroutineProcessor, float speed)
        {
            _unitTransform = unitTransform;
            _targetV3 = target;
            _coroutineProcessor = coroutineProcessor;
            _speed = speed;
            _movementCoroutine = coroutineProcessor.StartCoroutine(Movement());
        }

        private IEnumerator Movement()
        {
            while (_unitTransform != null && _targetV3 != null)
            {
                while ((_unitTransform.position - _targetV3).magnitude >= 0.1)
                {
                    _unitTransform.position += (_targetV3 - _unitTransform.position).normalized * _speed * Time.deltaTime;
                    yield return null;
                }
                yield break;
            }
        }

        
    }
}