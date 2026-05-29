using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ThrowTheBall : MonoBehaviour, IPointerClickHandler
{
    private float _forcePower;
    [SerializeField]
    private float _maxForce;
    private Vector3 _mousePos;


    private void Awake()
    {

    }
    void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.Mouse0))
        { StartCoroutine(WaitAddValue()); }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        _mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        Debug.Log("click");
        _forcePower = 0;
    }


    //add value to _forcePower
    private IEnumerator WaitAddValue()
    {
        if (_forcePower < _maxForce)
        {
            _forcePower += 0.1f;
            Debug.Log(_forcePower);

            yield return new WaitForSeconds(5f);
        }
        else { yield return new WaitForSeconds(10f); }

    }
}
