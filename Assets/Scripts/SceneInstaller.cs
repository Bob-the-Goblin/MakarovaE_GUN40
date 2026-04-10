
using UnityEngine;
using Project;
using Zenject;

public class SceneInstaller : MonoInstaller
{
    [SerializeField]
    private Controls _controls;
    [SerializeField]
    private Battlefield _battlefield;
    [SerializeField]
    private CellPalletSettings _cellPalletSettings;
    [SerializeField]
    private SignalBus _signalBus;
    [SerializeField]
    private ISharedData _data;

    private void OnEnable()
    {
    
    }
    public override void InstallBindings()
    {
        SignalBusInstaller.Install(Container);

        Container.DeclareSignal<GameEvent>();
        Container.DeclareSignal<GameStatus>();
        

        _controls = new Controls();
        _controls.Game.Enable();

        Container.BindInstance(_controls.Game).AsSingle();
        Container.BindInstance(_battlefield).AsSingle();
        Container.BindInstance(_cellPalletSettings).AsSingle();
        Container.BindInstance(_data).AsSingle();

        _battlefield.OnCellClicked += CellManagerOnCellClicked;

        
    }

    private void CellManagerOnCellClicked(Cell cell)
    {
        cell.SetSelect(_cellPalletSettings.SelectCell);
    }

    private void OnDestroy()
    {
        _controls.Dispose();
    }
}
