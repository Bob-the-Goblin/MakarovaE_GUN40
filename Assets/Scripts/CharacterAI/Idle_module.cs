using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Idle_module : MonoBehaviour
{
    private float _timeInIdle;

    public void IsTimeToStart()
    {
        _timeInIdle = 0f;
        StartCoroutine(TimeCheaker());
    }
    public float IsTimeToGo()
    {
        return _timeInIdle;
    }

    public void IsTimeToOver()
    {
        StopCoroutine(TimeCheaker());
        _timeInIdle = 0f;
    }
    private IEnumerator TimeCheaker()
    {
        while (true)
        {

            yield return new WaitForSeconds(1);

            _timeInIdle++;
        }
    }
}
