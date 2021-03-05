using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseActivatedObject : MonoBehaviour, IActivate
{

    public Transform GetTransform()
    {
        return transform;
    }

    public void OnDisable()
    {

    }

    public void OnEnable()
    {

    }


    void Start()
    {

    }


    void Update()
    {

    }
}
