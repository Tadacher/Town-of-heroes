public interface IObjectPooler
{
    public void ReturnToPool(IPoolableObject returnable);
}