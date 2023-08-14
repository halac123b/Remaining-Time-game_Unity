using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SubMonsterSword : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] Animator Gruntanimator;
 

    void Start()
    {
        
    }

    
    void Update()
    { 
       Destroy(GetComponent<BoxCollider2D>()); 
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        Vector2 attackVector = new Vector2((Gruntanimator.GetFloat("Vertical")==0)?Gruntanimator.GetFloat("Horizontal"):0,Gruntanimator.GetFloat("Vertical")); 
            if (other.gameObject.layer ==LayerMask.NameToLayer("Player") && other.GetComponent<PlayerAnimator>()){
                // Debug.LogError("fuck");        
                other.GetComponent<PlayerAnimator>().AstronautHurtClientRpc(1,attackVector,2);
            }
    }
    public void CreateTrigger(){

        float x = Gruntanimator.GetFloat("Horizontal");
        float y = Gruntanimator.GetFloat("Vertical");

        if (Mathf.Abs(y) >0.1f) x = 0;
        var trigg = gameObject.AddComponent<BoxCollider2D>();
        trigg.isTrigger = true;
        
            if (y == 0){
                trigg.offset =  new Vector2(0.5f*x, 0);
                trigg.size = new Vector2(0.7f, 1.5f);
            }else{
                trigg.offset =  new Vector2(0, 0.5f*y);
                trigg.size = new Vector2(1.5f, 0.7f);
            }
        
    
    }
}
