using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotCleanerSettings : MonoBehaviour
{
    [SerializeField]
    private float _speed;
    public float Speed
    { get { return _speed; } private set => _speed = value; }


    [SerializeField]
    private float _distanceOfMove;
    public float DistanceOfMove
    { get { return _distanceOfMove; } private set => _distanceOfMove = value; }


    [SerializeField]
    private float _distanceOfView;
    public float DistanceOfView
    { get { return _distanceOfView; } private set => _distanceOfView = value; }


    [SerializeField]
    private float _timeForCkeackingEnviropment;
    public float TimeForCkeackingEnviropment
    { get { return _timeForCkeackingEnviropment; } private set => _timeForCkeackingEnviropment = value; }


    [SerializeField]
    private LayerMask _layerMask;
    public LayerMask LayerMask
    { get { return _layerMask; } private set => _layerMask = value; }



    private bool _isInMove;
    public bool InMove
    { get { return _isInMove; } set { _isInMove = value; } }


    private bool _isHaveWay;
    public bool HaveWay
    { get { return _isHaveWay; } set { _isHaveWay = value; } }


    //There some Gizmos
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;

        Vector3 vectorForward = Vector3.forward * DistanceOfView;

        Gizmos.DrawLine(transform.position, transform.position + vectorForward);

        Gizmos.color = Color.red;

        Vector3 vectorRight = Vector3.right * DistanceOfView;
        Vector3 vectorLeft = Vector3.left * DistanceOfView;

        Gizmos.DrawLine(transform.position, transform.position + vectorRight);
        Gizmos.DrawLine(transform.position, transform.position + vectorLeft);

        Gizmos.color += Color.green;

        Gizmos.DrawWireSphere(transform.position, DistanceOfMove);

    }

    private void Awake()
    {
        _isHaveWay = false; _isInMove = false;
    }
}
