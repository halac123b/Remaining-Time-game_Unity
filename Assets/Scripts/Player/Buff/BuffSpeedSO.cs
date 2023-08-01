using UnityEngine;
using System;

[CreateAssetMenu(menuName = "Buff Speed", fileName = "New Speed Buff")]
public class BuffSpeedSO : BuffSO
{
  public override void Activate(PlayerStatus playerStatus)
  {
    playerStatus.moveSpeed += Convert.ToInt16(value);
  }

  public override void DeActivate(PlayerStatus playerStatus)
  {
    playerStatus.moveSpeed -= Convert.ToInt16(value);
  }
}
