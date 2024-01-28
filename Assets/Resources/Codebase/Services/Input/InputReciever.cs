using Services.Input;
using UnityEngine;
using Zenject;

namespace Infrastructure
{
    public class InputReciever : MonoBehaviour, IInputReciever
    {
        private AbstractInputService _inputListener;
       
       [Inject]
        public void Initialize(AbstractInputService inputListener) => 
            _inputListener = inputListener;

        private void Update()
        {
            if (Input.GetMouseButtonDown(0))
                _inputListener.PointerDown();

            else if (Input.GetMouseButtonUp(0))
                _inputListener.PointerUp();

            if (Input.GetKeyDown(KeyCode.LeftShift))
                _inputListener.ShiftDown();
            else if(Input.GetKeyUp(KeyCode.LeftShift))
                _inputListener.ShiftUp();
        }
    }
}