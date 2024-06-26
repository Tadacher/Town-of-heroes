﻿using Services.GridSystem;
using Services.Input;
using UnityEngine;

public class PointerFollower : MonoBehaviour
{
    private AbstractInputService _inputService;
    private AbstractGridService _gridAlignService;
    private Transform _transform;
    public void Initialize(AbstractInputService abstractInputService, AbstractGridService gridAlignService)
    {
        _gridAlignService = gridAlignService;
        _inputService = abstractInputService;
        _transform = GetComponent<Transform>();
        enabled = false;
    }
    private void Update() => 
        _transform.position = _gridAlignService.AlignToGrid(_inputService.GetPointerPositionWorld());
}
