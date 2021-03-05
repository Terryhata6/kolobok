using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainController : MonoBehaviour
{
    [SerializeField] private PlayerController _player;
    [SerializeField] private CameraController _cameraController;
    [SerializeField] private InputController _inputController; //главный инпут контроллер
    [SerializeField] private EnemyController _enemyController; 
    [SerializeField] private JoystickController _joystickController; 
    public EnemyController EnemyController => _enemyController;
    public InputController InputController => _inputController;
    public JoystickController JoystickController => _joystickController;

    private List<IExecute > _executes = new List<IExecute>();

    private void Awake()
    {
        _player = FindObjectOfType<PlayerController>();
        if (_player != null)
        {
            AddExecute(_player);
        }
        else
        {
            Debug.LogError("Player not found");
        }
        _cameraController = FindObjectOfType<CameraController>();
        if (_cameraController != null)
        {
            AddExecute(_cameraController);
            _cameraController.SetPursuedObject(_player.gameObject);
        }
        else
        {
            Debug.LogError("Camera not found");
        }
        _inputController = new InputController();
        if (_inputController != null)
        {
            AddExecute(_inputController);
        }
        else
        {
            Debug.LogError("InputController not found");
        }
        _enemyController = new EnemyController();
        if (_enemyController != null)
        {
            AddExecute(_enemyController);
            _enemyController.SetPlayer(_player);
        }
        else
        {
            Debug.LogError("EnemyController not found");
        }
        _joystickController = new JoystickController();
        if (_joystickController != null)
        {
            AddExecute(_joystickController);
        }
        else
        {
            Debug.LogError("EnemyController not found");
        }
    }

    private void Start()
    {
        foreach (IExecute exe in _executes)
        {
            if (exe is IInitialize initialize)
            {
                initialize.Initialize();
            }
        }
    }

    private void Update()
    {
        foreach (IExecute exe in _executes)
        {
            exe.Execute();            
        }
    }

    private void FixedUpdate()
    {
        foreach (IExecute exe in _executes)
        {
            if (exe is IFixedExecute FixedExecutable)
            {
                FixedExecutable.FixedExecute();
            }
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
