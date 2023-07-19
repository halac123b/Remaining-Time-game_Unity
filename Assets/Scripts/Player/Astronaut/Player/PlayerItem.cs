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

  [SerializeField] private List<ItemSlot> itemList = new List<ItemSlot>();

  private int currentItem = 0;

  public event EventHandler OnChangeItem;

  public ItemSlot GetCurrentItem()
  {
    return itemList[currentItem];
  }

  private void Update()
  {
    if (Input.GetKeyDown(KeyCode.F))
    {
      Debug.Log(3);
      itemList[currentItem].number--;
      if (itemList[currentItem].number == 0)
      {
        itemList.RemoveAt(currentItem);
        currentItem = (currentItem + itemList.Count - 1) % itemList.Count;
      }
      OnChangeItem?.Invoke(this, EventArgs.Empty);
    }
  }

  public void ChangeItem()
  {
    currentItem = (currentItem + 1) % itemList.Count;
    OnChangeItem?.Invoke(this, EventArgs.Empty);
  }
}
