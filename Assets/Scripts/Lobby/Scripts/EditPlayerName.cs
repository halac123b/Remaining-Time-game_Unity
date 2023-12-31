using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class EditPlayerName : MonoBehaviour
{


  public static EditPlayerName Instance { get; private set; }


  public event EventHandler OnNameChanged;


  [SerializeField] private Text playerNameText;


  private string playerName = "player_" ;

  private void Awake()
  {
    Instance = this;
    playerName += UnityEngine.Random.Range(10,99).ToString();
    GetComponent<Button>().onClick.AddListener(() =>
    {
      UI_InputWindow.Show_Static("Player Name", playerName, "abcdefghijklmnopqrstuvxywzABCDEFGHIJKLMNOPQRSTUVXYWZ .,-", 20,
      () =>
      {
        // Cancel
      },
      (string newName) =>
      {
        playerName = newName;

        playerNameText.text = playerName;

        OnNameChanged?.Invoke(this, EventArgs.Empty);
      });
    });

    playerNameText.text = playerName;
  }

  private void Start()
  {
    OnNameChanged += EditPlayerName_OnNameChanged;
  }

  private void EditPlayerName_OnNameChanged(object sender, EventArgs e)
  {
    LobbyManager.Instance.UpdatePlayerName(GetPlayerName());
  }

  public string GetPlayerName()
  {
    return playerName;
  }
}