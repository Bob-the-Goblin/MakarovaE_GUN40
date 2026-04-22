using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

public class BattleController
{
    private SignalBus _signal;
    private ISharedData _data;
    private Battlefield _battelField;
    private Controls.GameActions _controls;
    private IGameplayCommand _command;
    

    private void OnCancel(InputAction.CallbackContext callback)
    { 
        _data.Event = GameEvent.Cancel;
        _data.Status = GameStatus.Select;
    }

    private void OnConfirm(InputAction.CallbackContext callback)
    { 
        if (_data.Destination == null)
        {
            Debug.Log("There is not select cell");
            return;

        }
        _signal.Fire(GameStatus.Confirm);
        _signal.Fire(GameEvent.Confirm);
    }

    private void Callback(GameEvent arg)
    {
        /*
        if (arg is not GameEvent.Select)
            return;
        switch (_data.Status)
        {
            case GameStatus.Select: _signal.Fire(GameStatus.Move); break;
            case GameStatus.Move: _signal.Fire(GameStatus.Confirm); break;
            case GameStatus.Confirm: Debug.Log("Incorrect value"); break;
        }
        */
    }

    [Inject]
    public void Construct(SignalBus signal, ISharedData data, Battlefield battlefield, Controls.GameActions controls, IGameplayCommand command)
    { 
        _signal = signal;
        _data = data;
        _battelField = battlefield;
        _controls = controls;
        _command = command;

        _controls.Cancel.performed += OnCancel;
        _controls.Confirm.performed += OnConfirm;
        _battelField.OnCellClicked += _command.Interact;

        _signal.Subscribe<GameEvent>(Callback);
    }

    

    private void OnDestroy()
    {
        _battelField.OnCellClicked -= _command.Interact;
        _controls.Cancel.performed -= OnCancel;
        _controls.Confirm.performed -= OnConfirm;

    }

}
