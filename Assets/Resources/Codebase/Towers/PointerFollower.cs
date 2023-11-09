using Services.GridSystem;
using Services.Input;
using UnityEngine;

public class PointerFollower : MonoBehaviour
{
    private AbstractInputService _inputService;
    private GridAlignService _gridAlignService;
    private Transform _transform;
    public void Initialize(AbstractInputService abstractInputService, GridAlignService gridAlignService)
    {
        _gridAlignService = gridAlignService;
        _inputService = abstractInputService;
        _transform = GetComponent<Transform>();
        this.enabled = false;
    }
    private void Update() => 
        _transform.position = _gridAlignService.PosToGrid(_inputService.GetPointerPositionWorld());
}