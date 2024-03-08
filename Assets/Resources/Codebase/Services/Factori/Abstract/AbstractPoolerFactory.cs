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
        /// <summary>
        /// action on return to pool
        /// </summary>
        /// <param name="type">returnable thing</param>
        protected abstract void ActionOnRelease(TType type);
        /// <summary>
        /// creates new object of factory parameter type
        /// </summary>
        /// <returns>new object of factory parameter type</returns>
        protected abstract TType CreateNew();
        /// <summary>
        /// If pool es emty, returns new object of factory patameter type, otherwise returns object from pool
        /// </summary>
        /// <param name="poolable"></param>
        protected abstract void ActionOnGet(TType poolable);
        /// <summary>
        /// Describes instruction to destroy pooled object
        /// </summary>
        /// <param name="poolable"></param>
        protected abstract void ActionOnDestroy(TType poolable);
    }
}