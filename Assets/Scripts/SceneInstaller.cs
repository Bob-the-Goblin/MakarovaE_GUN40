
using UnityEngine;
using Project;
using Zenject;
using System.ComponentModel;
using System.Linq;
using UnityEngine.UIElements;

public class SceneInstaller : MonoInstaller
{
    [field: SerializeField]
    private Controls _controls;
    [SerializeField]
    private Battlefield _battlefield;
    [SerializeField]
    private CellPalletSettings _cellPalletSettings;
    [SerializeField]
    private SignalBus _signalBus;
    [SerializeField]
    private ISharedData _data;
    [SerializeField]
    private IGameplayCommand _command;
    [SerializeField]
    private ITeam _team;
    [SerializeField]
    BattleController _battleController;
    [SerializeField]
    PlayerController _playerController;
    
  
    public override void InstallBindings()
    {
        SignalBusInstaller.Install(Container);

        Container.DeclareSignal<GameEvent>();
        Container.DeclareSignal<GameStatus>();
        Container.DeclareSignal<Team>();

        _controls = new Controls();
        _controls.Game.Enable();
        Container.BindInstance(_controls.Game).AsSingle();
        
        _battlefield = FindObjectOfType<Battlefield>();
        _battlefield.OnCellClicked += CellManagerOnCellClicked;

        Container.BindInstance(_battlefield).AsSingle();
        Container.BindInstance(_cellPalletSettings).AsSingle();

        Container.BindInstance(_playerController).AsSingle();

        Container.Bind<ITeam>().To<White_Black>().AsSingle();
        Container.Bind<ISharedData>().To<SharedDataSignal>().AsSingle();
        Container.Bind<IGameplayCommand>().To<Command>().AsSingle();

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
