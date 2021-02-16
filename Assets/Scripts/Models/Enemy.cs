using System;
using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    [SerializeField] protected Transform _myHeadTransform;
    [SerializeField] protected Transform[] _weapons;
    [SerializeField] protected GameObject _projectile;
    [SerializeField] [Range(.1f, 1.0f)] protected float _cooldownAttackTime;
    [SerializeField] protected float _visibilityDistance;
    [SerializeField] protected float _projectileSpeed = 100;
    [SerializeField] protected bool _bodyHeadDifference = false;
    [SerializeField] protected float _distanceToPlayer;
   
    protected bool _rotateToHead = false;
    protected bool _attackReady = true;
    [SerializeField] protected bool _playerInTarget = false;
    protected bool _needCountDistance = true;
    protected bool _lookAtPlayer = true;
    [SerializeField] protected bool _isDead = false;
    protected bool _differenceSideIsLeft = false;
    
    protected float _rotationDifference;
    
    protected PlayerController _player;
    protected Animator _animator;
    protected EnemyController _enemyController;
    protected Vector3 viewVector;
    
    private GameObject _tempProjectile;
    private Rigidbody _tempRigidbody;


    public bool PlayerInTarget 
    {
        get => _playerInTarget;
    }    
    public Animator MyAnimator
    {
        get => _animator;
    }


    public virtual void Awake()
    {
        _animator = GetComponentInChildren<Animator>();
    }

    public virtual void Start()
    {
        _player = FindObjectOfType<PlayerController>();
        _enemyController = FindObjectOfType<EnemyController>();
        if (_enemyController != null)
        {
            _enemyController.AddEnemy(this);
        }
        else
        {
            Debug.LogError($"Can't add {this} in update list. Enemy controller not found");
        }
        
    }

    public virtual void Execute()
    {
        if (_bodyHeadDifference)
        {
            
            if (_distanceToPlayer < _visibilityDistance) 
            {
                CountRotationDifference();
                if (!_rotateToHead)
                {
                    OnBodyNotRotation();
                }
                if (_rotateToHead)
                {
                    OnBodyRotation();
                }
            }                       
        }
        if (_needCountDistance)
        {
            _distanceToPlayer = (_player.transform.position - transform.position).magnitude;
        }
        if (_distanceToPlayer < _visibilityDistance)
        {
            _playerInTarget = true;
        }
        else 
        { 
            _playerInTarget = false;
        }
        if (!_isDead)
        {
            if (_playerInTarget)
            {
                viewVector.x = _player.transform.position.x;
                viewVector.y = _myHeadTransform.position.y;
                viewVector.z = _player.transform.position.z;
                _myHeadTransform.LookAt(viewVector);
                
                //_myHeadTransform.rotation = Quaternion.RotateTowards(_myHeadTransform.rotation, Quaternion.FromToRotation(_player.transform.position, transform.position), 2.0f); 
                //transform.rotation = Quaternion.RotateTowards(_myHeadTransform.rotation, Quaternion.FromToRotation(_player.transform.position, transform.position), 2.0f); 
                
                SetAnimatorIdleState(true);                
            }
            else
            {
                SetAnimatorIdleState(false);
            }
        }        
    }

   
    protected virtual void OnBodyRotation()
    {        
        transform.rotation = Quaternion.RotateTowards(transform.rotation, _myHeadTransform.rotation, 0.5f);
        if (_rotationDifference < 3) 
        {
            transform.rotation = _myHeadTransform.rotation;
            _rotateToHead = false;
        }
    }

    protected virtual void OnBodyNotRotation()
    {
        if (_rotationDifference > 30)
            _rotateToHead = true;
    }

    /// <summary>
    /// Count difference between Body and Head rotations
    /// </summary>
    protected virtual void CountRotationDifference()
    {
        _rotationDifference = Math.Max(_myHeadTransform.rotation.eulerAngles.y, transform.rotation.eulerAngles.y) - 
            Math.Min(_myHeadTransform.rotation.eulerAngles.y, transform.rotation.eulerAngles.y);
        if (_rotationDifference > 180)
            _rotationDifference = 360 - _rotationDifference;
        
    }


    public virtual void SetAnimatorIdleState(bool value){}

    public virtual void Attack(Transform weapon){
        _tempProjectile = Instantiate(_projectile, weapon.position, Quaternion.identity);
        _tempRigidbody = _tempProjectile.GetComponent<Rigidbody>();
        _tempRigidbody.AddForce((_player.transform.position - transform.position).normalized * _projectileSpeed, ForceMode.Impulse);
        Destroy(_tempProjectile, 2.0f);
    }

}
