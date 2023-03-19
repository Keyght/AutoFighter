using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WhenAttackDone : StateMachineBehaviour
{
    private static readonly int _attacking = Animator.StringToHash("Attacking");

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.SetBool(_attacking, false);
    }
}
