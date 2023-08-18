using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Item Buff Time", fileName = "New Item")]
public class ItemPlusTimeSO : ItemSO
{
  public override void Activate(PlayerStatus playerStatus)
  {
    playerStatus.SetTimeLeft(playerStatus.GetTimeLeft() + 10);
    playerStatus.TriggerBuffTime();
  }
}
