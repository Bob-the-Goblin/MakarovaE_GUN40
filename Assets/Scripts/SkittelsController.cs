using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkittelsController : MonoBehaviour
{

    private bool _IsFalled;
    public bool IsFalled
    { get { return _IsFalled; } }

    public delegate void EventContainer();
    public event EventContainer OnSkittleFall;

    private Vector3 _startPos;
    private Quaternion _startQuaternion;

    
    
   
    private void Awake()
    { 
        _startPos = transform.position;
        _startQuaternion = transform.rotation;
        _IsFalled = false;
    }
    private void OnTriggerEnter(Collider other)
    {if (other.GetType() == typeof(MeshCollider) && !_IsFalled)
        {
            _IsFalled = true;
            OnSkittleFall();
        }
    }

    public void ResetSkittle()
    {
        transform.position = _startPos;
        transform.rotation = _startQuaternion;
        _IsFalled = false;
    }

    public void TransferSkittles()
    { 
        transform.position = _startPos + new Vector3(12, 0, 0);
        transform.rotation = _startQuaternion;
    }

}
