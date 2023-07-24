using UnityEngine;
using System.Collections;
using TMPro;
using System;

public class CountDown : MonoBehaviour
{
  [SerializeField] int amountTime;
  TextMeshProUGUI timeText;

  public event EventHandler OnTimeOut;

  private bool startCounting = false;

  private int timeLeft;
  private bool countTrigger = true;

  private void Start()
  {
    timeLeft = amountTime;
    timeText = GetComponent<TextMeshProUGUI>();
    timeText.text = amountTime.ToString();
  }

  private void Update()
  {
    if (startCounting && countTrigger && timeLeft > 0)
    {
      StartCoroutine(CountDownTime());
    }
  }

  IEnumerator CountDownTime()
  {
    countTrigger = false;
    yield return new WaitForSeconds(1);

    timeLeft--;
    timeText.text = timeLeft.ToString();
    Debug.Log("xxx" + timeLeft);

    if (timeLeft == 0)
    {
      OnTimeOut?.Invoke(this, EventArgs.Empty);
    }

    countTrigger = true;
  }

  public void SetStartCounting()
  {
    startCounting = true;
  }
}
