using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterSword : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] Animator monsterAnimator;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    { 
       Destroy(GetComponent<BoxCollider2D>()); 
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        Vector2 attackVector = new Vector2((monsterAnimator.GetFloat("Vertical")==0)?monsterAnimator.GetFloat("Horizontal"):0,monsterAnimator.GetFloat("Vertical")); 
            if (other.gameObject.layer ==LayerMask.NameToLayer("Player") && other.GetComponent<PlayerAnimator>()){        
                if (monsterAnimator.GetInteger("typeattack") == 1) other.GetComponent<PlayerAnimator>().AstronautHurtClientRpc(3,attackVector,5);
                if (monsterAnimator.GetInteger("typeattack") == 2) other.GetComponent<PlayerAnimator>().AstronautHurtClientRpc(5,attackVector,2);

            }
    }
    public void CreateTrigger( int type){

        if (type > 2 && type < 1) return;

        float x = monsterAnimator.GetFloat("Horizontal");
        float y = monsterAnimator.GetFloat("Vertical");

        if (Mathf.Abs(y) >0.1f) x = 0;
        var trigg = gameObject.AddComponent<BoxCollider2D>();
        trigg.isTrigger = true;

        if (type == 1){

            if (y == 0){
                trigg.offset =  new Vector2(1.2f*x, 0);
                trigg.size = new Vector2(2f, 3f);
            }else{
                trigg.offset =  new Vector2(0, 1f*y);
                trigg.size = new Vector2(3f, 2f);
            }
        }else if (type == 2){
            if (y == 0){
                trigg.offset =  new Vector2(0.5F*x, 0.5f);
                trigg.size = new Vector2(3f, 4f);
            }else{
                trigg.offset =  new Vector2(0, 0.5f*y);
                trigg.size = new Vector2(4f, 2.5f);
            }
        }        
    
    }
}
