using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotCleanerMovement : MonoBehaviour
{
    private RobotCleaner _mainScript;
    private RobotCleanerWayFound _wayFound;
    private RobotCleanerSettings _settings;
    
    private float _time = 0f;
    private float _duration = 3f;

    private Vector3 _direction;

    void Awake()
    {
        _mainScript = GetComponent<RobotCleaner>();
        _wayFound = GetComponent<RobotCleanerWayFound>();
        _settings = GetComponent<RobotCleanerSettings>();
    }

    private void FixedUpdate()
    {
        if (!_settings.HaveWay)
        {
            _direction = _wayFound.FindWay();
            _settings.HaveWay = true;
            
        }
        
        if (_settings.HaveWay && !_settings.InMove)
        {
            if (_direction == null || _direction == Vector3.zero)
            { Debug.Log("Error. current direction is null"); }
            else
            {
                _settings.InMove = true;
            }
        }
        if (_settings.InMove == true && transform.position != _direction)
        {
            if (_time < _duration)
            {
                _time += Time.deltaTime;
                float t = _time / _duration;
                transform.position = Vector3.Lerp(transform.position, _direction, t * _settings.Speed);

                if (!_settings.InMove)
                {
                    Debug.Log("Obstacle");
                    _time = 0;
                    _settings.InMove = false;
                    _settings.HaveWay = false;
                    return;
                }
            }
            else
            {
                Debug.Log("EndMove");
                _settings.InMove = false;
                _settings.HaveWay = false;
                _time = 0;
            }
        }
    }
}
