using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BullAnimatorManager : MonoBehaviour
{
    [SerializeField]private BullEnemy _bullEnemy;  
    private void Awake(){ }

    public void AttackRightGun()
    {        
        _bullEnemy.AttackRightGun();
    }

    public void AttackLeftGun()
    {        
        _bullEnemy.AttackLeftGun();
    }
}

    


