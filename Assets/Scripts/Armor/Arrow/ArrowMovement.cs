using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowMovement : MonoBehaviour
{
    // Start is called before the first frame update
    public float Speed = 20;
    private Quaternion rotation;
    public Vector2 moveVector;
    // [SerializeField] Rigidbody2D rigidbody2D;
    public void SetMoveVector(Vector2 movevector){
        moveVector = movevector;
    }

    void Start()
    {
        

        float angle = Mathf.Atan2(moveVector.y, moveVector.x) * Mathf.Rad2Deg;
        rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        // rigidbody2D.AddForce(moveVector*-moveDistance*5000);
    }

    // Update is called once per frame
    void Update()
    {   
        
        

        transform.rotation = rotation;
        transform.position += new Vector3(moveVector.x, moveVector.y) * -Speed*Time.deltaTime;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        Destroy(gameObject);
    }
}
