using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class Collect_module : MonoBehaviour
{
    private UnityEngine.UI.Image _slider;
    private bool _isEnable;



    private TMP_Text _count;
   

    private int _lootAmount;
    private float _partOfSlider;

    private float _maxTime;
    private float _refreshRate;


    private bool _canCollect;
    private bool _canShowQuantity;

    private bool _isCollectDone;

    private bool _goToNextState;
    public bool CanGoToNextState {  get => _goToNextState; }

    private bool _collecctInProcess;
    public bool CollectInProcess {  get => _collecctInProcess; }

    private bool _canAdd;
    private bool _isAllShowed;

    private void Awake()
    {
        _slider = GetComponentInChildren<Image>();
        _count = GetComponentInChildren<TMP_Text>();

        _refreshRate = 0.5f;

        
        _slider.enabled = false;
        _count.enabled = false;
        _collecctInProcess = false;
    }

    //Start corountine and doing somethig with boolean variable to start updates;
    public void StartCollect()
    {
        StartCoroutine(TimerForCollect());

        _slider.fillAmount = 0;
        _slider.enabled = true;

        _canCollect = true;
        _canShowQuantity = false;
        _isCollectDone = false;
        _goToNextState = false;
        _isAllShowed = false;

        _collecctInProcess = true;
    }
    private IEnumerator TimerForCollect()
    {
        while (true) 
        {       
            yield return new WaitForSeconds(_refreshRate);
            _partOfSlider += _refreshRate;
        }
    }

    private void Update()
    {
        DisplayUI();
        _isEnable = _slider.enabled;
    }
    //Manager for Canvas elements
    private void DisplayUI()
    {
        if (_canCollect)
        {
            if (IsAnimationInProcess())
            {
                SliderAnimation();
            }
            else
            {
                if (_isCollectDone)
                {
                    _canCollect = false;
                    _slider.enabled = false;
                    _canAdd = true;
                }
            }
        }
       
        if(_canAdd)
        { 
            AddLoot(); 
            _canAdd = false;
            _canShowQuantity = true;
        }

        if (_canShowQuantity)
        {
            if(_count.enabled == false) _count.enabled = true;
            ShowCollectedLoot();
            _canShowQuantity = false;
            _isAllShowed = true;
        }

        if(_isCollectDone && _isAllShowed)
        {
            _goToNextState = true;
        }
    }

    //return false if time for collect is gone
    private bool IsAnimationInProcess()
    {
        if (_partOfSlider > _maxTime)
        {
            _isCollectDone = true;
            return false;
        }
        else
            return true;
    }

    //Show "animation" of collecting process
    private void SliderAnimation()
    {
        _slider.fillAmount = _partOfSlider / _maxTime;
    }

    //Just add loot to _countCollectedLoot
    private void AddLoot()
    { 
        _lootAmount++;
        _canAdd = false;
    }

    //Show Amount of collected loots
    private void ShowCollectedLoot()
    {
        if (_canShowQuantity)
        {
            _count.enabled = true;
            _count.text = _lootAmount.ToString();
        }
        else
        { _count.enabled = false;}
        
    }

    //Stop corountine and cleare fields and bools
    public void CompleteCollect()
    {
        StopCoroutine(TimerForCollect());
        _partOfSlider = 0;
        _isCollectDone = false;

        _collecctInProcess = false;
    }


    [Inject]
    private void Counstruct(Settings_for_CharacterAI settings)
    {       
        _maxTime = settings.TimeForCollect;   
    }
}
