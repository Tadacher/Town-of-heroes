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
                _inputListener.LeftMouseButtonDown();

            else if (Input.GetMouseButtonUp(0))
                _inputListener.LeftMouseButtonUp();

            if (Input.GetMouseButtonUp(1))
                _inputListener.RightMouseButtonUp();


            if (Input.GetKeyDown(KeyCode.LeftShift))
                _inputListener.ShiftDown();
            else if(Input.GetKeyUp(KeyCode.LeftShift))
                _inputListener.ShiftUp();
        }
    }
}