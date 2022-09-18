using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRangedAttackFSM : StateMachineBehaviour
{
    private SpriteRenderer sr;
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        sr = animator.gameObject.GetComponent<SpriteRenderer>();
        sr.color = Color.red;
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        
    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        sr.color = Color.blue;
    }
    
}
