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
    private EventSkittlesFall[] events;

    private void Awake()
    {
        _text = GetComponent<TMP_Text>();
        _score = 0;
        _totallScore = 0;

        events = FindObjectsOfType<EventSkittlesFall>();
        for (int i = 0; i < events.Length; i++)
        {
            events[i].OnSkittleFall += AddScore;
        }
    }
    private void Update()
    {
        if (_score == 10)
        {
            _totallScore = _score;
        }
        _text.text = _score.ToString();
    }
    private void AddScore()
    {
        _score++;     
    }

    

    
}
