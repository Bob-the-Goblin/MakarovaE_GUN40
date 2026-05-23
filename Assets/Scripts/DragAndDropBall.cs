using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragAndDropBall : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    private Transform _transform;
    private Rigidbody _rb;
    private Vector3 _mousePos;
    private void Awake()
    {
        _transform = transform;
        _rb = GetComponent<Rigidbody>();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        
    }

    public void OnDrag(PointerEventData eventData)
    {
        
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        Debug.Log("End");
    }

    private Vector3 GetMousePos()
    { return Camera.main.WorldToScreenPoint(transform.position); }
}
