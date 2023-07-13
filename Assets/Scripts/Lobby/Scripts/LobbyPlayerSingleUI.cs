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
    private GameObject go;
    private  Material  material;
    private Image characterImage;

    private Player player;


    private void Awake() {
        kickPlayerButton.onClick.AddListener(KickPlayer);
        playerNameText = GetComponentInChildren<TextMeshProUGUI>();
        
        Image[] images = GetComponentsInChildren<Image>() ;
        foreach (var image in images)
        {
            if (image.gameObject.tag == "UI canedit"){
                characterImage = image;
            }
        }
        go = characterImage.gameObject; 
        // Shader shader = Shader.Find("");
        // Material material = new Material(shader);
        material = go.GetComponent<MeshRenderer>().material;
        characterImage.material = material;
    }
    public void SetKickPlayerButtonVisible(bool visible) {
        kickPlayerButton.gameObject.SetActive(visible);
        
    }

    public  void UpdatePlayer(Player player) {

        this.player = player;


        if(playerNameText != null) playerNameText.text = player.Data[LobbyManager.KEY_PLAYER_NAME].Value;
        
        string Body_Color = player.Data[LobbyManager.KEY_PLAYER_COLOR].Value;
        string[] rgba = Body_Color.Substring(5, Body_Color.Length - 6).Split(", ");
        Color color = new Color(float.Parse(rgba[0]), float.Parse(rgba[1]), float.Parse(rgba[2]), float.Parse(rgba[3]));
        if(characterImage != null) {
            characterImage.material.color = color;
        }
    }

    private void KickPlayer() {
        if (player != null) {
            LobbyManager.Instance.KickPlayer(player.Id);
        }
    }


}