using ModestTree;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
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

        if (_mainScript._targets.Any())
        {
            return WayToTarget();
        }
        if (_mainScript.Obstacle != null)
        {
            return WayAwayOfObstacle(minCoords, maxCoords);
        }
        if (_mainScript.Obstacle == null && !_mainScript._targets.Any())
        {
            return RandomWay(minCoords, minCoords);
        }

        if (direction == Vector3.zero)
        { Debug.Log("error direction is zero"); }
        return direction;
    }
    private Vector3 WayToTarget()
    {
        float currentDistance = 0;
        Vector3 direction = Vector3.zero ;

        foreach (var target in _mainScript._targets)
        {
            float distance = Vector3.Distance(target.transform.position, transform.position);
            if (distance < currentDistance || currentDistance == 0)
            {
                currentDistance = distance;
                direction = new(target.transform.position.x, transform.position.y, target.transform.position.z);
            }
        }

        Debug.Log($"Find Way to closest target - {direction}");
        return direction;

    }
    private Vector3 WayAwayOfObstacle(Vector3 min, Vector3 max)
    {
        Vector3 obstacle = _mainScript.Obstacle.transform.position;
        Vector3 distance = transform.position - obstacle;
        float xCoord;
        float zCoord;

        if (distance.x > 0 )
        { xCoord = Random.Range(transform.position.x + 1, max.x); }
        if (distance.x < 0 )
        { xCoord = Random.Range(min.x, transform.position.x - 1); }
        else 
        {
            if (obstacle.x < 0)
            { xCoord = Random.Range(transform.position.x + 1, max.x);}
            else { xCoord = Random.Range(min.x, transform.position.x - 1); }
        }
        if (distance.z > 0)
        { zCoord = Random.Range(transform.position.z + 1, max.z); }
        if (distance.z > 0)
        { zCoord = Random.Range(min.z, transform.position.z - 1); }
        else 
        { 
            if (obstacle.z < 0)
            { zCoord = Random.Range(transform.position.z + 1, max.z);}
            else { zCoord = Random.Range(min.z, transform.position.z - 1); }
        }

            Vector3 direction = new Vector3(xCoord, transform.position.y, zCoord);

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

    private float RandomCoords(float min, float max)
    {
        return Random.Range(min, max);
    }


}
