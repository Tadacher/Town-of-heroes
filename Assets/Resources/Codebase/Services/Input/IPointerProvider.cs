using UnityEngine;

namespace Services.Input
{
    internal interface IPointerProvider
    {
        Vector2 GetPointerPositionScreen();
        Vector2 GetPointerPositionWorld();
    }
}