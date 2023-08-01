using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonGruntAttack : StateMachineBehaviour
{
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
  
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
      // foreach (var param in animator.parameters)
      // {
      //    if (param.type == AnimatorControllerParameterType.Trigger)
      //    {
      //       animator.ResetTrigger(param.name);
      //    }
      // }
      //  Rigidbody2D rigidbody2D = animator.GetComponentInParent<Rigidbody2D>();
      //  rigidbody2D.AddForce(new Vector2());
      animator.GetComponent<SkeletonGruntAnimation>().SetCantMove();
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
      
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
      //  Rigidbody2D rigidbody2D = animator.GetComponentInParent<Rigidbody2D>();
      //  rigidbody2D.transform.position += new Vector3();
      animator.GetComponentInParent<Rigidbody2D>().velocity = Vector3.zero;
      // reset(animator); 
      animator.GetComponent<SkeletonGruntAnimation>().SetCanMove();
      
       
    }

    // private IEnumerable reset(Animator animator){
    //   yield return new WaitForSeconds(1f);
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
