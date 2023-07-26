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
  [SerializeField] private GameObject oxyPrefab;

  [SerializeField] private CoutDownTimer lifeTime;

  [SerializeField] private SceneName nextScene = SceneName.ResultPhase;

  [SerializeField] private Transform[] randomSpawnOxy;

  private Vector3 oxySpawnPoint;

  private PlayerStatus playerStatus;

  [SerializeField] private int beginTimeAmount = 30;

  private int currentRank = 2;

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
    countDown.OnTimeOut += StartGame;

    playerStatus.OnDeadTrigger += ResultRecord;

    if (NetworkManager.Singleton.LocalClientId == 0)
    {
      playerStatus.SetTimeLeft(playerStatus.GetTimeLeft() + 15);
    }
    else if (NetworkManager.Singleton.LocalClientId == 1)
    {
      playerStatus.SetTimeLeft(playerStatus.GetTimeLeft() + 10);
    }

    if (IsHost)
    {
      System.Random random = new System.Random();
      oxySpawnPoint = randomSpawnOxy[random.Next(randomSpawnOxy.Length)].position;
    }
  }

  private void ResultRecord(object sender, EventArgs e)
  {
    RecordServerRpc(NetworkManager.Singleton.LocalClientId);
  }

  [ServerRpc(RequireOwnership = false)]
  private void RecordServerRpc(ulong index)
  {
    PointManager.Instance.playerPoint[index].roundRank = currentRank;
    currentRank--;
  }

  private void StartGame(object sender, EventArgs e)
  {
    sceneName.text = "READY..";
    Destroy(countDown.gameObject);

    StartCoroutine(SpawnPlayer());
  }

  IEnumerator SpawnPlayer()
  {
    yield return new WaitForSeconds(1.5f);

    if (IsHost)
    {
      NetworkObjectSpawner.SpawnNewNetworkObject(oxyPrefab, oxySpawnPoint);

      for (ulong i = 0; i < 3; i++)
      {
        NetworkObjectSpawner.SpawnNewNetworkObjectChangeOwnershipToClient(PointManager.Instance.playerPoint[i].playerIndex == 0 ? monsterPrefab : playerPrefab, PointManager.Instance.playerPoint[i].spawnPoint, i);
      }
    }

    sceneName.text = "GO!!";
    yield return new WaitForSeconds(0.5f);

    Destroy(sceneName.gameObject);
    lifeTime.gameObject.SetActive(true);
    playerStatus.SetStartCounting(true);
  }

  private void LoadNextScene()
  {
    LoadingSceneManager.Instance.LoadScene(nextScene);
  }

  private void CalculateResult()
  {
    for (int i = 0; i < 3; i++)
    {
      if (PointManager.Instance.playerPoint[i].roundRank == 0)
      {
        PointManager.Instance.playerPoint[i].point += PointManager.Instance.playerPoint[i].bidAmount;
      }
      else if (PointManager.Instance.playerPoint[i].roundRank == 2)
      {
        PointManager.Instance.playerPoint[i].point -= PointManager.Instance.playerPoint[i].bidAmount;
      }
    }
    LoadNextScene();
  }

  [ClientRpc]
  private void ResetTimeClientRpc()
  {
    playerStatus.SetTimeLeft(beginTimeAmount);
  }

  private void Update()
  {
    if (currentRank == 0 && IsHost)
    {
      CalculateResult();
      ResetTimeClientRpc();
    }
  }
}
