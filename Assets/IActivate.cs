using UnityEngine;

public interface IActivate
{
    void OnEnable();

    void OnDisable();

    Transform GetTransform();
    
}
