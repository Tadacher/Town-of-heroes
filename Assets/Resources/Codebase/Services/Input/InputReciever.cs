using Services.Input;
using UnityEngine;

namespace Infrastructure
{
    public class InputReciever : MonoBehaviour
    {
        private readonly IInputListener _inputListener;
        public InputReciever(IInputListener inputListener) => 
            _inputListener = inputListener;

        private void Update()
        {
            if (Input.GetMouseButtonDown(0))
                _inputListener.PointerDown();

            else if (Input.GetMouseButtonUp(0))
                _inputListener.PointerUp();
        }
    }
}