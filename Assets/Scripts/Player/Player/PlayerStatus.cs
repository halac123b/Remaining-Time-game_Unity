using System.Collections;
using UnityEngine;
using System;

// Fix: GetTimer(0)

public class PlayerStatus : MonoBehaviour
{
  [SerializeField] private int timeLeft = 30;
  private bool countTrigger = true;
  private bool startCounting = false;
  
  public PlayerData playerData;

  public event EventHandler OnCountDownTrigger;
  public event EventHandler OnDeadTrigger;
  public event EventHandler OnAttackTrigger;

  //private int playerIndex;

  //private TimeManager timeManager;

  // private void Start()
  // {
  //   timeManager = FindObjectOfType<TimeManager>();
  //   //playerIndex = timeManager.GetRandomIndex();
  //   Debug.Log(playerIndex);
  //   timeLeft = timeManager.GetTimer();
  // }

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

  public void SetPlayerData(PlayerData playerData)
  {
    this.playerData = playerData;
    Debug.Log("Player ID is:" + playerData.Id);
    Debug.Log("Player color is:" + playerData.color);
  }

  public PlayerData GetPlayerData()
  {
    return this.playerData;
  }

  public void SetStartCounting(bool status)
  {
    startCounting = status;
  }
}
