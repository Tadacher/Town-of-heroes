using System;
using UnityEngine;

namespace Services.Input
{
    public abstract class AbstractInputService : IInputListener
    {
        protected Camera _camera;

        protected AbstractInputService(Camera camera) => 
            _camera = camera;


        public event Action OnPointerUp;
        public event Action OnPointerDown;

        public abstract Vector2 GetPointerPositionScreen();
        public abstract Vector2 GetPointerPositionWorld();

        public abstract bool IsPointerDown();
        public abstract bool IsPointerUp();

        void IInputListener.PointerDown() => 
            OnPointerDown?.Invoke();

        void IInputListener.PointerUp() => 
            OnPointerUp?.Invoke();
    }
}
