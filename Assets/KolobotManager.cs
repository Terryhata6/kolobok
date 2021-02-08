using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KolobotManager : MonoBehaviour
{
    private PlayerController _player;
    [SerializeField]private MeshRenderer[] _renderList;
    [SerializeField] bool _rendererState = true;
    // Start is called before the first frame update
    private void Awake()
    {
        _player = FindObjectOfType<PlayerController>();
        _renderList = GetComponentsInChildren<MeshRenderer>();
        SetRenderersState(_rendererState);
    }

    public void SetRenderersState(bool state)
    {
        foreach (MeshRenderer renderer in _renderList)
        {
            renderer.enabled = state;
        }
        _rendererState = state;
    }

    public void TurnStatesSpin()
    {
        Debug.Log("TurnStateSpin");
        _player.ChangeSpinningState(true);
    }
    public void TurnStatesToIdle()
    {
        Debug.Log("TurnStateToIdle");
        _player.ChangeSpinningState(false);
    }


}
