using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Moverr : MonoBehaviour
{
    public Vector3 _start;
    public Vector3 _end;
    public float _speed;
    public float _delay;
    
    private IEnumerator Start()
    {
        Rigidbody rb = GetComponent<Rigidbody>();
        float _time = 0f;
        float _moveTime = 1f;
        while (true)
        {
            
            rb.MovePosition(Vector3.Lerp(_start, _end, _time / _moveTime));
            _time += Time.fixedDeltaTime * _speed;
            yield return new WaitForFixedUpdate();

            if (rb.position == _end)
            {   
                yield return new WaitForSeconds(_delay);
                _time = 0f;
                _end = _start;
                _start = rb.position;
                
            }
            
        }
    }
  

}
