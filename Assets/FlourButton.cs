using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlourButton : MonoBehaviour
{
    [SerializeField]private Material _disabledButtonMaterial;
    [SerializeField]private Material _enabledButtonMaterial;
    [SerializeField]private MeshRenderer _renderer;
    private bool _enabled = false;
    // Start is called before the first frame update

    private void Awake()
    {
        _renderer = GetComponent<MeshRenderer>();
        ChangeMaterial();
    }

    private void ChangeMaterial()
    {
        if (_enabled)
        {
            _renderer.material = _enabledButtonMaterial;
        }
        else
        {
            _renderer.material = _disabledButtonMaterial;
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
