using System.Collections;
using System.Collections.Generic;
using Unity.Burst.Intrinsics;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.UI;

public class SkeletonHunterAnimation : NetworkBehaviour
{
    protected const string HORIZONTAL = "Horizontal";
    protected const string VERTICAL = "Vertical";
    protected const string SPEED = "speed";
    protected const string ATTACK = "attack";
    protected const string ATTACK_CANCEL = "attackcancel";
    protected const string HURT = "hurt";
    protected const string DEATH = "death";


    // Start is called before the first frame update
    [SerializeField] SkeletonMovement skeletonMovement;
    [SerializeField] Animator animator;
    [SerializeField] public Transform AimBar;

    private Weapon reset_weapon;
    
    private Vector2 shottarget;
    // Update is called once per frame
    private float countDownt = 0;
    void Update()
    {
        AimBar.GetComponentInChildren<Slider>().value = skeletonMovement.HP.Value/100f;
        if (skeletonMovement.HP.Value <= 0) {
            return;
        }
        countDownt += Time.deltaTime;
        Vector3 direction = skeletonMovement.Getdirection();
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
        if (!skeletonMovement.canmove)animator.SetFloat(SPEED,0);

        if (InRangeAttack() != new Vector2(0,0) && countDownt >=5) {
            animator.SetTrigger(ATTACK);
            countDownt = 0;
        }
        if (InRangeAttack() != new Vector2(0,0)) skeletonMovement.canmove = false;
        else skeletonMovement.canmove = true;
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
    public void GetHurt(int dame,Vector2 pos,int nockBack, Weapon weapon){
        // if (skeletonMovement.HP < 0) return;
        reset_weapon = weapon;
        if(!IsOwner) return;
        if (skeletonMovement.HP.Value > 0 ) {
            animator.SetTrigger(HURT);
            GetComponentInParent<Rigidbody2D>().AddForce(pos.normalized*nockBack,ForceMode2D.Impulse);
            skeletonMovement.HP.Value -= dame;
        }
        if (skeletonMovement.HP.Value <=0)
        {
            animator.SetTrigger(DEATH);
            reset_weapon.increateTime(5);
        }
    }

    public void ShowFloatText(string text){
            GameObject floatingtext = Instantiate(reset_weapon.FloatingText,reset_weapon.playerMovement.transform.position, Quaternion.identity,    reset_weapon.playerMovement.transform);
            floatingtext.transform.position += new Vector3(Random.Range(-1f, 1f),Random.Range(-1f, 1f),0);
            floatingtext.GetComponent<TextMesh>().text = text;
    }
    public void SetCantMove(){
        if(!IsOwner) return;
        skeletonMovement.canmove = false;
    }
    public void SetCanMove(){
        if(!IsOwner) return;
        skeletonMovement.canmove = true;

    }
    public Vector2 GetShotTarget(){
        return shottarget;
    }
}
