using UnityEngine;
using System;
using Unity.Netcode;

public class MonsterUI : NetworkBehaviour
{
  void Start()
  {
    if (PointManager.Instance.playerPoint[Convert.ToInt16(NetworkManager.Singleton.LocalClientId)].playerIndex == 0)
    {
      gameObject.SetActive(false);
    }

  }
}
