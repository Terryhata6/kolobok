using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    [SerializeField] protected Transform _myHeadTrransform;
    [SerializeField] protected Transform[] _weapons;
    [SerializeField] protected GameObject _projectile;
    [SerializeField] [Range(.1f, 1.0f)] protected float _cooldownAttackTime;
    [SerializeField] protected float _visibilityDistance;
    protected bool _attackReady = true;
    protected bool _playerInTarget = false;
    protected bool _needCountDistance = true;
    protected bool _lookAtPlayer = true;
    protected bool _isDead = false;
    protected float _distanceToPlayer;
    protected PlayerController _player;
    protected Animator _animator;
    protected EnemyController _enemyController;



    public bool PlayerInTarget 
    {
        get => _playerInTarget;
    }
    public bool IsDead
    {
        get => _isDead;
        set => _isDead = value;
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
        
    }

    public virtual void Attack(){ }

}
