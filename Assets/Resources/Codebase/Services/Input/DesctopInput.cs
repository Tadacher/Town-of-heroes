using UnityEngine;

namespace Services.Input
{
    public class DesctopInput : AbstractInputService
    {
        public DesctopInput(Camera camera) : base(camera)
        {
        }

        
        public override Vector2 GetPointerPositionScreen() => 
            UnityEngine.Input.mousePosition;

        public override Vector2 GetPointerPositionWorld() => 
            _camera.ScreenToWorldPoint(GetPointerPositionScreen());

        public override bool IsPointerDown() => 
            UnityEngine.Input.GetMouseButtonDown(0);

        public override bool IsPointerUp() => 
            UnityEngine.Input.GetMouseButtonUp(0);
    }
}
