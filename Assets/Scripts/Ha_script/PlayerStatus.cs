using System.Collections;
using UnityEngine;
using System;

public class PlayerStatus : MonoBehaviour
{
  [SerializeField] private int timeLeft = 120;
  private bool countTrigger = true;

  public event EventHandler OnCountDownTrigger;

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

    countTrigger = true;
    Debug.Log("1");
  }

  public int GetTimeLeft()
  {
    return timeLeft;
  }
}
