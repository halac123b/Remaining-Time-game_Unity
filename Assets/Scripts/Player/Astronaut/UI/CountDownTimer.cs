using UnityEngine;
using TMPro;
using System;
using Unity.Netcode;

public class CountDownTimer : NetworkBehaviour
{
  private PlayerStatus playerStatus;
  public TextMeshProUGUI textContent;
    public NetworkVariable<int> Time = new NetworkVariable<int>(-1, NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Owner);
    public NetworkVariable<int> TimeMax = new NetworkVariable<int>(-1, NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Owner);

  private void OnEnable()
  {
    playerStatus = FindObjectOfType<PlayerStatus>();
    playerStatus.OnCountDownTrigger += OnUpdateTime;
    textContent = GetComponent<TextMeshProUGUI>();
     
    if (IsOwner) {
      Time.Value = playerStatus.timeLeft; 
      TimeMax.Value = playerStatus.MaxTimeLeft;
    }
  }
  
  private void OnUpdateTime(object sender, EventArgs e)
  {
     if (IsOwner) {
      Time.Value = playerStatus.timeLeft; 
      TimeMax.Value = playerStatus.MaxTimeLeft;
    }
    if(Time.Value <= 10) textContent.color = Color.red;
    else  textContent.color = Color.white;
  }

}
