using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyChaseFSM : StateMachineBehaviour
{
    private EnemyController script;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        script = animator.gameObject.GetComponent<EnemyController>();
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        script.ChasePlayer();
        if (script.isInAttackRange) animator.SetTrigger("canAttack");
    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.ResetTrigger("canAttack");
        // script.StopMovement();
    }
    
}
