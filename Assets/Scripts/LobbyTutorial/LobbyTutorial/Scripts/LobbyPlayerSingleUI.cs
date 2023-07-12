using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Unity.Services.Lobbies.Models;
using UnityEngine.UI;
using Unity.Netcode;
using Unity.VisualScripting;
using System;

public class LobbyPlayerSingleUI : MonoBehaviour {


     private TextMeshProUGUI playerNameText;
    [SerializeField] private Button kickPlayerButton;
    public GameObject go;
    public  Material  material;
    private Image characterImage;

    

    // private NetworkVariable<Color> syncolor = new NetworkVariable<Color>(Color.red,NetworkVariableReadPermission.Everyone,NetworkVariableWritePermission.Owner);

    private Player player;


    private void Awake() {
        kickPlayerButton.onClick.AddListener(KickPlayer);
        playerNameText = GetComponentInChildren<TextMeshProUGUI>();
        
        Image[] images = GetComponentsInChildren<Image>() ;
        // GetComponent<Image>().material;
        foreach (var image in images)
        {
            if (image.gameObject.tag == "UI canedit"){
                characterImage = image;
            }
        }
        go = characterImage.gameObject; 
        // Shader shader = Shader.Find("");
        // Material material = new Material(shader);
        material = go.GetComponent<Image>().material;
        characterImage.material = material;
    }   
    private void Start(){
        // syncolor.OnValueChanged += OnsyncolorChanged;
        
    }

    // private void OnsyncolorChanged(Color previousValue, Color newValue)
    // {
    //     characterImage.material.color = newValue;
    // }

    public void SetKickPlayerButtonVisible(bool visible) {
        kickPlayerButton.gameObject.SetActive(visible);
        
    }

    public  void UpdatePlayer(Player player) {

        // if(!IsOwner) return;
        this.player = player;
        // Debug.Log("Update PLAYER");


        if(playerNameText != null) playerNameText.text = player.Data[LobbyManager.KEY_PLAYER_NAME].Value;
        
        string Body_Color = player.Data[LobbyManager.KEY_PLAYER_COLOR].Value;
        string[] rgba = Body_Color.Substring(5, Body_Color.Length - 6).Split(", ");
        Color color = new Color(float.Parse(rgba[0]), float.Parse(rgba[1]), float.Parse(rgba[2]), float.Parse(rgba[3]));
        if(characterImage != null) {
            characterImage.material.color = color;
        }
       // LobbyManager.playerColor playerCharacter = 
        //     System.Enum.Parse<LobbyManager.PlayerCharacter>(player.Data[LobbyManager.KEY_PLAYER_CHARACTER].Value);
        // characterImage.sprite = LobbyAssets.Instance.GetSprite(playerCharacter);
    }

    private void KickPlayer() {
        if (player != null) {
            LobbyManager.Instance.KickPlayer(player.Id);
        }
    }


}