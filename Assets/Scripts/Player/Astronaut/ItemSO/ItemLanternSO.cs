using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Item Lantern", fileName = "New Lantern")]
public class ItemLanternSO : ItemSO
{
  public override void Activate(PlayerStatus playerStatus)
  {
    playerStatus.TriggerLantern();
  }
}
