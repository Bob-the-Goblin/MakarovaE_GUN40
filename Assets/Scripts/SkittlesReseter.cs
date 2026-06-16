using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class SkittlesReseter : ScriptableObject
{
    private SkittelsController[] _skittles;
    private DataInGame _data;

    private void Awake()
    {
        _skittles = FindObjectsOfType<SkittelsController>();
    }

    public void UseReset()
    { 
        for (int i = 0;  i < _skittles.Length; ++i)
        {
            _skittles[i].ResetSkittle();
        }
        
    }

    public void UseClose()
    {
        for (int i = 0; i < _skittles.Length; i++)
        {
            if (_skittles[i].IsFalled)
            { _skittles[i].TransferSkittles(); }
            else { _skittles[i].ResetSkittle(); }
        }
    }  
}
