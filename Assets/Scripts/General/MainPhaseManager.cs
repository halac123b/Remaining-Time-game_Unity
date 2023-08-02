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

  private OxyStatus oxyStatus;

  private Vector3 oxySpawnPoint;

  private PlayerStatus playerStatus;

  [SerializeField] private int beginTimeAmount = 30;

  private bool endRound = false;

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

    if (currentRank == 0)
    {
      PointManager.Instance.playerPoint[index].roundRank = currentRank;
      currentRank--;
    }
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
      GameObject oxy = NetworkObjectSpawner.SpawnNewNetworkObject(oxyPrefab, oxySpawnPoint);

      oxyStatus = oxy.GetComponent<OxyStatus>();

      oxyStatus.OnOxyComplete += OxyVictory;

      for (ulong i = 0; i < 3; i++)
      {
        NetworkObjectSpawner.SpawnNewNetworkObjectChangeOwnershipToClient(PointManager.Instance.playerPoint[i].playerIndex == 0 ? monsterPrefab : playerPrefab, PointManager.Instance.playerPoint[i].spawnPoint, i);
      }
    }

    sceneName.text = "GO!!";
    yield return new WaitForSeconds(0.5f);

    Destroy(sceneName.gameObject);

    if (PointManager.Instance.playerPoint[Convert.ToInt16(NetworkManager.Singleton.LocalClientId)].playerIndex != 0)
    {
      lifeTime.gameObject.SetActive(true);
      playerStatus.SetStartCounting(true);
    }
  }

  private void OxyVictory(object sender, EventArgs e)
  {
    if (currentRank == 2)
    {
      PointManager.Instance.playerPoint[1].roundRank = PointManager.Instance.playerPoint[2].roundRank = 0;
      PointManager.Instance.playerPoint[0].roundRank = 2;
    }

    else if (currentRank == 1)
    {
      for (int i = 1; i < 3; i++)
      {
        if (PointManager.Instance.playerPoint[i].roundRank == -1)
        {
          PointManager.Instance.playerPoint[i].roundRank = 0;
          PointManager.Instance.playerPoint[0].roundRank = 1;
          break;
        }
      }
    }

    currentRank = -1;
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
        PointManager.Instance.playerPoint[i].point += PointManager.Instance.playerPoint[i].bidAmount * 2;
      }
      else if (PointManager.Instance.playerPoint[i].roundRank == 1)
      {
        PointManager.Instance.playerPoint[i].point += PointManager.Instance.playerPoint[i].bidAmount;
      }
    }
    LoadNextScene();
  }

  [ClientRpc]
  private void ResetTimeClientRpc()
  {
    playerStatus.SetStartCounting(false);
    playerStatus.SetTimeLeft(beginTimeAmount);
  }

  private void Update()
  {
    if (currentRank == -1 && IsHost && !endRound)
    {
      ResetTimeClientRpc();
      endRound = true;
      Invoke("CalculateResult", 1f);
    }
  }
}
