using UnityEngine;
using Zenject;
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
    private void Construct( SignalBus signal)
    {
        _signal = signal;

        _signal.Subscribe<Team>(ChangeImage);

    }
    private void OnDisable()
    {
        _signal.Unsubscribe<Team>(ChangeImage);
    }


}
