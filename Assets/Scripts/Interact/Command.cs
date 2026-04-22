using Project;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class Command : IGameplayCommand
{
    private ISharedData _data;
    private ITeam _team;
    
    private SignalBus _signal;
    
    private Battlefield _battlefield;

    private HashSet<Cell> _set = new HashSet<Cell>();

    public IEnumerable<Cell> Variants  => _set; 

    public void Interact(Cell cell)
    {
        _data.Status = GameStatus.Select;
        Debug.Log($"Set Status Select");
        _signal.Fire(GameStatus.Select);
        _data.Target = cell;
        if (_data.Target.unit != null)
        {
            _data.Destination = cell.unit;
            _data.Status = GameStatus.Move;
            _signal.Fire(GameStatus.Move);
            _data.Event = GameEvent.Select;
            _signal.Fire(GameEvent.Select);
        }

        /*
        if (_data.Status != null)
        {
            if (cell.IsEmpty())
            { _data.Target = cell; }
            else 
            {
                _data.Destination = cell.unit; 
                _signal.Fire(GameStatus.Move);
                switch (_data.Destination.piece)
                {
                    case ChessPieces.Pawn:
                        AvailableVariantsForPawn();
                        break;
                    case ChessPieces.Rook:
                        AvailableVariantsForRook();
                        break;
                    case ChessPieces.Knight:
                        AvailableVariantsForKnight();
                        break;
                    case ChessPieces.Bishop:
                        AvailableVariantsForBishop();
                        break;
                    case ChessPieces.Queen:
                        break;
                    case ChessPieces.King:
                        AvailacleVariantsForKing();
                        break;
                        
                }
                
            }
            _data.Status = GameStatus.Move;
            _signal.Fire(GameStatus.Move);
            return;

        }
        if (_data.Status == GameStatus.Move)
        {
            if (Variants.Equals(cell))
            {
                _data.Target = cell;
                _data.Status = GameStatus.Confirm;
            }
        }
        */


    }
    private bool CheckIsCanBeAttack(Cell cell)
    {
        if (_data.Destination.team == cell.unit.team) return false;
        else return true;
    }
    private void TryAddAtSet(Cell cell)
    {
        if (cell == null) { Debug.Log("TryAddAtSetNull"); return; }
        if (cell.IsEmpty()) _set.Add(cell);
        else if (CheckIsCanBeAttack(cell)) _set.Add(cell);
    }
    private void AvailableVariantsForPawn()
    {
        if (_data.Destination.team == Team.White)
        {
            _battlefield.TryGet(_data.Destination.Cell, NeighbourType.ForwardLeft, out Cell place);
            TryAddAtSet(place);
            _battlefield.TryGet(_data.Destination.Cell, NeighbourType.Forward, out Cell plac);
            TryAddAtSet(plac);
            _battlefield.TryGet(_data.Destination.Cell, NeighbourType.ForwardRight, out Cell pla);
            TryAddAtSet(pla);
        }
        else
        {
            _battlefield.TryGet(_data.Destination.Cell, NeighbourType.BackwardLeft, out Cell place);
            TryAddAtSet(place);
            _battlefield.TryGet(_data.Destination.Cell, NeighbourType.Backward, out Cell plac);
            TryAddAtSet(plac);
            _battlefield.TryGet(_data.Destination.Cell, NeighbourType.BackwardRight, out Cell pla);
            TryAddAtSet(pla);
        }
    }
    
    private void AvailableVariantsForRook()

    {
        bool HaveNeigbour = true;
         do 
        {
            _battlefield.TryGet(_data.Destination.Cell, NeighbourType.Forward, out Cell place);
            if (place != null) TryAddAtSet(place);
            else HaveNeigbour = false;
        }
        while(HaveNeigbour);  
        _battlefield.TryGet(_data.Destination.Cell, NeighbourType.Left, out Cell plac);
        TryAddAtSet(plac);
        _battlefield.TryGet(_data.Destination.Cell, NeighbourType.Backward, out Cell pla);
        TryAddAtSet(pla);
        _battlefield.TryGet(_data.Destination.Cell, NeighbourType.Right, out Cell pl);
        TryAddAtSet(pl);
    }

    private void AvailableVariantsForBishop()
    {
        _battlefield.TryGet(_data.Destination.Cell, NeighbourType.ForwardLeft, out Cell place);
        TryAddAtSet(place);
        _battlefield.TryGet(_data.Destination.Cell, NeighbourType.ForwardRight, out Cell plac);
        TryAddAtSet(plac);
        _battlefield.TryGet(_data.Destination.Cell, NeighbourType.BackwardLeft, out Cell pla);
        TryAddAtSet(pla);
        _battlefield.TryGet(_data.Destination.Cell, NeighbourType.BackwardRight, out Cell pl);
        TryAddAtSet(pl);

    }

    private void AvailableVariantsForKnight()
    {
        Cell neigbour;
        _battlefield.TryGet(_data.Destination.Cell, NeighbourType.Forward, out neigbour);
        if (neigbour != null)
        {
            _battlefield.TryGet(neigbour, NeighbourType.ForwardRight, out Cell place);
            TryAddAtSet(place);
            _battlefield.TryGet(neigbour, NeighbourType.ForwardLeft, out Cell plac);
            TryAddAtSet(plac);
        }
        _battlefield.TryGet(_data.Destination.Cell, NeighbourType.Left, out neigbour);
        if (neigbour != null)
        {
            _battlefield.TryGet(neigbour, NeighbourType.ForwardLeft, out Cell place);
            TryAddAtSet(place);
            _battlefield.TryGet(neigbour, NeighbourType.BackwardLeft, out Cell plac);
            TryAddAtSet(plac);
        }
        _battlefield.TryGet(_data.Destination.Cell, NeighbourType.Right, out neigbour);
        if (neigbour != null)
        {
            _battlefield.TryGet(neigbour, NeighbourType.ForwardRight, out Cell place);
            TryAddAtSet(place);
            _battlefield.TryGet(neigbour, NeighbourType.BackwardRight, out Cell plac);
            TryAddAtSet(plac);
        }
        _battlefield.TryGet(_data.Destination.Cell, NeighbourType.Backward, out neigbour);
        if (neigbour != null)
        {
            _battlefield.TryGet(neigbour, NeighbourType.BackwardLeft, out Cell place);
            TryAddAtSet(place);
            _battlefield.TryGet(neigbour, NeighbourType.BackwardRight, out Cell plac);
            TryAddAtSet(plac);
        }
    }

    private void AvailacleVariantsForKing()
    {
        _battlefield.TryGet(_data.Destination.Cell, NeighbourType.Forward, out Cell place);
        TryAddAtSet(place);
        _battlefield.TryGet(_data.Destination.Cell, NeighbourType.Left, out Cell plac);
        TryAddAtSet(plac);
        _battlefield.TryGet(_data.Destination.Cell, NeighbourType.Backward, out Cell pla);
        TryAddAtSet(pla);
        _battlefield.TryGet(_data.Destination.Cell, NeighbourType.Right, out Cell pl);
        TryAddAtSet(pl);
        _battlefield.TryGet(_data.Destination.Cell, NeighbourType.ForwardLeft, out Cell celll);
        TryAddAtSet(celll);
        _battlefield.TryGet(_data.Destination.Cell, NeighbourType.ForwardRight, out Cell cell);
        TryAddAtSet(cell);
        _battlefield.TryGet(_data.Destination.Cell, NeighbourType.BackwardLeft, out Cell cel);
        TryAddAtSet(cel);
        _battlefield.TryGet(_data.Destination.Cell, NeighbourType.BackwardRight, out Cell ce);
        TryAddAtSet(ce);
    }

    [Inject]
    public void Construct(ISharedData data, ITeam team, SignalBus signal, Battlefield battlefield)
    {
        _data = data;
        _team = team;
        _signal = signal;
        _battlefield = battlefield;
    }

    private void Fire(GameStatus status)
    { switch(status)
        {

        }
    }

    
}

    

