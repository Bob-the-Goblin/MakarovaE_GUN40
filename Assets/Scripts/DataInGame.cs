using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataInGame : ScriptableObject
{
    private Object _actuallBall;
    private bool _isDragAndDrop;
    private bool _isClicked;
  

    public Object ActuallBall
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
    public bool IsClicked
    {
        get { return _isClicked; }
        set
        {
            Debug.Log($"It's set clicked - {_isClicked}");
            _isClicked = value;
        }

    }


 

}