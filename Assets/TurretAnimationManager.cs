using UnityEngine;

public class TurretAnimationManager : MonoBehaviour
{
    [SerializeField]private TurretEnemy _turret;
    // Start is called before the first frame update

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
