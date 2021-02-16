using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainController : MonoBehaviour
{
    [SerializeField] private PlayerController _player;
    [SerializeField] private CameraController _cameraController;
    [SerializeField] private InputController _inputController; //главный инпут контроллер
    [SerializeField] private EnemyController _enemyController; //главный инпут контроллер
    private List<IExecute> _executes = new List<IExecute>();

    private void Awake()
    {
        _player = FindObjectOfType<PlayerController>();
        if (_player != null)
        {
            _executes.Add(_player);
        }
        else
        {
            Debug.LogError("Player not found");
        }        
        _cameraController = FindObjectOfType<CameraController>();
        if (_cameraController != null)
        {
            _executes.Add(_cameraController);
            _cameraController.SetPursuedObject(_player.gameObject);
        }
        else
        {
            Debug.LogError("Camera not found");
        }           
        _inputController = FindObjectOfType<InputController>();
        if (_inputController != null)
        {
            _executes.Add(_inputController);
        }
        else
        {
            Debug.LogError("InputController not found");
        }
        _enemyController = FindObjectOfType<EnemyController>();
        if (_enemyController != null)
        {
            _executes.Add(_enemyController);
        }
        else
        {
            Debug.LogError("EnemyController not found");
        }

    }

    private void Start()
    {
        
        
    }

    // Update is called once per frame
    private void Update()
    {
        foreach (IExecute exe in _executes)
        {
            exe.Execute();
        }
    }

    public void AddExecute(IExecute exe)
    {
        _executes.Add(exe);
    }

    public void RemoveExecute(IExecute exe)
    {
        _executes.Remove(exe);
    }
}
