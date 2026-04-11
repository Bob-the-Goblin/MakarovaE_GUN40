using System;
using UnityEngine;
using Zenject;

public class White_Black : ITeam
{
    public Team Current { get ; set ; } 
    private SignalBus _signal;

    private White_Black() 
    { 
        Current = Team.White;
    }
    public void Next()
    {
        if (Current == Team.White)
        {
            Current = Team.Black;
        }
        else
        {
            Current = Team.White;
        }
        _signal.Fire(Current);
    }

    [Inject]
    private void Construct(SignalBus signal)
    { 
        _signal = signal;
    }
    
}
