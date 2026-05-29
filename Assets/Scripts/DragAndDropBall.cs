using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragAndDropBall : MonoBehaviour
{

    private Transform _transform;
    private Rigidbody _rb;
    private Vector3 _mousePos;
    private Object _thisObject;


    private float _maxForce;
    //не придумала
    public float ForceThrow
    {
        get => forceThrow;
        set
        {
            if (value < _maxForce)
            { _forcePower = value; }
        }

    }
    private float _forcePower;
    private float forceThrow;
    private bool _isWasThrow;

    private void Awake()
    {
        _transform = transform;
        _rb = GetComponent<Rigidbody>();   
        _thisObject = GetComponent<Object>();
        _isWasThrow = false;
    }

    private void OnMouseDown()
    {
        _mousePos = Input.mousePosition - GetMousePos();
    }

    private void OnMouseDrag()
    {
        //transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition - _mousePos );
        _forcePower = 0.5f;
        if (!_isWasThrow)
        {
            _rb.AddForce(-Camera.main.ScreenToWorldPoint(Input.mousePosition + _mousePos) * _forcePower);
        }
    }

    private void OnMouseUp()
    {
        _isWasThrow = true;
    }





    private Vector3 GetMousePos()
    { 
        return Camera.main.WorldToScreenPoint(_transform.position); 
    }

}
