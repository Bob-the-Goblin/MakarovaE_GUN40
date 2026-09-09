using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class Collect : StateMachineBehaviour
{
    Search_module _search_module;
    Collect_module _collect_module;
    private NavMeshAgent _agent;

    private bool _isOntheWay;
    private bool _isItCloseToLoot;

    private float _collectDistance = 0.75f;

    private bool _isDeleted;

    private bool _isFirst;

    private void Awake()
    {
        _isFirst = true;
    }

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Transform loot;

        Debug.Log("Character AI in state Collect");
        animator.SetBool("FindTarget", false);

        if (_isFirst) { UploadData(animator); _isFirst = false; }
        
        _isItCloseToLoot = false;
        _isOntheWay = true;
        _isDeleted = false;

        if(_search_module.Loot == null)
        { Debug.LogError("Bad deleter. Loot is null"); }
        else 
        {   loot = _search_module.Loot.transform;
            _search_module.DeleteLootOnModule();
            _agent.destination = loot.transform.position;
        }

        _collect_module.StartCollect();
    }

    private void UploadData(Animator animator)
    {
        animator.TryGetComponent<Search_module>(out _search_module);
        if (_search_module == null) 
        { Debug.LogError($"There is no Search module on {animator.name} - Collect state"); }

        animator.TryGetComponent<Collect_module>(out _collect_module);
        if (_collect_module == null)
        { Debug.LogError($"There is no Search module on {animator.name} - Collect state"); }

        animator.TryGetComponent<NavMeshAgent>(out _agent);
        if (_agent == null)
        { Debug.LogError($"There is no NavMeshAgent on {animator.name} - Collect state"); }
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

        if (_agent.remainingDistance <= _collectDistance && _isOntheWay)
        {
            _agent.destination = animator.transform.position;
            _isItCloseToLoot = true;
            _isOntheWay = false;
        }
        if(_isItCloseToLoot)
        { 
            _collect_module.StartCollect();
            _isItCloseToLoot = false;
            _isDeleted = false;
        }



        if (_collect_module.CanGoToNextState && !_isDeleted)
        {
            _isDeleted = true;
            _search_module.DeleteLootOnModule();
            animator.SetBool("isCollected", true);
        }

    }
    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    { 
        _collect_module.CompleteCollect();
    }

    

}
