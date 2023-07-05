using UnityEngine;
using TMPro;
using System;

public class CoutDownTimer : MonoBehaviour
{
  [SerializeField] private PlayerStatus playerStatus;
  private TextMeshProUGUI textContent;

  void Start()
  {
    playerStatus.OnCountDownTrigger += OnUpdateTime;
    textContent = GetComponent<TextMeshProUGUI>();
    textContent.text = String.Format($"{playerStatus.GetTimeLeft() / 60:D2}:{playerStatus.GetTimeLeft() % 60:D2}");
  }

  void OnUpdateTime(object sender, EventArgs e)
  {
    textContent.text = String.Format($"{playerStatus.GetTimeLeft() / 60:D2}:{playerStatus.GetTimeLeft() % 60:D2}");
  }
}
