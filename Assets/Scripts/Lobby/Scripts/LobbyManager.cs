using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Services.Authentication;
using Unity.Services.Core;
using Unity.Services.Lobbies;
using Unity.Services.Lobbies.Models;
using UnityEngine;
using Unity.Netcode;
using UnityEngine.UI;

public class LobbyManager : MonoBehaviour
{
  public static LobbyManager Instance { get; private set; }

  public const string KEY_PLAYER_NAME = "PlayerName";
  public const string KEY_PLAYER_COLOR = "Color";
  public const string KEY_GAME_MODE = "GameMode";
  public const string KEY_READY_STATE = "ReadyState";
  public const string KEY_START_GAME = "StartGame_RelayCode";

  [SerializeField] private SceneName nextScene = SceneName.StandbyPhase;

  public event EventHandler OnLeftLobby;

  public event EventHandler<LobbyEventArgs> OnJoinedLobby;
  public event EventHandler<LobbyEventArgs> OnJoinedLobbyUpdate;

  public event EventHandler<LobbyEventArgs> OnKickedFromLobby;
  public event EventHandler<LobbyEventArgs> OnLobbyGameModeChanged;

  public event EventHandler<EventArgs> OnGameStarted;
  public class LobbyEventArgs : EventArgs
  {
    public Lobby lobby;
  }

  public event EventHandler<OnLobbyListChangedEventArgs> OnLobbyListChanged;
  public class OnLobbyListChangedEventArgs : EventArgs
  {
    public List<Lobby> lobbyList;
  }


  public enum GameMode
  {
    CaptureTheFlag,
    Conquest
  }

  private float heartbeatTimer;
  private float lobbyPollTimer;
  private float refreshLobbyListTimer = 5f;
  private Lobby joinedLobby;

  // private string playerName;
  // public Color playerColor;
  private PlayerData playerData;

  [SerializeField] private RelayManager relayManager;

  private async void Awake()
  {
    Debug.Log("aaa");
    AudioManager.Instance.SetAndPlay(1);
    playerData.color = Color.red;
    playerData.playerWeapon = -1;
    try
    {
      await UnityServices.InitializeAsync();
    }
    catch (Exception e)
    {
      Debug.LogException(e);
    }
    Instance = this;

    SetupEvents();
    // ParrelSync should only be used within the Unity Editor so you should use the UNITY_EDITOR define

#if UNITY_EDITOR
    if (ParrelSync.ClonesManager.IsClone())
    {
      // When using a ParrelSync clone, switch to a different authentication profile to force the clone
      // to sign in as a different anonymous user account.
      string customArgument = ParrelSync.ClonesManager.GetArgument();
      AuthenticationService.Instance.SwitchProfile($"Clone_{customArgument}_Profile");
    }
#endif
    LobbyManager.Instance.Authenticate(EditPlayerName.Instance.GetPlayerName());
  }

  private IEnumerator Start()
  {
    // Wait for the network Scene Manager to start
    yield return new WaitUntil(() => NetworkManager.Singleton.SceneManager != null);

    // Set the events on the loading manager
    // Doing this because every time the network session ends the loading manager stops
    // detecting the events
    LoadingSceneManager.Instance.Init();

    relayManager.OnClientConnect += LoadScene;

  }

  // Setup authentication event handlers if desired
  void SetupEvents()
  {
    AuthenticationService.Instance.SignedIn += () =>
    {
      // Shows how to get a playerID
      Debug.Log($"PlayerID: {AuthenticationService.Instance.PlayerId}");
      // Shows how to get an access token
      Debug.Log($"Access Token: {AuthenticationService.Instance.AccessToken}");
    };

    AuthenticationService.Instance.SignInFailed += (err) =>
    {
      Debug.LogError(err);
    };

    AuthenticationService.Instance.SignedOut += () =>
    {
      Debug.Log("Player signed out.");
    };

    AuthenticationService.Instance.Expired += () =>
    {
      Debug.Log("Player session could not be refreshed and expired.");
    };
  }

  private void Update()
  {
    //HandleRefreshLobbyList(); // Disabled Auto Refresh for testing with multiple builds
    // Debug.Log(Data[KEY_PLAYER_NAME);
    HandleLobbyHeartbeat();
    HandleLobbyPolling();
  }

