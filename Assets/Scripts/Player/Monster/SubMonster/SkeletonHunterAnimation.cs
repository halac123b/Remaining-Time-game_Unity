using System.Collections;
using System.Collections.Generic;
using Unity.Burst.Intrinsics;
using UnityEngine;

public class SkeletonHunterAnimation : MonoBehaviour
{
    protected const string HORIZONTAL = "Horizontal";
    protected const string VERTICAL = "Vertical";
    protected const string SPEED = "speed";
    protected const string ATTACK = "attack";
    protected const string ATTACK_CANCEL = "attackcancel";
    protected const string HURT = "hurt";

    // Start is called before the first frame update
    [SerializeField] SkeletonGruntMovement skeletonGruntMovement;
    [SerializeField] Animator animator;

    private Vector2 shottarget;
    // Update is called once per frame
    void Update()
    {
        Vector3 direction = skeletonGruntMovement.Getdirection();
        if (direction != new Vector3(0,0)) shottarget = direction;
        float x = direction.x;
        float y =direction.y;

        if (y > 0.01f)
        {
        animator.SetFloat(VERTICAL, 1f);
        }
        else if (y < -0.01f)
        {
        animator.SetFloat(VERTICAL, -1f);
        }

        if (x > 0.4f)
        {
        animator.SetFloat(VERTICAL, 0f);
        animator.SetFloat(HORIZONTAL, 1f);
        }
        else if (x < -0.4f)
        {
        animator.SetFloat(VERTICAL, 0f);
        animator.SetFloat(HORIZONTAL, -1f);
        }

        animator.SetFloat(SPEED,direction.magnitude);
        if (InRangeAttack() != new Vector2(0,0)) animator.SetTrigger(ATTACK);
    }

    /////////////////////Support//////////////////////////

     public Vector2 InRangeAttack(){
        Collider2D[] colliders = Physics2D.OverlapCircleAll(new Vector2(transform.position.x,transform.position.y+1f), 5f);

        // Lọc ra vật thể gần nhất có layer là "Player"
        Transform nearestPlayer = null;
        float nearestDistance = Mathf.Infinity;
        foreach (Collider2D collider in colliders)
        {
            if (collider.gameObject.layer == LayerMask.NameToLayer("Player"))
            {
                float distance = Vector2.Distance(transform.position, collider.transform.position);
                if (distance < nearestDistance)
                {
                    nearestPlayer = collider.transform;
                    nearestDistance = distance;
                }
            }
        }

        // Nếu có vật thể "Player" gần nhất, di chuyển vật thể của bạn đến gần vật thể đó
        if (nearestPlayer != null)
        {
           Vector2 direction = (nearestPlayer.position - transform.position).normalized;
           return direction;
        }
        return new Vector2 (0,0);
    }
    public void GetHurt(int dame,int nockBack){
        animator.SetTrigger(HURT);
    }
    public void SetCantMove(){
        skeletonGruntMovement.canmove = false;
    }
    public void SetCanMove(){
        skeletonGruntMovement.canmove = true;

    }
    public Vector2 GetShotTarget(){
        return shottarget;
    }
}
