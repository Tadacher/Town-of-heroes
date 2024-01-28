using UnityEngine.Pool;
using Zenject;

namespace Services.Factories
{
    public abstract class AbstractPoolerFactory<TType> : IObjectPooler, IFactory<TType> where TType : class
    {
        protected ObjectPool<TType> _pool;
        protected DiContainer _container;

        protected AbstractPoolerFactory(DiContainer diContainer)
        {
            _container = diContainer;
            InitializePool();
        }

        private void InitializePool()
        {
            _pool = new(
                createFunc: CreateNew,
                actionOnGet: ActionOnGet,
                actionOnRelease: ActionOnRelease,
                actionOnDestroy: ActionOnDestroy);
        }
        public virtual void ReturnToPool(IPoolableObject returnable) =>
            _pool.Release((TType)returnable);
        public virtual TType GetObject() =>
            _pool.Get();

        protected abstract void ActionOnRelease(TType type);
        protected abstract TType CreateNew();
        protected abstract void ActionOnGet(TType poolable);
        protected abstract void ActionOnDestroy(TType poolable);
    }
}