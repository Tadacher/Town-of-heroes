using System;
using UnityEngine;

namespace Services.Input
{
    public abstract class AbstractInputService : IPointerEventListener, IPointerProvider, IShiftEventProvider
    {

        protected Camera Camera => Camera.main;

        public event Action OnRightButtonUp;
        public event Action OnPointerUp;
        public event Action OnPointerDown;

        public event Action OnShiftUp;
        public event Action OnShiftDown;

        public abstract Vector2 GetPointerPositionScreen();
        public abstract Vector2 GetPointerPositionWorld();

        public abstract bool IsPointerDown();
        public abstract bool IsPointerUp();

        public void LeftMouseButtonDown() => 
            OnPointerDown?.Invoke();

        public void LeftMouseButtonUp() => 
            OnPointerUp?.Invoke();

        public bool LeftMouseDown() => 
            UnityEngine.Input.GetMouseButtonDown(0);
       
        public void ShiftDown() => 
            OnShiftDown?.Invoke();

        public void ShiftUp() => 
            OnShiftUp?.Invoke();

        public void RightMouseButtonUp() => 
            OnRightButtonUp?.Invoke();

        public bool RightMouseDown() => 
            UnityEngine.Input.GetMouseButtonDown(1);

        public float ZoomInput()
        {
            if (UnityEngine.Input.mouseScrollDelta.y > 0)
                return 1f;
            else if (UnityEngine.Input.mouseScrollDelta.y < 0)
                return -1f;
            else 
                return 0f;
        }
    }
}
