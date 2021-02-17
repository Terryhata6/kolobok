using UnityEngine;
using UnityEngine.UI;

public class JoystickController : MonoBehaviour
{
    [SerializeField] private GameObject _circle;
    [SerializeField] private GameObject _outerCircle;

    private Vector2 _pointA;
    private Vector2 _pointB;
    private Vector2 _offset;
    private Vector2 _direction;
    private bool _touchStart;

    public bool TouchStart
    {
        get => _touchStart;
    }

    private void Start()
    {
        _circle.GetComponent<Image>().enabled = false;
        _outerCircle.GetComponent<Image>().enabled = false;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            _pointA = new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.transform.position.z);

            _circle.transform.position = _pointA;
            _outerCircle.transform.position = _pointA;

            _circle.GetComponent<Image>().enabled = true;
            _outerCircle.GetComponent<Image>().enabled = true;
        }

        if (Input.GetMouseButton(0))
        {
            _touchStart = true;
            _pointB = new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.transform.position.z);
        }
        else
        {
            _touchStart = false;
        }
    }

    private void FixedUpdate()
    {
        if (_touchStart)
        {
            _offset = Vector2.ClampMagnitude((_pointB - _pointA), 100f);
            _direction = Vector2.ClampMagnitude(_offset, 1f);

            _circle.transform.position = new Vector2(_pointA.x + _offset.x, _pointA.y + _offset.y);
        }
        else
        {
            _circle.GetComponent<Image>().enabled = false;
            _outerCircle.GetComponent<Image>().enabled = false;
            _direction = Vector3.zero;
        }
    }

    public Vector3 GetDirection()
    {
        return new Vector3(_direction.x, 0, _direction.y);
    }
}