using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using Unity.Services.Lobbies.Models;
using UnityEngine;

public class AstronautStatus : NetworkBehaviour
{
    // Start is called before the first frame update
    private Astronaut_inputSystem astronaut_InputSystem;
    private Animator anim;
    [SerializeField] private SpriteRenderer spriteRenderer; 

    private NetworkVariable<Color> color = new NetworkVariable<Color>(Color.red,NetworkVariableReadPermission.Everyone,NetworkVariableWritePermission.Owner);    
    private NetworkVariable<bool> flipX = new NetworkVariable<bool>(false,NetworkVariableReadPermission.Everyone,NetworkVariableWritePermission.Owner);    

    private void Awake()
    {
        astronaut_InputSystem = GetComponent<Astronaut_inputSystem>();    
        anim = GetComponent<Animator>();
        
    }
    void Start()
    {
        // Shader shader = Shader.Find("CharacterSprite_Material");
        // spriteRenderer.material = new Material(shader);
        flipX.OnValueChanged += OnFlipXChanged;
        color.OnValueChanged += OnColorChanged;
    }

    private void OnColorChanged(Color previousValue, Color newValue)
    {
        spriteRenderer.material.color = newValue;
    }
    // Update is called once per frame
    void Update()
    {
        
        
        if(!IsOwner) return;
        color.Value = Color.blue;
        float x = astronaut_InputSystem.GetDirection().x;
        float y =astronaut_InputSystem.GetDirection().y;
        
        Lobby lobby = LobbyManager.Instance.GetJoinedLobby();
        

        //  anim.SetFloat("x", x);
        //   anim.SetFloat("y", y);
        if (x <= -0.01f){
            flipX.Value = false;
        }else if (x >= 0.01f){
            flipX.Value = true;
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

    private void OnFlipXChanged(bool oldValue, bool newValue)
    {
        spriteRenderer.flipX = newValue;
    }

}
