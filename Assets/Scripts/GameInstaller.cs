using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;


public class GameInstaller : MonoInstaller
{
    Settings_for_CharacterAI _settings_for_characterAI;


    public override void InstallBindings()
    {
        SignalBusInstaller.Install(Container);

        _settings_for_characterAI = FindObjectOfType<Settings_for_CharacterAI>();
        Container.BindInstance(_settings_for_characterAI).AsSingle();
        
       
    }

}
