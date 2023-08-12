using UnityEngine;
using UnityEngine.UI;
using Unity.Netcode;
using System;

public class SpawnPointManager : NetworkBehaviour
{
  private Button[] targetPoint;

  [SerializeField] CountDown countDown;

  Vector3 currentSpawnPoint;

  private void Start()
  {

    targetPoint = GetComponentsInChildren<Button>();
    currentSpawnPoint = targetPoint[UnityEngine.Random.Range(0, targetPoint.Length - 1)].transform.position;

    foreach (Button btn in targetPoint)
    {
      btn.onClick.AddListener(delegate { SetSpawnPoint(btn.transform.position); });
    }

    Debug.Log("Sending");
    countDown.OnTimeOut += SendSpawnPoint;
  }

  private void SetSpawnPoint(Vector3 pos)
  {
    currentSpawnPoint = pos;
  }

  private void SendSpawnPoint(object sender, EventArgs e)
  {
    SetSpawnPointServerRpc(Convert.ToInt32(NetworkManager.Singleton.LocalClientId), currentSpawnPoint);
    Destroy(gameObject, 0.5f);
  }

  [ServerRpc(RequireOwnership = false)]
  private void SetSpawnPointServerRpc(int clientId, Vector3 pos)
  {
    PointManager.Instance.playerPoint[clientId].spawnPoint = pos;
  }
}
