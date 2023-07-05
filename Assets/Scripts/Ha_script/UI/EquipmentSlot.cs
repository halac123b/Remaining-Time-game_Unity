using UnityEngine;
using System;
using UnityEngine.UI;

public class EquipmentSlot : MonoBehaviour
{
  [SerializeField] private PlayerEquip playerEquip;
  private Image image;

  private void Start()
  {
    image = GetComponent<Image>();
    image.sprite = playerEquip.GetCurrentEquip().GetSprite();
    playerEquip.OnChangeEquip += ChangeEquipSprite;
  }

  private void ChangeEquipSprite(object sender, EventArgs e)
  {
    image.sprite = playerEquip.GetCurrentEquip().GetSprite();
  }
}
