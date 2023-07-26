using UnityEngine;
using Unity.Netcode;
using TMPro;
using System;
using System.Collections;

public class MainPhaseManager : SingletonNetwork<MainPhaseManager>
{
  private int numberConnected = 0;

  [SerializeField] CountDown countDown;
  [SerializeField] TextMeshProUGUI sceneName;

  [SerializeField] private GameObject playerPrefab;
  [SerializeField] private GameObject monsterPrefab;

  [SerializeField] private CoutDownTimer lifeTime;

  [SerializeField] private SceneName nextScene = SceneName.ResultPhase;

  private PlayerStatus playerStatus;

  public void ServerSceneInit()
  {
    numberConnected++;

    // Check if is the last client
    if (numberConnected != LoadingSceneManager.Instance.GetNumPlayer())
      return;

    StartCountClientRpc();
  }

  [ClientRpc]
  private void StartCountClientRpc()
  {
    countDown.SetStartCounting();
  }

  private void Start()
  {
    playerStatus = FindObjectOfType<PlayerStatus>();

    if (!IsServer)
      return;
    countDown.OnTimeOut += StartGame;
    playerStatus.OnDeadTrigger += LoadNextScene;
  }

  private void StartGame(object sender, EventArgs e)
  {
    ReadyClientRpc();
    StartCoroutine(SpawnPlayer());
  }

  [ClientRpc]
  private void ReadyClientRpc()
  {
    sceneName.text = "READY..";
    Destroy(countDown.gameObject);
  }

  [ClientRpc]
  private void StartGameClientRpc()
  {
    sceneName.text = "GO!!";
    Destroy(sceneName.gameObject, 0.5f);
    lifeTime.gameObject.SetActive(true);
    playerStatus.SetStartCounting(true);
  }

  IEnumerator SpawnPlayer()
  {
    yield return new WaitForSeconds(1.5f);

    for (ulong i = 0; i < 3; i++)
    {
      NetworkObjectSpawner.SpawnNewNetworkObjectChangeOwnershipToClient(PointManager.Instance.playerPoint[i].playerIndex == 0 ? monsterPrefab : playerPrefab, PointManager.Instance.playerPoint[i].spawnPoint, i);
    }

    StartGameClientRpc();
  }

  private void LoadNextScene(object sender, EventArgs e)
  {
    LoadingSceneManager.Instance.LoadScene(nextScene);
  }
}
