using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : StateMachineBehaviour
{
    private EnemyController script;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Destroy(animator.gameObject);

    }

}
