using UnityEngine;

[CreateAssetMenu(menuName = "Equipment Detail", fileName = "New Equipment")]
public class EquipmentSO : ScriptableObject
{
  [SerializeField] private string equipName = "Enter name of equip";
  [SerializeField] private int TypeWeapon;
  [SerializeField] private Sprite image;
  [SerializeField] private int price = 30;
  public int damage = 15;
  public float speed = 0.8f;
  public int range = 15;
  public int nockBack = 10;


  public Sprite GetSprite()
  {
    return image;
  }

  public string GetName()
  {
    return equipName;
  }

  public int GetTypeWeapon()
  {
    return TypeWeapon;
  }

  public int GetPrice()
  {
    return price;
  }
}
