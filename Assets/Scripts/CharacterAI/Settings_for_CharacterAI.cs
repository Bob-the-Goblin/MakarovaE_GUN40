using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class Settings_for_CharacterAI : MonoBehaviour
{
    //Need for Searching Loot
    [SerializeField]
    private LayerMask _layerMask;

    public LayerMask LayerMask { get { return _layerMask; } private set => _layerMask = value; }



    //The max distance a character can Move also distance for Searhcing Loot
    [SerializeField]
    private float _distanceForMove;

    public float DistanceOfView { get { return _distanceForMove; } private set => _distanceForMove = value;}


    //Value "time" for collect animation in sec
    [SerializeField]
    private float _timeForCollect;
    public float TimeForCollect { get { return _timeForCollect; } private set => _timeForCollect = value;}



}
