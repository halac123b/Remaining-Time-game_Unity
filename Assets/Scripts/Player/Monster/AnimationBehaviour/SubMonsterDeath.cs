using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SubMonsterDeath : StateMachineBehaviour
{
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        //  deathTime(animator);
        SkeletonGruntAnimation skeletonGruntAnimation = animator.GetComponent<SkeletonGruntAnimation>();
        SkeletonHunterAnimation skeletonHunterAnimation = animator.GetComponent<SkeletonHunterAnimation>();
        if (skeletonGruntAnimation){
            skeletonGruntAnimation.ShowFloatText("+5s");
        }
        if (skeletonHunterAnimation){
            skeletonHunterAnimation.ShowFloatText("+5s");

        }
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (stateInfo.normalizedTime >= 3)
        Destroy(animator.GetComponentInParent<SkeletonMovement>().gameObject);
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    // override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    // {
       
    // }

    // OnStateMove is called right after Animator.OnAnimatorMove()
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that processes and affects root motion
    //}

    // OnStateIK is called right after Animator.OnAnimatorIK()
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that sets up animation IK (inverse kinematics)
    //}
}
