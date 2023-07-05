using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.InputSystem;

public class Astronaut_inputSystem : NetworkBehaviour
{
    private Rigidbody2D rigidify2D;
    private PlayerInput playerInput;
    private PlayerAction playerAction;
    private float speed;
    // Start is called before the first frame update

    [SerializeField] private Transform spawnObj;

    private void Awake()
    {
        playerInput = GetComponent<PlayerInput>();            
        playerAction = new PlayerAction();
        speed = 5f;
        playerAction.Player.Enable();
        playerAction.Player.Run.performed += Run;
        playerAction.Player.Duck.performed += Duck;
    }
    void Start()
    {
        
    }

   
    // Update is called once per frame
    void Update()
    {
        if(!IsOwner) return;
        if(Input.GetKeyDown(KeyCode.T)){
            Instantiate(spawnObj);
        }
        Vector2 dic =  playerAction.Player.Move.ReadValue<Vector2>();
        transform.position += new Vector3(dic.x,dic.y,0f)*speed*Time.deltaTime;
        
    }
    public Vector2 GetDirection(){
        return playerAction.Player.Move.ReadValue<Vector2>();
    }
    private void Run(InputAction.CallbackContext context){
        if(context.ReadValueAsButton()){
            speed = 8f;
        }else speed = 5f;
    }
    private void Duck(InputAction.CallbackContext context){
        if(context.ReadValueAsButton()){
            speed = 3f;
        }else speed = 5f;
    }
    public int GetSpeedScale(){
        if(speed > 5f) return 1;
        if(speed < 5f) return -1;
        return 0;
    }

    [ServerRpc]
    private void TestServerRPC(){
        Debug.Log("TestServerRPC: " + OwnerClientId);
    }
}
