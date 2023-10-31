using Services.Input;
using UnityEngine;

public class PointerFollower : MonoBehaviour
{
    private AbstractInputService _inputService;
    private Transform _transform;
    public void Initialize(AbstractInputService abstractInputService)
    {
        _inputService = abstractInputService;
        _transform = GetComponent<Transform>();
        this.enabled = false;
    }
    private void Update() => 
        _transform.position = _inputService.GetPointerPositionWorld();
}