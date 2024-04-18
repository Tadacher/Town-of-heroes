using System.Collections;
using UnityEngine;

namespace MovementModules
{
    public class CentaurMovementModule : StraightMovementModule
    {
        private readonly IHitpointInfoProvider _hpInfo;
        public float _initialspeed;
        public CentaurMovementModule(Transform unitTransform,
                                     IHitpointInfoProvider hpInfo,
                                     Vector3 target,
                                     MonoBehaviour coroutineProcessor,
                                     float speed) : base(unitTransform, target, coroutineProcessor, speed)
        {
            _hpInfo = hpInfo;
            _initialspeed = speed;
        }
        public override void SetSpeed(float speed)
        {
            _initialspeed = speed;
            RecalculateSpeed();
        }
        protected override IEnumerator MovementCoroutine()
        {
            while ((_unitTransform.position - _targetV3).magnitude >= 1)
            {
                RecalculateSpeed();
                _unitTransform.position += (_targetV3 - _unitTransform.position).normalized * Speed * Time.deltaTime;
                yield return null;
            }
            EnemyReached();
        }
        public void RecalculateSpeed()
        {
            if (_hpInfo == null) 
                return;

            float mod = _hpInfo.CurrentHealth / _hpInfo.MaxHealth;
            if (mod < 0.3f) mod = 0.3f;
            Speed = _initialspeed * mod;
        }
    }

}