using System.Collections;
using UnityEngine;
using System;

public class PlayerStatus : MonoBehaviour
{
  [SerializeField] private int timeLeft = 120;
  private bool countTrigger = true;

  public event EventHandler OnCountDownTrigger;
  public event EventHandler OnDeadTrigger;

  private void Update()
  {
    if (countTrigger && timeLeft > 0)
    {
      StartCoroutine(CountDownTime());
    }
  }

  IEnumerator CountDownTime()
  {
    countTrigger = false;
    yield return new WaitForSeconds(1);

    timeLeft--;
    OnCountDownTrigger?.Invoke(this, EventArgs.Empty);

    if (timeLeft == 0)
    {
      OnDeadTrigger?.Invoke(this, EventArgs.Empty);
    }

    countTrigger = true;
  }

  public int GetTimeLeft()
  {
    return timeLeft;
  }
}
