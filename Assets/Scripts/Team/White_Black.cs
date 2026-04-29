using System;
using UnityEngine;
using Zenject;

public class White_Black : ITeam
{
    public Team Current { get ; set ; } 
    private SignalBus _signal;
    private ISharedData _data;

    

    private White_Black() 
    { 
        Current = Team.White;
    }
    public void Next()
    {
        if (_data.Event != GameEvent.Confirm && _data.Status != GameStatus.Unlock)
        { return; }
        if (Current == Team.White)
        {
            Current = Team.Black;
        }
        else
        {
            Current = Team.White;
        }
        _signal.Fire<Team>(Current);
    }

    [Inject]
    private void Construct(SignalBus signal, ISharedData data)
    { 
        _signal = signal;
        _data = data;

        _signal.Subscribe<GameEvent>(Next);
    }
    
}
