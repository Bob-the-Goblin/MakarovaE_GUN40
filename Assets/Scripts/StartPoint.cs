
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
    private Object _actualBall;
    private DataInGame _data;
    private DragAndDropBall _dragScript;

    private void Awake()
    {
        _pointTransform = GetComponent<Transform>();
    }
    void Start()
    {
         Instantiate(_arrovPrefab, _pointTransform.position + new Vector3(0, 0, 0.1f), transform.rotation);

        //DragAndDropBall  ball = _actualBall.GetComponent<DragAndDropBall>();
        //ball.enabled = false;
        _actualBall =Instantiate(_prefabOfBall, _pointTransform.position, _pointTransform.rotation);
        SavingToData(_actualBall);

    }

    private void SavingToData(UnityEngine.Object prefab)
    {
        _data.ActuallBall = prefab;
    }

    [Inject]
    private void Counstruction(DataInGame data)
    {
        _data = data;
    }
    
}

