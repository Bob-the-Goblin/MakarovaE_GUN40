using Project;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;


public class Unit : MonoBehaviour, IPointerEnterHandler, IPointerClickHandler, IPointerExitHandler
{
    [SerializeField]
    public Cell Cell {  get; set; }
    Transform _transform;
    public ChessPieces piece;
    public Team team;
    private Action OnMoveEndCallback;


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
        StartCoroutine(OnMove(cell));
    }

    private IEnumerator OnMove(Cell cell)
    {
        var start = _transform.position;
        var end = cell.transform.position;
        var time = 0f;
        var delta = 1f;
        end.y = -1;
        while (time < delta)
        {
            _transform.position = Vector3.Lerp(start, end, time/delta);
            time+= Time.deltaTime;
            yield return null;
        }
        OnMoveEndCallback?.Invoke();
    }


}
