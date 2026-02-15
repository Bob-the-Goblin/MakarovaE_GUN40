using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Rotator : MonoBehaviour
{
    public Vector3 _rotate;
   
   private IEnumerator Start()
    {
        Quaternion quaternion = Quaternion.Euler(_rotate);
        Rigidbody _rb = GetComponent<Rigidbody>();
        Transform _tr = GetComponent<Transform>();
        while (true)
        {      
            yield return new WaitForFixedUpdate();
            _rb.MoveRotation(_rb.rotation * quaternion);
        }
    }
}
