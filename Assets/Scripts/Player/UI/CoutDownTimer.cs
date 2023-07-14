using UnityEngine;
using TMPro;
using System;

public class CoutDownTimer : MonoBehaviour
{
  private PlayerStatus playerStatus;
  private TextMeshProUGUI textContent;

  private void OnEnable()
  {
    playerStatus = FindObjectOfType<PlayerStatus>();
    Debug.Log(playerStatus);
    playerStatus.OnCountDownTrigger += OnUpdateTime;
    textContent = GetComponent<TextMeshProUGUI>();
    textContent.text = String.Format($"{playerStatus.GetTimeLeft() / 60:D2}:{playerStatus.GetTimeLeft() % 60:D2}");
  }

  private void OnUpdateTime(object sender, EventArgs e)
  {
    textContent.text = String.Format($"{playerStatus.GetTimeLeft() / 60:D2}:{playerStatus.GetTimeLeft() % 60:D2}");
  }
}
