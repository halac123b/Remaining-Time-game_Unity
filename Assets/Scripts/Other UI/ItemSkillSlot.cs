using UnityEngine;
using System;
using Unity.Netcode;

public class ItemSkillSlot : NetworkBehaviour
{
  [SerializeField] GameObject itemSlots;
  [SerializeField] GameObject skillSlots;

  private void Start()
  {
    if (PointManager.Instance.playerPoint[Convert.ToInt16(NetworkManager.Singleton.LocalClientId)].playerIndex == 0)
    {
      itemSlots.gameObject.SetActive(false);
      skillSlots.gameObject.SetActive(true);
    }
    else
    {
      itemSlots.gameObject.SetActive(true);
      skillSlots.gameObject.SetActive(false);
    }
  }
}