  public async void Authenticate(string playerName)
  {

    playerData.playerName = playerName;
    InitializationOptions initializationOptions = new InitializationOptions();
    initializationOptions.SetProfile(playerName);

    await UnityServices.InitializeAsync(initializationOptions);

    AuthenticationService.Instance.SignedIn += () =>
    {
      playerData.Id = 0;
      Debug.Log(playerName + " Signed in! " + AuthenticationService.Instance.PlayerId);
      RefreshLobbyList();
    };

    if (AuthenticationService.Instance.IsSignedIn)
    {
      Debug.Log(AuthenticationService.Instance.PlayerId + " is Sign in before");
    }
    await AuthenticationService.Instance.SignInAnonymouslyAsync();
  }

  private void HandleRefreshLobbyList()
  {
    if (UnityServices.State == ServicesInitializationState.Initialized && AuthenticationService.Instance.IsSignedIn)
    {
      refreshLobbyListTimer -= Time.deltaTime;
      if (refreshLobbyListTimer < 0f)
      {
        float refreshLobbyListTimerMax = 5f;
        refreshLobbyListTimer = refreshLobbyListTimerMax;

        RefreshLobbyList();
      }
    }
  }

  private async void HandleLobbyHeartbeat()
  {
    if (IsLobbyHost())
    {
      heartbeatTimer -= Time.deltaTime;
      if (heartbeatTimer < 0f)
      {
        float heartbeatTimerMax = 15f;
        heartbeatTimer = heartbeatTimerMax;

        Debug.Log("Heartbeat");
        await LobbyService.Instance.SendHeartbeatPingAsync(joinedLobby.Id);
      }
    }
  }

  private async void HandleLobbyPolling()
  {
    if (joinedLobby != null)
    {
      lobbyPollTimer -= Time.deltaTime;
      if (lobbyPollTimer < 0f)
      {
        float lobbyPollTimerMax = 1.1f;
        lobbyPollTimer = lobbyPollTimerMax;

        joinedLobby = await LobbyService.Instance.GetLobbyAsync(joinedLobby.Id);

        OnJoinedLobbyUpdate?.Invoke(this, new LobbyEventArgs { lobby = joinedLobby });

        if (!IsPlayerInLobby())
        {
          // Player was kicked out of this lobby
          Debug.Log("Kicked from Lobby!");

          OnKickedFromLobby?.Invoke(this, new LobbyEventArgs { lobby = joinedLobby });

          joinedLobby = null;
        }
        if (joinedLobby.Data[KEY_START_GAME].Value != "0")
        {
          if (!IsLobbyHost())
          {

            RelayManager.Instance.JoinRelay(joinedLobby.Data[KEY_START_GAME].Value, playerData);
          }
          joinedLobby = null;
          OnGameStarted?.Invoke(this, EventArgs.Empty);
        }
      }
    }
  }

  public PlayerData GetPlayerData()
  {
    return playerData;
  }

  public Lobby GetJoinedLobby()
  {
    return joinedLobby;
  }

  public bool IsLobbyHost()
  {
    return joinedLobby != null && joinedLobby.HostId == AuthenticationService.Instance.PlayerId;
  }

  private bool IsPlayerInLobby()
  {
    if (joinedLobby != null && joinedLobby.Players != null)
    {
      foreach (Player player in joinedLobby.Players)
      {
        if (player.Id == AuthenticationService.Instance.PlayerId)
        {
          // This player is in this lobby
          return true;
        }
      }
    }
    return false;
  }

  private Player GetPlayer()
  {
    return new Player(AuthenticationService.Instance.PlayerId, null, new Dictionary<string, PlayerDataObject> {
            { KEY_PLAYER_NAME, new PlayerDataObject(PlayerDataObject.VisibilityOptions.Public, playerData.playerName) },
            { KEY_PLAYER_COLOR, new PlayerDataObject(PlayerDataObject.VisibilityOptions.Public, playerData.color.ToString()) },
            { KEY_READY_STATE, new PlayerDataObject(PlayerDataObject.VisibilityOptions.Public, "no")}
        });
  }

  public void ChangeGameMode()
  {
    if (IsLobbyHost())
    {
      GameMode gameMode =
          Enum.Parse<GameMode>(joinedLobby.Data[KEY_GAME_MODE].Value);

      switch (gameMode)
      {
        default:
        case GameMode.CaptureTheFlag:
          gameMode = GameMode.Conquest;
          break;
        case GameMode.Conquest:
          gameMode = GameMode.CaptureTheFlag;
          break;
      }

      UpdateLobbyGameMode(gameMode);
    }
  }

