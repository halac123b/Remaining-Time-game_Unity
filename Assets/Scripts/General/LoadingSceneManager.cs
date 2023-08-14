using System;
using System.Collections;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum SceneName : byte
{
  Bootstrap,
  Menu,
  LobbySelection,
  StandbyPhase,
  ShoppingPhase,
  MainPhase,
  MainPhasev2,
  MainPhasev3,
  ResultPhase,
  SummaryPhase
};

public class LoadingSceneManager : SingletonPersistent<LoadingSceneManager>
{
  public SceneName SceneActive => m_sceneActive;

  private SceneName m_sceneActive;

  private int numPlayer = 0;

  private int currentRound = 1;
  public int GetRound()
  {
    return currentRound;
  }
  public void GoNextRound()
  {
    currentRound++;
  }

  public int nextMap = 0;


  // After running the menu scene, which initiates this manager, we subscribe to these events
  // due to the fact that when a network session ends it cannot longer listen to them.
  public void Init()
  {
    NetworkManager.Singleton.SceneManager.OnLoadComplete -= OnLoadComplete;
    NetworkManager.Singleton.SceneManager.OnLoadComplete += OnLoadComplete;
  }

  public void LoadScene(SceneName sceneToLoad, bool isNetworkSessionActive = true)
  {
    StartCoroutine(Loading(sceneToLoad, isNetworkSessionActive));
  }

  public void RefreshGame()
  {
    numPlayer = 0;
    currentRound = 1;
  }

  // Coroutine for the loading effect. It use an alpha in out effect
  private IEnumerator Loading(SceneName sceneToLoad, bool isNetworkSessionActive)
  {
    if (sceneToLoad != SceneName.Menu)
    {
      LoadingFadeEffect.Instance.FadeIn();
      // Here the player still sees the black screen
      yield return new WaitUntil(() => LoadingFadeEffect.s_canLoad);
    }

    if (isNetworkSessionActive)
    {
      if (NetworkManager.Singleton.IsServer)
      {
        LoadSceneNetwork(sceneToLoad);
      }
    }
    else
    {
      LoadSceneLocal(sceneToLoad);
    }
    // Because the scenes are not heavy we can just wait a second and continue with the fade.
    // In case the scene is heavy instead we should use additive loading to wait for the
    // scene to load before we continue
    yield return new WaitForSeconds(1f);

    if (sceneToLoad != SceneName.Menu)
    {
      LoadingFadeEffect.Instance.FadeOut();
    }
  }

  private void LoadSceneLocal(SceneName sceneToLoad)
  {
    Debug.Log(sceneToLoad.ToString());
    SceneManager.LoadScene(sceneToLoad.ToString());
  }

  // Load the scene using the SceneManager from NetworkManager. Use this when there is an active
  // network session
  private void LoadSceneNetwork(SceneName sceneToLoad)
  {
    Debug.Log(sceneToLoad.ToString());
    NetworkManager.Singleton.SceneManager.LoadScene(
        sceneToLoad.ToString(),
        LoadSceneMode.Single);
  }

  // This callback function gets triggered when a scene is finished loading
  // Here we set up what to do for each scene, like changing the music
  private void OnLoadComplete(ulong clientId, string sceneName, LoadSceneMode loadSceneMode)
  {

    // We only care the host/server is loading because every manager handles
    // their information and behavior on the server runtime
    if (!NetworkManager.Singleton.IsServer)
      return;

    Enum.TryParse(sceneName, out m_sceneActive);

    // if (!ClientConnection.Instance.CanClientConnect(clientId))
    //   return;

    // What to initially do on every scene when it finishes loading
    switch (m_sceneActive)
    {
      // When a client/host connects tell the manager
      case SceneName.StandbyPhase:
        StandbyManager.Instance.ServerSceneInit();
        break;

      case SceneName.ShoppingPhase:
        ShoppingManager.Instance.ServerSceneInit();
        break;

      case SceneName.MainPhase:
        MainPhaseManager.Instance.ServerSceneInit();
        break;

      case SceneName.MainPhasev2:
        MainPhaseManager.Instance.ServerSceneInit();
        break;

      case SceneName.MainPhasev3:
        MainPhaseManager.Instance.ServerSceneInit();
        break;

      case SceneName.ResultPhase:
        ResultPhaseManager.Instance.ServerSceneInit();
        break;
    }
  }

  public void SetNumPlayer(int num)
  {
    numPlayer = num;
  }

  public int GetNumPlayer()
  {
    return numPlayer;
  }
}