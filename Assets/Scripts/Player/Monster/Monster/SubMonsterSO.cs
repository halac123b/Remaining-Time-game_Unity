using UnityEngine;

[CreateAssetMenu(menuName = "Submonster Detail", fileName = "New Monster")]
public class SubMonsterSO : ScriptableObject
{
  public string equipName = "Enter name of monster";
  public int TypeWeapon;
  public Sprite image;
  public int price = 30;
  public int damage = 15;
  public float speed = 0.8f;
  public int range = 15;
}
