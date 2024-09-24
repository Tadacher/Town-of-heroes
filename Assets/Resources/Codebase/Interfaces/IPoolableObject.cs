public interface IPoolableObject
{
    public void ReturnToPool();
    public void ActionOnGet();
}