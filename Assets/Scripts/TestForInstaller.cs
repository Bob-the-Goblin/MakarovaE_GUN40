using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.UIElements;
using Zenject;

public class TestForInstaller : MonoBehaviour, IPointerClickHandler
{   
    [SerializeField]
    private ISharedData _data;
    public UnityEngine.UI.Image _image;
    



    public void OnPointerClick(PointerEventData eventData)
    {
        if (_data.Status == GameStatus.Select)
        {
            _image.color = Color.blue;

        }
        else
        { _image.color = Color.red; }

    }

    [Inject]
    public void Construct(ISharedData data)
    {
        _data = data;
    }
}
