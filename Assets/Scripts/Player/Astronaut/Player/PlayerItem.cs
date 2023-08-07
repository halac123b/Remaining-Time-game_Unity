using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerItem : MonoBehaviour
{
  [System.Serializable]
  public class ItemSlot
  {
    public ItemSO item;
    public int number;
  }

  [SerializeField] public List<ItemSlot> itemList = new List<ItemSlot>();


  // public event EventHandler OnChangeItem;

  // public ItemSlot GetCurrentItem()
  // {
  //   return itemList[currentItem];
  // }

  public ItemSlot GetItem(string name)
  {
    foreach (var Item in itemList)
    {

      if (Item.item.GetName() == name)
      {
        return Item;
      }
    }
    return null;
  }

  public void UpdateItem()
  {
    for (int i = 0; i < itemList.Count; i++)
    {
      if (itemList[i].number == 0)
      {
        itemList.RemoveAt(i);
      }
    }
  }
}
