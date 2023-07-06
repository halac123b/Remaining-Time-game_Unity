using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Item Detail", fileName = "New Item")]
public class ItemSO : ScriptableObject
{
  [SerializeField] private string equipName = "Enter name of item";
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
