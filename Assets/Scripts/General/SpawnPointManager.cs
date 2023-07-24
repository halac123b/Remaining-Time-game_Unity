using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpawnPointManager : MonoBehaviour
{
  [SerializeField] Button[] targetPoint;

  Transform currentSpawnPoint;

  private void Start()
  {
    currentSpawnPoint = targetPoint[0].transform;

    foreach (Button btn in targetPoint)
    {
      btn.onClick.AddListener(delegate { SetSpawnPoint(btn.transform); });
    }
  }

  private void SetSpawnPoint(Transform pos)
  {
    currentSpawnPoint = pos;
  }
}
