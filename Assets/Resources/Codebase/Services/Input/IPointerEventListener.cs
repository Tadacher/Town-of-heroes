namespace Services.Input
{
    public interface IPointerEventListener
    {
        bool IsPointerDown();
        bool IsPointerUp();
    }
}