using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretEnemy : Enemy
{
    
    
    // Start is called before the first frame update 
    // Update is called once per frame

    public override void Awake()
    {
        base.Awake();

    }

    public override void Execute()
    {
        base.Execute();
               
    }
    public override void SetAnimatorIdleState(bool value)
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
        
    }
}
