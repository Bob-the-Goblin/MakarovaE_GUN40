using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class UIScore : MonoBehaviour
{   
    [SerializeField]
    private TMP_Text _text;
   
    private int _score;
    private int _totallScore;
    private ScoreCalculator _calculator;

    private void Update()
    {
        _score = _calculator.ActuallScore;
        _text.text = _score.ToString();
    }

    [Inject]
    private void Counstruct(ScoreCalculator calculator)
    { _calculator = calculator;}
    
}
