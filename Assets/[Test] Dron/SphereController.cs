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
        //_horAxis = Input.GetAxis(_horizontal);
        //_verAxis = Input.GetAxis(_vertical);

        //_moveVector = new Vector3(Input.GetAxis(_horizontal), 0, Input.GetAxis(_vertical));
        //_rigidbody.AddForce(_moveVector * _speed * Time.fixedDeltaTime);

        if (Input.GetKey(KeyCode.A)) _horAxis = -1f;
        else if (_horAxis < 0) _horAxis = 0;
        if (Input.GetKey(KeyCode.D)) _horAxis = 1f;
        else if (_horAxis > 0) _horAxis = 0;
        if (Input.GetKey(KeyCode.S)) _verAxis = -1f;
        else if (_verAxis < 0) _verAxis = 0;
        if (Input.GetKey(KeyCode.W)) _verAxis = 1f;
        else if (_verAxis > 0) _verAxis = 0;

        _moveVector = new Vector3(_horAxis, 0, _verAxis);
        _rigidbody.AddForce(_moveVector * _speed * Time.fixedDeltaTime);
    }
}