  public async void CreateLobby(string lobbyName, bool isPrivate)
  {
    Player player = GetPlayer();

    CreateLobbyOptions options = new CreateLobbyOptions
    {
      Player = player,
      IsPrivate = isPrivate,
      Data = new Dictionary<string, DataObject> {
                // { KEY_GAME_MODE, new DataObject(DataObject.VisibilityOptions.Public, gameMode.ToString()) },
                {KEY_START_GAME, new DataObject(DataObject.VisibilityOptions.Member,"0")}
            }
    };

    Lobby lobby = await LobbyService.Instance.CreateLobbyAsync(lobbyName, 3, options);

    Debug.Log(player.Id + " Join " + lobby.Id);

    joinedLobby = lobby;

    OnJoinedLobby?.Invoke(this, new LobbyEventArgs { lobby = lobby });

    Debug.Log("Created Lobby " + lobby.Name);
  }

  public async void RefreshLobbyList()
  {
    try
    {
      QueryLobbiesOptions options = new QueryLobbiesOptions();
      options.Count = 25;

      // Filter for open lobbies only
      options.Filters = new List<QueryFilter> {
                new QueryFilter(
                    field: QueryFilter.FieldOptions.AvailableSlots,
                    op: QueryFilter.OpOptions.GT,
                    value: "0")
            };

      // Order by newest lobbies first
      options.Order = new List<QueryOrder> {
                new QueryOrder(
                    asc: false,
                    field: QueryOrder.FieldOptions.Created)
            };

      QueryResponse lobbyListQueryResponse = await Lobbies.Instance.QueryLobbiesAsync();

      OnLobbyListChanged?.Invoke(this, new OnLobbyListChangedEventArgs { lobbyList = lobbyListQueryResponse.Results });
    }
    catch (LobbyServiceException e)
    {
      Debug.Log(e);
    }
  }

  public async void JoinLobbyByCode(string lobbyCode)
  {
    Player player = GetPlayer();

    Lobby lobby = await LobbyService.Instance.JoinLobbyByCodeAsync(lobbyCode, new JoinLobbyByCodeOptions
    {
      Player = player
    });

    joinedLobby = lobby;

    OnJoinedLobby?.Invoke(this, new LobbyEventArgs { lobby = lobby });
  }

  public async void JoinLobby(Lobby lobby)
  {
    Player player = GetPlayer();

    Debug.Log(player.Id + " Join " + lobby.Id);

    joinedLobby = await LobbyService.Instance.JoinLobbyByIdAsync(lobby.Id, new JoinLobbyByIdOptions
    {
      Player = player
    });

    OnJoinedLobby?.Invoke(this, new LobbyEventArgs { lobby = lobby });
  }

  public async void UpdatePlayerName(string playerName)
  {
    // playerData.playerName = playerName; 
    playerData.playerName = playerName;

    if (joinedLobby != null)
    {
      try
      {
        UpdatePlayerOptions options = new UpdatePlayerOptions();

        options.Data = new Dictionary<string, PlayerDataObject>() {
                    {
                        KEY_PLAYER_NAME, new PlayerDataObject(
                            visibility: PlayerDataObject.VisibilityOptions.Public,
                            value: playerName)
                    }
                };

        string playerId = AuthenticationService.Instance.PlayerId;

        Lobby lobby = await LobbyService.Instance.UpdatePlayerAsync(joinedLobby.Id, playerId, options);
        joinedLobby = lobby;

        OnJoinedLobbyUpdate?.Invoke(this, new LobbyEventArgs { lobby = joinedLobby });
      }
      catch (LobbyServiceException e)
      {
        Debug.Log(e);
      }
    }
  }

  public async void UpdatePlayerColor(Color playerColor)
  {

    playerData.color = playerColor;
    if (joinedLobby != null)
    {
      try
      {
        UpdatePlayerOptions options = new UpdatePlayerOptions();

        options.Data = new Dictionary<string, PlayerDataObject>() {
                    {
                        KEY_PLAYER_COLOR, new PlayerDataObject(
                            visibility: PlayerDataObject.VisibilityOptions.Public,
                            value: playerColor.ToString())
                    }
                };

        string playerId = AuthenticationService.Instance.PlayerId;

        Lobby lobby = await LobbyService.Instance.UpdatePlayerAsync(joinedLobby.Id, playerId, options);
        joinedLobby = lobby;

        OnJoinedLobbyUpdate?.Invoke(this, new LobbyEventArgs { lobby = joinedLobby });
      }
      catch (LobbyServiceException e)
      {
        Debug.Log(e);
      }
    }
  }

