using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Unity.Netcode;

// Fix: GetTimer(0)

public class PlayerStatus : SingletonPersistent<PlayerStatus>
{
  [SerializeField] private int timeLeft = 30;
  private bool countTrigger = true;
  private bool startCounting = false;

  private int point = 100;

  public int bid = 0;

  public int moveSpeed = 5;

  public int monsterAttack = 5;
  public int monsterHealth = 20;
  public int monsterRebornTime = 5;

  public bool ezrealEnable = false;
  public bool garenEnable = false;

  public List<BuffSO> buffList = new List<BuffSO>();

  public int GetPoint()
  {
    return point;
  }

  public void SetPoint(int number)
  {
    point = number;
  }

  public event EventHandler OnCountDownTrigger;
  public event EventHandler OnDeadTrigger;

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

  public void SetTimeLeft(int amount)
  {
    timeLeft = amount;
  }

  public void SetStartCounting(bool status)
  {
    startCounting = status;
  }
}
