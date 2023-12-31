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
    playerStatus.OnCountDownTrigger += OnUpdateTime;
    textContent = GetComponent<TextMeshProUGUI>();
    textContent.text = String.Format($"{playerStatus.GetTimeLeft() / 60:D2}:{playerStatus.GetTimeLeft() % 60:D2}");
  }

  private void OnUpdateTime(object sender, EventArgs e)
  {
    textContent.text = String.Format($"{playerStatus.GetTimeLeft() / 60:D2}:{playerStatus.GetTimeLeft() % 60:D2}");
  }

  private void OnDestroy()
  {
    playerStatus.OnCountDownTrigger -= OnUpdateTime;
  }

  private void Update()
  {
    if (playerStatus.GetTimeLeft() <= 0)
    {
      Destroy(gameObject);
    }
  }
}
