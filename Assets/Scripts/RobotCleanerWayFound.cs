using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotCleanerWayFound : MonoBehaviour
{
    private RobotCleaner _mainScript;
    private RobotCleanerSettings _settings;

    private float _distance;

    private void Awake()
    {
        _mainScript = GetComponent<RobotCleaner>();
        _settings = GetComponent<RobotCleanerSettings>();
        _distance = _settings.DistanceOfMove;
    }
    public Vector3 FindWay()
    {
        Vector3 direction = Vector3.zero;

        Vector3 maxCoords = transform.position + new Vector3(_distance, 0f, _distance);
        Vector3 minCoords = transform.position - new Vector3(_distance, 0f, _distance);

        if (_mainScript._target != null)
        {
            return FindTarget();
        }
        if (_mainScript._obstacle != null)
        {
            return WayAwayOfObstacle(minCoords, maxCoords);
        }
        if (_mainScript._obstacle == null && _mainScript._target == null)
        {
            return RandomWay(minCoords, minCoords);
        }

        if (direction == Vector3.zero)
        { Debug.Log("error direction is zero"); }
        return direction;
    }

    private Vector3 FindTarget()
    {
        Vector3 target = _mainScript._target.transform.position;
        Vector3 direction = new (target.x, transform.position.y, target.z);
        _mainScript._target = null;
        
        Debug.Log($"Find Way to target - {direction}");

        return direction;

    }
    private Vector3 WayAwayOfObstacle(Vector3 min, Vector3 max)
    {
        Vector3 obstacle = _mainScript._obstacle.transform.position;
        Vector3 distance = obstacle - transform.position;
        float xCoord;
        float zCoord;

        if (distance.x <= 0 )
        { xCoord = Random.Range(transform.position.x, max.x); }
        else { xCoord = Random.Range(min.x, transform.position.x); }
        if (distance.z <= 0 )
        { zCoord = Random.Range(transform.position.z, max.z); }
        else { zCoord = Random.Range(min.z, transform.position.z); }

        Vector3 direction = new Vector3(xCoord, transform.position.y, zCoord);
        _mainScript._obstacle = null;

        Debug.Log($"Find way away of obstacle - {direction} - {obstacle}");

        return direction;

    }
    private Vector3 RandomWay(Vector3 min, Vector3 max)
    {
        
        float xCoord = Random.Range(min.x, max.x);
        float zCoord = Random.Range(min.z, max.z);

        Vector3 direction = new(xCoord, transform.position.y, zCoord);

        Debug.Log($"Find Random way - {direction}");

        return direction;

    }


}
