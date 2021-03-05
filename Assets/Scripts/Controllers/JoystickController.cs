using UnityEngine;
using UnityEngine.UI;

public class JoystickController : IExecute, IInitialize, IFixedExecute
{
    private GameObject _circle;
    private GameObject _outerCircle;
    private JoystickModel _joystickModel;

    private Vector2 _pointA;
    private Vector2 _pointB;
    private Vector2 _offset;
    private Vector2 _direction;
    private bool _touchStart;
    public bool TouchStart
    {
        get => _touchStart;
    }

    #region Constructors
    public JoystickController()
    {
        _joystickModel = GameObject.FindObjectOfType<JoystickModel>();
        _circle = _joystickModel.Circle;
        _outerCircle = _joystickModel.OuterCircle;
    }

    public JoystickController(GameObject circle, GameObject outerCircle)
    {
        _circle = circle;
        _outerCircle = outerCircle;
    }
    #endregion
    #region Initialize
    public void Initialize()
    {
        _circle.GetComponent<Image>().enabled = false;        
        _outerCircle.GetComponent<Image>().enabled = false;
    }
    #endregion
    #region Execute
    public void Execute()
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
    #endregion
    #region FixedExecute
    public void FixedExecute()
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
    #endregion

    public Vector3 GetDirection()
    {
        return new Vector3(_direction.x, 0, _direction.y);
    }
}