using UnityEngine;
using System;

[CreateAssetMenu(menuName = "Buff Detail", fileName = "New Health Buff")]
public class BuffHealthSO : BuffSO
{
  public override void Activate(PlayerStatus playerStatus)
  {
    playerStatus.monsterHealth += Convert.ToInt16(value);
  }

  public override void DeActivate(PlayerStatus playerStatus)
  {
    playerStatus.monsterHealth -= Convert.ToInt16(value);
  }
}
