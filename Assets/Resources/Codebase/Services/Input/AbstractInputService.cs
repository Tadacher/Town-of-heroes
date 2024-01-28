using System;
using UnityEngine;

namespace Services.Input
{
    public abstract class AbstractInputService : IPointerEventListener, IPointerProvider, IShiftEventProvider
    {
        protected Camera Camera => Camera.main;

        public event Action OnPointerUp;
        public event Action OnPointerDown;

        public event Action OnShiftUp;
        public event Action OnShiftDown;

        public abstract Vector2 GetPointerPositionScreen();
        public abstract Vector2 GetPointerPositionWorld();

        public abstract bool IsPointerDown();
        public abstract bool IsPointerUp();

        public void PointerDown() => 
            OnPointerDown?.Invoke();

        public void PointerUp() => 
            OnPointerUp?.Invoke();

        internal void ShiftDown() => 
            OnShiftDown?.Invoke();

        internal void ShiftUp() => 
            OnShiftUp?.Invoke();
    }
}
