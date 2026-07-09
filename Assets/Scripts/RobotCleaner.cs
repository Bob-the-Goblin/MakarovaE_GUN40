using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class RobotCleaner : MonoBehaviour
{
    private RobotCleanerSettings _settings;
 
    //Vectors for Physics.Raycast
    private Vector3[] vectors = new Vector3[3] {Vector3.forward, Vector3.right, -Vector3.right};


    //for FindWay
    public Transform _target;
    public Transform _obstacle;

  
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

                    if ((_settings.LayerMask.value & (1 << obj.layer)) != 0)
                    {
                        _target = hit.transform;
                        Debug.Log("CheackEnviropment find target");
                    }
                    else
                    {
                        Debug.Log("CheackEnviropment find Obstacle");
                        if (Mathf.Abs(transform.position.x - hit.transform.position.x) < 5 || Mathf.Abs(transform.position.z - hit.transform.position.z)  < 5)
                        {
                            _obstacle = hit.transform;
                            _settings.InMove = false;
                        }
                    }
                 }           
            }
            
            yield return new WaitForSeconds(_settings.TimeForCkeackingEnviropment);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.transform == _target || ((_settings.LayerMask.value & (1 << other.gameObject.layer)) != 0))
        {
            Destroy(other.gameObject);
            Debug.Log("trigerr on trash. destroy it.");
        }
        else
        {
            Debug.Log("Can't go. Trigger on obstacle");
            _obstacle = other.gameObject.transform;
            
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
    


