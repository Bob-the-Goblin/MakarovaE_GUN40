using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine;
using Zenject;

public class ScoreCalculator : ScriptableObject
{
    private SkittelsController[] events;
    private int _actuallScore;
    public int ActuallScore
    {
        get { return _actuallScore; }
        private set { }
    }


    private void Awake()
    {
        events = FindObjectsOfType<SkittelsController>();
        for (int i = 0; i < events.Length; i++)
        {
            events[i].OnSkittleFall += AddScore;
        }
        _actuallScore = 0;

    }
    private void OnDisable()
    {
        for (int i = 0;i < events.Length; i++)
        {
            events[i].OnSkittleFall -= AddScore;
        }
    }

    private void AddScore()
    {   _actuallScore++;

    }
    public void ResetScore()
    { _actuallScore = 0;}
}
