using UnityEngine;

namespace Services.Factories
{
    public abstract class MonobehaviourAbstractPoolerFactory<TType> : AbstractPoolerFactory<TType> where TType : MonoBehaviour
    {
        protected MonobehaviourAbstractPoolerFactory() : base()
        {
        }

        protected override void ActionOnRelease(TType type) =>
            type.gameObject.SetActive(false);
        protected override void ActionOnGet(TType poolable) =>
            poolable.gameObject.SetActive(true);
        protected override void ActionOnDestroy(TType poolable) =>
            Object.Destroy(poolable.gameObject);
    }
}