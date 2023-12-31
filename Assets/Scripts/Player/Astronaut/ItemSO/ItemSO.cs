using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSO : ScriptableObject
{
  public string equipName = "ItemName";
  public Sprite image;
  public int price = 30;
  public string description = "This item is so good";

  public Sprite GetSprite()
  {
    return image;
  }

  public string GetName()
  {
    return equipName;
  }

  virtual public void Activate(PlayerStatus playerStatus)
  {
  }
}
