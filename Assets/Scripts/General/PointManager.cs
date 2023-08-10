using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Unity.Netcode;


[Serializable]
public struct PlayerPoint : INetworkSerializable
{
  public int playerIndex;
  public int point;
  public PlayerData playerData;
  public int rank;
  public Vector3 spawnPoint;
  public int bidAmount;
  public int roundRank;

  public void NetworkSerialize<T>(BufferSerializer<T> serializer) where T : IReaderWriter
  {
    serializer.SerializeValue(ref playerIndex);
    serializer.SerializeValue(ref point);
    serializer.SerializeValue(ref playerData);
    serializer.SerializeValue(ref rank);
    serializer.SerializeValue(ref spawnPoint);
    serializer.SerializeValue(ref roundRank);
    serializer.SerializeValue(ref bidAmount);
  }
}

public class PointManager : SingletonNetworkPersistent<PointManager>
{
  public PlayerPoint[] playerPoint = new PlayerPoint[3];

  private void Start()
  {
    RandomPlayerIndex();
  }

  public void IncreasePointAll(int amount)
  {
    for (int i = 0; i < playerPoint.Length; i++)
    {
      playerPoint[i].point += amount;
    }
  }

  public void IncreasePointSpecific(int index, int amount)
  {
    playerPoint[index].point += amount;
  }

  public void RandomPlayerIndex()
  {
    List<int> index = new List<int> { 0, 1, 2 };
    System.Random random = new System.Random();
    for (int i = 0; i < 3; i++)
    {
      playerPoint[i].playerIndex = (i) % 3;//index[random.Next(index.Count)];
      index.Remove(playerPoint[i].playerIndex);
    }

    for (int i = 0; i < playerPoint.Length; i++)
    {
      playerPoint[i].rank = 1;
    }
  }

  public void RollPlayerIndex()
  {
    for (int i = 0; i < playerPoint.Length; i++)
    {
      playerPoint[i].playerIndex = (playerPoint[i].playerIndex + 1) % 3;
    }
  }

  public PlayerData GetPlayerData(int index)
  {
    return playerPoint[index].playerData;
  }

  public void SetPlayerData(ulong index, PlayerData playerData)
  {
    playerData.Id = index;
    playerPoint[index].playerData = playerData;
    // Debug.Log("Player ID is:" + playerData.Id);
    // Debug.Log("Player color is:" + playerData.color);
  }
}
