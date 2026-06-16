using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class DragAndDropBall : MonoBehaviour
{
    private Transform _transform;
    private Rigidbody _rb;
    private Vector3 _mousePos;

    [SerializeField]
    private float _forcePower;
    
    bool _itWasDragging;
    public delegate void EventHandler();
    public event EventHandler OnBallWasDrag;

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
        if (!_itWasDragging)
        {
           _rb.AddForce(-Camera.main.ScreenToWorldPoint(Input.mousePosition + _mousePos) * _forcePower);
        }
    }
    private void OnMouseUp()
    {
        if (!_itWasDragging)
        { _itWasDragging = true;
            OnBallWasDrag?.Invoke();
        }
    }
    private Vector3 GetMousePos()
    { 
        return Camera.main.WorldToScreenPoint(_transform.position); 
    }

    private void OnTriggerExit(Collider other)
    {
        if (other is MeshCollider)
        { 
            _rb.velocity = Vector3.zero;
        }
    }
}
