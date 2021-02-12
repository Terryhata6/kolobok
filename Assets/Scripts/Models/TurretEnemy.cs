using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretEnemy : Enemy
{
    private Vector3 viewVector;   
    // Start is called before the first frame update 
    // Update is called once per frame

    public override void Awake()
    {
        base.Awake();

    }

    public override void Execute()
    {
        base.Execute();
        if (!_isDead)
        {
            if (PlayerInTarget)
            {
                viewVector.x = _player.transform.position.x;
                viewVector.y = 0;
                viewVector.z = _player.transform.position.z;
                _myHeadTrransform.LookAt(viewVector);
                MyAnimator.SetBool("Attack", true);
                MyAnimator.SetBool("true", false);
            }
            else
            {
                MyAnimator.SetBool("Idle", true);
                MyAnimator.SetBool("Attack", false);
            }
        }        
    }

    public override void Attack() 
    { 
        
    }
}
