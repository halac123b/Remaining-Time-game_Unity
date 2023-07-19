using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MonsterShot : StateMachineBehaviour
{
  [SerializeField] GameObject Bullet;
  private float TimeAim = 1f;
  // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
  private float x, y;
  override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
  {
    MonsterAnimator monsterAnimator = animator.gameObject.GetComponent<MonsterAnimator>();
     monsterAnimator.AimBar.gameObject.SetActive(true);
     monsterAnimator.AimBar.GetComponentInChildren<Slider>().value = 0;

  }
  // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
  override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
  {
     MonsterAnimator  monsterAnimator = animator.gameObject.transform.parent.gameObject.GetComponentInChildren< MonsterAnimator>();
     monsterAnimator.UpdataMousePos();
    Vector2 TargetVector = new Vector2(animator.gameObject.transform.position.x, animator.gameObject.transform.position.y) -  monsterAnimator.GetMousePos();
    TargetVector.Normalize();
    x = -TargetVector.x;
    y = -TargetVector.y;
    if (x < 0.5f && x > -0.5f) x = 0f;
     monsterAnimator.Set_VERTICAL_HORIZONTAL(x, y);

     monsterAnimator.AimBar.GetComponentInChildren<Slider>().value = stateInfo.normalizedTime / TimeAim;

  }

  // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
  override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
  {

     MonsterAnimator  monsterAnimator = animator.gameObject.transform.parent.gameObject.GetComponentInChildren< MonsterAnimator>();
    Vector2 TargetVector = new Vector2(animator.gameObject.transform.position.x, animator.gameObject.transform.position.y) -  monsterAnimator.GetMousePos();
    TargetVector.Normalize();
     monsterAnimator.AimBar.gameObject.SetActive(false);
    if (stateInfo.normalizedTime >= TimeAim)
    {
      float x_delta = 0f;
      if (x <= -0.5f)
      {
        x_delta = -0.5f;
      }
      else if (x >= 0.5f) x_delta = 0.5f;

      float y_delta = 0f;
      if (x == 0f && y >= 0.05f)
      {
        y_delta = 0.5f;
      }
      else if (x == 0f && y <= -0.05f) y_delta = -0.5f;

      GameObject arrow = Instantiate(Bullet, new Vector3(animator.gameObject.transform.position.x + x_delta, animator.gameObject.transform.position.y + 1f + y_delta), new Quaternion(0,0,180,0));
      arrow.GetComponent<BulletItemMovement>().SetMoveVector(TargetVector);
    }
  }
  //}
}
