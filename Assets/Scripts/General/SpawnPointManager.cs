using UnityEngine;
using UnityEngine.UI;
using Unity.Netcode;
using System;

public class SpawnPointManager : NetworkBehaviour
{
  [SerializeField] Button[] targetPoint;

  [SerializeField] CountDown countDown;

  Vector3 currentSpawnPoint;

  private void Start()
  {
    currentSpawnPoint = targetPoint[0].transform.position;

    foreach (Button btn in targetPoint)
    {
      btn.onClick.AddListener(delegate { SetSpawnPoint(btn.transform.position); });
    }

    countDown.OnTimeOut += SendSpawnPoint;
  }

  private void SetSpawnPoint(Vector3 pos)
  {
    currentSpawnPoint = pos;
  }

  private void SendSpawnPoint(object sender, EventArgs e)
  {
    SetSpawnPointServerRpc(Convert.ToInt32(NetworkManager.Singleton.LocalClientId));
    Destroy(gameObject, 0.5f);
  }

  [ServerRpc]
  private void SetSpawnPointServerRpc(int clientId)
  {
    PointManager.Instance.playerPoint[clientId].spawnPoint = currentSpawnPoint;
  }
}
