using Project;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Unit : MonoBehaviour, IPointerEnterHandler, IPointerClickHandler, IPointerExitHandler
{
    [SerializeField]
    public Cell Cell {  get; set; }
    Transform _transform;
    private void Awake()
    {
        _transform = GetComponent<Transform>();
    }
    public void OnPointerClick(PointerEventData eventData)
    {
        Cell.OnPointerClick(eventData);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        Cell.OnPointerEnter(eventData);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        Cell.OnPointerExit(eventData);
    }

    public void Move (Cell cell)
    {
        do
        {if (cell.transform.position.x > _transform.position.x)
            {
                _transform.Translate(0.1f, 0, 0 * Time.deltaTime);
            }
        if (cell.transform.position.x < _transform.position.x)
            {
                _transform.Translate(-0.1f, 0, 0 * Time.deltaTime);
            }
        if (cell.transform.position.z > _transform.position.z)
            { 
                _transform.Translate( 0, 0, 0.1f * Time.deltaTime); 
            }
        if (cell.transform.position.z < _transform.position.z) ;
            { 
                _transform.Translate(0, 0, -0.1f * Time.deltaTime); 
            }
        }
        while (_transform.position.x == cell.transform.position.x && _transform.position.z == cell.transform.position.z);

        //OnMoveEndCallback()
    }


}
