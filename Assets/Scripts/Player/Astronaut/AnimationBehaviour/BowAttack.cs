using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class BowAttack : StateMachineBehaviour
{

  [SerializeField] GameObject Arrow;
  private float TimeAim = 1.5f;
  // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
  override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
  {
    PlayerAnimator playerAnim = animator.gameObject.transform.parent.gameObject.GetComponentInChildren<PlayerAnimator>();
    playerAnim.AimBar.gameObject.SetActive(true);
    playerAnim.AimBar.GetComponentInChildren<Slider>().value = 0;

  }

  // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
  override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
  {
    PlayerAnimator playerAnim = animator.gameObject.transform.parent.gameObject.GetComponentInChildren<PlayerAnimator>();
    playerAnim.UpdataMousePos();
    Vector2 TargetVector = new Vector2(animator.gameObject.transform.position.x, animator.gameObject.transform.position.y) - playerAnim.GetMousePos();
    TargetVector.Normalize();
    float x = -TargetVector.x;
    float y = -TargetVector.y;
    if (x < 0.5f && x > -0.5f) x = 0f;
    playerAnim.Set_VERTICAL_HORIZONTAL(x, y);

    playerAnim.AimBar.GetComponentInChildren<Slider>().value = stateInfo.normalizedTime / TimeAim;
  }

  // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
  override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
  {

    PlayerAnimator playerAnim = animator.gameObject.transform.parent.gameObject.GetComponentInChildren<PlayerAnimator>();
    Vector2 TargetVector = new Vector2(animator.gameObject.transform.position.x, animator.gameObject.transform.position.y) - playerAnim.GetMousePos();
    TargetVector.Normalize();
    playerAnim.AimBar.gameObject.SetActive(false);

    ; if (stateInfo.normalizedTime >= TimeAim)
    {
      GameObject arrow = Instantiate(Arrow, new Vector3(animator.gameObject.transform.position.x, animator.gameObject.transform.position.y + 0.5f), new Quaternion());
      arrow.GetComponent<BulletItemMovement>().SetMoveVector(TargetVector);
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
