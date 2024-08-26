using UnityEngine;
using Zenject;

namespace Services.Factories
{
    public abstract class MonobehaviourAbstractPoolerFactory<TType> : AbstractPoolerFactory<TType> where TType : MonoBehaviour, IPoolableObject
    {
        protected MonobehaviourAbstractPoolerFactory(DiContainer diContainer) : base(diContainer)
        {
        }

        protected override void ActionOnRelease(TType type) =>
            type.gameObject.SetActive(false);
        protected override void ActionOnGet(TType poolable)
        {
            poolable.ActionOnGet();
            poolable.gameObject.SetActive(true);
        }

        protected override void ActionOnDestroy(TType poolable) =>
            Object.Destroy(poolable.gameObject);
    }
}