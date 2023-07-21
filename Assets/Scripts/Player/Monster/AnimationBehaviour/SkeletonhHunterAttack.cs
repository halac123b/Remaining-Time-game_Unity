using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonHunterAttack : StateMachineBehaviour
{
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    [SerializeField] GameObject Arrow;
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
       animator.GetComponent<SkeletonHunterAnimation>().SetCantMove();
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    //override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
       SkeletonHunterAnimation anim =  animator.GetComponent<SkeletonHunterAnimation>();
       GameObject arrow = Instantiate(Arrow, new Vector3(animator.gameObject.transform.position.x, animator.gameObject.transform.position.y + 0.5f), new Quaternion());
       arrow.GetComponent<BulletItemMovement>().SetMoveVector(new Vector2(- anim.GetShotTarget().x,-anim.GetShotTarget().y));
       anim.SetCanMove();
       
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
