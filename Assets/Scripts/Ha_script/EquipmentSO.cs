using UnityEngine;

[CreateAssetMenu(menuName = "Equipment Detail", fileName = "New Equipment")]
public class EquipmentSO : ScriptableObject
{
  [SerializeField] private string equipName = "Enter name of equip";
  [SerializeField] private Sprite image;

  public Sprite GetSprite()
  {
    return image;
  }

  public string GetName()
  {
    return equipName;
  }
}
