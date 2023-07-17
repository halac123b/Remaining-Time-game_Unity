using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class BowAttack : StateMachineBehaviour
{

    [SerializeField] GameObject Arrow;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    //override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
       Vector2 mouse_pos = Input.mousePosition;
        mouse_pos = Camera.main.ScreenToWorldPoint(mouse_pos);
        Vector2 TargetVector =new Vector2(animator.gameObject.transform.position.x,animator.gameObject.transform.position.y) -  mouse_pos;
        TargetVector.Normalize();

        PlayerAnimator playerAnimator = animator.gameObject.transform.parent.gameObject.GetComponentInChildren<PlayerAnimator>();
        float x = -TargetVector.x;
        float y = -TargetVector.y;
        if (x<0.5f && x > -0.5f) x= 0f; 
        playerAnimator.Set_VERTICAL_HORIZONTAL(x,y);
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Vector2 mouse_pos =Input.mousePosition;
        mouse_pos = Camera.main.ScreenToWorldPoint(mouse_pos);
     
        Debug.LogWarning(mouse_pos + "  VS  " + animator.gameObject.transform.position );
     
        Vector2 TargetVector =new Vector2(animator.gameObject.transform.position.x,animator.gameObject.transform.position.y) -  mouse_pos;
        TargetVector.Normalize();
        GameObject arrow = Instantiate(Arrow,new Vector3(animator.gameObject.transform.position.x,animator.gameObject.transform.position.y+0.5f),new Quaternion());
        arrow.GetComponent<ArrowMovement>().SetMoveVector(TargetVector);
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
