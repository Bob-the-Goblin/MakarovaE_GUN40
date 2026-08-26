using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;
using Project;
using System;

public class BattleController : MonoBehaviour
{
    private SignalBus _signal;
    private ISharedData _data;
    private Battlefield _battelField;
    private Controls.GameActions _controls;
    private IGameplayCommand _command;

    public Action ClearSet;
    
    private void OnCancel(InputAction.CallbackContext callback)
    {
        Debug.Log("OnCancel");
        if (_data.Event == GameEvent.Confirm) { return; }
        _data.Destination = null;
        _data.Target = null;
        _command.ClearSet();
        _data.Status = GameStatus.Select;
        if (_data.Event != GameEvent.Cancel)
        {
            _data.Event = GameEvent.Cancel;
        }
        else _signal.Fire<GameEvent>(GameEvent.Cancel);
        
    }

    private void OnConfirm(InputAction.CallbackContext callback)
    {
        Debug.Log("OnConfirm");
        if (_data.Destination == null)
        {
            Debug.Log("There is not select cell");
            return;

        }
        if (_data.Status == GameStatus.Confirm)
        {
            _data.Event = GameEvent.Confirm;
        }
    }

    private void Callback(GameEvent arg)
    {
        
        if (arg is not GameEvent.Select)
            return;
        switch (_data.Status)
        {
            case GameStatus.Select:/* _signal.Fire(GameStatus.Move)*/; break;
            case GameStatus.Move: /*_signal.Fire(GameStatus.Confirm)*/; break;
            case GameStatus.Confirm: Debug.Log("Incorrect value"); break;
        }
        
    }

    [Inject]
    private void Construct(SignalBus signal, ISharedData data, Battlefield battlefield, Controls.GameActions actions, IGameplayCommand command)
    {
        _signal = signal;
        _data = data;
        _battelField = battlefield;
        _command = command;
        _controls = actions;

        _controls.Cancel.performed += OnCancel;
        _controls.Confirm.performed += OnConfirm;

        _signal.Subscribe<GameEvent>(Callback);

    }
}
