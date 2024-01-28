using System;

namespace Services.Input
{
    public interface IShiftEventProvider
    {
        event Action OnShiftUp;
        event Action OnShiftDown;
    }
}