using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class FlourButton : MonoBehaviour
{
    [SerializeField] private Material[] _materials = new Material[5];
    [SerializeField] private ButtonType type;
    [SerializeField] private BaseActivatedObject[] _elements;
    [SerializeField] private MeshRenderer _renderer;

    private Material _enabledMaterial;
    private Material _disabledMaterial;
    private bool _enabled = false;

    public BaseActivatedObject[]  Elements => _elements;


    
    private void Awake()
    {
        _renderer = GetComponent<MeshRenderer>();
        switch (type)
        {
            case ButtonType.ButtonOn: _disabledMaterial = _materials[0]; break;
            case ButtonType.ArrowButton: _disabledMaterial = _materials[1]; break;
            case ButtonType.PlusButton: _disabledMaterial = _materials[2]; break;
            case ButtonType.PowerButton: _disabledMaterial = _materials[3]; break;
            case ButtonType.XButton: _disabledMaterial = _materials[4]; break;
            default: break;
        }
        _enabledMaterial = _materials[0];
        ChangeMaterial();
    }
    private void ChangeMaterial()
    {
        if (_enabled)
        {
            _renderer.material = _enabledMaterial;
        }
        else
        {
            _renderer.material = _disabledMaterial;
        }
    }    

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            TurnState();
        }
    }
    /// <summary>
    /// Turn button state to another state
    /// </summary>
    private void TurnState()
    {
        if (!_enabled)
        {
            _enabled = true;
            OnEnable();
        }
        else
        {
            _enabled = false;
            OnDisable();
        }
        ChangeMaterial();
    }

    /// <summary>
    /// Call OnDisable methods in all connected IActivate elements
    /// </summary>
    private void OnDisable()
    {
        //if (_elements[0] != null) 
        //{
        //    foreach (IActivate element in _elements)
        //    {
        //        element.OnDisable();
        //    }
        //}        
    }

    /// <summary>
    /// Call OnDisable methods in 
    /// </summary>
    private void OnEnable()
    {
        /*
        if (_elements[0] != null)
        {
            foreach (IActivate element in _elements)
            {
                element.OnEnable();
            }
        }*/
    }

}
