using UnityEngine;
using System;
using UnityEngine.UI;
using Unity.Netcode;

public class EquipmentSlot : NetworkBehaviour
{
  private PlayerEquip playerEquip;
  private Image image;

  private void OnEnable()
  {
    image = GetComponent<Image>();

    playerEquip = FindObjectOfType<PlayerEquip>();

    if (PointManager.Instance.playerPoint[Convert.ToInt16(NetworkManager.Singleton.LocalClientId)].playerIndex != 0)
    {
      if (playerEquip.GetCurrentEquip() != null)
      {
        image.sprite = playerEquip.GetCurrentEquip().GetSprite();
      }
    }
    else
    {
      if (playerEquip.GetCurrentMonster() != null)
      {
        image.sprite = playerEquip.GetCurrentMonster().image;
      }
    }

    playerEquip.OnChangeEquip += ChangeEquipSprite;
  }

  private void ChangeEquipSprite(object sender, EventArgs e)
  {

    if (PointManager.Instance.playerPoint[Convert.ToInt16(NetworkManager.Singleton.LocalClientId)].playerIndex != 0)
    {
      image.sprite = playerEquip.GetCurrentEquip().GetSprite();
    }
    else
    {
      image.sprite = playerEquip.GetCurrentMonster().image;
    }
  }
}
