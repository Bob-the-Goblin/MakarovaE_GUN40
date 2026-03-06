using Project;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;


public class CellManager : MonoBehaviour
{
    private Dictionary<NeighbourType, Cell> _neigbours;
    private Cell[] _cells;
    private Unit[] _units;

    public event Action<Cell> OnCellClicked;

    private void Awake()
    {
        _cells = FindObjectsOfType<Cell>();
        _neigbours = new Dictionary<NeighbourType, Cell>(_cells.Length * 8);
        var positions = Array.ConvertAll(_cells, t => t.transform.position);
        var distance = 0f;
        for (int i = 0, iMax = _cells.Length; i < iMax; i ++)
        {
            _cells[i].OnPointerClickEvent += OnCellClicked;
#if UNITY_EDITOR
            _cells[i].OnPointerClickEvent += DebugOnPointerClick;
#endif

            for (int j = 0, jMax = _cells.Length; j < jMax; j ++)
            {
                if (i == j) continue;
                var source = positions[i];
                var destination = positions[j];

                var forward = destination.z.CompareTo(source.z);
                var right = destination.x.CompareTo(source.x);
                var type = (forward, right) switch
                {
                    (1, 1) => NeighbourType.ForwardRight,
                    (1, 0) => NeighbourType.Forward,
                    (1, -1) => NeighbourType.ForwardLeft,
                    (0, 1) => NeighbourType.Right,
                    (0, -1) => NeighbourType.Left,
                    (-1, 1) => NeighbourType.BackwardRight,
                    (-1, 0) => NeighbourType.Backward,
                    (-1, -1) => NeighbourType.BackwardLeft,
                    _ => default
                };
            }

        }
        _units = FindObjectsOfType<Unit>();
        for (int i = 0, iMax = _units.Length; i < iMax; i++)
        {
            for (int j = 0, jMax = _cells.Length; j < jMax; j ++)
            {
                if (_units[i].transform.position.x == _cells[j].transform.position.x && _units[i].transform.position.z == _cells[j].transform.position.z)
                {
                    var unit = _units[i];
                    var cell = _cells[j];
                    unit.Cell = cell;
                    cell.unit = unit;
                }
            }
        }

    }

    private void DebugOnPointerClick(Cell cell)
    {
        throw new NotImplementedException();
    }

}