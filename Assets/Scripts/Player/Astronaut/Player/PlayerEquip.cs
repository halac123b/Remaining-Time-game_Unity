using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerEquip : MonoBehaviour
{
  [SerializeField] public List<EquipmentSO> equipmentList = new List<EquipmentSO>();

  private int currentEquip = 0;

  public event EventHandler OnChangeEquip;

  private void Update()
  {
    if (Input.GetKeyDown(KeyCode.Q) && equipmentList.Count>=0)
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
    if (equipmentList.Count == 0) return -1;
    return currentEquip;
  }

  public EquipmentSO GetEquip(int i)
  {
    foreach (var weapon in equipmentList)
    {
      if (weapon.GetTypeWeapon() == i)
      {
        return weapon;
      }
    }
    return null;
  }

  public void AddEquip(EquipmentSO equip)
  {
    equipmentList.Add(equip);
  }
}
