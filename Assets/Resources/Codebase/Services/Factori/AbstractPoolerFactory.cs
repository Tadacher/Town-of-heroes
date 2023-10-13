using System;
using UnityEngine.Pool;

public abstract class AbstractPoolerFactory<TType> : IObjectPooler, IFactory<TType> where TType : class
{
    protected ObjectPool<TType> _pool;

    protected AbstractPoolerFactory() => InitializePool();

    private void InitializePool()
    {
        _pool = new(
            createFunc: CreateNew,
            actionOnGet: ActionOnGet,
            actionOnRelease: ActionOnRelease,
            actionOnDestroy: ActionOnDestroy);
    }

    protected abstract void ActionOnRelease(TType type);
    protected abstract TType CreateNew();
    protected abstract void ActionOnGet(TType poolable);
    protected abstract void ActionOnDestroy(TType poolable);
    public abstract void ReturnToPool(IPoolableObject returnable);
    public abstract TType GetObject();
}