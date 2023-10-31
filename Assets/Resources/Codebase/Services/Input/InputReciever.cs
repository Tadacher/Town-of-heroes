using Services.Input;
using UnityEngine;
using Zenject;

namespace Infrastructure
{
    public class InputReciever : MonoBehaviour, IInputReciever
    {
        private IInputListener _inputListener;
       
       [Inject]
        public void Initialize(IInputListener inputListener) => 
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