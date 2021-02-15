using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BullEnemy : Enemy
{
    [SerializeField]protected float _AttackDistance = 3;
    [SerializeField]protected float _CloseDistance = 2;
    [SerializeField] protected float _movementSpeed = 3;
    [SerializeField] protected Rigidbody _rig;
    protected Vector3 _movementVector;
    protected BullState _state = BullState.Idle;
    protected bool _pursue = false;
    protected float _oldRotationY;

    private float _verticalBlend = 0;

    public override void Awake()
    {
        base.Awake();
        _rig = GetComponent<Rigidbody>();
        _oldRotationY = transform.rotation.eulerAngles.y;
    }

    public override void Execute() 
    {
        base.Execute();
        if (_distanceToPlayer < _AttackDistance)
        {
            _animator.SetBool("Attack", true);
        }
        else 
        {
            _animator.SetBool("Attack", false);
        }
        if (_distanceToPlayer >= _CloseDistance && _distanceToPlayer <= _AttackDistance)
        {
            SetAnimatorIdleState(false);
            _movementVector = _player.transform.position - transform.position;
            _rig.AddForce(_movementVector.normalized * Time.deltaTime * _movementSpeed, ForceMode.Acceleration);
        }
        else if (_distanceToPlayer < _CloseDistance)
        {
            SetAnimatorIdleState(false);
        }
        
    }


    protected override void OnBodyRotation()
    {
        base.OnBodyRotation();
        if (transform.rotation.eulerAngles.y > _oldRotationY)
        {
            _differenceSideIsLeft = false;
            _verticalBlend = 0.5f;
        }
        else
        {
            _differenceSideIsLeft = true;
            _verticalBlend = -0.5f;
        }        
        _oldRotationY = transform.rotation.eulerAngles.y;
        _animator.SetFloat("VerticalRota", _verticalBlend);
        
    }

    protected override void OnBodyNotRotation()
    {
        base.OnBodyNotRotation();
    }

    public override void SetAnimatorIdleState(bool value) 
    {
        base.SetAnimatorIdleState(value);
        _animator.SetBool("Idle", !value);
        _animator.SetBool("Pursue", value);
        _pursue = value;
    }


}
