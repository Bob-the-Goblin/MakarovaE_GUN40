using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class StartPoint : MonoBehaviour
{
    [SerializeField]
    private Object _prefabOfBall;
    [SerializeField]
    private float _timeBeforeDestroy;

    [SerializeField]
    private Object _arrovPrefab;
    private Transform _transform;
    private Object _actualBall;

    private void Awake()
    {
        _transform = GetComponent<Transform>();
    }
    void Start()
    {
        _actualBall = Instantiate(_arrovPrefab, _transform.position + new Vector3(0, 0, 0.5f), transform.rotation);
        //DragAndDropBall  ball = _actualBall.GetComponent<DragAndDropBall>();
        //ball.enabled = false;
        Instantiate(_prefabOfBall, _transform.position, _transform.rotation);
    }
    private void Update()
    {
        
    }




}

