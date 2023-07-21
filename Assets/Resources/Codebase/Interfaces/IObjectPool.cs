public interface IObjectPool<Ttype>
{
    public abstract void ReturnToPool(Ttype returnable);
}