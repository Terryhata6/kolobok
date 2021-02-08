using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinningBotManager : MonoBehaviour
{
    [SerializeField] private SkinnedMeshRenderer _render;
    [SerializeField] bool _rendererState = true;
    
    private void Awake()
    {
        _render = GetComponentInChildren<SkinnedMeshRenderer>();
        SetRenderersState(_rendererState);
    }

    public void SetRenderersState(bool state)
    {
        _render.enabled = state;
        _rendererState = state;
    }
}
