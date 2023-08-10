using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

// Fix: GetTimer(0)

public class PlayerStatus : SingletonPersistent<PlayerStatus>
{
  [SerializeField] private int timeLeft = 30;

  private bool countTrigger = true;
  private bool startCounting = false;

  public bool canattack = true;
  public bool canMove = true;

  private int point;

  public int bid = 0;

  public int moveSpeed = 5;
  public int protection = 5;

  // Astronaut stat
  public int processSpeed = 5;

  // Monster start
  public int monsterAttack = 5;
  public int monsterHealth = 20;
  public int monsterRebornTime = 5;
  public bool ezrealEnable = false;
  public bool garenEnable = false;

  public List<BuffSO> buffList = new List<BuffSO>();

  public event EventHandler enableStopWatch;
  public event EventHandler enableBuffTime;
  public event EventHandler enableCompass;

  public void TriggerStopWatch()
  {
    enableStopWatch?.Invoke(this, EventArgs.Empty);
  }

  public void TriggerBuffTime()
  {
    enableBuffTime?.Invoke(this, EventArgs.Empty);
  }

  public void TriggerCompass()
  {
    enableCompass?.Invoke(this, EventArgs.Empty);
  }

  public void Renew()
  {
    canattack = true;
    canMove = true;
  }

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

    if (timeLeft <= 0)
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
