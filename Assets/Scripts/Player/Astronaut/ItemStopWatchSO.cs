using UnityEngine;
using System.Collections;

[CreateAssetMenu(menuName = "Item Stop watch", fileName = "New Item")]
public class ItemStopWatchSO : ItemSO
{
  public override void Activate(PlayerStatus playerStatus)
  {
    playerStatus.canMove = false;
    playerStatus.canattack = false;
    playerStatus.SetStartCounting(false);

    float delayTime = 2.5f; // Delay time in seconds
                            // Start the timer delay using the TimerDelayHandler
    TimerDelayHandler.StartTimer(delayTime, () =>
    {
      playerStatus.canMove = true;
      playerStatus.canattack = true;
      playerStatus.SetStartCounting(true);
    });
  }
}
