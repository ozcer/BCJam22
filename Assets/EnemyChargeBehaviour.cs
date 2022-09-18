using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyChargeBehaviour : StateMachineBehaviour
{
    private EnemyCharge script;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        script = animator.gameObject.GetComponent<EnemyCharge>();
        script.ChargeBegin();
    }
}
