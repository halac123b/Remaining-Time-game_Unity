using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterAnimator : AnimatorController
{
    // Start is called before the first frame update
    
    protected override void Awake(){
        
    }  
    // Update is called once per frame
     protected override void Update()
    {
        base.Update();
        if (!IsOwner) return;
    }
}
