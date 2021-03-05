using UnityEngine;

public class JoystickModel : MonoBehaviour
{
    [SerializeField] private GameObject _circle;
    [SerializeField] private GameObject _outerCircle;

    public GameObject Circle => _circle;
    public GameObject OuterCircle => _outerCircle;
}
