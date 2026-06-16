using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using Zenject;

public class GameController : MonoBehaviour
{
    [SerializeField]
    GameObject _ball;
    [SerializeField]
    private float _ballLifeTime;

    //Injected
    private DataInGame _data;
    private StartPoint _point;
    private ScoreCalculator _scoreCalculator;
    private SkittlesReseter _reseter;

    private UnityEngine.Object _object;
    private DragAndDropBall _dragAndDropBall;
    private UIScoreForFrame[] _frames;

    private bool _isOver;
    private bool _shouldSpawnBall;
    private bool _weCalculate;
    private bool _weCleare;

    private bool _isWasStrike;


    private void Awake()
    {
        _frames = FindObjectsOfType<UIScoreForFrame>();
        _shouldSpawnBall = false;
        _weCalculate = false;
        _weCleare = false;
        _isWasStrike = false;
    }
    private void Start()
    {
        _object = _point.SpawnBall(_ball);
        if (_data.Frame == 0 || _data.Cast == 2 && _data.Frame == 10)
        { _point.FirstSavingData(); }
        subscribe();
    }
    private void subscribe()
    {
        _object = _data.ActuallBall;
        _dragAndDropBall = _data.ActuallBall.GetComponent<DragAndDropBall>();
        _dragAndDropBall.OnBallWasDrag += Delete;
    }
    private void Delete()
    {
        _dragAndDropBall.OnBallWasDrag -= Delete;
        _data.ActuallBall = null;
        StartCoroutine(DestroyTop());
    }

    private void Update()
    {
        if ( _weCalculate)
        {
            ShowScore();
            _weCalculate = false;
            _weCleare = true;
        }

        if (_weCleare)
        {
            ResetSkittles();

            if (_isOver)
            { Debug.LogAssertion("GameOver"); }

            if (_isWasStrike) { NextCastForStrike(); _isWasStrike = false; }
            else { NextCast(); }

            _weCleare = false;
            _shouldSpawnBall = true;
        }

        if (_shouldSpawnBall)
        { MainActions();
          _shouldSpawnBall = false;
        }
    }

    private void ResetSkittles()
    {
        if (_data.Cast == 1)
        { _reseter.UseClose(); }
        else
        { _reseter.UseReset(); }
    }
    private void MainActions()
    {
        _point.SpawnBall(_ball);
        subscribe(); 
    }
    private void ShowScore()
    {
        int score = _scoreCalculator.ActuallScore;
        _scoreCalculator.ResetScore();
        for (int i = 0; i < _frames.Length; i++) 
        {
            if (_frames[i].Status == FrameStatus.Strike || _frames[i].Status == FrameStatus.Spea )
            {
                if (_data.Cast == 1)
                {
                    _frames[i].AddAtTotal(score);
                    
                }
                if ( _data.Cast == 2)
                {
                    _frames[i].AddAtTotal(score);
                    _frames[i].Status = FrameStatus.None;
                }
                _data.TotalScore += score;
            }


            if (_frames[i].FrameNumber == _data.Frame)
            {
                if (_data.Cast == 1)
                {
                    _frames[i].Write_cast1(score);
                    if (score == 10) { _isWasStrike = true; _data.TotalScore = _frames[i].WriteTotal(_data.TotalScore); }
                }
                if (_data.Cast == 2)
                {
                    _frames[i].Write_cast2(score);
                    _data.TotalScore = _frames[i].WriteTotal(_data.TotalScore);
                }
            }
            
        }
        score = 0;
    }
    private void NextCastForStrike()
    {
        if (_data.Frame == 10)
        { _isOver = true; }
        else { _data.Frame = 2; }

        if (_data.Cast != 1)
        { _data.Cast = 1; }
    }
    private void NextCast()
    {
        if (_data.Cast == 1)
        { _data.Cast = 2; }
        else
        {
            if (_data.Frame == 10)
            {
                _isOver = true; 
            }
            else 
            {
                _data.Frame = 2;
                _data.Cast = 3;
            }
        }
    }
    public IEnumerator DestroyTop()
    { yield return new WaitForSeconds(_ballLifeTime);
        Destroy(_object);
        _weCalculate = true;
    }

    [Inject]
    private void Counstruct(DataInGame data, StartPoint startPoint, ScoreCalculator calculator, SkittlesReseter skittlesReseter)
    { 
        _data = data;
        _point = startPoint;
        _scoreCalculator = calculator; 
        _reseter = skittlesReseter;
    }
}
