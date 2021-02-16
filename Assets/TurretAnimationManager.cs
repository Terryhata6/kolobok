using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretAnimationManager : MonoBehaviour
{
    private TurretEnemy _turret;
    // Start is called before the first frame update

    private void Awake()
    {
        _turret = FindObjectOfType<TurretEnemy>();
    }

    public void AttackRightGun()
    {
        Debug.Log("Выстрел правой");
        _turret.AttackRightGun();
    }

    public void AttackLeftGun()
    {
        Debug.Log("Выстрел левой");
        _turret.AttackLeftGun();
    }

}
