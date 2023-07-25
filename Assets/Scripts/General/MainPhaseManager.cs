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
    if (IsServer)
    {
      countDown.OnTimeOut += StartGame;
    }
  }

  private void StartGame(object sender, EventArgs e)
  {
    sceneName.text = "READY..";
    StartCoroutine(SpawnPlayer());
  }

  IEnumerator SpawnPlayer()
  {
    yield return new WaitForSeconds(1.5f);

    for (ulong i = 0; i < 3; i++)
    {
      NetworkObjectSpawner.SpawnNewNetworkObjectChangeOwnershipToClient(PointManager.Instance.playerPoint[i].rank == 0 ? monsterPrefab : playerPrefab, PointManager.Instance.playerPoint[i].spawnPoint, i);
    }

    sceneName.text = "GO!!";
    sceneName.gameObject.SetActive(false);
  }
}
