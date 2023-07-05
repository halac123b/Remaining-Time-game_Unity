using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player2Controller : NetworkBehaviour
{
    private float speed =7;
    private Vector3 dir;

    private void Awake()
    {

    }
    void Start()
    {
      
    }

    // Update is called once per frame
    void Update()
    {
        dir = Vector3.zero;
        if(Input.GetKey(KeyCode.A)){
            dir.x -=1;
        }
        if(Input.GetKey(KeyCode.D)){
            dir.x +=1;
        }
        if(Input.GetKey(KeyCode.S)){
            dir.y -=1;
        }
        if(Input.GetKey(KeyCode.W)){
            dir.y +=1;
        }
        transform.position += dir*speed*Time.deltaTime;
    }

    public void Move(InputAction.CallbackContext callback){
        // Debug.Log("2 Move!!" + callback.phase);

    }
}
