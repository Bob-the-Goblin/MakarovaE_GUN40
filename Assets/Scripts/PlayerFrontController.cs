using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
   
    [SerializeField]
    private int _speed;
    [SerializeField]
    private float _jumpPower;
    private Rigidbody2D _rb;
    private Collider2D _collider;
    private Controls _controls;
    private bool _isOnGround;

    private void Awake()
    {
        _controls = new  Controls();
        _controls.PlayerFront.Jump.performed += context => Jump();
      
    }
    private void OnEnable()
    {
        _controls.Enable();
    }
    private void OnDisable()
    {
        _controls.Disable();
    }
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();  
        _collider = GetComponent<Collider2D>();
    }

    private void Update()
    {
        Vector2 value = _controls.PlayerFront.Move.ReadValue<Vector2>();
        Move(value);
    }
    private void Jump()
    {
        if (_isOnGround == true)
        {
            Debug.Log("Jump");
            Vector2 _jump = new Vector2(_rb.velocity.x, 2) * _jumpPower;
            _rb.velocity += _jump;
            _isOnGround = false;
        }
    }
    private void Move(Vector2 value)
    {
        _rb.velocity = new Vector2(value.x * _speed, _rb.velocity.y);

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        _isOnGround = true;
    }
}
