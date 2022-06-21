using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] 
    private float _speed = 2f;
    [SerializeField] 
    private FloatingJoystick _floatingJoystick = null;
    [SerializeField] 
    private GameObject _scythePrefab = null;
    
    private float _currentVertical = 0f;
    private float _currentHorizontal = 0f;

    private const float Smoothness = 10f;

    private int _moveSpeedHash = 0;
    private int _isHarvestingHash = 0;

    private Vector3 _currentDirection = Vector3.zero;
    private Vector3 _direction = Vector3.zero;
    private Animator _animator = null;

    private void Awake()
    {
        _animator = GetComponent<Animator>();

        _scythePrefab.SetActive(false);
        
        _moveSpeedHash = Animator.StringToHash("MoveSpeed");
        _isHarvestingHash = Animator.StringToHash("isHarvesting");
    }

    private void FixedUpdate()
    {
        var v = _floatingJoystick.Vertical;
        var h = _floatingJoystick.Horizontal;

        _currentVertical = Mathf.Lerp(_currentVertical, v, Time.deltaTime * Smoothness);
        _currentHorizontal = Mathf.Lerp(_currentHorizontal, h, Time.deltaTime * Smoothness);

        _direction = Camera.main.transform.forward * _currentVertical + Camera.main.transform.right * _currentHorizontal;

        var directionLength = _direction.magnitude;
        _direction.y = 0;
        _direction = _direction.normalized * directionLength;


        if (_direction != Vector3.zero)
        {
            _currentDirection = Vector3.Slerp(_currentDirection, _direction, Time.deltaTime * Smoothness);

            transform.rotation = Quaternion.LookRotation(_currentDirection);
            transform.position += _currentDirection * _speed * Time.deltaTime;

            _animator.SetFloat(_moveSpeedHash, _direction.magnitude);
        }
        
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.TryGetComponent(out CropStateManager cropStateManager) && cropStateManager._currentState == cropStateManager._harvestingState)
        {
            _scythePrefab.SetActive(true);
            _animator.SetBool(_isHarvestingHash, true);
        }
    }
    
    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent(out CropStateManager cropStateManager))
        {
            _scythePrefab.SetActive(false);
            _animator.SetBool(_isHarvestingHash, false);
        }
    }
}
