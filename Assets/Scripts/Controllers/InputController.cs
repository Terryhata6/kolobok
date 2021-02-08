using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputController : MonoBehaviour, IExecute
{
	public Vector3 TouchPosition;
	[SerializeField]private bool UseMouse = false;
	[SerializeField]private Camera CameraForInput;

	private bool _dragingStarted = false;	
	private Touch touch;
	[SerializeField] private Vector3 _targetVector;
	private Vector3 _startTouchPosition;


	public Vector3 GetTargetVector
	{
		get => _targetVector;
	}

	public bool DragingStarted
	{
		get => _dragingStarted;
	}


	private void Start()
	{
		TouchPosition = Vector3.zero;
	}

    #region IExecute
    public void Execute()
	{
		if (!UseMouse)
		{
			if (Input.touchCount > 0)
			{
				touch = Input.GetTouch(0);
				if (touch.phase == TouchPhase.Began)
				{
					_dragingStarted = true;
					///_startTouchPosition = touch.position;
					TouchPosition = CameraForInput.ScreenToWorldPoint(touch.position);
				}
				else if (touch.phase == TouchPhase.Moved)
				{
					TouchPosition = CameraForInput.ScreenToWorldPoint(touch.position);
				}
			}
			else
			{
				_dragingStarted = false;
				
			}
		}
		else
		{
			if (Input.GetMouseButton(0))
			{
				if (DragingStarted == false)
				{
					_startTouchPosition = Input.mousePosition / 100;
				}
				_dragingStarted = true;
				TouchPosition = Input.mousePosition / 100;
				_targetVector = TouchPosition - _startTouchPosition;
				//_targetVector = _targetVector.normalized;
				
				if (_targetVector.magnitude < 100)
				{ 
					
				}
			}
			else
			{
				_dragingStarted = false;
				_startTouchPosition = Vector3.zero;
				_targetVector = Vector3.zero;
			}
		}
	}
    #endregion
    
}
