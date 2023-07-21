using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class ShoppingManager : SingletonNetwork<ShoppingManager>
{
  [SerializeField] private GameObject oxyBottle;
  [SerializeField] private GameObject playerPrefab;
  [SerializeField] private GameObject monsterPrefab;

  [SerializeField] private GameObject gameUI;
  private PlayerStatus playerStatus;

  private int numberConnected = 0;

  public override void Awake()
  {
    base.Awake();
    playerStatus = FindObjectOfType<PlayerStatus>();
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

    NetworkObjectSpawner.SpawnNewNetworkObject(oxyBottle);
    switch (numberConnected)
    {
      case 1:
        NetworkObjectSpawner.SpawnNewNetworkObject(monsterPrefab, new Vector3(-10, 0, 0));
        break;
      case 2:
        NetworkObjectSpawner.SpawnNewNetworkObject(monsterPrefab, new Vector3(-10, 0, 0));
        NetworkObjectSpawner.SpawnNewNetworkObjectChangeOwnershipToClient(playerPrefab, new Vector3(10, 0, 0), 1);
        break;
      case 3:
        NetworkObjectSpawner.SpawnNewNetworkObject(monsterPrefab, new Vector3(-10, 0, 0));
        NetworkObjectSpawner.SpawnNewNetworkObjectChangeOwnershipToClient(playerPrefab, new Vector3(10, 0, 0), 1);
        NetworkObjectSpawner.SpawnNewNetworkObjectChangeOwnershipToClient(playerPrefab, new Vector3(0, -5, 0), 2);
        break;
    }

    playerStatus.SetStartCounting(true);

    gameUI.SetActive(true);
  }
}
