using MovementModules;
using UnityEngine;

namespace Enemies
{
    public class MurlocHunter : AbstractMurloc
    {
        protected override void Awake()
        {
            _enemyMovementModule = new StraightMovementModule(transform, new Vector3(16.5f, 7.5f, 0f), this, _speed);
            _enemyMovementModule.OnEnemyReached += OnReachedTargetHandler;
        }
    }
}