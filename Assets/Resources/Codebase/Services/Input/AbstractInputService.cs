using System;
using UnityEngine;

namespace Services.Input
{
    public abstract class AbstractInputService : IInputListener
    {
        protected Camera Camera => Camera.main;

        public event Action OnPointerUp;
        public event Action OnPointerDown;

        public abstract Vector2 GetPointerPositionScreen();
        public abstract Vector2 GetPointerPositionWorld();

        public abstract bool IsPointerDown();
        public abstract bool IsPointerUp();

        void IInputListener.PointerDown() => 
            OnPointerDown?.Invoke();

        void IInputListener.PointerUp()
        {
            Debug.Log("up");
            OnPointerUp?.Invoke();
        }
    }
}
