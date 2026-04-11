using UnityEngine;
using Zenject;
using UnityEngine.UIElements;
using UnityEngine.UI;
using Image = UnityEngine.UI.Image;

public class PanelColorChange : MonoBehaviour
{
    private Image _image;
    private ITeam _team;
    SignalBus _signal;

    private void OnEnable()
    {
        _image = GetComponent<Image>();
    }
    private void ChangeImage()
    {
        if (_image != null)
        {

            if (_image.isActiveAndEnabled)
            { _image.enabled = false; }
            else
            { _image.enabled = true; }
        }
    }
    [Inject]
    private void Construct(ITeam team, SignalBus signal)
    {
        _team = team;
        _signal = signal;

        _signal.Subscribe<ITeam>(ChangeImage);
    }
    private void OnDisable()
    {
        _signal.Unsubscribe<ITeam>(ChangeImage);
    }


}
