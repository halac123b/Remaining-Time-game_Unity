using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterDeath : StateMachineBehaviour
{
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
        MonsterAnimator monsterAnimator ;

     override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        //  deathTime(animator);
       
        monsterAnimator = animator.GetComponent<MonsterAnimator>();
        if (monsterAnimator){
            monsterAnimator.SetMove(false);
            monsterAnimator.ShowFloatText("+20s");
        }
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        monsterAnimator.SetMove(false);    
        if (stateInfo.normalizedTime >=0.2)
        animator.SetTrigger("revive");
    }


    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Destroy(animator.GetComponent<CapsuleCollider2D>());
        monsterAnimator.SetMove(false);
        
    }

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
