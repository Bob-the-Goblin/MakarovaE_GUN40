using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Gates : MonoBehaviour
{
    
    private int score = 0;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "ball")
            score++;
            Debug.Log(score);
            Destroy(other.gameObject);   
    }   
}
