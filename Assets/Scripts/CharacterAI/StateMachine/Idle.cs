using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditorInternal;
using UnityEngine;
using UnityEngine.AI;

public class Idle : StateMachineBehaviour
{
    private Idle_module _module;
    private bool _isFirst;

    private void Awake()
    {
        _isFirst = true;
    }

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Debug.Log("Character AI in state Idle");
        if (_isFirst)
        { 
            UploadData(animator);
            _isFirst = false;
        }

        _module.IsTimeToStart();
        
    }

    private void UploadData(Animator animator)
    {
        animator.TryGetComponent<Idle_module>(out _module);
        if (_module == null)
        { Debug.LogError($"There is no Idle_module on {animator.name} - in Idle State"); }
            
    }

    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
            animator.SetFloat("timeInIdle", _module.IsTimeToGo());      
    }
    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        _module.IsTimeToOver();
    }
}
