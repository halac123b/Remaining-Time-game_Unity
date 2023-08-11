using UnityEngine;
using Unity.Netcode;
using System;

public class PlayerEffect : NetworkBehaviour
{
  private PlayerStatus playerStatus;

  [SerializeField] private GameObject iceBar;
  [SerializeField] private GameObject plusTime;
  [SerializeField] private GameObject latern;


  private void Awake()
  {
    playerStatus = FindObjectOfType<PlayerStatus>();

    playerStatus.enableStopWatch += TriggerStopWatch;
    playerStatus.enableBuffTime += TriggerBuffTime;
    playerStatus.enableLantern += TriggerLantern;
  }

  public void TriggerStopWatch(object sender, EventArgs e)
  {
    if (!IsOwner)
    {
      return;
    }
    TurnOnIceClientRpc();
  }
  [ClientRpc]
  public void TurnOnIceClientRpc(){
     iceBar.SetActive(!iceBar.activeSelf);
  }

  public void TriggerLantern(object sender, EventArgs e)
  {
    if (!IsOwner)
    {
      return;
    }
    TurnOnLaternClientRpc();
  }

  [ClientRpc]
  public void TurnOnLaternClientRpc(){
     latern.SetActive(!latern.activeSelf);
  }
 
  public void TriggerBuffTime(object sender, EventArgs e)
  {
    if (!IsOwner)
    {
      return;
    }
    TurnOnPlusTimeClientRpc();
  }

  [ClientRpc]
  public void TurnOnPlusTimeClientRpc(){
     plusTime.SetActive(!plusTime.activeSelf);
  }

  private void OnDisable()
  {
    playerStatus.enableStopWatch -= TriggerStopWatch;
    playerStatus.enableBuffTime -= TriggerBuffTime;
    playerStatus.enableLantern -= TriggerLantern;
  }
}
