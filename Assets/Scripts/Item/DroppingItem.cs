using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroppingItem : MonoBehaviour
{
    private const float Y = 0.2f;
    private float y_pos;
    private float detal_y=0.005f; 
    private float y=0;
    void Start()
    {
        y_pos = transform.position.y;
    }

    // Update is called once per frame
    void Update()
    {
        y+= detal_y;
        if (Mathf.Abs(y) >= Y){
            detal_y =- detal_y;
        } 
        transform.position = new Vector3(transform.position.x,y_pos+y);

    }
}
