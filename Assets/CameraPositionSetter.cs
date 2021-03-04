using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraPositionSetter : MonoBehaviour
{
    private enum SetterState
    { 
        SetPosition = 0,
        ToBasePosition = 1
    }

    private CameraController _camera;
    private PlayerController _player;
    [SerializeField] private SetterState _state;
    [SerializeField] private GameObject _cameraPosition;
    [SerializeField] private GameObject _cameraRotationObject;
    
    private void Awake()
    {
        _camera = FindObjectOfType<CameraController>();
        _player = FindObjectOfType<PlayerController>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (_state == SetterState.SetPosition)
            {
                _camera.SetPursuedObject(_cameraPosition);
                _camera.SetCameraRotation(_cameraPosition.transform.position, _cameraRotationObject.transform.position);
            }
            else if (_state == SetterState.ToBasePosition)
            {
                _camera.SetPursuedObject(_player.gameObject);
                _camera.SetCameraBaseRotation();
            }
        }
    }



}
