using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventSkittlesFall : MonoBehaviour
{

    private bool _IsFalled;
    public delegate void EventContainer();
    public event EventContainer OnSkittleFall;
    
   
    private void Awake()
    { 
        _IsFalled = false;
    }
    private void OnTriggerEnter(Collider other)
    {if (other.GetType() == typeof(MeshCollider) && !_IsFalled)
        {
            _IsFalled = true;
            OnSkittleFall();
        }
    }
}
