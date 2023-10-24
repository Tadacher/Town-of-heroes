using UnityEngine;

namespace Services.Input
{
    public class DesctopInput : AbstractInputService
    {        
        public override Vector2 GetPointerPositionScreen() => 
            UnityEngine.Input.mousePosition;

        public override Vector2 GetPointerPositionWorld() => 
            Camera.ScreenToWorldPoint(GetPointerPositionScreen());

        public override bool IsPointerDown() => 
            UnityEngine.Input.GetMouseButtonDown(0);

        public override bool IsPointerUp() => 
            UnityEngine.Input.GetMouseButtonUp(0);
    }
}
