using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class FlourButton : MonoBehaviour
{
    [SerializeField]private Material[] _materials = new Material[5];
    [SerializeField]private ButtonType type;
    private Material _disabledMaterial;
    private Material _enabledMaterial;


    [SerializeField]private MeshRenderer _renderer;
    private bool _enabled = false;
    // Start is called before the first frame update
       

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

    private void TurnState()
    {
        if (!_enabled)
        {
            _enabled = true;
        }
        else
        {
            _enabled = false;
        }
        ChangeMaterial();
    }
}
