using Services.Input;
using UnityEngine;
using Zenject;

public class RectPointerFollower : MonoBehaviour
{
    private AbstractInputService _inputService;
    private RectTransform    _transform;
    [Inject]
    public void Initialize(AbstractInputService abstractInputService)
    {
        _inputService = abstractInputService;
        _transform = GetComponent<RectTransform>();
        this.enabled = false;
    }

    private void Update()
    {
        _transform.position = _inputService.GetPointerPositionScreen();
    }
}