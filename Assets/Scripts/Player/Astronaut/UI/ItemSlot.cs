using UnityEngine;
using System;
using UnityEngine.UI;
using TMPro;

public class ItemSlot : MonoBehaviour
{
  // private PlayerItem playerItem;
  [SerializeField] private InventoryUI inventoryUI;
  [SerializeField] private int slot;
  [SerializeField] private Image image;
  [SerializeField] private TextMeshProUGUI numberText;
  Button slotBtn;

  PlayerStatus playerStatus;
  PlayerItem playerItem;

  private void Start()
  {
    playerStatus = FindObjectOfType<PlayerStatus>();
    playerItem = FindObjectOfType<PlayerItem>();
    slotBtn = GetComponent<Button>();

    if (slotBtn != null)
    {
      slotBtn.onClick.AddListener(delegate { ActivateItem(slot); });
    }
  }

  private void Update()
  {
    if (inventoryUI.GetInventory(slot).item != null && int.Parse(inventoryUI.GetInventory(slot).number) > 0)
    {
      image.sprite = inventoryUI.GetInventory(slot).item.GetSprite();
      numberText.text = inventoryUI.GetInventory(slot).number;
    }
    else
    {
      image.sprite = null;
      numberText.text = "";
    }
  }

  private void ActivateItem(int slot)
  {
    inventoryUI.GetInventory(slot).item.Activate(playerStatus);

    for (int i = 0; i < playerItem.itemList.Count; i++)
    {
      if (playerItem.itemList[i].item.name == inventoryUI.GetInventory(slot).item.name)
      {
        playerItem.itemList[i].number--;
        break;
      }
    }

    //playerItem.UpdateItem();
  }
}
