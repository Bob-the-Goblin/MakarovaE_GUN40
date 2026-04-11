using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class PlayerController : MonoBehaviour
{
    private SignalBus _signal;
    private ISharedData _data;

    [Inject]
    private void Construct(SignalBus signal, ISharedData data)
    {  _signal = signal; _data = data;
        _signal.Subscribe<GameEvent>(StartPlay);
    }
    private void StartPlay(GameEvent arg)
    {
        if (arg is not GameEvent.Confirm) { return; }
        if (_data.Status is not GameStatus.Confirm) { return; }

        _data.Status = GameStatus.Lock;
        var destination = _data.Destination;
        if (_data.Target.IsEmpty() )
        { 
            destination.OnMoveEndCallback += OnEndPlay;
            destination.Move(_data.Target);
        }
        else
        {
            var target = _data.Target.unit;
            _data.Target.unit = null;
            Destroy(target.gameObject);
            _data.Target = null;
            _data.Status = GameStatus.Unlock;
        }
            
    }

    private void OnEndPlay()
    {
        _data.Status = GameStatus.Unlock;
        _data.Destination.OnMoveEndCallback -= OnEndPlay;
    
    }
}
