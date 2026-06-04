using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class MainInstaller : MonoInstaller
{
    private DataInGame _gameData;
    private EventSkittlesFall _eventOfFall;
    private UIScore _uiScore;
    

    public override void InstallBindings()
    {
        _gameData = new DataInGame();
        Container.BindInstance(_gameData).AsSingle();
    }
}
