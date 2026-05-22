using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFieldOfView : MonoBehaviour
{
    [SerializeField]
    private  Transform _transform;
    [SerializeField]
    private float _viewingRadius;
    [SerializeField]
    private Vector3 _reviewCenter;

    private void Awake()
    {
        
        
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        
        _transform = GetComponent<Transform>();
        _reviewCenter = _transform.position;

        Gizmos.DrawWireSphere(_reviewCenter, _viewingRadius);
    }
}
