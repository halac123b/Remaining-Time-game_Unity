using System.Collections;
using UnityEngine;
using System;

public class OxyStatus : MonoBehaviour
{
  private int process = 0;
  [SerializeField] private int threshold = 100;

  private int speed = 5;

  private bool countTrigger = true;
  private bool startCounting = false;

  public event EventHandler<IntEventArg> OnProcessing;

  public class IntEventArg : EventArgs
  {
    public int value;
  }

  public void Update()
  {
    if (countTrigger && startCounting && process < threshold)
    {
      StartCoroutine(Processing());
    }
  }

  IEnumerator Processing()
  {
    countTrigger = false;
    yield return new WaitForSeconds(1);

    process += speed;
    //OnCountDownTrigger?.Invoke(this, EventArgs.Empty);

    // if (timeLeft == 0)
    // {
    //   OnDeadTrigger?.Invoke(this, EventArgs.Empty);
    // }
    OnProcessing?.Invoke(this, new IntEventArg { value = process });

    countTrigger = true;
  }

  public void SetProcess(bool status, int speed)
  {
    startCounting = status;
    this.speed = speed;
  }
}
