using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataInGame : ScriptableObject
{
    private GameObject _actuallBall;
    private bool _isDragAndDrop;
    private int _frame;
    private int _cast;
    private int _totalScore;
  
    public GameObject ActuallBall
    {
        get { return _actuallBall; }
        set { _actuallBall = value; }
    }
    public bool IsDragAndDrop
    {
        get { return _isDragAndDrop; }
        set
        {
            _isDragAndDrop = value;
            Debug.Log($"It's DragAnddrop {_isDragAndDrop}");
        }
    }
    public int Frame
    {
        get { return _frame; }
        set
        {   
            if (_frame < 10 )
            { _frame ++; }
            else { _frame = 1; }
        }
    }
    public int Cast
    {
        get { return _cast; }
        set
        {   
            if (_cast < 2)
                { _cast ++;}
            else {_cast = 1; }
        }
    }
    public int TotalScore
    {
        get { return _totalScore; }
        set { _totalScore = value; }
    }

    private DataInGame()
    {
        _totalScore = 0;
    }
}