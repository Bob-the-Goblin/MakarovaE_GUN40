using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class RobotCleaner : MonoBehaviour
{
    private RobotCleanerSettings _settings;
 
    //Vectors for Physics.Raycast
    private Vector3[] vectors = new Vector3[3] {Vector3.forward, Vector3.right, -Vector3.right};

    public List<Collider> _targets;
    
    private Collider[] _colliders;
    

    public Transform Obstacle
    { get { return _obstacle; } set => _obstacle = value; }
    private Transform _obstacle;


  
    private IEnumerator CheackEnviropment()
    {
        RaycastHit hit;
        GameObject obj;

        while (true)
        {
            Debug.Log("Cheaking");

            foreach (Vector3 vector in vectors)
            {
                if (Physics.Raycast(transform.position, vector, out hit, _settings.DistanceOfView))
                { 
                    obj = hit.transform.gameObject;

                    if ((_settings.LayerMask.value & (1 << obj.layer)) == 0)
                    { 
                        Debug.Log("CheackEnviropment find Obstacle");
                        if (Mathf.Abs(transform.position.x - hit.transform.position.x) < 5 || Mathf.Abs(transform.position.z - hit.transform.position.z)  < 5)
                        {
                            Obstacle = hit.transform;
                            _settings.InMove = false;
                        }
                    }
                 }           
            }
            _colliders = Physics.OverlapSphere(transform.position, _settings.DistanceOfView, _settings.LayerMask.value);
            for (int i = 0; i < _colliders.Length; i++)
            { 
                if (!_targets.Contains(_colliders[i]))
                {
                    Debug.Log($"Find target in Sphere - {_colliders[i].name}");
                    _targets.Add(_colliders[i]);
                } 
                
            }
            Array.Clear(_colliders, 0, _colliders.Length);
            
            yield return new WaitForSeconds(_settings.TimeForCkeackingEnviropment);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (_targets.Contains(other))
        {
            Destroy(other.gameObject);
            _targets.Remove(other);
            Debug.Log("trigerr on trash. destroy it.");
        }
        else
        {
            Debug.Log("Can't go. Trigger on obstacle");
            Obstacle = other.gameObject.transform;
            
        }

    }
    private void Awake()
    {
        _settings = GetComponent<RobotCleanerSettings>();
    }
    private void Start()
    {
        StartCoroutine(CheackEnviropment());

    }
   
    
}
    


