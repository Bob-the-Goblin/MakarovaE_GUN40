using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using Zenject;

public class ThrowTheBall : MonoBehaviour, IPointerClickHandler
{//попытка реализовать бросок м€ча по клику, не упела
    [SerializeField]
    private float _maxForce;
    private float _forcePower;

    private Vector3 _mousePos;
    private DataInGame _data;

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

    [Inject]
    private void Counstruct(DataInGame data)
    { 
        _data = data;
    }
}
