using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class ShoppingManager : SingletonNetwork<ShoppingManager>
{
  // [SerializeField] private GameObject oxyBottle;
  [SerializeField] private GameObject playerPrefab;
  [SerializeField] private GameObject monsterPrefab;

  [SerializeField] CountDown countDown;

  [SerializeField] private GameObject gameUI;

  private int numberConnected = 0;

  Vector3[] spawnPoint = new Vector3[2] { new Vector3(-10, 0, 0), new Vector3(10, 0, 0) };

  [SerializeField] SceneName nextScene = SceneName.MainPhase;

  public override void Awake()
  {
    base.Awake();
    countDown.OnTimeOut += LoadNextScene;
  }

  // So this method is called on the server each time a player enters the scene.
  // Because of that, if we create the ship when a player connects we could have a sync error
  // with the other clients because maybe the scene on the client is no yet loaded.
  // To fix this problem we wait until all clients call this method then we create the ships
  // for every client connected
  public void ServerSceneInit()
  {
    numberConnected++;

    // Check if is the last client
    if (numberConnected != LoadingSceneManager.Instance.GetNumPlayer())
      return;

    // NetworkObjectSpawner.SpawnNewNetworkObject(oxyBottle);
    ulong monsterIndex = 10;

    for (ulong i = 0; Convert.ToInt16(i) < numberConnected; i++)
    {
      if (PointManager.Instance.playerPoint[i].playerIndex == 0)
      {
        monsterIndex = i;
        break;
      }
    }

    switch (numberConnected)
    {
      case 1:
        NetworkObjectSpawner.SpawnNewNetworkObject(playerPrefab, new Vector3(-10, 0, 0));
        break;
      case 2:
        NetworkObjectSpawner.SpawnNewNetworkObject(playerPrefab, new Vector3(-10, 0, 0));
        NetworkObjectSpawner.SpawnNewNetworkObjectChangeOwnershipToClient(monsterPrefab, new Vector3(0, -5, 0), 1);
        break;
      case 3:
        for (ulong i = 0; i < 3; i++)
        {
          if (i != monsterIndex)
          {
            NetworkObjectSpawner.SpawnNewNetworkObjectChangeOwnershipToClient(monsterPrefab, new Vector3(0, -5, 0), monsterIndex);
          }
          else
          {
            NetworkObjectSpawner.SpawnNewNetworkObjectChangeOwnershipToClient(playerPrefab, spawnPoint[(i <= 1) ? i : 1], i);
          }
        }
        break;
    }

    countDown.SetStartCounting();

    gameUI.SetActive(true);
  }

  private void LoadNextScene(object sender, EventArgs e)
  {
    LoadingSceneManager.Instance.LoadScene(nextScene);
  }
}
