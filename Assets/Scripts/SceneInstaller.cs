
using UnityEngine;
using Project;
using Zenject;
using System.ComponentModel;
using System.Linq;

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
    [SerializeField]
    private IGameplayCommand _command;
    [SerializeField]
    private ITeam _team;
    public override void InstallBindings()
    {
        SignalBusInstaller.Install(Container);

        Container.DeclareSignal<GameEvent>();
        Container.DeclareSignal<GameStatus>();
        Container.DeclareSignal<ITeam>();

        _controls = new Controls();
        _controls.Game.Enable();
        Container.BindInstance(_controls.Game).AsSingle();

        _battlefield = FindObjectOfType<Battlefield>();
        Container.BindInstance(_battlefield).AsSingle();
        Container.BindInstance(_cellPalletSettings).AsSingle();

        _battlefield.OnCellClicked += CellManagerOnCellClicked;

        var units = FindObjectsOfType<Unit>();
        Container.BindInstance(units).AsSingle();

        var teams = units.Select(t => t.team).Distinct().ToList();
        teams.Sort();
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
