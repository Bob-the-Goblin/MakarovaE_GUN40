using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFieldOfView : MonoBehaviour
{
    [SerializeField]
    private  Transform _target;


    [SerializeField]
    private float _radius;
    [SerializeField, Range(0f, 180f)]
    private float _angel;
    private Vector3 _reviewCenter;


    [SerializeField]
    private LayerMask _playerMask;

    private void Awake()
    {
        _reviewCenter = transform.position;
    }
    private void Start()
    {
        StartCoroutine(CoroutinFoV());
    }


    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        
        _reviewCenter = transform.position;

        Gizmos.DrawWireSphere(_reviewCenter, _radius);

        Vector3 viewAngel1 = DirectionFromAngel(transform.eulerAngles.y, -_angel / 2);
        Vector3 viewAngel2 = DirectionFromAngel(transform.eulerAngles.y, _angel / 2);

        Gizmos.color = Color.blue;

        Gizmos.DrawLine(transform.position, transform.position + viewAngel1 * _radius);
        Gizmos.DrawLine(transform.position, transform.position + viewAngel2 * _radius);

    }

    private IEnumerator CoroutinFoV()
    {
        WaitForSeconds wait = new WaitForSeconds(0.2f);

        while (true)
        {
            yield return wait;
            OnCheckFieldOfView();
        }


    }
    private void OnCheckFieldOfView()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, _radius, _playerMask);

        if (colliders.Length != 0)
        { 
            Transform target = colliders[0].transform;
            Vector3 directionToTarget = (target.position - transform.position).normalized;
            RaycastHit hit;

            if (Vector3.Angle(transform.forward, directionToTarget) < _angel / 2)
            {
                if (Physics.Raycast(transform.position, directionToTarget, out hit, _radius))
                {
                    if (hit.transform == _target)
                    {
                        Debug.Log($"{this.gameObject.name} see player");
                    }
                }
            }
        
        }

    }

    private Vector3 DirectionFromAngel(float euler, float angelInDegrees)
    {
        angelInDegrees += euler;

        return new Vector3(Mathf.Sin(angelInDegrees * Mathf.Deg2Rad), 0 , Mathf.Cos(angelInDegrees * Mathf.Deg2Rad));
    }

}
