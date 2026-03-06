using Project;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;


public class GameInstaller : MonoInstaller
{
    [SerializeField]
    Controls _controls;

    [SerializeField]
    CellManager _cellManager;

    [SerializeField]
    CellPalletSettings _cellPalletSettings;

    public override void InstallBindings()
    {
        Container.BindInstance(_controls.Game).AsSingle();

        _cellManager.OnCellClicked += CellManageronCellClicked;
    }

    private void CellManageronCellClicked(Cell obj)
    {
        obj.SetSelect(_cellPalletSettings.SelectCell);
    }
}
