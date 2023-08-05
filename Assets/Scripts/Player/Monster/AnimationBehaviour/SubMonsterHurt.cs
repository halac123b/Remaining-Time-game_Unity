using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SubMonsterHurt : StateMachineBehaviour
{
    MonsterAnimator monsterAnimation;
    SkeletonGruntAnimation skeletonGruntAnimation;
    SkeletonHunterAnimation skeletonHunterAnimation;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
     //   monsterAnimation = animator.GetComponent<MonsterAnimator>();
     //   skeletonGruntAnimation = animator.GetComponent<SkeletonGruntAnimation>();
     //   skeletonHunterAnimation = animator.GetComponent<SkeletonHunterAnimation>();
     //    Destroy(animator.GetComponent<CapsuleCollider2D>());

    }
 
    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    //override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

     //    Destroy(animator.GetComponent<CapsuleCollider2D>());
     //   if (monsterAnimation){
     //        CapsuleCollider2D capsuleCollider2D = monsterAnimation.gameObject.AddComponent<CapsuleCollider2D>();
     //        capsuleCollider2D.isTrigger = true;
     //        capsuleCollider2D.size = new Vector2(1f,2.5f);
     //        capsuleCollider2D.offset = new Vector2(0f,2f);
     //   }else if (skeletonGruntAnimation){
     //        CapsuleCollider2D capsuleCollider2D = skeletonGruntAnimation.gameObject.AddComponent<CapsuleCollider2D>();
     //        capsuleCollider2D.isTrigger = true;
     //        capsuleCollider2D.size = new Vector2(0,0.2f);
     //        capsuleCollider2D.offset = new Vector2(0.1f,0.3f);
     //   }else if (skeletonHunterAnimation){
     //        CapsuleCollider2D capsuleCollider2D = skeletonHunterAnimation.gameObject.AddComponent<CapsuleCollider2D>();
     //        capsuleCollider2D.isTrigger = true;
     //        capsuleCollider2D.size = new Vector2(0,0.2f);
     //        capsuleCollider2D.offset = new Vector2(0.1f,0.3f);
     //   }
      animator.GetComponentInParent<Rigidbody2D>().velocity = Vector3.zero;
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
