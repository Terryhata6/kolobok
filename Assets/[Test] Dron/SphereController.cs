using UnityEngine;

[RequireComponent (typeof(Rigidbody))]
public class SphereController : MonoBehaviour
{
    [SerializeField] private float _speed;

    [SerializeField] private float _horAxis;
    [SerializeField] private float _verAxis;

    private Vector3 _moveVector;
    private Rigidbody _rigidbody;

    private string _horizontal = "Horizontal";
    private string _vertical = "Vertical";


    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        _horAxis = Input.GetAxis(_horizontal);
        _verAxis = Input.GetAxis(_vertical);

        _moveVector = new Vector3(Input.GetAxis(_horizontal), 0, Input.GetAxis(_vertical));
        _rigidbody.AddForce(_moveVector * _speed * Time.fixedDeltaTime);
    }
}
