using System.Collections;
using System.Collections.Generic;

using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour, IExecute
{
    [SerializeField] private KolobotManager _idlePrefab;
    [SerializeField] private SpinningBotManager _runnerPrefab;
    [SerializeField] private Transform _rotateParentTransform;
    [SerializeField] private Transform _leftWeapon;
    [SerializeField] private Transform _rightWeapon;
    [SerializeField] private GameObject _playersProjectile;
    [SerializeField] private GameObject _pursuedEnemy;
    [SerializeField] private GameObject _head;
    [SerializeField] [Range(1.0f, 10.0f)] private float _sensetivity = 1.0f;
    [SerializeField] [Range(1.0f, 1000.0f)] private float _speedModifyer = 1.0f;
    [SerializeField] [Range(2.0f, 100.0f)] private float _smoothRange = 5.0f;
    [SerializeField] [Range(1.0f, 10.0f)] private float _weaponPower = 3.0f;
    [SerializeField] [Range(1.0f, 10.0f)] private float _attackDistance = 5.0f;
    [SerializeField]private PlayerState _playerState = PlayerState.Idle;


    private Vector3 _movingVector;
    private Rigidbody _rig;
    private Animator _animatorIdle;
    private Animator _animatorRunner;
    private InputController _inputController;
    private JoystickController _input;
    private float _smoothAnimation;
    private float _animationBlend;
    private bool _delayCounted;
    private bool _stateSpinning = false;
    private float _maxDegreesDelta = 2.0f;
    private Vector3 _tempVector;
    private GameObject _tempProjectile;
    private float _currentRotationY;
    private float _fromToEnemyRotationY;
    [SerializeField] private float _rotationDifference;
    [SerializeField] private bool _rotateHeadToTarget = false;

    public float AttackDistance
    {
        get => _attackDistance;
    }

    #region Unity
    private void Awake()
    {
        _rig = GetComponent<Rigidbody>();
        _inputController = FindObjectOfType<InputController>();
        _animatorIdle = _idlePrefab.GetComponent<Animator>();
        _animatorRunner = _runnerPrefab.GetComponent<Animator>();
        _input = FindObjectOfType<JoystickController>();
    }

    void Start()
    {
        _runnerPrefab.SetRenderersState(false);
    }

    /// <summary>
    /// executable in one update
    /// </summary>
    public void Execute()
    {
        if (_playerState != PlayerState.Spinning)
        {
            if (_pursuedEnemy == null)
            {
                _rotateHeadToTarget = false;
                SetPlayerState(PlayerState.Idle);
            }            
        }
        switch (_playerState)
        {
            case PlayerState.Spinning:
                {
                    break;
                }
            case PlayerState.Idle:
                {
                    if (_pursuedEnemy != null)
                    {
                        SetPlayerState(PlayerState.RotateToAttack);
                    }
                    else
                    {
                        _head.transform.rotation = _rotateParentTransform.rotation;
                    }
                    break;
                }
            case PlayerState.RotateToAttack:
                {
                    _rotateHeadToTarget = true;
                    RotateDifference();
                    if (_rotationDifference < 3.0f)
                    {
                        _playerState = PlayerState.Attack;
                    }
                    else
                    {
                        
                    }                    
                    break;
                }
            case PlayerState.Attack:
                {
                    LookHeadAtEnemy();
                    
                    _animatorIdle.SetBool("Attack", true);
                    break;
                }
            default: break;
        }
        if (_playerState != PlayerState.Attack)
        {
            _animatorIdle.SetBool("Attack", false);
        }
    }

    public void LateUpdate()
    {
        if (_rotateHeadToTarget)
        {            
            //RotateHeadToTarget();
            LookHeadAtEnemy();
        }
    }

    private void LookHeadAtEnemy()
    {
        if (_pursuedEnemy != null)
        { 
        _tempVector.x = _pursuedEnemy.transform.position.x;
        _tempVector.y = _head.transform.position.y;
        _tempVector.z = _pursuedEnemy.transform.position.z;
        _head.transform.LookAt(_tempVector);
        }
    }

    /// <summary>
    /// Change current pursued object
    /// </summary>
    /// <param New object="newPursuedObject"></param>
    public void ChangePursuedObject(GameObject newPursuedObject) 
    {
        if (newPursuedObject != null)
        {
            _pursuedEnemy = newPursuedObject;
            _playerState = PlayerState.RotateToAttack;
        }        
    }

    public void ChangePursuedObject(Enemy enemy)
    {
        ChangePursuedObject(enemy.gameObject);
    }

    public void SetPursedObjectNull()
    {
        _pursuedEnemy = null;
    }

    public void FixedUpdate()
    {
        OnMovement();
    }
    #endregion    

    /// <summary>
    /// Absolette
    /// RotateToward head to target position
    /// </summary>
    private void RotateHeadToTarget()
    {
        _head.transform.rotation = Quaternion.RotateTowards(_head.transform.rotation, Quaternion.LookRotation(_pursuedEnemy.transform.position - transform.position, Vector3.up), _maxDegreesDelta);
    }

    /// <summary>
    /// Return RotationDifference between player _rotateParentTransform.rotation and rotation to _pursuedEnemy
    /// </summary>
    /// <returns></returns>
    private float RotateDifference()
    {
        _fromToEnemyRotationY = Quaternion.LookRotation( _pursuedEnemy.transform.position - transform.position, Vector3.up).eulerAngles.y;
        _currentRotationY = _head.transform.rotation.eulerAngles.y;
        _rotationDifference = System.Math.Max(_fromToEnemyRotationY, _currentRotationY) - System.Math.Min(_fromToEnemyRotationY, _currentRotationY);
        if (_rotationDifference > 180)
        {
            _rotationDifference = 360 - _rotationDifference;
        }                
        return _rotationDifference;
    }

    private void OnMovement()
    {
        _movingVector = _input.GetDirection();

        if (_input.TouchStart)
        {
            SetPlayerState(PlayerState.Spinning);
            _animatorIdle.SetBool("Spinning", true);

            _rig.AddForce(_movingVector * _speedModifyer * Time.fixedDeltaTime);

            _rotateParentTransform.rotation = Quaternion.RotateTowards(_rotateParentTransform.rotation, Quaternion.LookRotation(_movingVector), 30.0f);
            _smoothAnimation = _inputController.Magnitude;
            _animationBlend = _smoothAnimation / _smoothRange;
            if (_animationBlend > 1.0f)
            {
                _animationBlend = 1.0f;
            }
            _animatorRunner.SetFloat("Blend", _animationBlend);
            Debug.Log($"Blend:{_animationBlend}");
        }
        else
        {
            _delayCounted = false;
            _animatorIdle.SetBool("Spinning", false);
        }

    }
    private void Attack(Transform weapon)
    {
        if (_pursuedEnemy != null)
        {
            if (_playersProjectile != null)
            {
                _tempProjectile = Instantiate(_playersProjectile, weapon.position, Quaternion.identity);
                _tempProjectile.GetComponent<Rigidbody>().AddForce((_pursuedEnemy.transform.position - weapon.transform.position).normalized * _weaponPower, ForceMode.Impulse);
                Destroy(_tempProjectile, 2.0f);
            }
            else
            {
                Debug.LogError("Player Projectile prefab is missing");
            }
        }
    }

    public void ChangeSpinningState(bool State)
    {
        _stateSpinning = State;
        if (!State)
        {
            _idlePrefab.SetRenderersState(true);
            _runnerPrefab.SetRenderersState(false);
        }
        else
        {
            _idlePrefab.SetRenderersState(false);
            _runnerPrefab.SetRenderersState(true);
        }
    }
    public void SetPlayerState(PlayerState state)
    {
        _playerState = state;
    }
    public void AttackLeftWeapon()
    {
        Attack(_leftWeapon);
    }
    public void AttackRightWeapon()
    {
        Attack(_rightWeapon);
    }

    public GameObject PursuedObject
    {
        get => _pursuedEnemy;
    }

    
}