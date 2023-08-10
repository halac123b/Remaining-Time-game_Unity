using UnityEngine;
using System;

[CreateAssetMenu(menuName = "Buff Process Speed", fileName = "New Process Buff")]
public class BuffProcessSO : BuffSO
{
  public override void Activate(PlayerStatus playerStatus)
  {
    playerStatus.processSpeed += Convert.ToInt16(value);

    Debug.Log(playerStatus.processSpeed);
  }

  public override void DeActivate(PlayerStatus playerStatus)
  {
    playerStatus.processSpeed -= Convert.ToInt16(value);
  }
}
