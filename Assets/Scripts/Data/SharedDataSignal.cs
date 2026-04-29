using Project;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class SharedDataSignal : ISharedData
{
    [SerializeField]
    private SignalBus _signal;
    private bool _lock;
    private GameEvent _event;
    private GameStatus _status;
    private Unit _destination;
    private Cell _target;

    public bool Lock 
    {
        get => _lock;
        set 
        { 
            if (_lock == value) return;
            _lock = value;
            _signal.Fire(_lock ? GameStatus.Lock : GameStatus.Unlock);
        }
    }
    public GameEvent Event 
    { get => _event ; 
      set 
        {
            if (_event == value) Debug.Log($"GameEven repeat:{value}");
            else
            {
                Debug.Log($"New GameEvent: {value}");
                _event = value;
                _signal.Fire(value);
            }
        }
        
    }
    public GameStatus Status
    {
        get => _status;
        set
        {
            if (_status == value) Debug.Log($"Game repeat:{value}");
            else
            {
                Debug.Log($"New GameStatus: {value}");
                _status = value;
                _signal.Fire(value);
            }
        }
    }

    public Unit Destination { get ; set ; }
    public Cell Target { get ; set ; }

    [Inject]
    private void Construct(SignalBus signal)
    {
        _signal = signal;
    }

}