  public async void QuickJoinLobby()
  {
    try
    {
      QuickJoinLobbyOptions options = new QuickJoinLobbyOptions();

      Lobby lobby = await LobbyService.Instance.QuickJoinLobbyAsync(options);
      joinedLobby = lobby;

      OnJoinedLobby?.Invoke(this, new LobbyEventArgs { lobby = lobby });
    }
    catch (LobbyServiceException e)
    {
      Debug.Log(e);
    }
  }

  public async void LeaveLobby()
  {
    if (joinedLobby != null)
    {
      try
      {
        await LobbyService.Instance.RemovePlayerAsync(joinedLobby.Id, AuthenticationService.Instance.PlayerId);

        joinedLobby = null;

        OnLeftLobby?.Invoke(this, EventArgs.Empty);
      }
      catch (LobbyServiceException e)
      {
        Debug.Log(e);
      }
    }
  }

  public async void KickPlayer(string playerId)
  {
    if (IsLobbyHost())
    {
      try
      {
        await LobbyService.Instance.RemovePlayerAsync(joinedLobby.Id, playerId);
      }
      catch (LobbyServiceException e)
      {
        Debug.Log(e);
      }
    }
  }

  public async void UpdateLobbyGameMode(GameMode gameMode)
  {
    try
    {
      Debug.Log("UpdateLobbyGameMode " + gameMode);

      Lobby lobby = await Lobbies.Instance.UpdateLobbyAsync(joinedLobby.Id, new UpdateLobbyOptions
      {
        Data = new Dictionary<string, DataObject> {
                    { KEY_GAME_MODE, new DataObject(DataObject.VisibilityOptions.Public, gameMode.ToString()) }
                }
      });

      joinedLobby = lobby;

      OnLobbyGameModeChanged?.Invoke(this, new LobbyEventArgs { lobby = joinedLobby });
    }
    catch (LobbyServiceException e)
    {
      Debug.Log(e);
    }
  }

  public async void StartGame(Button button)
  {
    if (IsLobbyHost())
    {
      try
      {
        if (GetReadyNumber() != LobbyManager.Instance.GetJoinedLobby().Players.Count)
        {
          Debug.Log("There is player not ready!");
          return;
        }
        button.interactable = false;
        Debug.Log("StartGame" + playerData.color);

        string relayCode = await RelayManager.Instance.CreateRelay(playerData);

        Lobby lobby = await Lobbies.Instance.UpdateLobbyAsync(joinedLobby.Id, new UpdateLobbyOptions
        {
          Data = new Dictionary<string, DataObject> {
                        {KEY_START_GAME, new DataObject(DataObject.VisibilityOptions.Member,relayCode)}
                   }
        });
        joinedLobby = lobby;

        LoadingSceneManager.Instance.SetNumPlayer(lobby.Players.Count);
        if (lobby.Players.Count == 1)
        {
          LoadingSceneManager.Instance.LoadScene(nextScene);
        }
      }
      catch (LobbyServiceException e)
      {
        Debug.Log(e);
      }
    }

  }
  public void LoadScene(object sender, EventArgs e)
  {
    LoadingSceneManager.Instance.LoadScene(nextScene);
  }

  public async void UpdatePlayerReady(string status)
  {
    if (joinedLobby != null)
    {
      try
      {
        UpdatePlayerOptions options = new UpdatePlayerOptions();

        options.Data = new Dictionary<string, PlayerDataObject>() {
                    {
                        KEY_READY_STATE, new PlayerDataObject(
                            visibility: PlayerDataObject.VisibilityOptions.Public,
                            value: status)
                    }
                };

        string playerId = AuthenticationService.Instance.PlayerId;

        Lobby lobby = await LobbyService.Instance.UpdatePlayerAsync(joinedLobby.Id, playerId, options);
        joinedLobby = lobby;

        OnJoinedLobbyUpdate?.Invoke(this, new LobbyEventArgs { lobby = joinedLobby });
      }
      catch (LobbyServiceException e)
      {
        Debug.Log(e);
      }
    }
  }

  private int GetReadyNumber()
  {
    int count = 0;
    foreach (Player player in LobbyManager.Instance.GetJoinedLobby().Players)
    {
      if (player.Data[LobbyManager.KEY_READY_STATE].Value == "yes")
      {
        count++;
      }
    }
    return count;
  }
}