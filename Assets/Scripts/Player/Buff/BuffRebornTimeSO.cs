using UnityEngine;
using System;

[CreateAssetMenu(menuName = "Buff Reborn Time", fileName = "New Reborn Buff")]
public class BuffRebornTimeSO : BuffSO
{
  public override void Activate(PlayerStatus playerStatus)
  {
    playerStatus.monsterRebornTime -= Convert.ToInt16(value);
  }

  public override void DeActivate(PlayerStatus playerStatus)
  {
    playerStatus.monsterRebornTime += Convert.ToInt16(value);
  }
}
