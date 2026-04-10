using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

public class BattleController : MonoBehaviour
{
    private SignalBus _signal;
    private ISharedData _data;
    private Battlefield _battelField;
    private Controls _controls;
    

    private void OnCancel(InputAction.CallbackContext callback)
    { 
    _data.Event = GameEvent.Cancel;
    }

    private void OnConfirm(InputAction.CallbackContext callback)
    { 
    _data.Event = GameEvent.Confirm;
    }

    private void Callback(GameEvent arg)
    {
        if (arg is not GameEvent.Select)
            return;
        switch (_data.Status)
        {
            case GameStatus.Select: _signal.Fire(GameStatus.Move); break;
            case GameStatus.Confirm: Debug.Log("Incorrect value"); break;
        }
    }

    [Inject]
    private void Construct(SignalBus signal, ISharedData data, Battlefield battlefield, Controls controls)
    { 
        _signal = signal;
        _data = data;
        _battelField = battlefield;
        _controls = controls;

        
        _signal.Subscribe<GameEvent>(Callback);
    }

}
