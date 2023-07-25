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

  private void OnEnable()
  {
    // playerItem = FindObjectOfType<PlayerItem>();
  }

  private void Update()
  {
    if(inventoryUI.GetInventory(slot).item != null &&  inventoryUI.GetInventory(slot).number!="0"){
      image.sprite = inventoryUI.GetInventory(slot).item.GetSprite();
      numberText.text = inventoryUI.GetInventory(slot).number;
    }else{
      image.sprite = null;
      numberText.text = "";
    }
  }
  
}
