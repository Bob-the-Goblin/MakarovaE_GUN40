using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class MainInstaller : MonoInstaller
{
    private Controls _controls;

    public override void InstallBindings()
    {
        _controls = new Controls();
        Container.BindInstance<Controls.BaseActions>(_controls.Base).AsSingle();
    }
}
