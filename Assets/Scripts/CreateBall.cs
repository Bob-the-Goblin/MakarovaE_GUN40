using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class CreateBall : MonoBehaviour
{
    [SerializeField]
    private Object _prefab;
    [SerializeField]
    private float _timeBeforeDestroy;
    private Transform _transform;
    private Transform _arrow;

    private void Awake()
    {
        _transform = GetComponent<Transform>();
        _arrow = GetComponentInChildren<Transform>();
    }
    void Start()
    {
        Instantiate(_prefab, _transform.position, _transform.rotation);
    }
    private void Update()
    {
        var pos = Camera.main.ViewportToWorldPoint(Input.mousePosition);
        _arrow.LookAt(pos);
    }


}

