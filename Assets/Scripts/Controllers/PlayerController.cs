using System;
using UnityEngine;
using Zenject;
using Project;

public class PlayerController : MonoBehaviour
{
    private SignalBus _signal;
    private ISharedData _data;
    
    private void StartPlay(GameEvent arg)
    {
        
        if (arg is not GameEvent.Confirm) { return; }
        if (_data.Status is not GameStatus.Confirm) { return; }

        _data.Status = GameStatus.Lock;
        var destination = _data.Destination;
        destination.OnMoveEndCallback += OnEndPlay;
        if (_data.Target.IsEmpty() )
        { 
            destination.Move(_data.Target);
        }
        else
        {
            var target = _data.Target.unit;
            _data.Target.unit = null;
            GameObject.Destroy(target.gameObject);
            destination.Move(_data.Target);
            _data.Status = GameStatus.Unlock;
            
        }
        _data.Destination = null;
        _data.Target = null;
            
    }
    private void OnEndPlay()
    {
        _data.Status = GameStatus.Unlock;
        _data.Destination.OnMoveEndCallback -= OnEndPlay;

        Debug.Log("OnEndPlay by PlayerController");
    
    }

    [Inject]
    private void Construct(ISharedData data, SignalBus signal)
    {
        _data = data; _signal = signal;

        _signal.Subscribe<GameEvent>(StartPlay);
    }
}
