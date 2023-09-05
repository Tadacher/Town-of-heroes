using System.Collections;
using UnityEngine;

namespace MovementModules
{
    public class StraightMovementModule : AbstractMovementModule
    {
        

        public StraightMovementModule(Transform unitTransform, Vector3 target, MonoBehaviour coroutineProcessor, float speed)
        {
            _unitTransform = unitTransform;
            _targetV3 = target;
            _coroutineProcessor = coroutineProcessor;
            _speed = speed;
            StartMovementCoroutine(coroutineProcessor);
        }

        protected override IEnumerator MovementCoroutine()
        {
            Debug.Log("move");
            while ((_unitTransform.position - _targetV3).magnitude >= 0.1)
            {
                _unitTransform.position += (_targetV3 - _unitTransform.position).normalized * _speed * Time.deltaTime;
                yield return null;
            }
        }

    }
}