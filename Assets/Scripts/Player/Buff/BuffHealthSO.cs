using UnityEngine;
using System;

[CreateAssetMenu(menuName = "Buff Detail", fileName = "New Health Buff")]
public class BuffHealthSO : BuffSO
{
  public override void Activate(PlayerStatus playerStatus)
  {
    playerStatus.SetMax_HP(playerStatus.GetMax_HP() + Convert.ToInt16(value));
  }

  public override void DeActivate(PlayerStatus playerStatus)
  {
        playerStatus.SetMax_HP(playerStatus.GetMax_HP() - Convert.ToInt16(value));

  }
}
