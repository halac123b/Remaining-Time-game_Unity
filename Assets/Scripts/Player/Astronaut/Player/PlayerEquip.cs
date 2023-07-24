using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerEquip : MonoBehaviour
{
  [SerializeField] private List<EquipmentSO> equipmentList = new List<EquipmentSO>();

  private int currentEquip = 0;

  public event EventHandler OnChangeEquip;

  private void Update()
  {
    if (Input.GetKeyDown(KeyCode.Q))
    {
      currentEquip = (currentEquip + 1) % equipmentList.Count;
      OnChangeEquip?.Invoke(this, EventArgs.Empty);
    }
  }

  public EquipmentSO GetCurrentEquip()
  {
    if (equipmentList.Count == 0)
    {
      return null;
    }
    return equipmentList[currentEquip];
  }

  public int GetTypeWeapon()
  {
    return currentEquip;
  }

  public EquipmentSO GetEquip(int i)
  {
    if (i >= equipmentList.Count)
    {
      return null;
    }
    return equipmentList[i];
  }

  public void AddEquip(EquipmentSO equip)
  {
    equipmentList.Add(equip);
  }
}
