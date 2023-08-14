using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterRevive : StateMachineBehaviour
{
    HealthLazer healthLazer ;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
             healthLazer = FindAnyObjectByType<HealthLazer>();
            Destroy(animator.GetComponent<CapsuleCollider2D>());
            if(healthLazer) healthLazer.relive = true;
            
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        MonsterAnimator monsterAnimation = animator.GetComponent<MonsterAnimator>();
        // Debug.LogError("length: "+ stateInfo.length);
        // Debug.LogError("normtime: "+ stateInfo.normalizedTime);

        if (monsterAnimation.HP.Value < monsterAnimation.MAX_HP)  monsterAnimation.ADDhp(0.3f);
        else{
            animator.SetTrigger("exit");
            if(healthLazer) healthLazer.relive = false;
        }
        

    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        MonsterAnimator monsterAnimation = animator.GetComponent<MonsterAnimator>();
        Destroy(animator.GetComponent<CapsuleCollider2D>());
       CapsuleCollider2D capsuleCollider2D = monsterAnimation.gameObject.AddComponent<CapsuleCollider2D>();
        capsuleCollider2D.size = new Vector2(1,2.5f);
        capsuleCollider2D.offset = new Vector2(0,2);
        capsuleCollider2D.isTrigger = true;
        monsterAnimation.NumMonsterReal = monsterAnimation.playerStatus.NumMonster;
        monsterAnimation.SetMove(true);

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
