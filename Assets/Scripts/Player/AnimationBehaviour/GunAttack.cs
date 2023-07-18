using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GunAttack : StateMachineBehaviour
{
     [SerializeField] GameObject Bullet;
     private float TimeAim =0.1f;
     private float count = 0f;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    private float x,y;
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        count =0f;
        // PlayerAnim playerAnim = animator.gameObject.transform.parent.gameObject.GetComponentInChildren<PlayerAnim>();
        // // playerAnim.AimBar.gameObject.SetActive(true);
        // playerAnim.UpdataMousePos();
        // Vector2 TargetVector =new Vector2(animator.gameObject.transform.position.x,animator.gameObject.transform.position.y) -  playerAnim.GetMousePos();
        // TargetVector.Normalize();

        // x= -TargetVector.x;
        // y= -TargetVector.y;
        
        //  if (playerAnim.AimBar.GetComponentInChildren<Slider>().value < 0.81f){
        //     float x_delta = 0f;
        //     if (x <= -0.5f ){
        //         x_delta = -0.5f; 
        //     }else if (x >= 0.5f ) x_delta = 0.5f;

        //     float y_delta = 0f;
        //     if ( x == 0f && y >= 0.05f ){
        //         y_delta = 0.5f; 
        //     }else if (x == 0f && y <= -0.05f ) y_delta = -0.5f;

        //     GameObject arrow = Instantiate(Bullet,new Vector3(animator.gameObject.transform.position.x+x_delta,animator.gameObject.transform.position.y+0.5f + y_delta),new Quaternion());
        //     arrow.GetComponent<BulletItemMovement>().SetMoveVector(TargetVector);
        //     playerAnim.AimBar.GetComponentInChildren<Slider>().value += TimeAim;
        // }

    }   
    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        PlayerAnim playerAnim = animator.gameObject.transform.parent.gameObject.GetComponentInChildren<PlayerAnim>();
        playerAnim.UpdataMousePos();
        Vector2 TargetVector =new Vector2(animator.gameObject.transform.position.x,animator.gameObject.transform.position.y) - playerAnim.GetMousePos();
        TargetVector.Normalize();
        x = -TargetVector.x;
        y = -TargetVector.y;
        if (x<0.5f && x > -0.5f) x= 0f; 
        playerAnim.Set_VERTICAL_HORIZONTAL(x,y);

         if (playerAnim.AimBar.GetComponentInChildren<Slider>().value < 0.91f && stateInfo.normalizedTime >= count){
            float x_delta = 0f;
            if (x <= -0.5f ){
                x_delta = -0.5f; 
            }else if (x >= 0.5f ) x_delta = 0.5f;

            float y_delta = 0f;
            if ( x == 0f && y >= 0.05f ){
                y_delta = 0.5f; 
            }else if (x == 0f && y <= -0.05f ) y_delta = -0.5f;

            GameObject arrow = Instantiate(Bullet,new Vector3(animator.gameObject.transform.position.x+x_delta,animator.gameObject.transform.position.y+0.5f + y_delta),new Quaternion());
            arrow.GetComponent<BulletItemMovement>().SetMoveVector(TargetVector);
            playerAnim.AimBar.GetComponentInChildren<Slider>().value += TimeAim;

            count += 2f;
        }

    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    // override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    // {
      
        // PlayerAnim playerAnim = animator.gameObject.transform.parent.gameObject.GetComponentInChildren<PlayerAnim>();
        // Vector2 TargetVector =new Vector2(animator.gameObject.transform.position.x,animator.gameObject.transform.position.y) -  playerAnim.GetMousePos();
        // TargetVector.Normalize();
    // }
    //}
}
