using System;
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

  PlayerStatus playerStatus;
  [SerializeField] PlayerEquip playerEquip;

  private int clientSendPoint = 0;

  public event EventHandler OnUpdatePoint;

  public override void Awake()
  {
    base.Awake();
    playerStatus = FindObjectOfType<PlayerStatus>();
    playerEquip = FindObjectOfType<PlayerEquip>();
  }
  private void Start()
  {
    if (IsHost)
    {
      UpdateStatusClientRpc(PointManager.Instance.playerPoint[1].point, PointManager.Instance.playerPoint[2].point);
    }
    countDown.OnTimeOut += UpdatePointBid;
    playerEquip.canTriggerSkill = false;
  }

  private void UpdatePointBid(object sender, EventArgs e)
  {
    UpdatePointServerRpc(NetworkManager.Singleton.LocalClientId, playerStatus.GetPoint(), playerStatus.bid);
    playerEquip.canTriggerSkill = true;
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

    Dictionary<int, GameObject> Character = new Dictionary<int, GameObject>()
{
    {0, monsterPrefab},
    {1, playerPrefab},
    {2, playerPrefab}

};

    for (ulong i = 0; Convert.ToInt32(i) < NetworkManager.Singleton.ConnectedClients.Count; i++)
    {
      NetworkObjectSpawner.SpawnNewNetworkObjectChangeOwnershipToClient(Character[PointManager.Instance.playerPoint[Convert.ToInt32(i)].playerIndex], new Vector3(i, i, 0), i);
    }


    // switch (PointManager.Instance.playerPoint.Length)
    // {
    //   case 1:
    //     NetworkObjectSpawner.SpawnNewNetworkObject(playerPrefab, new Vector3(-10, 0, 0));
    //     break;
    //   case 2:
    //     NetworkObjectSpawner.SpawnNewNetworkObject(playerPrefab, new Vector3(-10, 0, 0));
    //     NetworkObjectSpawner.SpawnNewNetworkObjectChangeOwnershipToClient(monsterPrefab, new Vector3(0, -5, 0), 1);
    //     break;
    //   case 3:
    //     for (ulong i = 0; i < 3; i++)
    //     {
    //       if (i != monsterIndex)
    //       {
    //         NetworkObjectSpawner.SpawnNewNetworkObjectChangeOwnershipToClient(playerPrefab, new Vector3(0, -5, 0), i);
    //       }
    //       else
    //       {
    //         NetworkObjectSpawner.SpawnNewNetworkObjectChangeOwnershipToClient(monsterPrefab, spawnPoint[(i <= 1) ? i : 1], monsterIndex);
    //       }
    //     }
    //     break;
    // }

    StartCountClientRpc(PointManager.Instance.playerPoint[1].point, PointManager.Instance.playerPoint[2].point);

    gameUI.SetActive(true);
  }

  private void LoadNextScene()
  {
    LoadingSceneManager.Instance.LoadScene(nextScene);
    playerStatus.Renew();
  }

  [ClientRpc]
  private void StartCountClientRpc(int point1, int point2)
  {
    countDown.SetStartCounting();

    if (NetworkManager.Singleton.LocalClientId == 1)
    {
      playerStatus.SetPoint(point1);
    }
    else if (NetworkManager.Singleton.LocalClientId == 2)
    {
      playerStatus.SetPoint(point2);
    }
    else
    {
      playerStatus.SetPoint(PointManager.Instance.playerPoint[0].point);
    }

    OnUpdatePoint?.Invoke(this, EventArgs.Empty);
  }

  [ClientRpc]
  private void UpdateStatusClientRpc(int point1, int point2)
  {
    ulong index = NetworkManager.Singleton.LocalClientId;
    if (index == 1)
    {
      PointManager.Instance.playerPoint[index].point = point1;

      playerStatus.SetPoint(point1);
    }
    else if (index == 2)
    {
      PointManager.Instance.playerPoint[index].point = point2;

      playerStatus.SetPoint(point2);
    }
  }

  [ServerRpc(RequireOwnership = false)]
  private void UpdatePointServerRpc(ulong index, int point, int bidAmount)
  {
    PointManager.Instance.playerPoint[index].point = point;
    PointManager.Instance.playerPoint[index].bidAmount = bidAmount;
    clientSendPoint++;
    if (clientSendPoint == NetworkManager.Singleton.ConnectedClients.Count)
    {
      LoadNextScene();
    }
  }
}
