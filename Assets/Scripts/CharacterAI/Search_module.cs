using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using Zenject;

public class Search_module : MonoBehaviour
{

    private float _radius;
    private LayerMask _mask;


    List <GameObject> _targets;


    private GameObject _loot;
    public GameObject Loot {  get => _loot;  }


    private bool _isThereLoot;
    public bool IsThereLoot { get => _isThereLoot; private set => _isThereLoot = value; }


    private Vector3 _randomCoordinates;
    public Vector3 RandomCoordinates { get => _randomCoordinates; }


    private bool _canWeSearch;
    public bool CanWeSearch { get => _canWeSearch; set { if (value == true) _canWeSearch = true; } }

    private bool _write;


    public void SearchPosition()
    {
        Collider[] _colliders = FindObjectInTheFielOfView(gameObject.transform.position);

        if (_colliders != null)
        {
            for (int i = 0 ; i < _colliders.Length; i++)
            {
                if (IsInMaskLayer(_colliders[i]))
                {
                    _targets.Add(_colliders[i].gameObject);         
                }    
            }
            if (_targets.Count != 0) 
            {
                _loot = FindNearestTarget(_targets); 
                if (_write)
                {
                    Debug.Log($"_loot is on {_loot.transform.position}");
                    _write = false;
                }
                _targets.Clear();
                _isThereLoot = true;
            }
            else 
            {
                Debug.Log("There is no loot");
                _isThereLoot = false;
                _randomCoordinates = FindRandowWay();
            }  

        }
        else 
        { 
            _isThereLoot = false;
            _randomCoordinates = FindRandowWay();
            
        }
        _canWeSearch = false;
    }

    //Search colliders in radius
    private Collider[] FindObjectInTheFielOfView (Vector3 _gameObjectPosition)
    {
        Collider[] colliders = Physics.OverlapSphere (_gameObjectPosition, _radius, _mask);
        if (colliders.Length == 0)
        {
            return null;
        }
        else 
        { 
            return colliders;
        }
    }

    //Cheaks whether the object is loot by it layer
    private bool IsInMaskLayer(Collider collider)
    {
        GameObject obj = collider.gameObject;
        if ((_mask.value & (1 << obj.layer)) != 0)
        { 
            return true;
        }    
        return false;
    }

    //Find nearest target if there are more than one
    private GameObject FindNearestTarget(List<GameObject> targets)
    {
        GameObject nearest;

        switch (targets.Count)
        {
            case 0: nearest = null; Debug.Log("FindNearestTarget ERROR - there no target") ; break;
            case 1: nearest = targets[0]; break;
            default:
                {
                    nearest = targets[0];
                    foreach (GameObject _target in targets)
                    { 
                        nearest = WhatNearest(nearest, _target);  
                    }
                }
                break;
        }
        return nearest;
    }

    //Calculate what object is nearest to our gameObject
    private GameObject WhatNearest(GameObject first, GameObject second)
    { 
        float distance_1 = Vector3.Distance(gameObject.transform.position, first.transform.position);
        float distance_2 = Vector3.Distance(gameObject.transform.position, second.transform.position);

        if (distance_1 <= distance_2)
        {
            return first;
        }
        else 
        { 
            return second; 
        }
    
    }


    //Find Random coordinates cause there is no loot
    private Vector3 FindRandowWay() 
    { 
        Vector3 pos = gameObject.transform.position;
        return  new Vector3 (RandomCoord(pos.x), pos.y, RandomCoord(pos.z));
    }
    private float RandomCoord(float pos)
    {
        return Random.Range(pos - _radius, pos + _radius);
    }

    public void StartCheakPositionCoruntin()
    {
        StartCoroutine(CheakPosition());
        _write = true;
    }

    public void StopCheakPositionCorountine()
    { 
        StopCoroutine(CheakPosition()); 

        _isThereLoot = false;
        _randomCoordinates = gameObject.transform.position;
        

    }

    //Once in a sec cheack position Character AI and founded _target
    private IEnumerator CheakPosition()
    {
        while (true)
        {

            if (_randomCoordinates != null)
            {
                if (gameObject.transform.position == _randomCoordinates || (Vector3.Distance(gameObject.transform.position, _randomCoordinates)) < 1)
                { _canWeSearch = true; }
                else
                { _canWeSearch = false; }
            }
            else
            { Debug.Log("XYEVO"); }

                yield return new WaitForSeconds(1);
        }
    }

    public void DeleteLootOnModule()
    {
        GameObject obj = _loot;
        _loot = null;
        Destroy(obj);
    }




    [Inject]
    private void Counstruct(Settings_for_CharacterAI settings)
    {

        _radius = settings.DistanceOfView;
        _mask = settings.LayerMask;
        _targets = new List<GameObject>();
    }

}
