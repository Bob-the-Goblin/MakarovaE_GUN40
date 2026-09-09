using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class Search : StateMachineBehaviour
{
    private GameObject _characterAI;

    private Collect_module _collect_module;
    private Search_module _module;
    private NavMeshAgent _agent;

    private bool _canGo;

    private Vector3 _target;

    private bool _isFirst;


    private void Awake()
    {
        _isFirst = true;
    }
    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Debug.Log("Character AI in state Search");
        animator.SetFloat("timeInIdle", 0f);

        if(_isFirst){ UploadData(animator); _isFirst = false;}
        

        _canGo = true;
        _module.StartCheakPositionCoruntin();
    }

    private void UploadData(Animator animator)
    {
        _characterAI = animator.gameObject;

        _characterAI.TryGetComponent<Search_module>(out _module);
        if (_module == null)
        { Debug.LogError($"There is no Search_module on {_characterAI.name} - Search state"); return;}


        _characterAI.TryGetComponent<NavMeshAgent>(out _agent);
        if (_agent == null)
        { Debug.LogError($"There is no NavMeshAgent on {_characterAI.name} - Search state"); return; }

        _characterAI.TryGetComponent<Collect_module>(out _collect_module);

    }

    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

        if (_canGo)
        { 
            _module.SearchPosition();
            if (_module.IsThereLoot)
            {
                if (_module.Loot != null)
                {
                    animator.SetBool("FindTarget", true);
                    return;
                }
            }
            else
            {
                _target = _module.RandomCoordinates;
                if (!_collect_module.CollectInProcess)
                {
                    Debug.Log($"_target coords - {_target}");
                    _agent.destination = _target;
                }
            }
            _canGo = false;
        }
        else
        {
            if (_module.CanWeSearch == true)
            { _canGo = true;}
        }
        
    }

    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        _module.StopCheakPositionCorountine();
        _canGo = false;
        
    }
}
