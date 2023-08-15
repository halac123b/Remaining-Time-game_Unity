using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;


public class CameraControl : MonoBehaviour
{
    // Start is called before the first frame update
    PlayerMovement playerMovement;
    [SerializeField] public float speed = 0.2f;
    [SerializeField] public float size = 4.5f;
    // private bool zoom = false;

    private float zoomspeed;

    // Update is called once per frame
    private void FixedUpdate() {
        foreach(var movement in FindObjectsByType<PlayerMovement>(sortMode: FindObjectsSortMode.InstanceID)){
            if(movement.GetClientId() == NetworkManager.Singleton.LocalClientId){
                playerMovement = movement;
            }
        }    
        if (FindObjectsByType<PlayerMovement>(sortMode: FindObjectsSortMode.InstanceID).Length == 0) playerMovement = null;
    }
    void Update()
    {
        Vector3 target = new Vector3();
        if (playerMovement) target  = playerMovement.transform.position;
        
        target.z= -10f;
        if(playerMovement && Vector2.Distance(transform.position, playerMovement.transform.position) > 1f){
            transform.position = Vector3.Lerp ( transform.position, target, Time.deltaTime * 5f);
        }
        if (playerMovement && Mathf.Abs(Camera.main.orthographicSize - size) > 0.05f){
            Camera.main.orthographicSize -= speed* (Camera.main.orthographicSize - size) / Vector2.Distance(transform.position, playerMovement.transform.position);
        }
        
    }
}
