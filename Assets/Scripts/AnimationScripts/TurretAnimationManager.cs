using UnityEngine;

public class TurretAnimationManager : MonoBehaviour
{
    [SerializeField]private TurretEnemy _turret;
    private void Awake()
    {
       
    }
    public void AttackRightGun()
    {        
        _turret.AttackRightGun();
    }
    public void AttackLeftGun()
    {        
        _turret.AttackLeftGun();
    }
}
