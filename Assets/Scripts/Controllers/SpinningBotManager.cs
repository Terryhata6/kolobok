using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinningBotManager : MonoBehaviour
{
    [SerializeField] private SkinnedMeshRenderer _render;
    [SerializeField] bool _rendererState = false;
    

    public void SetRenderersState(bool state)
    {
        if (_render == null)
        {
            _render = GetComponentInChildren<SkinnedMeshRenderer>();
        }
        _render.enabled = state;
        _rendererState = state;
    }


}
