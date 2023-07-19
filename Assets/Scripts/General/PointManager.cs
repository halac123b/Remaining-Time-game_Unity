using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PointManager : SingletonNetworkPersistent<PointManager>
{
  [Serializable]
  public struct PlayerPoint
  {
    public int playerIndex;
    public int point;
  }

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
    for (int i = 0; i < playerPoint.Length; i++)
    {
      playerPoint[i].playerIndex = random.Next(index.Count);
      index.Remove(playerPoint[i].playerIndex);
    }
  }

  public void RollPlayerIndex()
  {
    for (int i = 0; i < playerPoint.Length; i++)
    {
      playerPoint[i].playerIndex = (playerPoint[i].playerIndex + 1) % 3;
    }
  }
}
