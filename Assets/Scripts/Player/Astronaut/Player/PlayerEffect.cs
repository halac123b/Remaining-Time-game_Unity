using UnityEngine;
using Unity.Netcode;
using System;

public class PlayerEffect : NetworkBehaviour
{
  private PlayerStatus playerStatus;

  [SerializeField] private GameObject iceBar;
  [SerializeField] private GameObject plusTime;


  private void Awake()
  {
    playerStatus = FindObjectOfType<PlayerStatus>();

    playerStatus.enableStopWatch += TriggerStopWatch;
    playerStatus.enableBuffTime += TriggerBuffTime;
  }

  public void TriggerStopWatch(object sender, EventArgs e)
  {
    if (!IsOwner)
    {
      return;
    }
    iceBar.SetActive(!iceBar.activeSelf);
  }

  public void TriggerBuffTime(object sender, EventArgs e)
  {
    if (!IsOwner)
    {
      return;
    }
    plusTime.SetActive(!plusTime.activeSelf);
  }

  private void OnDisable()
  {
    playerStatus.enableStopWatch -= TriggerStopWatch;
    playerStatus.enableBuffTime -= TriggerBuffTime;
  }
}
