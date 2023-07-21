using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonGruntAnimation : MonoBehaviour
{
    protected const string HORIZONTAL = "Horizontal";
    protected const string VERTICAL = "Vertical";
    protected const string SPEED = "speed";
    protected const string ATTACK = "attack";
    protected const string ATTACK_CANCEL = "attackcancel";
    // Start is called before the first frame update
    [SerializeField] SkeletonGruntMovement skeletonGruntMovement;
    [SerializeField] Animator animator;

    // Update is called once per frame
    void Update()
    {
        Vector3 direction = skeletonGruntMovement.Getdirection();
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

        if (x > 0.3f)
        {
        animator.SetFloat(VERTICAL, 0f);
        animator.SetFloat(HORIZONTAL, 1f);
        }
        else if (x < -0.3f)
        {
        animator.SetFloat(VERTICAL, 0f);
        animator.SetFloat(HORIZONTAL, -1f);
        }

        animator.SetFloat(SPEED,direction.magnitude);
        if (InRangeAttack()) animator.SetTrigger(ATTACK);
    }

    /////////////////////Support//////////////////////////

     private bool InRangeAttack(){
        Collider2D[] colliders = Physics2D.OverlapCircleAll(new Vector2(transform.position.x,transform.position.y+1f), 0.6f);

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
            return true;
        }
        return false;
    }

    public void SetCantMove(){
        skeletonGruntMovement.canmove = false;
    }
    public void SetCanMove(){
        skeletonGruntMovement.canmove = true;

    }
}
