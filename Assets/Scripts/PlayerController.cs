using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody _rb;
    [SerializeField]
    private int _speed;
    [SerializeField]
    private int _powerOfJump;
    private bool _isOnGround;



    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        if(Input.GetKey(KeyCode.W))
        { 
            Vector3 _movement = Vector3.forward * _speed * Time.fixedDeltaTime;
            _rb.MovePosition(_rb.position + _movement);
        }
        if (Input.GetKey(KeyCode.S))
        {
            Vector3 _movement = Vector3.back * _speed * Time.fixedDeltaTime;
            _rb.MovePosition(_rb.position + _movement);
        }
        if (Input.GetKey(KeyCode.A))
        {
            Vector3 _movement = Vector3.left * _speed * Time.fixedDeltaTime;
            _rb.MovePosition(_rb.position + _movement);
        }
        if (Input.GetKey(KeyCode.D))
        {
            Vector3 _movement = Vector3.right * _speed * Time.fixedDeltaTime;
            _rb.MovePosition(_rb.position + _movement);
        }

        if (Input.GetKey(KeyCode.Space) && _isOnGround )
        {
            _rb.AddForce(Vector3.up * _powerOfJump, ForceMode.Impulse);
        }

    }
    private void OnCollisionEnter()
    {
        _isOnGround = true;
    }
    private void OnCollisionExit()
    {
        _isOnGround = false;
    }
}
