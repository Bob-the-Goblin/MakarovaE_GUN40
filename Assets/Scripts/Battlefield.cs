using Project;
using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class Battlefield : MonoBehaviour, IDisposable
{
    private Dictionary<CellNeighbour, Cell> _neigbours;
    private Cell[] _cells;
    private Unit[] _units;
    private ISharedData _data;
    private CellPalletSettings _pallets;
    [Inject]
    private IGameplayCommand _command;

    public event Action<Cell> OnCellClicked;

    public bool TryGet (Cell source, NeighbourType type, out Cell cell)
    {
        var data = new CellNeighbour(type, source); 
        return _neigbours.TryGetValue(data, out cell);
    }

    private void Awake()
    {
        _cells = UnityEngine.Object.FindObjectsOfType<Cell>();
        _neigbours = new Dictionary<CellNeighbour, Cell>(_cells.Length * 8);
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
                var key = new CellNeighbour(type, _cells[i]);
                var check = _neigbours.TryGetValue(key, out var cell)
                ? Vector3.Distance(source, cell.transform.position)
                : float.MaxValue;
                distance = Vector3.Distance(source, destination);
                if (distance < check)
                    _neigbours[key] = _cells[i];
            }

        }
        _units = UnityEngine.Object.FindObjectsOfType<Unit>();
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
    public void Dispose()
        {
           for (int i = 0, iMax = _cells.Length; i < iMax; i++)
            {
            _cells[i].OnPointerClickEvent -= OnCellClicked;
#if UNITY_EDITOR
            _cells[i].OnPointerClickEvent -= DebugOnPointerClick;
#endif
            }
        }

    private void DebugOnPointerClick(Cell cell)
    {
        
         
    }

    private void CallBack(GameEvent arg)
    {
        foreach (Cell cell in _cells)
        {
            cell.ResetSelect();
        }
        if (_data.Destination != null)
        { _data.Destination.Cell.SetSelect(_pallets.SelectCell); }

        var mat = _data.Status switch
        {
            GameStatus.Move => _pallets.MoveCell,
            _ => default(Material)
        };

        if (mat != null)
        {
            foreach (var cell in _command.Variants)
            { 
                cell.SetSelect(mat);
            }
        }
        if (_data.Target != null)
        { _data.Destination.Cell.SetSelect(_pallets.ConfirmCell); }


    }

    Battlefield(SignalBus signal, ISharedData data, CellPalletSettings cellPallet)
    {
        (_data, _pallets) = (data, cellPallet);
        signal.Subscribe<GameEvent>(CallBack); 
    }

    private struct CellNeighbour : IEquatable<CellNeighbour>
    {
        private readonly NeighbourType _type;
        private readonly Cell _value;

        public CellNeighbour( NeighbourType type, Cell value )
        {  _type = type; _value = value;}

        public bool Equals(CellNeighbour other)
            => _type == other._type && Equals(_value, other._value);

        public override bool Equals(object obj)
        {
            return obj is CellNeighbour other && Equals(other);
        }
        public override int GetHashCode()
        {
            return unchecked(HashCode.Combine(_type, _value) - 13);
        }
        
    }
     
}