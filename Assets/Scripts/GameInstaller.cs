
using UnityEngine;
using Project;
using Zenject;

public class GameInstaller : MonoInstaller
{
    [SerializeField]
    Controls _controls;
    public Battlefield _battlefield;
    public CellPalletSettings _cellPalletSettings;
    public override void InstallBindings()
    {
        _controls = new Controls();
        _controls.Game.Enable();

        _cellPalletSettings = new CellPalletSettings();

        Container.BindInstance(_controls.Game).AsSingle();
        Container.BindInstance(_battlefield).AsSingle();
        Container.BindInstance(_cellPalletSettings).AsSingle();

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
