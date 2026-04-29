using Project;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Unit : MonoBehaviour, IPointerEnterHandler, IPointerClickHandler, IPointerExitHandler
{
    [SerializeField]
    public Cell Cell {  get; set; }

    Transform _transform;
    public ChessPieces piece;
    public Team team;
    public Action OnMoveEndCallback;
    private Dictionary<NeighbourType, Cell> _neighboursCell = new Dictionary<NeighbourType, Cell>(8);

    
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
        _transform.position = cell.transform.position + new Vector3(0,1.2f,0);
        OnMoveEndCallback?.Invoke();
        var before = Cell;
        Cell = cell;
        Cell.unit = this;
        before.unit = null;
        //StartCoroutine(OnMove(cell));
    }

    private IEnumerator OnMove(Cell cell)
    {
        var start = _transform.position;
        var end = cell.transform.position + new Vector3(0,2,0);
        var time = 0f;
        var delta = 1f;
        end.y = -1;
        while (time < delta)
        {
            _transform.position = Vector3.Lerp(start, end, time/delta);
            time+= Time.deltaTime;
            yield return null;
        }
        Cell = cell;
        OnMoveEndCallback?.Invoke();
    }

   
}
