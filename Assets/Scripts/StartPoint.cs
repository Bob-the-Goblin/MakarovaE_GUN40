
using System.Runtime.InteropServices.WindowsRuntime;
using Unity.VisualScripting;
using UnityEngine;
using Zenject;


public class StartPoint : MonoBehaviour
{
    [SerializeField]
    private Object _prefabOfBall;
    [SerializeField]
    private float _timeBeforeDestroy;
    [SerializeField]
    private Object _arrovPrefab;

    private Transform _pointTransform;
    private GameObject _actualBall;
    private DataInGame _data;
    private DragAndDropBall _dragScript;

    private void Awake()
    {
        _pointTransform = transform;
    }
    void Start()
    {
         Instantiate(_arrovPrefab, _pointTransform.position + new Vector3(0, 0, 0.1f), transform.rotation);
    }

    public void FirstSavingData()
    {
        _data.Cast = 1;
        _data.Frame = 1;
    }
    public GameObject SpawnBall(GameObject prefab)
    {
        _actualBall = Instantiate (prefab, _pointTransform.position, _pointTransform.rotation);
        _data.ActuallBall = _actualBall;
        return _actualBall;
    }

    [Inject]
    private void Counstruction(DataInGame data)
    {
        _data = data;
    }
    
}

