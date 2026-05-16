using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTopDownController : MonoBehaviour
{
    [SerializeField]
    float _speed;
    private Controls _controls;
    private Transform _transform;
    void Awake()
    {
        _controls = new Controls();
        _transform = GetComponent<Transform>();
    }

    private void OnEnable()
    {
        _controls.Enable();
    }
    private void OnDisable()
    {
        _controls.Disable();
    }

    void Update()
    {
        Vector2 value = _controls.Player.Move.ReadValue<Vector2>();
        Vector3 vector = value;
        Move(value);
    }
    private void Move(Vector3 value)
    {
        _transform.Translate(value * _speed * Time.deltaTime);
    }
}
