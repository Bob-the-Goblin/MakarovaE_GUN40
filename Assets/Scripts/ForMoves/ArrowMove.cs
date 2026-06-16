using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowMove : MonoBehaviour
{
    private Transform _transform;
    private float _rotation;

    private void Awake()
    {
        _transform = transform;
    }
    void Update()
    {
        _rotation = Input.GetAxis("Mouse X");
        _transform.Rotate(Vector3.up, _rotation, Space.World);
    }
}
