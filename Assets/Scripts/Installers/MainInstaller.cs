using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class MainInstaller : MonoInstaller
{
    private DataInGame _gameData;
    private ScoreCalculator _scoreCalculator;
    private StartPoint _startPoint;
    private SkittlesReseter _skittlesReseter;
    

    public override void InstallBindings()
    {
        _gameData = ScriptableObject.CreateInstance<DataInGame>();
        Container.BindInstance(_gameData).AsSingle();

        _scoreCalculator = ScriptableObject.CreateInstance<ScoreCalculator>();
        Container.BindInstance(_scoreCalculator).AsSingle();    

        _startPoint = FindObjectOfType<StartPoint>();
        Container.BindInstance(_startPoint).AsSingle();

        _skittlesReseter = ScriptableObject.CreateInstance<SkittlesReseter>();
        Container.BindInstance(_skittlesReseter).AsSingle();
    }
}
