using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretEnemy : Enemy
{  
    public override void Awake()
    {
        base.Awake();
    }
    public override void Execute()
    {
        base.Execute();               
    }
    protected override void SetAnimatorIdleState(bool value)
    {
        base.SetAnimatorIdleState(value);
        MyAnimator.SetBool("Attack", value);
        MyAnimator.SetBool("Idle", !value);
    }
    public void AttackLeftGun()
    {
        Attack(_weapons[0]);
    }
    public void AttackRightGun()
    {
        Attack(_weapons[1]);
    }

    public override void Attack(Transform weapon) 
    {
        base.Attack(weapon);
        _tempProjectile = Instantiate(_projectile, weapon.position, Quaternion.identity);
        _tempRigidbody = _tempProjectile.GetComponent<Rigidbody>();
        _tempRigidbody.AddForce((_player.transform.position - transform.position).normalized * _projectileSpeed, ForceMode.Impulse);
        Destroy(_tempProjectile, 2.0f);
    }
}
