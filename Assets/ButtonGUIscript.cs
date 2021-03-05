using UnityEngine;

[ExecuteInEditMode]
public class ButtonGUIscript : MonoBehaviour
{
    [SerializeField] private FlourButton _model;
    private BaseActivatedObject[] _elements;
    private bool _elementsWasFound = false;
    void Awake()
    {
        if (_model == null)
        {
            _model = GetComponent<FlourButton>();
        }
        if (_model != null)
        {
            _elements = _model.Elements;
            if (_elements != null)
            {
                _elementsWasFound = true;
            }
        }
    }
#if UNITY_EDITOR
    void Update()
    {
        if (UnityEditor.Selection.activeObject == this.transform.gameObject)
        {
            if (_elementsWasFound)
            {
                foreach (BaseActivatedObject element in _elements)
                {
                    if (element != null)
                    {
                        Debug.DrawLine(_model.transform.position, element.transform.position, Color.red);
                    }
                }
            }
            if (_elements != _model.Elements)
            {
                _elements = _model.Elements;
                if (_elements == null)
                {
                    _elementsWasFound = false;
                }
            }
        }        
    }
#endif

    void OnGUI()
    {
        if (GUILayout.Button("Press Me"))
            Debug.Log("Hello!");
    }


}
