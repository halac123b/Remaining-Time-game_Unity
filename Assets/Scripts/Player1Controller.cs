using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// using System.InputAction;
public class Player1Controller : MonoBehaviour
{
    private float speed =7;
    private Vector3 dir;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
        dir = Vector3.zero;
        if(Input.GetKey(KeyCode.UpArrow)){
            dir.x -=1;
        }
        if(Input.GetKey(KeyCode.LeftArrow)){
            dir.x +=1;
        }
        if(Input.GetKey(KeyCode.DownArrow)){
            dir.y -=1;
        }
        if(Input.GetKey(KeyCode.RightArrow)){
            dir.y +=1;
        }
        transform.position += dir*speed*Time.deltaTime;
    }

    public void Move(){
        
    }
}
