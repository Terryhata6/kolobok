﻿using UnityEngine;
using UnityEditor;

/// <summary>
/// Объявляем и меняем объект+
/// Управляем расстоянием от камеры до объекта+
/// Плавно перемещаем Камеру к новому объекту слежения - в fixedupdate+
/// *Изменяем угол обзора камеры - меняем ротейт оператора
/// *Сохранять ротейт в скриптейбл обджект 
/// Смена стейта меняет целевую позицию камеры, при смене камера плавно и быстро проворачивается к нужному ротейту
/// </summary>

//[InitializeOnLoad]
public class CameraController : MonoBehaviour, IExecute
{
    [SerializeField] private GameObject _pursuedObject;
    [SerializeField] private Camera _mainCamera;
    [SerializeField] [Range(3f, 30f)] private float _cameraDistance = 3;
    [SerializeField] private float _smooth = 0.05f;
    [SerializeField] private float _stopChangingDistance = 0.05f;
    [SerializeField] private float _rotationSpeed = 1.0f;
    private bool _objectChanging;
    [SerializeField] private bool _cameraRotate;
    private Vector3 _newDirection;
    private Quaternion _inputRotation;
    private float _deltaEpsilonForRotate = 0.001f;
    private bool _deltaChecked = false;
    private Quaternion _baseRotation;


    private void Start()
    {
        _objectChanging = false;
        _baseRotation = transform.rotation;
        
        
    }

    public void Execute()
    {
        if (_objectChanging)
        {
            if (Vector3.Distance(_pursuedObject.transform.position, transform.position) < _stopChangingDistance)
            {
                _objectChanging = false;
            }
        }
    }

    private void LateUpdate()
    {
        if (!_objectChanging)
        {
            transform.position = _pursuedObject.transform.position;
        }
        else
        {
            Vector3 pos = new Vector3(_pursuedObject.transform.position.x, _pursuedObject.transform.position.y, _pursuedObject.transform.position.z); // сохраняем Z координату камеры
            transform.position = Vector3.Lerp(transform.position, pos, _smooth);
        }
        if (_cameraRotate)
        {
            
            transform.rotation = Quaternion.RotateTowards(transform.rotation, _inputRotation, 2.0f);
            //CheckDelta();
            Invoke("InvokedMethod", 2);
        }
    }

    private void InvokedMethod()
    {
        //transform.rotation = Quaternion.LookRotation(_inputRotation);
        _cameraRotate = false;
        //_deltaChecked = false;
    }

    /*
    private void CheckDelta()
    {
        Debug.Log($"1:{transform.rotation.x}  2:{_inputRotation.x}");
        Debug.Log($"1:{transform.rotation.y}  2:{_inputRotation.y}");
        Debug.Log($"1:{transform.rotation.z}  2:{_inputRotation.z}");
        if ((transform.rotation.eulerAngles.x - _inputRotation.x < _deltaEpsilonForRotate) || (transform.rotation.eulerAngles.x -_inputRotation.x > -_deltaEpsilonForRotate))
        {
            if ((transform.rotation.eulerAngles.y - _inputRotation.y < _deltaEpsilonForRotate) || (transform.rotation.eulerAngles.y - _inputRotation.y > -_deltaEpsilonForRotate))
            {
                if ((transform.rotation.eulerAngles.z - _inputRotation.z < _deltaEpsilonForRotate) || (transform.rotation.eulerAngles.z - _inputRotation.z > -_deltaEpsilonForRotate))
                    _deltaChecked = true;
            }
        } 
    }
    */

    public void SetPursuedObject(GameObject obj)
    {
        _pursuedObject = obj;
        _objectChanging = true;
    }

    public void SetCameraRotation(Vector3 fromRotation, Vector3 toRotation)
    {
        _cameraRotate = true;
        _inputRotation = Quaternion.FromToRotation(fromRotation, toRotation);
    }

    public void SetCameraRotation(Quaternion inputRotation)
    {
        transform.rotation = inputRotation;
    }


    public void SetCameraDistance(float value) 
    { 
    
    }

    public void SetCameraBaseRotation( )
    {
        SetCameraRotation(_baseRotation);
    }

    public GameObject GetPursuedObject() 
    {
        return _pursuedObject;
    }
}
