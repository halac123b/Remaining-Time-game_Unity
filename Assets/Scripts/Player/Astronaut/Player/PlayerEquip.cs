using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Unity.Netcode;

public class PlayerEquip : NetworkBehaviour
{
  public List<EquipmentSO> equipmentList = new List<EquipmentSO>();

  public List<SubMonsterSO> subMonsterList = new List<SubMonsterSO>();

  public int currentEquip = 0;
  public int currentMonster = 0;

  public event EventHandler OnChangeEquip;

  public bool canTriggerSkill = false;

  private void Update()
  {
    if (Input.GetKeyDown(KeyCode.Q) && (equipmentList.Count > 0 || PointManager.Instance.playerPoint[Convert.ToInt16(NetworkManager.Singleton.LocalClientId)].playerIndex == 0))
    {
      if (equipmentList.Count > 0)
      {
        currentEquip = (currentEquip + 1) % equipmentList.Count;
      }
      currentMonster = (currentMonster + 1) % subMonsterList.Count;

      Debug.Log("vit " + currentMonster);
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

  public SubMonsterSO GetCurrentMonster()
  {
    if (subMonsterList.Count == 0)
    {
      return null;
    }
    return subMonsterList[currentMonster];
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
