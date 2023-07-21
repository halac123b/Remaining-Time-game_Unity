using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SummonSubMonster : StateMachineBehaviour
{
    [SerializeField] GameObject submonter;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    //override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    // override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    // {
        
    // }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if(stateInfo.normalizedTime >= 1f){
            
            Instantiate(submonter,new Vector3(animator.transform.position.x+1,animator.transform.position.y + 2f),new Quaternion());
            Instantiate(submonter,new Vector3(animator.transform.position.x-1,animator.transform.position.y + 2f),new Quaternion());
            Instantiate(submonter,new Vector3(animator.transform.position.x,animator.transform.position.y+1 + 2f),new Quaternion());
            Instantiate(submonter,new Vector3(animator.transform.position.x,animator.transform.position.y-1 + 2f),new Quaternion());
        }
        
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
