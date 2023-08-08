using UnityEngine;
using System;

[CreateAssetMenu(menuName = "Buff Protect", fileName = "New Protect Buff")]
public class BuffProtectSO : BuffSO
{
  public override void Activate(PlayerStatus playerStatus)
  {
    playerStatus.protection -= Convert.ToInt16(value);
  }

  public override void DeActivate(PlayerStatus playerStatus)
  {
    playerStatus.protection += Convert.ToInt16(value);
  }
}
