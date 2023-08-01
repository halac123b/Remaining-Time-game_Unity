using UnityEngine;
using System;

[CreateAssetMenu(menuName = "Buff Attack", fileName = "New Attack Buff")]
public class BuffAttackSO : BuffSO
{
  public override void Activate(PlayerStatus playerStatus)
  {
    playerStatus.monsterAttack += Convert.ToInt16(value);
  }

  public override void DeActivate(PlayerStatus playerStatus)
  {
    playerStatus.monsterAttack -= Convert.ToInt16(value);
  }
}
