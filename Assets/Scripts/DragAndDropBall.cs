using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;
using Zenject;

public class DragAndDropBall : MonoBehaviour
{
    private Transform _transform;
    private Rigidbody _rb;
    private Vector3 _mousePos;

    [SerializeField]
    private float _forcePower;
    
    bool _itWasDragging;

    private void Awake()
    {
        _transform = GetComponent<Transform>();
        _rb = GetComponent<Rigidbody>();
        _itWasDragging = false;
    }
    private void OnMouseDown()
    {
        _mousePos = Input.mousePosition - GetMousePos();
    }
    private void OnMouseDrag()
    {
        //transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition - _mousePos );
        _forcePower = 0.5f;
        if (!_itWasDragging)
        {
            _rb.AddForce(-Camera.main.ScreenToWorldPoint(Input.mousePosition + _mousePos) * _forcePower);
        }
    }
    private void OnMouseUp()
    {
        if (!_itWasDragging)
        { _itWasDragging = true; }
    }
    private Vector3 GetMousePos()
    { 
        return Camera.main.WorldToScreenPoint(_transform.position); 
    }
    
}
