using UnityEngine;

[CreateAssetMenu(menuName = "Item Compass", fileName = "New Compass")]
public class ItemCompassSO : ItemSO
{
  public override void Activate(PlayerStatus playerStatus)
  {
    playerStatus.TriggerCompass();
  }
}
