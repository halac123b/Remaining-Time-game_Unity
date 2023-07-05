using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class AstronautStatus : NetworkBehaviour
{
    // Start is called before the first frame update
    private Astronaut_inputSystem astronaut_InputSystem;
    private Animator anim;

    private void Awake()
    {
        astronaut_InputSystem = GetComponent<Astronaut_inputSystem>();    
        anim = GetComponent<Animator>();
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(!IsOwner) return;
        float x = astronaut_InputSystem.GetDirection().x;
        float y =astronaut_InputSystem.GetDirection().y;
        
        //  anim.SetFloat("x", x);
        //   anim.SetFloat("y", y);
        if (x <= -0.01f){
            transform.localScale = new Vector3(transform.localScale.y,transform.localScale.y,transform.localScale.z);
        }else if (x >= 0.01f){
            transform.localScale = new Vector3(-transform.localScale.y,transform.localScale.y,transform.localScale.z);
        }
        if (y > 0.01) {
            anim.SetFloat("y", 1f);
        }
        else if (y < -0.01){
            anim.SetFloat("y", -1f);
        }
        
        if (x > 0.01) {
            anim.SetFloat("x", 1f);
            anim.SetFloat("y", 0f);
        }
        else if (x < -0.01) {
            anim.SetFloat("x", -1f);
            anim.SetFloat("y", 0f);
        }
        anim.SetFloat("speed", astronaut_InputSystem.GetDirection().magnitude);
        anim.SetInteger("Scale",astronaut_InputSystem.GetSpeedScale());
    }

}
