using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class MainInstaller : MonoInstaller
{
    private DataInGame _gameData;

    public override void InstallBindings()
    {
        Container.BindInstance<DataInGame>(_gameData).AsSingle();
    }
}
