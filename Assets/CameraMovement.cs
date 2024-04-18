using Services.Input;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class CameraMovement : MonoBehaviour
{
    private AbstractInputService _inputService;
    [SerializeField] private Camera _camera;
    /// <summary>
    /// what part of a screen is a trigger of camera movement;
    /// </summary>
    [SerializeField] float _speed = 2;

    [Range(0,1)]
    [SerializeField] float _borderGap;

    [SerializeField] float _xMin, _xMax;
    [SerializeField] float _yMin, _yMax;
    [SerializeField] float _zoomMin, _zoomMax;
    [SerializeField] float _zoomSpeed;

    private float _screenWidght;
    private float _screenHeight;
    [Inject]
    private void Initialize(AbstractInputService inputService)
    {
        _inputService = inputService;
    }
    private void Awake()
    {
        _screenHeight = Screen.height;
        _screenWidght = Screen.width;
    }
   
    private void Update()
    {
        Vector2 mousepos = Input.mousePosition;

        if (mousepos.x < _screenWidght * _borderGap || Input.GetKey(KeyCode.A))
            MoveLeft();
        else if (mousepos.x > _screenWidght * (1 - _borderGap) || Input.GetKey(KeyCode.D))
            MoveRight();

        if (mousepos.y < _screenHeight * _borderGap || Input.GetKey(KeyCode.S))
            MoveDown();
        else if (mousepos.y > _screenHeight * (1 - _borderGap) || Input.GetKey(KeyCode.W))
            MoveUp();

        _camera.orthographicSize += _inputService.ZoomInput() * _zoomSpeed;
        _camera.orthographicSize = Mathf.Clamp(_camera.orthographicSize, _zoomMin, _zoomMax);
    }

    private void MoveUp()
    {
        if (transform.position.y < _yMax)
            transform.Translate(_speed * Time.deltaTime * Vector3.up);
    }

    private void MoveDown()
    {
        if (transform.position.y > _yMin)
            transform.Translate(-_speed * Time.deltaTime * Vector3.up);
    }

    private void MoveRight()
    {
        if (transform.position.x < _xMax)
            transform.Translate(_speed * Time.deltaTime * Vector3.right);
    }

    private void MoveLeft()
    {
        if (transform.position.x > _xMin)
            transform.Translate(-_speed * Time.deltaTime * Vector3.right);
    }
}
