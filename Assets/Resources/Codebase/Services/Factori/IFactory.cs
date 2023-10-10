public interface IFactory<out TType>
{
    TType GetObject();
}