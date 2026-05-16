using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Follow : MonoBehaviour
{
    [SerializeField]
    private Transform _follow;
    private Transform _transform;
    void Start()
    {
        _transform = GetComponent<Transform>();
    }
    void Update()
    {
        
    }
}
