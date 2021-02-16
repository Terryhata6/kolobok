using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BullEnemy : Enemy
{    
    [SerializeField]protected float _CloseDistance = 2;
    [SerializeField] protected float _movementSpeed = 3;
    [SerializeField] protected Rigidbody _rig;
    protected Vector3 _movementVector;
    protected BullState _state = BullState.Idle;
    protected bool _pursue = false;
    protected float _oldRotationY;

    private float _verticalBlend = 0;
    private float _movementBlend = 0;

    public override void Awake()
    {
        base.Awake();
        _rig = GetComponent<Rigidbody>();
        _oldRotationY = transform.rotation.eulerAngles.y;
    }

    public override void Execute() 
    {
        base.Execute();
        if (_distanceToPlayer < _visibilityDistance)
        {
            _animator.SetBool("Attack", true);
        }
        else 
        {
            _animator.SetBool("Attack", false);
        }
        if (_distanceToPlayer >= _CloseDistance && _distanceToPlayer <= _visibilityDistance)
        {
            SetAnimatorIdleState(true);
            _movementVector = transform.position - _player.transform.position;
            //_rig.AddForce(_movementVector.normalized * Time.deltaTime * _movementSpeed, ForceMode.Impulse);
            //_rig.AddForce(transform.forward *Time.deltaTime * 10, ForceMode.Impulse);
            //transform.Translate(_movementVector.normalized * Time.deltaTime * _movementSpeed);
            transform.Translate(transform.forward * Time.deltaTime * _movementSpeed);
            ToMovementState();
        }
        else if (_distanceToPlayer < _CloseDistance)
        {
            SetAnimatorIdleState(false);
            FromMovementState();
        }
        else if (_distanceToPlayer >= _visibilityDistance)
        {
            FromMovementState();
        }
        
    }


    protected void ToMovementState()
    {
        if (_movementBlend < 1)
        {
            _movementBlend += 0.02f;
        }
        else
        {
            _movementBlend = 1;
        }
        _animator.SetFloat("MovementBlend", _movementBlend);   
    }

    protected void FromMovementState()
    {
        if (_movementBlend > 0)
        {
            _movementBlend -= 0.01f;
        }
        else
        {
            _movementBlend = 0;
        }
        _animator.SetFloat("MovementBlend", _movementBlend);
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
        _verticalBlend = 0;
        _animator.SetFloat("VerticalRota", _verticalBlend);
    }

    public override void SetAnimatorIdleState(bool value) 
    {
        base.SetAnimatorIdleState(value);
        _animator.SetBool("Idle", !value);
        _animator.SetBool("Pursue", value);
        _pursue = value;
    }


}
