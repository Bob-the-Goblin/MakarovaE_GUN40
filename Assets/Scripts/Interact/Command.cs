using Project;
using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;
using Zenject;

public class Command : IGameplayCommand, IDisposable
{
    private ISharedData _data;
    private ITeam _team;
    
    private SignalBus _signal;
    
    private Battlefield _battlefield;

    private List<Cell> _set = new();

    private BattleController _battleController;

    private Cell[] _cells;

    public IEnumerable<Cell> Variants  => _set;

    public void Interact(Cell cell)
    {
        switch (_data.Status)
        {
            case GameStatus.Error:
                Debug.Log("Game Status - error");
                break;

            case GameStatus.Lock:
                Debug.Log("Game in Status Lock.");
                break;

            case GameStatus.Unlock:
                _cells = UnityEngine.Object.FindObjectsOfType<Cell>();

                Debug.Log("Game in Status Unlock");
                if (cell.unit != null && cell.unit.team == _team.Current)
                {
                    _data.Destination = cell.unit;
                    FindVariants();
                    _data.Status = GameStatus.Move;
                    
                }
                else { _data.Status = GameStatus.Select; }
                _data.Event = GameEvent.Select;
                break;

            case GameStatus.Select:
                Debug.Log("Game in Status Select");
                if (cell.unit != null && cell.unit.team == _team.Current)
                {
                    _data.Destination = cell.unit;
                    FindVariants();
                    _data.Status = GameStatus.Move;
                }
                _signal.Fire<GameEvent>(GameEvent.Select);
                break;

            case GameStatus.Move:
                if (_set.Contains(cell))
                {
                    _data.Target = cell;
                    _data.Status = GameStatus.Confirm;
                    _set.Clear();
                }
                else { cell.ResetSelect(); return; }
                break;
            case GameStatus.Confirm:
                Debug.Log($"Game in Status Confirm. Please press - space to confirm your choise.");
                Debug.Log($"Or press escape to go to Game Status Select");
                break;
            default:
                break;
        }

    }

    private void TryAddRook(Vector3 currentPosition)
    {

        Dictionary<float, Cell> pass = new Dictionary<float, Cell>();
        foreach (Cell cell in _cells)
        {
            var dif = cell.transform.position - currentPosition;
            if (dif.x != 0 && dif.z == 0)
            {
                pass.Add(dif.x, cell);
            }
        }
        ForCycleForRookAndBishop(pass);
        pass.Clear();
        foreach (Cell cell in _cells)
        {
            var dif = cell.transform.position - currentPosition;
            if (dif.x == 0 && dif.z != 0)
            {
                pass.Add(dif.z, cell);
            }
        }
        ForCycleForRookAndBishop(pass);

    }

    private void TryAddBishop(Vector3 current)
    {
        Dictionary<float, Cell> pass = new Dictionary<float, Cell>();
        foreach (Cell cell in _cells)
        {
            var dif = cell.transform.position - current;
            if (dif.x == dif.z)
            {
                pass.Add(dif.x, cell);
            }
        }
        ForCycleForRookAndBishop(pass);
        pass.Clear();
        foreach (Cell cell in _cells)
        {
            var dif = cell.transform.position - current;
            if ((Math.Abs(dif.x) == dif.z || Math.Abs(dif.z) == dif.x) && dif.z != dif.x)
            { pass.Add(dif.x, cell); }
        }
        ForCycleForRookAndBishop(pass);

    }
    private void TryAddQueen(Vector3 current)
    { 
        TryAddRook(current);
        TryAddBishop(current);
    }
    private void TryAddKnight(Vector3 current)
    { 
        foreach (Cell cell in _cells)
        {
            var dif = cell.transform.position - current;
            if ((Math.Abs(dif.x) == 4 && Math.Abs(dif.z) == 2)||(Math.Abs(dif.z) == 4 && Math.Abs(dif.x)==2))
            {
                if (cell.unit == null || cell.unit.team != _team.Current)
                { _set.Add(cell); }
            }
        }
    }
    private void TryAddPawn(Vector3 current)
    {
        int n;
        if (_data.Destination.team == Team.White)
        { n = 2; }
        else { n = -2; }

        foreach (Cell cell in _cells)
        {
            var dif = cell.transform.position - current;
            if (dif.x == n && dif.z == 0)
            {
                if (cell.unit == null || cell.unit.team != _team.Current)
                { _set.Add(cell); }
            }
            if (dif.x == n && Math.Abs(dif.z) == 2)
            {
                if (cell.unit != null && cell.unit.team != _team.Current)
                    { _set.Add(cell); }
            }
        }
    }
    private void TryAddKing(Vector3 current)
    {
        foreach (Cell cell in _cells)
        { var dif = cell.transform.position - current;
            if ((Math.Abs(dif.x) == 2 || dif.x == 0) && (Math.Abs(dif.z) == 2 || dif.z == 0))
            {
                if (cell.unit == null || cell.unit.team != _team.Current && cell.unit != _data.Destination)
                {
                    _set.Add(cell);
                }
            }
        }
    }

    private void FindVariants()
    {
        var pos = _data.Destination.Cell.transform.position;

        switch (_data.Destination.piece) 
        { 
            case ChessPieces.Pawn: TryAddPawn(pos);
                break;
            case ChessPieces.Rook: TryAddRook(pos);
                break;
            case ChessPieces.Bishop: TryAddBishop(pos);
                break;
            case ChessPieces.Knight: TryAddKnight(pos);
                break;
            case ChessPieces.Queen: TryAddQueen(pos);
                break;
            case ChessPieces.King: TryAddKing(pos);
                break;
            default: Debug.Log("Error - Try find Variants by wrong piece");
                break;
        }  
    }

    private void ForCycleForRookAndBishop(Dictionary<float, Cell> pass)
    {
        for (float i = 2; i <= 14; i += 2)
        {
            if (pass.ContainsKey(i))
            {
                var cell = pass[i];
                if (cell.unit == null)
                {
                    _set.Add(cell);
                }
                else if (cell.unit.team != _team.Current)
                {
                    _set.Add(cell);
                    break;
                }
                else { break; }
            }

        }
        for (float i = -2; i >= -14; i -= 2)
        {
            if (pass.ContainsKey(i))
            {
                var cell = pass[i];
                if (cell.unit == null)
                {
                    _set.Add(cell);
                }
                else if (cell.unit.team != _team.Current)
                {
                    _set.Add(cell);
                    break;
                }
                else { break; }
            }
        }
    }

    public void ClearSet()
    { _set.Clear(); }

    [Inject]
    private void Construct(ISharedData data, ITeam team, SignalBus signal, Battlefield battlefield)
    {
        _data = data;
        _team = team;
        _signal = signal;
        _battlefield = battlefield;

        _battlefield.OnCellClicked += Interact;
    }

    public void Dispose()
    {
        _battlefield.OnCellClicked -= Interact;
    }
}

    

