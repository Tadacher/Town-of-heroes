public interface IObjectPooler<TObject>
{
    public void ReturnToPool(TObject returnable);
}