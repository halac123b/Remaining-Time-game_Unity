using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Services.Authentication;
using Unity.Services.Lobbies.Models;
using UnityEngine;
using UnityEngine.UI;

public class LobbyUI : MonoBehaviour
{


  public static LobbyUI Instance { get; private set; }


  [SerializeField] private Transform playerSingleTemplate;
  [SerializeField] private Transform container;
  [SerializeField] private TextMeshProUGUI lobbyNameText;
  [SerializeField] private TextMeshProUGUI playerCountText;

  [SerializeField] private Button leaveLobbyButton;
  [SerializeField] private Button StartGameButton;

  private void Awake()
  {
    Instance = this;

    playerSingleTemplate.gameObject.SetActive(false);


    leaveLobbyButton.onClick.AddListener(() =>
    {
      LobbyManager.Instance.LeaveLobby();
    });

    // changeGameModeButton.onClick.AddListener(() =>
    // {
    //     LobbyManager.Instance.ChangeGameMode();
    // });
    StartGameButton.onClick.AddListener(() =>
    {
      LobbyManager.Instance.StartGame(StartGameButton);
    });
  }


  private void Start()
  {
    LobbyManager.Instance.OnJoinedLobby += UpdateLobby_Event;
    LobbyManager.Instance.OnJoinedLobbyUpdate += UpdateLobby_Event;
    LobbyManager.Instance.OnLobbyGameModeChanged += UpdateLobby_Event;
    LobbyManager.Instance.OnLeftLobby += LobbyManager_OnLeftLobby;
    LobbyManager.Instance.OnKickedFromLobby += LobbyManager_OnLeftLobby;

    Hide();
  }

  private void LobbyManager_OnLeftLobby(object sender, System.EventArgs e)
  {
    ClearLobby();
    Hide();
  }

  private void UpdateLobby_Event(object sender, LobbyManager.LobbyEventArgs e)
  {
    UpdateLobby();
  }

  private void UpdateLobby()
  {
    if (playerSingleTemplate != null)
      UpdateLobby(LobbyManager.Instance.GetJoinedLobby());
  }

  public void UpdateLobby(Lobby lobby)
  {

    ClearLobby();

    foreach (Player player in lobby.Players)
    {
      Transform playerSingleTransform = Instantiate(playerSingleTemplate, container);
      playerSingleTransform.gameObject.SetActive(true);
      LobbyPlayerSingleUI lobbyPlayerSingleUI = playerSingleTransform.GetComponent<LobbyPlayerSingleUI>();

      lobbyPlayerSingleUI.SetKickPlayerButtonVisible(
          LobbyManager.Instance.IsLobbyHost() &&
          player.Id != AuthenticationService.Instance.PlayerId // Don't allow kick self
      );
      lobbyPlayerSingleUI.UpdatePlayer(player);
    }

    // changeGameModeButton.gameObject.SetActive(LobbyManager.Instance.IsLobbyHost());
    StartGameButton.gameObject.SetActive(LobbyManager.Instance.IsLobbyHost());


    lobbyNameText.text = lobby.Name;
    playerCountText.text = lobby.Players.Count + "/" + lobby.MaxPlayers;
    // gameModeText.text = lobby.Data[LobbyManager.KEY_GAME_MODE].Value;

    if (lobby.Data[LobbyManager.KEY_START_GAME].Value == "0") Show();
    else Hide();
  }

  private void ClearLobby()
  {
    if (container)
      foreach (Transform child in container)
      {
        if (child != null)
        {
          if (child == playerSingleTemplate) continue;
          Destroy(child.gameObject);
        }
      }
  }

  private void Hide()
  {
    gameObject.SetActive(false);
  }

  private void Show()
  {
    gameObject.SetActive(true);
  }
}