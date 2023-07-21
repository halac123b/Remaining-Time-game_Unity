using UnityEngine;
using System;
using UnityEngine.UI;

public class EquipmentSlot : MonoBehaviour
{
  private PlayerEquip playerEquip;
  private Image image;

  private void OnEnable()
  {
    image = GetComponent<Image>();

    playerEquip = FindObjectOfType<PlayerEquip>();
    image.sprite = playerEquip.GetCurrentEquip().GetSprite();
    playerEquip.OnChangeEquip += ChangeEquipSprite;
  }

  private void ChangeEquipSprite(object sender, EventArgs e)
  {
    if (image) image.sprite = playerEquip.GetCurrentEquip().GetSprite();
  }
}
